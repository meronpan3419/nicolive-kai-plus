//-------------------------------------------------------------------------
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

            //// 色設定
            //if (mCommentList.RowsDefaultCellStyle.BackColor != Properties.Settings.Default.back_color)
            //{
            //    mCommentList.RowsDefaultCellStyle.BackColor = Properties.Settings.Default.back_color;
            //    mCommentList.BackgroundColor = Properties.Settings.Default.back_color;
            //}

            //if (mCommentList.RowsDefaultCellStyle.ForeColor != Properties.Settings.Default.text_color)
            //{
            //    mCommentList.RowsDefaultCellStyle.ForeColor = Properties.Settings.Default.text_color;
            //}

            // 配信方法更新
            mUseHQ.Checked = Properties.Settings.Default.use_hq;
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
                Utils.WriteLog("update wakumachi()");
                UpdateWakumachi();
                mLastWakusuTime = DateTime.Now;


            }

            // 外部配信ステータス更新
            bool nowHQ = HQ.hasHQ();

            // 配信方法が変わったら
            if (mLastHQ != nowHQ)
            {
                // 外部配信ではない時
                if (!nowHQ)
                {
                    using (Bouyomi bm = new Bouyomi())
                    {
                        if (Properties.Settings.Default.use_nle)
                        {
                            bm.Talk("えぬえるいー、停止を確認");
                        }
                        else
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

            if (HQ.hasHQ())
            {
                mHQStatus.Text = "動作中";
                mHQStatus.ForeColor = System.Drawing.Color.Red;
                mHQStartBtn.Enabled = false;
                mHQStopBtn.Enabled = true;
                mHQRestartBtn.Enabled = true;
            }
            else
            {
                mHQStatus.Text = "停止中";
                mHQStatus.ForeColor = System.Drawing.Color.Green;
                mHQStartBtn.Enabled = true;
                mHQStopBtn.Enabled = false;
                mHQRestartBtn.Enabled = false;
            }

            if (!mDisconnect)
            {

                string remaining_time = "残り時間　00:00";
                int remaining_sec = Utils.CalcRemainingTime();
                if (remaining_sec >= 0)
                {
                    remaining_time = String.Format("残り時間  {0:D2}:{1:D2}", (int)remaining_sec / 60, (int)remaining_sec % 60);
                }
                else
                {


                    remaining_time = String.Format("残り時間 -{0:D2}:{1:D2}", (int)((remaining_sec * -1) / 60), (int)((remaining_sec * -1) % 60));
                }


                this.Invoke((Action)delegate()
                {
                    //　残り時間3分以下30分以上で文字色を赤に
                    if (remaining_sec < 3 * 60 || remaining_sec > 30 * 60)
                    {
                        mRemainingTime.ForeColor = System.Drawing.Color.Red;
                    }
                    else
                    {
                        mRemainingTime.ForeColor = System.Drawing.Color.Black;
                    }

                    mRemainingTime.Text = remaining_time;
                });


                //３０分で終了
                if (!mContWaku.Checked 
                    && mLiveInfo.StartTime != 0 
                    && !mIsExtend 
                    && !mDoingGetNextWaku
                    && remaining_sec　< 0)
                {
                        mNico.SendOwnerComment(LiveID, "/disconnect", "", mLiveInfo.Token);
                        mNico.LiveStop(LiveID, mLiveInfo.Token);
                    

                }
            }

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
                if (ts.Seconds >= 30 && !mLoginWorker.IsBusy && !mDisconnect)
                {
                    System.Net.Sockets.TcpClient tc = mNico.getTcpClient();
                    if (tc != null)
                    {
                        mNico.SendPING();
                        mLastConnectionCheckTime = DateTime.Now;
                        if (!tc.Connected)
                        {
                            mNico.setIsLogin(false);
                            Utils.WriteLog("UITimer_Tick()", "!mNico.getTcpClient().Connected");
                        }
                        Utils.WriteLog("ConnectionCheck:" + tc.Connected.ToString());

                    }
                }

                // 過去コメ吐き出し
                if (mPastChat)
                {
                    ts = DateTime.Now - this.mLastChatTime;
                    if (ts.Seconds >= 1)
                    {
                        mPastChat = false;
                        if (mPastChatList.Count <= 0) return;
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
                        SendComment(Properties.Settings.Default.reset_message, true);
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
                    if (Properties.Settings.Default.imakoko_genzaichi_auto_comment && !mDoingGetNextWaku)
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
                Utils.WriteLog("GC PRE :" + GC.GetTotalMemory(false));
                GC.Collect();
                Utils.WriteLog("GC POST:" + GC.GetTotalMemory(false));
                mNextGC = 0;
            }
        }
    }
}
//-------------------------------------------------------------------------
// Copyright (c) 金時豆(http://ch.nicovideo.jp/community/co48276)
// $Id$
//-------------------------------------------------------------------------
