﻿//-------------------------------------------------------------------------
// Main Form
//
// Copyright (c) 金時豆(http://ch.nicovideo.jp/community/co48276)
// $Id$
//-------------------------------------------------------------------------
using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;

namespace NicoLive
{
    partial class Form1
    {
        //-------------------------------------------------------------------------
        // 放送情報更新
        //-------------------------------------------------------------------------
        private void UpdateMovieInfo()
        {
            if (mNico != null && mNico.IsLogin && !mNico.WakutoriMode )
            {
                string id="";
                try
                {
                    this.Invoke((Action)delegate()
                    {
                        id = LiveID;
                    });
                }
                catch (Exception )
                {

                }

                if (id.Length <= 2) return;

                InfoErr ret = mLiveInfo.GetInfo(id);
                if (ret == InfoErr.ERR_NO_ERR)
                {
#if DEBUG
                    try
                    {
                        this.Invoke((Action)delegate()
                        {
                            this.Text = "豆ライブ(NicoLive)" + "　【" + mLiveInfo.Title + "】" + "　[GC:" + GC.GetTotalMemory(false) + "]";
                        });
                    }
                    catch (Exception )
                    {

                    }
#else
                    // タイトル設定
                    try
                    {
                        this.Invoke((Action)delegate()
                        {
                            if (mLiveInfo.Title.Length > 0)
                                this.Text = "豆ライブ(NicoLive)" + "　【" + mLiveInfo.Title + "】";
                            else
                                this.Text = "豆ライブ(NicoLive)";
                        });
                    }
                    catch (Exception)
                    {

                    }
#endif
                    // 来場者数
                    mVisitorCnt = (int)mLiveInfo.WatchCount;

                    try
                    {
                        this.Invoke((Action)delegate()
                        {
                            this.mTotalCnt.Text = "来場者数：" + mVisitorCnt;
                        });
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }





                    // 経過時間取得
                    if (Properties.Settings.Default.talk_3min)
                    {
                        UInt32 time = mLiveInfo.Time;
                        UInt32 btime = mLiveInfo.StartTime;

                        if (btime < time)
                        {
                            UInt32 lim = 30 * 60;        // 30分
                            UInt32 sub = time - btime;  // 経過時間

                            //
                            if (sub > lim)
                            {
                                UInt32 mod = sub / lim;
                                sub = sub - mod * lim;
                            }

                            // 残り3分通事
                            if (!mIsExtend)
                            {

                                int rest_time = Properties.Settings.Default.rest_time;
                                string msg = String.Format(mMsg.GetMessage("のこり{0}ふんくらいです"), rest_time);
                                if (!this.mTalkLimit && sub > (lim - rest_time * 60))
                                {
                                    this.mBouyomi.Talk(msg);

                                    this.mTalkLimit = true;

                                    // 連続枠取り通知
                                    if (Properties.Settings.Default.cont_waku_notice)
                                    {
                                        if (mContWaku.Checked)
                                        {
                                            msg = "連続枠取が設定されています";
                                        }
                                        else
                                        {
                                            msg = "連続枠取が設定されていません";
                                        }
                                    }
                                    this.mBouyomi.Talk(msg);
                                }

                            }
                            if (sub < 1 * 60)
                            {
                                this.mTalkLimit = false;
                            }
                            Debug.WriteLine("Time: " + sub.ToString());
                        }
                    }

                    // 自分の配信かどうか
                    string token = mLiveInfo.Token;
                    if (token.Length != 0)
                    {
                        mOwnLive = true;
                    }

                    // Twitterポスト
                    if (mTwPost == false &&
                        mOwnLive &&
                        Properties.Settings.Default.tw_start_enable
                        //Properties.Settings.Default.tw_token.Length == 0
                        
                        )
                    {
                        TwitterPoster(true);
                    }
                }
                else if (ret == InfoErr.ERR_NOT_LOGIN)
                {
                    if (!mDisconnect && mNico != null && mNico.IsLogin)
                    {
                        //Connect(false);
                    }
                }
            }

        }

