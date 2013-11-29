//-------------------------------------------------------------------------
// 高画質配信クラス
//
// Copyright (c) 金時豆(http://ch.nicovideo.jp/community/co48276)
// Copyright (c) meronpan(http://ch.nicovideo.jp/community/co274186)
//-------------------------------------------------------------------------

using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace NicoLive
{
    class HQ
    {
        //-------------------------------------------------------------------------
        // 外部エンコーダー起動
        //-------------------------------------------------------------------------
        public static void Exec(string LiveID)
        {


            Thread th = new Thread(delegate()
            {

                // NLE
                if (Properties.Settings.Default.use_nle)
                {
                    if (NLE.IsAlive)
                    {
                        NLE.Start();
                        using (Bouyomi bm = new Bouyomi())
                        {
                            bm.Talk("えぬえるいー配信を開始します。");
                        }
                    }
                    else
                    {
                        using (Bouyomi bm = new Bouyomi())
                        {
                            bm.Talk("えぬえるいー配信なんてなかった。");
                        }
                    }
                    return;
                }

                // XSplit
                if (Properties.Settings.Default.use_xsplit)
                {

                    XSplit.Start();
                    using (Bouyomi bm = new Bouyomi())
                    {
                        bm.Talk("エックスプリット配信を開始します。");
                    }
                    return;
                }


                // FME
                if (Properties.Settings.Default.use_fme)
                {
                    string path = Properties.Settings.Default.fmle_profile_path + "\\" + Properties.Settings.Default.fmle_default_profile; ;

                    if (!File.Exists(path) && !Properties.Settings.Default.use_xsplit && !Properties.Settings.Default.use_nle)
                    {
                           using (Bouyomi bm = new Bouyomi())
                        {
                            bm.Talk("えふえむいープロファイルが見つかりませんでした");
                        }
                           return;
                    }
                    Nico nico = Nico.Instance;
                    string fmle_profile_path = Properties.Settings.Default.fmle_profile_path + "\\" + Properties.Settings.Default.fmle_default_profile;

                    string lv = LiveID;
                    if (lv.Length > 2)
                    {
                        Dictionary<string, string> arr = nico.GetFMEProfile(lv);
                        if (arr["status"].Equals("ok"))
                        {
                            FMLE.Start(arr, fmle_profile_path);
                        }
                    }
                }
            });
            th.Name = "NivoLive.HQ.Exec(): FME";
            th.Start();

        }

        public static void Stop()
        {
            // 非同期に by meronpan
            Thread th = new Thread(delegate()
            {
                FMLE.Stop();
                NLE.Stop();
                XSplit.Stop();

            });
            th.Name = "NivoLive.HQ.Stop()";
            th.Start();
        }

        public static void Restart(string LiveID)
        {
            Stop();

            Thread th = new Thread(delegate()
            {

                while (hasHQ())
                {
                    //Utils.WriteLog("NLE require status: " + NLE.require_status.ToString());
                    System.Threading.Thread.Sleep(500);
                }
                Exec(LiveID);
            });
            th.Name = "NivoLive.HQ.Restart()";
            th.Start();
        }

        public static bool hasHQ()
        {
            return FMLE.hasFME() || NLE.IsBroadCast || XSplit.IsBroadCast;

        }




    }
}
