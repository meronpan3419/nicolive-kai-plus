﻿//-------------------------------------------------------------------------
// 枠取りダイアログ
//
// Copyright (c) 金時豆(http://ch.nicovideo.jp/community/co48276)
// $Id$
//-------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;
using System.Web;
using System.Drawing;
using System.Text.RegularExpressions;

namespace NicoLive
{
    public enum WakuResult
    {
        NO_ERR,
        JUNBAN,
        ERR
    };

    public partial class WakuDlg : Form
    {
        Nico mNico;
        public string mLv;
        public WakuResult mState;
        private static bool mAbort;
        private bool mChangeProp;
        private bool mPostTweet;
        //-------------------------------------------------------------------------
        // コンストラクタ
        //-------------------------------------------------------------------------
        public WakuDlg(string iLv, bool iChangeProp)
        {
            mNico = Nico.Instance;
            mState = WakuResult.ERR;
            mChangeProp = iChangeProp;

            mAbort = false;
            mPostTweet = false;
            mLv = iLv;
            InitializeComponent();
            mLabel.Text = "初期化中";

            if (iChangeProp) this.Text = "前枠の続きの枠取り";
        }

        //-------------------------------------------------------------------------
        // フォームロード
        //-------------------------------------------------------------------------
        private void WakuDlg_Load(object sender, EventArgs e)
        {

        }

        private void WakuDlg_Shown(object sender, EventArgs e)
        {
            mWorker.RunWorkerAsync();
        }

        //-------------------------------------------------------------------------
        // キャンセル
        //-------------------------------------------------------------------------
        private void mCancel_Click(object sender, EventArgs e)
        {
            mAbort = true;

            mState = WakuResult.ERR;

            mWorker.CancelAsync();

            Close();
        }

        //-------------------------------------------------------------------------
        // 枠取りワーカー
        //-------------------------------------------------------------------------
        private void mWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            string lv = "";
            int try_cnt = 0;

            if (mChangeProp)
            {
                SetLabelFromThread("ログイン中", Color.Black, false);
                mNico.WakutoriMode = true;
                bool login = mNico.Login(Properties.Settings.Default.user_id,
                                           Properties.Settings.Default.password);
                if (!login)
                {
                    SetLabelFromThread("ログインできませんでした", Color.Red, false);
                    return;
                }
            }

            //SetLabelFromThread("放送枠があるか確認中", Color.Black, false);

            //string aLv = mNico.GetAlreadtLive();
            //if (!aLv.Equals(""))
            //{
            //    //枠あった
            //    mLv = aLv;
            //    mState = WakuResult.NO_ERR;
            //    //return;
            //}


            SetLabelFromThread("放送履歴取得中", Color.Black, false);
            Utils.WriteLog("WakuDlg: mWorker_DoWork(): 放送履歴取得中");

            if (mLv.Length <= 2)
                mLv = mNico.GetRecentLive(Properties.Settings.Default.user_id,
                                Properties.Settings.Default.password);

            SetLabelFromThread("前枠番組情報の取得中", Color.Black, false);
            Utils.WriteLog("WakuDlg: mWorker_DoWork(): 前枠番組情報の取得中");

            // Utils.WriteLog(mLv);

            Dictionary<string, string> arr = new Dictionary<string, string>();
            Dictionary<string, string> comu = new Dictionary<string, string>();
            Dictionary<string, string> tag = new Dictionary<string, string>();
            Dictionary<string, string> taglock = new Dictionary<string, string>();
        GET_OLD_INFO:
            if (!mNico.GetOldLiveInfo(mLv, ref arr, ref comu, ref tag, ref taglock))
            {
                if (mAbort) return;

                try_cnt++;
                if (try_cnt < 10)
                {
                    Utils.WriteLog("WakuDlg: mWorker_DoWork(): 前枠番組情報の取得中: " + try_cnt);
                    Thread.Sleep(100);
                    goto GET_OLD_INFO;
                }

                SetLabelFromThread("ERR:前枠番組情報の取得に失敗", Color.Red, false);
                Utils.WriteLog("WakuDlg: mWorker_DoWork(): ERR:前枠番組情報の取得に失敗");
                return;
            }

            if (mAbort) return;