        //-------------------------------------------------------------------------
        // ログイン状態更新
        //-------------------------------------------------------------------------
        private void UpdateLogin()
        {
            // ステータスを接続中に
            if (!this.mConnectBtn.Enabled && !mLoginWorker.IsBusy)
            {
                this.mLoginLabel.Text = "接続中";
                this.mLoginLabel.ForeColor = Color.Black;
                return;
            }

            if (mNico != null)
            {
                if (!mLoginWorker.IsBusy)
                {
                    this.mLoginLabel.Text = (mNico.IsLogin) ? "ログイン済" : "未ログイン";
                    this.mLoginLabel.ForeColor = (mNico.IsLogin) ? Color.Red : Color.Black;
                }

                // 再接続処理中？
                if (mAutoReconnectOnGoing)
                {
                    if (mNico.IsLogin)
                    {
                        // 再接続成功
                        string msg = "再接続に成功しました";

                        if (this.mBouyomiBtn.Checked)
                        {
                            this.mBouyomi.Talk(msg);
                        }
                        mAutoReconnectOnGoing = false;
                        mConnectCount = 0;
                    }
                    else
                    {
                        // 再接続失敗
//                        string msg = "再接続に失敗しました。再び再接続を行います";
//                       if (this.mBouyomiBtn.Checked)
//                        {
//                            this.mBouyomi.Talk(msg);
//                        }

                        // 再接続を試みながら、リターン
                        this.Invoke((Action)delegate()
                        {
                            this.mConnectBtn.Enabled = false;
                            this.Connect(false);
                        });
                        mConnectCount++;
                        this.mLoginLabel.Text = "接続中(" + mConnectCount + ")";
                        this.mLoginLabel.ForeColor = Color.Black;
                       return;
                    }
                }

                // ログアウト通知
                bool show_err = false;

                if (this.mPrevLogin && !mNico.IsLogin)
                    show_err = true;

                this.mPrevLogin = mNico.IsLogin;

                if (show_err)
                {
                    if (Properties.Settings.Default.auto_reconnect)
                    {
                        // 再接続スタート
                        string msg = "切断されました。再接続を開始します";

                        if (this.mBouyomiBtn.Checked)
                        {
                            this.mBouyomi.Talk(msg);
                        }

                        mAutoReconnectOnGoing = true;
                        this.Invoke((Action)delegate()
                        {
                            this.mConnectBtn.Enabled = false;
                            this.Connect(false);
                        });
                    }
                    else
                    {
                        string msg = "切断されました";

                        if (this.mBouyomiBtn.Checked)
                        {
                            this.mBouyomi.Talk(msg);
                        }
                        this.mPrevLogin = mNico.IsLogin;
                        MessageBox.Show(msg, "豆ライブ");
                    }
                }
            }
            else
            {
                this.mLoginLabel.Text = "未ログイン";
                this.mLoginLabel.ForeColor = Color.Black;
                this.mPrevLogin = false;
            }
        }

        //-------------------------------------------------------------------------
        // ユニークユーザー数更新
        //-------------------------------------------------------------------------
        private void UpdateUniq()
        {
            mUIStatus.SetUniq(ref mUniqCnt);
        }

        //-------------------------------------------------------------------------
        // 負荷情報更新
        //-------------------------------------------------------------------------
        private void UpdateHardInfo()
        {
            UIStatus.HardInfo info = mUIStatus.SetHardInfo(ref mPerfCnt, ref mCpuInfo, ref mBattery);
            if (info == UIStatus.HardInfo.HARD_INFO_BAT_LOW)
            {
                string str = String.Format(mMsg.GetMessage("バッテリー残量が{0}%以下になりました"), Properties.Settings.Default.rest_batt);
                SendComment(str, true);
            }
        }
        //-------------------------------------------------------------------------
        // ネットワーク情報更新
        //-------------------------------------------------------------------------
        private void UpdateNetworkInfo()
        {
            mUIStatus.SetNetworkInfo(ref mUpLink);
        }

        //-------------------------------------------------------------------------
        // アクティブユーザー数更新
        //-------------------------------------------------------------------------
        private void UpdateActive()
        {
            if (!mDisconnect)
                mUIStatus.UpdateActive(ref mActiveCnt);
        }

        //-------------------------------------------------------------------------
        // 枠待ち数更新
        //-------------------------------------------------------------------------
        private void UpdateWakumachi()
        {
            Thread th = new Thread(delegate()
            {
                string url = "http://nicolive-wakusu.b72.in/getwakusu.php?ver=" + Program.VERSION_KAI_PLUS;
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
                string result = "";
                try
                {
                    WebResponse res = req.GetResponse();

                    // read response
                    Stream resStream = res.GetResponseStream();
                    using (StreamReader sr = new StreamReader(resStream, System.Text.Encoding.UTF8))
                    {
                        result = sr.ReadToEnd();
                        sr.Close();
                        resStream.Close();
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLine("UpdateWakumachi:" + e.Message);
                }

                Match match;
                match = Regex.Match(result, "<wakusu>(.*?)</wakusu>");
                if (match.Success)
                {
                    _Wakusu = match.Groups[1].Value;

                }
                else
                {
                    _Wakusu = "?";
                }
                match = Regex.Match(result, "<wakumachi>(.*?)</wakumachi>");
                if (match.Success)
                {
                    _Wakumachi = match.Groups[1].Value;
                }
                else
                {
                    _Wakumachi = "?";
                }

                try
                {
                    this.Invoke((Action)delegate()
                    {
                        this.mWakumachi.Text = "枠数：" + _Wakusu + "/" + _Wakumachi;
                    });
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            });
            th.Name = "NivoLive.Form1.UpdateStatus.UpdateWakumachi()";
            th.Start();

        }

    }
}
//-------------------------------------------------------------------------
// Copyright (c) 金時豆(http://ch.nicovideo.jp/community/co48276)
// $Id$
//-------------------------------------------------------------------------
