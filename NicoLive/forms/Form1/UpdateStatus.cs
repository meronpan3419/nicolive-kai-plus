//-------------------------------------------------------------------------
// Main Form
//
// Copyright (c) 金時豆(http://ch.nicovideo.jp/community/co48276)
// $Id$
//-------------------------------------------------------------------------
using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

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
            if (mNico == null) return;
            if (!mNico.IsLogin) return;
            if (mNico.WakutoriMode) return;
            if (mDisconnect) return;


            string id = "";
            try
            {
                this.Invoke((Action)delegate()
                {
                    id = LiveID;
                });
            }
            catch (Exception)
            {

            }

            if (id.Length <= 2) return;

            InfoErr ret = mLiveInfo.GetInfo(id);

            if (ret == InfoErr.ERR_NOT_LOGIN)
            {
                //Connect(false);
            }

            if (ret != InfoErr.ERR_NO_ERR) return;


#if DEBUG
            try
            {
                this.Invoke((Action)delegate()
                {
                    string member_only = "";
                    if (mLiveInfo.IsMemberOnly)
                    {
                        member_only = "　【コミュ限】";
                    }
                    this.Text = "豆ライブ(NicoLive)" + member_only + "　【" + mLiveInfo.Title + "】" + "　[GC:" + GC.GetTotalMemory(false) + "]";
                });
            }
            catch (Exception)
            {

            }
#else
                    // タイトル設定
                    try
                    {
                        this.Invoke((Action)delegate()
                        {
                            if (mLiveInfo.Title.Length > 0)
                            {
                                string member_only = "";
                                if(mLiveInfo.IsMemberOnly){
                                    member_only = "　【コミュ限】";
                                }
                                this.Text = "豆ライブ(NicoLive)" + member_only +  "　【" + mLiveInfo.Title + "】";
                            }
                            else
                            {
                                this.Text = "豆ライブ(NicoLive)";
                            }
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
                Utils.WriteLog(ex.Message);
            }





            int remaining_sec = Utils.CalcRemainingTime();

            //30分経過で自動継枠取り
            if (mUseHQ.Checked &&
                remaining_sec < 0 &&
                !mDoingGetNextWaku &&
                 mContWaku.Checked)
            {
                mDoingGetNextWaku = true;
                Thread.Sleep(500);

                // 棒読みちゃんで自動枠取り通知
                this.mBouyomi.Talk(mMsg.GetMessage("枠取りを開始します"));

                mNico.LiveStop(LiveID, mLiveInfo.Token);

                GetNextWaku(true);


            }


            // 残り時間を言う
            if (Properties.Settings.Default.talk_3min)
            {

                // 延長してないときだけ
                if (!mIsExtend)
                {

                    int rest_min = Properties.Settings.Default.rest_time;
                    string msg = String.Format(mMsg.GetMessage("のこり{0}ふんくらいです。"), rest_min);
                    if (!this.mTalkRestMin && remaining_sec < rest_min * 60)
                    {
                        this.mTalkRestMin = true;

                        // 連続枠取り通知
                        if (Properties.Settings.Default.cont_waku_notice)
                        {
                            if (mContWaku.Checked)
                            {
                                msg = msg + "連続枠取が設定されています";
                            }
                            else
                            {
                                msg = msg + "連続枠取が設定されていません";
                            }
                        }
                        this.mBouyomi.Talk(msg);
                    }

                }
                if (remaining_sec > 25 * 60)
                {
                    this.mTalkRestMin = false;
                }

                Utils.WriteLog("StartTime: " + mLiveInfo.StartTime);
                Utils.WriteLog("EndTime: " + mLiveInfo.EndTime);

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

        //-------------------------------------------------------------------------
        // ログイン状態更新　(UITimer_Tick)
        //-------------------------------------------------------------------------
        private void UpdateLogin()
        {

            if (mNico == null)
            {
                this.mLoginLabel.Text = "未接続";
                this.mLoginLabel.ForeColor = Color.Black;
                this.mPrevLogin = false;
                return;
            }

            // ステータスを接続中に
            if (!this.mConnectBtn.Enabled && mLoginWorker.IsBusy && !mAutoReconnectOnGoing)
            {
                this.mLoginLabel.Text = "接続中";
                this.mLoginLabel.ForeColor = Color.Black;
                return;
            }

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
                    //string msg = "再接続に成功しました";

                    //if (this.mBouyomiBtn.Checked)
                    //{
                    //    this.mBouyomi.Talk(msg);
                    //}
                    mAutoReconnectOnGoing = false;
                    mConnectCount = 0;
                    return;
                }

                //if (mConnectCount > 10)
                //{
                //    //再接続失敗
                //    string msg = "再接続に失敗しました。再接続を中止します。";
                //    if (this.mBouyomiBtn.Checked)
                //    {
                //        this.mBouyomi.Talk(msg);
                //    }
                //    mAutoReconnectOnGoing = false;
                //}

                // 再接続を試みながら、リターン
                this.Invoke((Action)delegate()
                {
                    this.mConnectBtn.Enabled = false;
                    this.Connect(false);  //falseがチェックする //クッキーは有効である前提で、クッキー有効確認のページ読み込み省略できるハズ
                });
                mConnectCount++;
                this.mLoginLabel.Text = "再接続中(" + mConnectCount + "回目)";
                this.mLoginLabel.ForeColor = Color.Black;


            }

            // ログアウト通知
            bool show_err = false;

            show_err = this.mPrevLogin && !mNico.IsLogin && !mDisconnect;


            this.mPrevLogin = mNico.IsLogin;

            if (show_err)
            {
                if (Properties.Settings.Default.auto_reconnect)
                {
                    // 再接続スタート
                    string msg = "再接続を開始します";

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
                    //MessageBox.Show(msg, "豆ライブ");
                }
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

                string xml = Utils.HTTP_GET(url);

                Match match;
                match = Regex.Match(xml, "<wakusu>(.*?)</wakusu>");
                if (match.Success)
                {
                    _Wakusu = match.Groups[1].Value;

                }
                else
                {
                    _Wakusu = "?";
                }
                match = Regex.Match(xml, "<wakumachi>(.*?)</wakumachi>");
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
                    Utils.WriteLog(ex.Message);
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
