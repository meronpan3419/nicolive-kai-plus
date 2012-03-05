﻿//-------------------------------------------------------------------------
// Main Form
//
// Copyright (c) 金時豆(http://ch.nicovideo.jp/community/co48276)
// $Id$
//-------------------------------------------------------------------------
using System;
using System.Diagnostics;


namespace NicoLive
{
    partial class Form1
    {
        private bool mLastHQ = false;

        //-------------------------------------------------------------------------
        // タイマー
        //-------------------------------------------------------------------------
        private void UITimer_Tick(object sender, EventArgs e)
        {
            Properties.Settings.Default.last_lv = this.mLiveID.Text;

            // フォントを設定しておく
            if (!this.mCommentList.Font.Equals(Properties.Settings.Default.font))
            {
                Utils.SetCommentFont(ref mCommentList);
            }

            // 色設定
            if (mCommentList.RowsDefaultCellStyle.BackColor != Properties.Settings.Default.back_color)
            {
                mCommentList.RowsDefaultCellStyle.BackColor = Properties.Settings.Default.back_color;
                mCommentList.BackgroundColor = Properties.Settings.Default.back_color;
            }

            if (mCommentList.RowsDefaultCellStyle.ForeColor != Properties.Settings.Default.text_color)
            {
                mCommentList.RowsDefaultCellStyle.ForeColor = Properties.Settings.Default.text_color;
            }

            // 配信方法更新
            mUseHQBtn.Text = Properties.Settings.Default.use_hq ? "外部配信" : "通常配信";
            updateBroadcastType();


            // ログイン状態更新
            UpdateLogin();

            // ユニークユーザー数更新
            UpdateUniq();

            // アクティブ数更新
            UpdateActive();

            // 枠数・枠待ち
            TimeSpan ts = DateTime.Now - this.mLastWakusuTime;
            if (ts.Minutes >= 1)
            {
                System.Diagnostics.Debug.WriteLine("update wakumachi()");
                UpdateWakumachi();
                mLastWakusuTime = DateTime.Now;


            }

            // 外部コメントウィンド
            this.Invoke((Action)delegate()
            {
                // 経過時間
                long resttime = 0;
                if (mNico != null && mNico.IsLogin && !mNico.WakutoriMode)
                {

                    resttime = mLiveInfo.Time - mLiveInfo.StartTime + (Utils.GetUnixTime(DateTime.Now) - mLiveInfo.UnixTime);
                }
                if (resttime > 60 * 60 * 48) resttime = 0;
                long min = resttime / 60;
                long sec = resttime - min * 60;
                long hour = min / 60;
                min -= hour * 60;

                mCommentForm.RestTime = (hour > 0 ? hour + ":" : "") + ((hour > 0 && min < 10) ? "0" : "") + min + ":" + (sec < 10 ? ("0" + sec) : sec.ToString());

                // 残り時間
                resttime = 0;
                if (mNico != null && mNico.IsLogin && !mNico.WakutoriMode)
                {
                    resttime = mLiveInfo.EndTime - mLiveInfo.Time - (Utils.GetUnixTime(DateTime.Now) - mLiveInfo.UnixTime);
                }
                if (resttime < 180)
                {
                    mCommentForm.RestTimeForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    mCommentForm.RestTimeForeColor = System.Drawing.Color.Black;
                }

                mCommentForm.ActiveCnt = mActiveCnt.Text;
                mCommentForm.TotalCnt = mTotalCnt.Text;
                mCommentForm.UniqCnt = mUniqCnt.Text;
                mCommentForm.UpLink = mUpLink.Text;
                mCommentForm.UpLinkForColor = mUpLink.ForeColor;
                mCommentForm.Battery = mBattery.Text;
                mCommentForm.CpuInfo = mCpuInfo.Text;
            });

            // 外部配信ステータス更新
            bool nowHQ =HQ.hasHQ();
            

            if (mLastHQ != nowHQ)
            {
                if (!nowHQ)
                {
                    using (Bouyomi bm = new Bouyomi())
                    {
                        if (Properties.Settings.Default.use_nle)
                        {
                            bm.Talk("えぬえるいー、停止を確認");
                        } else 
                        if (Properties.Settings.Default.use_xsplit)
                        {
                            bm.Talk("エックスプリット、停止を確認");
                        }
                        else
                        {
                            bm.Talk("えふえむいー停止を確認");
                        }
                    }
                }
            }
            mLastHQ = nowHQ;            

            // コメントサーバーとの接続キープ用
            if (mNico != null && mNico.IsLogin && !mDisconnect && !mNico.WakutoriMode)
            {
                // /keepaliveを送る
                if (Properties.Settings.Default.send_keepalive)
                {
                    ts = DateTime.Now - this.mLastChatTime;
                    if (ts.Minutes >= KEEP_ALIVE_TIME)
                    {
                        SendComment("/keepalive", true);
                        mLastChatTime = DateTime.Now;
                    }
                }

                // コネクションチェック
                ts = DateTime.Now - this.mLastConnectionCheckTime;
                if (ts.Seconds >= 30)
                {
                    System.Net.Sockets.TcpClient tc = mNico.getTcpCliant();
                    if (tc != null)
                    {
                        mNico.SendNULLComment();
                        mLastConnectionCheckTime = DateTime.Now;
                        if(!tc.Connected){
                            mNico.setIsLogin(false);
                        }
                        Debug.WriteLine("ConnectionCheck:" + tc.Connected.ToString());
                    }
                }

                // 過去コメ吐き出し
                if (mPastChat)
                {
                    ts = DateTime.Now - this.mLastChatTime;
                    if (ts.Seconds >= 1)
                    {
                        mPastChat = false;
                        //mPastChatList.Reverse();
                        this.mCommentList.Rows.AddRange(mPastChatList.ToArray());
                    }
                }


                // FME 配信設定告知
                if (FMLE.FMEsettingExist && FMLE.hasFME())
                {
                    if (Properties.Settings.Default.show_fme_setting)
                    {
                        SendComment(FMLE.FMEsetting, true);
                    }
                    FMLE.FMEsettingExist = false;
                }

                // FME ラグ対策
                COMPACT_TIME = Properties.Settings.Default.sekigaeMinutes;
                ts = DateTime.Now - this.mLastCompctTime;

                // 席替え予告
                if (!mCompactForcast && ts.Add(TimeSpan.FromSeconds(15)).Minutes >= COMPACT_TIME)
                {
                    mCompactForcast = true;
                    if (Properties.Settings.Default.fme_compact)
                    {
                        SendComment("ラグ対策のため,15秒後にリロードを実施します", true);
                    }
                }

                // 席替え実施
                if (ts.Minutes >= COMPACT_TIME)
                {
                    if (Properties.Settings.Default.fme_compact)
                    {
                        SendComment("/reset", true);
                    }
                    mLastCompctTime = DateTime.Now;
                    mCompactForcast = false;
                }


                //自動現在地なんたら
                GENZAICHI_TIME = Properties.Settings.Default.imakoko_genzaichi_auto_comment_int;
                ts = DateTime.Now - this.mLastGenzaichiCommentTime;
                if (ts.Minutes >= GENZAICHI_TIME)
                {
                    if (Properties.Settings.Default.imakoko_genzaichi_auto_comment)
                    {
                        SendCurrentPosition();
                    }
                    mLastGenzaichiCommentTime = DateTime.Now;
                }

            }
        }

        //-------------------------------------------------------------------------
        // ハード情報更新タイマー
        //-------------------------------------------------------------------------
        private void HardInfoTimer_Tick(object sender, EventArgs e)
        {
            // ハードウェア情報更新
            UpdateHardInfo();

            // ネットワーク情報更新
            UpdateNetworkInfo();

            // ガベージ・コレクション
            mNextGC++;
            if (mNextGC > 60 * 3)
            {
                Console.WriteLine("GC PRE :" + GC.GetTotalMemory(false));
                GC.Collect();
                Console.WriteLine("GC POST:" + GC.GetTotalMemory(false));
                mNextGC = 0;
            }
        }
    }
}
//-------------------------------------------------------------------------
// Copyright (c) 金時豆(http://ch.nicovideo.jp/community/co48276)
// $Id$
//-------------------------------------------------------------------------
