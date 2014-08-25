//-------------------------------------------------------------------------
// Main Form
//
// Copyright (c) 金時豆(http://ch.nicovideo.jp/community/co48276)
// $Id$
//-------------------------------------------------------------------------
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace NicoLive
{
    partial class Form1
    {


        //-------------------------------------------------------------------------
        // ログイン用ワーカー
        //-------------------------------------------------------------------------
        private void LoginWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            string user_id = Properties.Settings.Default.user_id;
            string passwd = Properties.Settings.Default.password;

            string live_id = LiveID;
            int try_cnt = 0;
            int try_new_connect = 0;

            bool disconnect = true;



        GET_LIVE_ID:
            Utils.WriteLog("LoginWorker_DoWork() GET_LIVE_ID");
            if (this.mLogin_cancel)
            {
                Utils.WriteLog("LoginWorker_DoWork() Login_cancel");
                goto END;
            }

            // LVが入ってないときは自動で取ってくる
            if (live_id.Length <= 2)
            {

                Utils.WriteLog("LoginWorker_DoWork() GetCurrentLive()");
                live_id = GetCurrentLive();
                if (live_id.Length > 2)
                {
                    this.Invoke((Action)delegate()
                    {
                        LiveID = live_id;
                    });
                }
            }

            if (live_id.Length <= 2)
            {
                try_cnt++;
                if (try_cnt < 10)
                {
                    //this.Invoke((Action)delegate()
                    //{
                    //    this.mLoginLabel.Text = "LV番号取得中(" + try_cnt + ")";
                    //    this.mLoginLabel.ForeColor = System.Drawing.Color.Black;
                    //});
                    Utils.WriteLog("Retry: GET_LIVE_ID " + try_cnt);
                    Thread.Sleep(500);

                    goto GET_LIVE_ID;
                }
            }

            if (live_id.Length <= 2)
            {
                using (Bouyomi bm = new Bouyomi())
                {
                    bm.Talk("放送IDがわかりませんでした");
                }
                goto END;
            }

            try_cnt = 0;

            mCurrentLiveID = live_id;

            // ログイン
            if (mSkipLogin) mNico.IsLogin = true;
            bool login = (mSkipLogin) ? true : mNico.Login(user_id, passwd);
            if (!login)
            {
                using (Bouyomi bm = new Bouyomi())
                {
                    bm.Talk("ログインできませんでした");
                }
            }
        GET_COMMENT:
            Utils.WriteLog("LoginWorker_DoWork() GET_COMMENT");
            if (this.mLogin_cancel)
            {
                goto END;
            }


            if (!mNico.IsLogin)
            {
                mNico.Login(user_id, passwd);
            }

            //最終コメントレス番号リセット
            mLastChatNo = 0;
            mLastChatNo2 = 0;

            // 過去コメ読み込みスタート
            mPastChat = true;



            // コメントサーバーに接続
            NicoErr err = mNico.ConnectToCommentServer(live_id, Properties.Settings.Default.comment_max);
            Utils.WriteLog("ConnectToCommentServer(): Result = NicoErr:" + err.ToString());

            switch (err)
            {
                case NicoErr.ERR_COULD_NOT_CONNECT_COMMENT_SERVER:
                    if (try_cnt < 30)
                    {
                        try_cnt++;
                        Thread.Sleep(100);
                        Utils.WriteLog("Retry: ERR_COULD_NOT_CONNECT_COMMENT_SERVER" + try_cnt);
                        goto GET_COMMENT;
                    }
                    using (Bouyomi bm = new Bouyomi())
                    {
                        bm.Talk("コメントサーバーに接続できませんでした");
                    }
                    //MessageBox.Show("コメントサーバーにログインできませんでした。", "NicoLive");
                    break;
                case NicoErr.ERR_COMMENT_SERVER_IS_FULL:
                    using (Bouyomi bm = new Bouyomi())
                    {
                        bm.Talk("コメントサーバーが満員で接続できません");
                    }
                    //MessageBox.Show("コメントサーバーが満員でログインできません。", "NicoLive");
                    break;
                case NicoErr.ERR_NOT_LIVE:
                    try_new_connect++;

                    if (try_new_connect < 30)
                    {
                        Thread.Sleep(100);
                        live_id = "";
                        Utils.WriteLog("Retry: ERR_NOT_LIVE" + try_cnt);
                        goto GET_LIVE_ID;
                    }
                    using (Bouyomi bm = new Bouyomi())
                    {
                        bm.Talk("放送は既に終了しています");
                    }

                    break;

                case NicoErr.ERR_CLOSED:
                    using (Bouyomi bm = new Bouyomi())
                    {
                        bm.Talk("放送は既に終了しています");
                    }

                    break;

                case NicoErr.ERR_COMMUNITY_ONLY:
                    using (Bouyomi bm = new Bouyomi())
                    {
                        bm.Talk("コミュニティ限定放送です");
                    }
                    break;
            }

            if (err != NicoErr.ERR_NO_ERR)
            {
                mPastChat = false;
                mNico.Close();
                goto END;
            }


            disconnect = false;
            mPastChatList.Clear();
            mWelcomeList.Clear();
            mOmikujiList.Clear();
            mAishouList.Clear();
            // 過去コメ読み込みスタート
            mPastChat = true;

            using (Bouyomi bm = new Bouyomi())
            {
                bm.Talk("接続しました");
            }

            this.Invoke((Action)delegate()
            {

                // 簡易ビュアー自動起動
                if (Properties.Settings.Default.viewer_auto_boot)
                {

                    if (mViewer == null || mViewer.IsDisposed)
                    {
                        mViewer = new Viewer(LiveID);
                    }
                    mViewer.Show();
                    mViewer.Activate();

                }

                // 配信コンソール自動表示
                if (Properties.Settings.Default.use_flash_console)
                {
                    if (mLiveConsole == null || mLiveConsole.IsDisposed)
                    {
                        mLiveConsole = new LiveConsole(LiveID);
                    }
                    mLiveConsole.Show();
                    mLiveConsole.Activate();

                }

                mNico.WakutoriMode = false;

                mCommentList.Rows.Clear();



                // 情報を取得
                Thread th = new Thread(delegate()
                {
                    Utils.WriteLog("Start: LoginWorker_DoWork(): GetInfo(),GetMemberOnlyInfo()");
                    mLiveInfo.GetInfo(live_id);
                    mLiveInfo.GetMemberOnlyInfo(live_id);
                    Utils.WriteLog("Finish: LoginWorker_DoWork(): GetInfo(),GetMemberOnlyInfo()");

                    // 開演待ちスキップするか？
                    if (Properties.Settings.Default.skip5min)
                    {
                        // 開演5分待ちスキップ
                        mNico.LiveStart(live_id, mLiveInfo.Token);
                        Utils.WriteLog("LiveStart skip 5 min");
                    }

                    // らんちゃー
                    if (Properties.Settings.Default.use_launcher)
                    {
                        mLauncher.Exec(this.LiveID);
                    }

                    //debug
                    Nico nico = Nico.Instance;
                    System.Collections.Generic.Dictionary<string, string> arr = nico.GetFMEProfile(this.LiveID);
                    if (arr.Count <= 1) return;
                    string url = arr["url"];
                    string stream = arr["stream"];
                    //Utils.WriteLog("LoginWorker_DoWork:url: " + url);
                    //Utils.WriteLog("LoginWorker_DoWork:stream: " + stream);

                    //Utils.WriteLog(@"ffmpeg  -f alsa -ac 1 -i pulse -acodec nellymoser  -f x11grab -s 1366x768 -i :0.0+1366,0 -r 10 -vcodec libx264 -crf 26 -keyint_min 0  -bufsize 600k  -r 10 -pix_fmt yuv420p -ar 44100 -b:a 96k -b:v 300k -f flv """ + url + "/" + stream + " flashVer=FMLE/3.0\\20(compatible;\\20FMSc/1.0) swfUrl=" + url + "/" + stream + "\"");

                });
                th.Name = "LoginWorker_DoWork(): GetInfo(),GetMemberOnlyInfo()";
                th.Start();



                // ロガー起動
                if (Properties.Settings.Default.save_log)
                {
                    try
                    {
                        Directory.CreateDirectory("log");
                        mLogger = new StreamWriter("log/" + LiveID + ".xml", true);
                    }
                    catch (Exception ex)
                    {
                        Utils.WriteLog("LoginWorker_DoWork:" + ex.Message);
                    }
                }


                if (mViewer != null && !mViewer.IsDisposed)
                {
                    mViewer.SetLiveID(LiveID);
                }

                if (mLiveConsole != null && !mLiveConsole.IsDisposed)
                {
                    mLiveConsole.LoadMovie(LiveID);
                }




            });


        END:
            if (mLogin_cancel)
            {
                mAutoReconnectOnGoing = false;
            }
            mLogin_cancel = false;
            this.Invoke((Action)delegate()
            {
                this.mConnectBtn.Enabled = true;
                this.mLastChatTime = DateTime.Now;
                this.mLastCompctTime = DateTime.Now;
                this.mLastConnectionCheckTime = DateTime.Now;
                this.mCompactForcast = false;
                this.mStartHQ = false;
                this.mDisconnect = disconnect;
            });
        }
    }
}
//-------------------------------------------------------------------------
// Copyright (c) 金時豆(http://ch.nicovideo.jp/community/co48276)
// $Id$
//-------------------------------------------------------------------------
