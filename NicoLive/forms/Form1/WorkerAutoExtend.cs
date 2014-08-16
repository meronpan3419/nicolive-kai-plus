//-------------------------------------------------------------------------
// 自動無料延長用ワーカー(+自動配信開始処理)
//
// Copyright (c) 金時豆(http://ch.nicovideo.jp/community/co48276)
// Copyright (c) meronpan(http://ch.nicovideo.jp/community/co274186)
//-------------------------------------------------------------------------
using System;
using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Diagnostics;

namespace NicoLive
{
    partial class Form1
    {

        int mAutoExtendWorker_WAIT = 1000 * 2;
        bool mAutoExtendWorker_extend_wait = false;
        DateTime mExtendedTime;

        bool NEED_FREE_EXTEND_0 = false;
        bool NEED_FREE_EXTEND_1 = false;
        bool NEED_FREE_EXTEND_2 = false;


        private void AutoExtendWorker_DoWork(object sender, DoWorkEventArgs e)
        {

            while (true)
            {

                // 延長処理
                AutoExtend();

                // 自動配信開始処理
                AutoLiveStartHQ();
                AutoLiveStartConsole();

                // ウェイト
                Thread.Sleep(mAutoExtendWorker_WAIT);

            }
        }

        //-------------------------------------------------------------------------
        // 延長処理
        //-------------------------------------------------------------------------
        private void AutoExtend()
        {

            if (mNico == null) return;

            //if (!mNico.IsLogin) continue;

            // ウェイト
            //if (this.WindowState == FormWindowState.Minimized)
            //{
            //    Thread.Sleep(wait);
            //    continue;
            //}

            //if (this.Bounds.Width < 50 || this.Bounds.Height < 50)
            //{
            //    //Thread.Sleep(wait);
            //    return;
            //}

            // 延長が完了したらしばらく休憩
            if (mAutoExtendWorker_extend_wait)
            {
                if ((DateTime.Now - mExtendedTime).Minutes < 5) return;

                mAutoExtendWorker_extend_wait = false;
                NEED_FREE_EXTEND_0 = false;
                NEED_FREE_EXTEND_1 = false;
                NEED_FREE_EXTEND_2 = false;

            }

            //mIsExtend = false;

            UInt32 time = mLiveInfo.Time;
            UInt32 btime = mLiveInfo.StartTime;

            if (btime < time)
            {
                int remaining_sec = Utils.CalcRemainingTime(); // 残り時間
                int remaining_min = remaining_sec / 60;

                //--------------------- 無料延長開始 -----------------------
                if (mAutoExtendBtn.Checked && remaining_min <= 4 && !NEED_FREE_EXTEND_0)  // 25分経過
                {
                    Utils.WriteLog("AutoExtend(): 無料延長開始");
                    NEED_FREE_EXTEND_0 = true;
                    NEED_FREE_EXTEND_1 = true;
                }

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
                //--------------------- 無料延長処理 -----------------------
                if (id.Length > 0 && !mIsExtend)
                {
                    if (mOwnLive && NEED_FREE_EXTEND_1)
                    {

                        List<SaleList> list = new List<SaleList>();
                        mNico.GetSaleList(id, ref list);

                        // 無料延長探し
                        foreach (SaleList sale in list)
                        {
                            if (sale.mItem != null && sale.mItem.Equals("freeextend"))
                            {
                                Utils.WriteLog("AutoExtend(): 無料延長あった");
                                mFreeExtendItem = sale;
                                NEED_FREE_EXTEND_2 = true;
                                NEED_FREE_EXTEND_1 = false;
                            }
                        }
                    }

                    if (mOwnLive && NEED_FREE_EXTEND_2)
                    {
                        if (mFreeExtendItem.mItem != null && mFreeExtendItem.mItem.Equals("freeextend"))
                        {
                            if (mNico.Purchase(id, mLiveInfo.Token, mFreeExtendItem))
                            {
                                Utils.WriteLog("AutoExtend(): 無料延長完了");
                                NEED_FREE_EXTEND_2 = false;



                                mNico.LiveStart(LiveID, mLiveInfo.Token);


                                mAutoExtendWorker_extend_wait = true;
                                mIsExtend = true;
                                mExtendedTime = DateTime.Now;

                                this.SendComment(mMsg.GetMessage("15秒後に再接続します"), true);
                                Thread.Sleep(15 * 1000);

                                Thread th = new Thread(delegate()
                                {
                                    Connect(false);
                                });
                                th.Name = "AutoExtend(): 再接続";
                                th.Start();




                            }
                        }
                    }


                }


                if (remaining_sec > (20 * 60))
                {
                    mIsExtend = false;
                    NEED_FREE_EXTEND_0 = false;
                }
            }
        }