            // タイトルの番号の自動更新
            if (Properties.Settings.Default.title_auto_inc)
            {
                arr["title"] = Utils.incTitle(arr["title"]);
            }

            // 放送情報変更
            if (mChangeProp)
            {
                Utils.WriteLog("WakuDlg: mWorker_DoWork(): ChangeProp()");
                ChangeProp dlg = new ChangeProp();

                dlg.Title = arr["title"];
                dlg.Desc = arr["description"];
                dlg.TimeShift = (arr["timeshift_enabled"].Equals("1")) ? true : false;
                dlg.CommunityOnly = (arr["public_status"].Equals("2")) ? true : false;
                dlg.Totumachi = (arr["tags[2]"].Length > 0) ? true : false;
                dlg.Cruise = (arr["tags[3]"].Length > 0) ? true : false;
                dlg.Face = (arr["tags[1]"].Length > 0) ? true : false;
                dlg.Ad = (arr["ad_enable"].Equals("0")) ? true : false;
                if (arr.ContainsKey("livetaglockall"))
                    dlg.LiveTagLockAll = (arr["livetaglockall"].Length > 0) ? true : false;

                dlg.SetCommunity(comu);
                dlg.SetTag(tag, taglock);

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    arr["title"] = dlg.Title;
                    arr["description"] = dlg.Desc;
                    arr["public_status"] = (dlg.CommunityOnly) ? "2" : "";
                    arr["timeshift_enabled"] = (dlg.TimeShift) ? "1" : "0";
                    arr["tags[1]"] = (dlg.Face) ? "顔出し" : "";
                    arr["tags[2]"] = (dlg.Totumachi) ? "凸待ち" : "";
                    arr["tags[3]"] = (dlg.Cruise) ? "クルーズ待ち" : "";
                    arr["default_community"] = dlg.Community;
                    arr["ad_enable"] = dlg.Ad ? "0" : "1";
                    if (arr.ContainsKey("livetaglockall"))
                        arr["livetaglockall"] = (dlg.LiveTagLockAll) ? "ロックする" : "";

                    tag = dlg.GetTag();
                    taglock = dlg.GetTaglock();
                }
                else
                {
                    try
                    {
                        this.Invoke((MethodInvoker)delegate()
                        {
                            Close();
                        });
                    }
                    catch (Exception)
                    {
                    }
                    return;
                }
            }

            // 枠取り開始
            SetLabelFromThread("枠取り中", Color.Black, false);
            Utils.WriteLog("WakuDlg: mWorker_DoWork(): 枠取り中");


            // taglockのロック内容が無いものを削除
            Dictionary<string, string> new_taglock = new Dictionary<string, string>();
            foreach (string key in taglock.Keys)
            {
                if (taglock[key].Length > 0)
                {
                    new_taglock.Add(key, taglock[key]);
                }
            }
            taglock = new_taglock;


            // タグ追加
            foreach (string key in tag.Keys)
            {
                arr.Add(key, tag[key]);
            }
            foreach (string key in taglock.Keys)
            {
                arr.Add(key, taglock[key]);
            }

            try_cnt = 0;
            bool accept_kiyaku = false;

        RETRY:
            if (mAbort) return;

            WakuErr err = mNico.GetWaku(ref arr, ref lv);
            Utils.WriteLog("WakuErr:" + err.ToString());
            if (err == WakuErr.ERR_NO_ERR)
            {
                mLv = lv;
                Utils.WriteLog("lv:" + mLv);
                mState = WakuResult.NO_ERR;

                using (Bouyomi bm = new Bouyomi())
                {
                    MessageSettings msg = MessageSettings.Instance;
                    bm.Talk(msg.GetMessage("枠が取れたよ"));
                }

                SetLabelFromThread("枠取り完了", Color.Black, true);
                Utils.WriteLog("WakuDlg: mWorker_DoWork(): 枠取り完了");
            }
            else if (err == WakuErr.ERR_MAINTE)
            {
                SetLabelFromThread("メンテナンス中です", Color.Red, false);
            }
            else if (err == WakuErr.ERR_KIYAKU)
            {
                SetLabelFromThread("枠取り中（規約確認中）", Color.Black, false);
                Utils.WriteLog("WakuDlg: mWorker_DoWork(): WakuErr.ERR_KIYAKU: 枠取り中（規約確認中）");
                if (!accept_kiyaku)
                {
                    arr["kiyaku"] = "true";
                    arr["description"] = Utils.ToBase64(arr["description"]);
                    accept_kiyaku = true;
                }
                else
                {
                    //規約確認したけどダメだったから最初から
                    // そもそも規約確認じゃないのにWakuErr.ERR_KIYAKUしてるのがあれなので修正するまでのコード↓
                    SetLabelFromThread("枠取り中（再取得中）", Color.Black, false);
                    Utils.WriteLog("WakuDlg: mWorker_DoWork(): WakuErr.ERR_KIYAKU: 枠取り中（再取得中）");
                    arr.Remove("kiyaku");
                    arr.Remove("confirm");
                }
                goto RETRY;
            }
            else if (err == WakuErr.ERR_WEB_PAGE_SESSION)
            {
                SetLabelFromThread("枠取り中（WEBページの有効期限切れ）", Color.Black, false);
                Utils.WriteLog("WakuDlg: mWorker_DoWork(): WakuErr.ERR_WEBPAGESESSION: 枠取り中（WEBページの有効期限切れ）");
                arr.Remove("kiyaku");
                arr.Remove("confirm");
                accept_kiyaku = false;
                goto RETRY;
            }
            else if (err == WakuErr.ERR_DESCRIPTION_EMPTY)
            {
                SetLabelFromThread("枠取り中（説明文なし）", Color.Black, false);
                Utils.WriteLog("WakuDlg: mWorker_DoWork(): WakuErr.ERR_WEBPAGESESSION: 枠取り中（説明文なし）");
                arr.Remove("kiyaku");
                arr.Remove("confirm");
                accept_kiyaku = false;
                arr["description"] = "説明ありません";
                goto RETRY;
            }
            else if (err == WakuErr.ERR_LOGIN)
            {
                SetLabelFromThread("ログイン中", Color.Black, false);
                Utils.WriteLog("WakuDlg: mWorker_DoWork(): WakuErr.ERR_LOGIN: ログイン中");

                mNico.Login(Properties.Settings.Default.user_id,
                                           Properties.Settings.Default.password);
                goto RETRY;
            }
            else if (err == WakuErr.ERR_TAJU)
            {
                SetLabelFromThread("枠取り中（多重投稿)", Color.Black, false);
                Utils.WriteLog("WakuDlg: mWorker_DoWork(): WakuErr.ERR_TAJU: 枠取り中（多重投稿)");
                arr.Remove("kiyaku");
                arr.Remove("confirm");
                arr["description"] = Utils.FromBase64(arr["description"]);
                accept_kiyaku = false;
                goto RETRY;
            }
            else if (err == WakuErr.ERR_KONZATU)
            {
                SetLabelFromThread("枠取り中（混雑中）", Color.Black, false);
                arr.Remove("kiyaku");
                arr.Remove("confirm");
                arr["description"] = Utils.FromBase64(arr["description"]);
                accept_kiyaku = false;
                goto RETRY;
            }