        private void AutoLiveStartHQ()
        {
            // 高画質配信
            if (!Properties.Settings.Default.use_hq) return;
            if (!Properties.Settings.Default.auto_connect) return;
            if (mStartHQ) return;
            if (mNico == null) return;
            if (!mNico.IsLogin) return;
            if (mDisconnect) return;

            Utils.WriteLog("AutoLiveStartHQ(): HQ.Restart(): " + LiveID);
            HQ.Restart(LiveID);
            mStartHQ = true;

            // Compact 予告時刻設定
            mLastCompctTime = DateTime.Now;
            mCompactForcast = false;

        }

        private void AutoLiveStartConsole()
        {
            // 通常配信
            //if (!Properties.Settings.Default.use_flash_console) return;
            //if (Properties.Settings.Default.use_hq) return;     // 高画質配信の時はスタート幼い
            if (!Properties.Settings.Default.auto_connect) return;
            if (mPushStart) return;    // 配信開始ボタン押す前か
            if (mNico == null) return;
            if (!mNico.IsLogin) return;
            if (mDisconnect) return;
            if (mLiveConsole == null) return; // 配信コンソールがあるか
            if (mLiveConsole.IsDisposed) return;

            IntPtr hWnd = IntPtr.Zero;
            try
            {
                this.Invoke((Action)delegate()
                {
                    hWnd = mLiveConsole.getFlashHandle();

                });
            }
            catch (Exception e)
            {
                Utils.WriteLog("AutoLiveStartConsole(): フラッシュウィンドウハンドル取得失敗: " + e.Message);
            }

            if (hWnd == IntPtr.Zero) return;

            Utils.WriteLog("AutoLiveStartConsole(): hWnd = " + hWnd.ToString());

            using (ScreenCapture scr = new ScreenCapture())
            {
                Bitmap bmp = scr.Capture2(hWnd);
                Color color = bmp.GetPixel(45, 150);
                if (color.Name.Equals("ff323232") || color.Name.Equals("ffffffff"))     //フラッシュが読み込み完了してない
                {
                    Utils.WriteLog("AutoLiveStartConsole(): フラッシュが読み込み完了してない: " + color.Name);
                    return;
                }
            }

            if (!Properties.Settings.Default.use_hq)
            {
                Input.MouseClickHWnd(hWnd, 300, 20);    //簡単配信
            }
            else
            {
                Input.MouseClickHWnd(hWnd, 400, 20);    //外部配信
            }
            Thread.Sleep(1000);
            Input.MouseClickHWnd(hWnd, 400, 145);   //はいボタン
            Thread.Sleep(1000);
            Input.MouseClickHWnd(hWnd, 180, 150);    //配信開始
            Thread.Sleep(1000);
            Input.MouseClickHWnd(hWnd, 400, 145);    //はいボタン

            mPushStart = true;  //配信開始ボタン押したよフラグ
            if (mPushStart)
            {
                mBouyomi.Talk(mMsg.GetMessage("配信を開始しました"));
                Utils.WriteLog("AutoLiveStartConsole(): 配信開始");
                this.mLastChatTime = DateTime.Now;
                this.mLastCompctTime = DateTime.Now;
                this.mCompactForcast = false;
            }
        }



    }
}