            else if (err == WakuErr.ERR_JUNBAN_WAIT)
            {
                SetLabelFromThread("順番待ちに並びます", Color.Black, false);
                Utils.WriteLog("WakuDlg: mWorker_DoWork(): WakuErr.ERR_JUNBAN_WAIT: 順番待ちに並びます");
                arr["is_wait"] = "wait";
                goto RETRY;
            }
            else if (err == WakuErr.ERR_JUNBAN)
            {
                mState = WakuResult.NO_ERR;
                SetLabelFromThread("順番待ち", Color.Black, false);
                Utils.WriteLog("WakuDlg: mWorker_DoWork(): WakuErr.ERR_JUNBAN: 順番待ち");
                if (lv.Length <= 2)
                    goto RETRY;
                do
                {
                    if (mAbort) return;
                    Dictionary<string, string> waitInfo = mNico.GetJunban(lv);
                    if (waitInfo.ContainsKey("stream_status") && waitInfo["stream_status"].Equals("3"))
                    {
                        SetLabelFromThread("ERR:別の放送が開始されています。", Color.Red, false);
                        Utils.WriteLog("WakuDlg: mWorker_DoWork(): 別の放送が開始されています");
                        break;
                    }

                    //Utils.WriteLog(lv);

                    if (!waitInfo.ContainsKey("count"))
                        continue;
                    int cnt = int.Parse(waitInfo["count"]);
                    if (cnt <= 1)
                    {
                        mLv = lv;
                        SetLabelFromThread("枠取り完了", Color.Black, true);
                        Utils.WriteLog("WakuDlg: mWorker_DoWork(): 枠取り完了");
                        mState = WakuResult.NO_ERR;
                        break;
                    }
                    else
                    {
                        Match match = Regex.Match(waitInfo["start_time"], "日(.*?)時(.*?)分");
                        SetLabelFromThread("順番待ち: " + waitInfo["count"] + "人 （" + match.Groups[1].Value + "時" + match.Groups[2].Value + "分）", Color.Black, false);
                        Utils.WriteLog("WakuDlg: mWorker_DoWork(): 順番待ち: " + waitInfo["count"] + "人 （" + match.Groups[1].Value + "時" + match.Groups[2].Value + "分）");

                        // 100人以上順番待ちの時はTweet
                        if (cnt >= 102)
                        {
                            mPostTweet = true;
                        }

                        // 順番待ち100人未満に成ったらTweet
                        if (mPostTweet && cnt < 100)
                        {
                            Utils.TweetWait(cnt);
                            mPostTweet = false;
                        }
                    }
                    Thread.Sleep(2000);
                } while (true);
            }
            else if (err == WakuErr.ERR_JUNBAN_ALREADY)
            {
                SetLabelFromThread("ERR:既に順番待ちの放送があります", Color.Red, false);
                Utils.WriteLog("WakuDlg: mWorker_DoWork(): WakuErr.ERR_JUNBAN_ALREADY");
            }
            else if (err == WakuErr.ERR_TAG)
            {
                SetLabelFromThread("ERR:18文字以上のタグは利用できません", Color.Red, false);
                Utils.WriteLog("WakuDlg: mWorker_DoWork(): WakuErr.ERR_TAG");
            }
            else if (err == WakuErr.ERR_ALREADY_LIVE)
            {
                if (try_cnt < 5)
                {
                    if (lv.Length > 2)
                    {
                        mLv = lv;
                        mState = WakuResult.NO_ERR;
                    }
                    try_cnt++;
                    Thread.Sleep(1000);
                    goto RETRY;
                }
                mState = WakuResult.ERR;
                mLv = "";
                SetLabelFromThread("ERR:既に放送中です。", Color.Red, true);
                Utils.WriteLog("WakuDlg: mWorker_DoWork(): WakuErr.ERR_ALREADY_LIVE");
            }
            else if (err == WakuErr.ERR_MOJI)
            {
                SetLabelFromThread("ERR:文字数制限エラー。", Color.Red, false);
                Utils.WriteLog("WakuDlg: mWorker_DoWork(): WakuErr.ERR_MOJI");
            }
            else if (err == WakuErr.ERR_UNKOWN)
            {
                if (try_cnt < 5)
                {
                    try_cnt++;
                    Thread.Sleep(250);
                    goto RETRY;
                }
                SetLabelFromThread("ERR:予期せぬエラーです。", Color.Red, false);
                Utils.WriteLog("WakuDlg: mWorker_DoWork(): WakuErr.ERR_UNKOWN");
            }
            arr = null;
        }

        //-------------------------------------------------------------------------
        // ステータスラベル設定
        //-------------------------------------------------------------------------
        void SetLabelFromThread(string iStr, Color iCol, bool iClose)
        {
            try
            {
                this.Invoke((MethodInvoker)delegate()
                {
                    mLabel.Text = iStr;
                    mLabel.ForeColor = iCol;
                    if (iClose)
                        Close();
                });
            }
            catch (Exception)
            {
            }
        }


    }
}
//-------------------------------------------------------------------------
// 枠取りダイアログ
//
// Copyright (c) 金時豆(http://ch.nicovideo.jp/community/co48276)
// $Id$
//-------------------------------------------------------------------------
