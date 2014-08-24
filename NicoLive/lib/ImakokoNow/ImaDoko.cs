using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml;
using System.Diagnostics;

namespace NicoLive
{
    class ImaDoko
    {
        private const double MINIMAM_INTERVAL = 20;
        private static SendCommentDelegate dSendComment = null;
        private static Object thisLock = new Object();
        private static DateTime next_enter_time = DateTime.Now;

        private static double mNowSpeed;
        private static string mNowPlace;
        private static double mNowAltitude;

        private static double mLastSpeed;
        private static string mLastPlace;
        private static double mLastAltitude;

        private static int mAltitudeEGM96;

        public static double Speed
        {
            get { return mNowSpeed; }
        }

        public static string Place
        {
            get { return mNowPlace; }
        }

        public static double Altitude
        {
            get { return mNowAltitude; }
        }
        
        public static int AltitudeEGM96{
            get { return mAltitudeEGM96;}
        }




        const string GetPosURI = "http://imakoko-gps.appspot.com/api/latest";
        static Regex Reg_Lon_Lat = new Regex("\"lon\":\"(\\d+\\.\\d+)\".*?\"lat\":\"(\\d+\\.\\d+)\"");
        static Regex Reg_Speed = new Regex("\"velocity\":\"(\\d+\\.\\d+)\"");
        static Regex Reg_Altitude = new Regex("\"altitude\":\"(\\d+\\.\\d+)\"");

        public static bool IsCheckOnGoing = false;

        //-------------------------------------------------------------------------
        // SendCommentデリゲートの設定
        //-------------------------------------------------------------------------
        public static void setSendCommentDelegate(SendCommentDelegate i_say)
        {
            dSendComment = i_say;
        }

        //-------------------------------------------------------------------------
        // 逆ジオコーディング
        //-------------------------------------------------------------------------
        public static string ReverseGeocode(double lat, double lon)
        {
            // http://nicolive_wakusu.b72.in/getAddressNicolive.php
            // http://geocode.didit.jp/reverse/

            try
            {
                // HTTP用WebRequestの作成
                string url = Properties.Settings.Default.geocode_address_url
                    + "?lat=" + lat.ToString("##0.000") + "&lon=" + lon.ToString("##0.000");
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
                req.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
                req.Method = WebRequestMethods.Http.Get;
                req.Timeout = 6000;
                req.ContentType = "application/x-www-form-urlencoded";

                Utils.WriteLog(url);

                // レスポンスを取得
                WebResponse res = req.GetResponse();
                using (StreamReader sr = new StreamReader(res.GetResponseStream()))
                {
                    string xml = sr.ReadToEnd();
                    using (XmlTextReader reader = new XmlTextReader(new StringReader(xml)))
                    {
                        while (reader.Read())
                        {
                            if (reader.NodeType == XmlNodeType.Element &&
                                reader.Name == "AddressShort")
                            {
                                reader.Read();
                                if (reader.NodeType == XmlNodeType.Text)
                                {
                                    string result = reader.Value;
                                    Utils.WriteLog(result);
                                    return result;
                                }
                                else
                                {
                                    break;
                                }
                            }

                        }
                        Utils.WriteLog("ReverseGeocode: AddressShort not found", xml);
                    }
                }
            }
            catch (Exception e)
            {
                Utils.WriteLog(e.ToString());
                Utils.WriteLog("ReverseGeocode:", e.ToString());
            }
            return "";
        }

        //-------------------------------------------------------------------------
        // 現在の速度を取得し、出力
        //-------------------------------------------------------------------------
        public static void UpdateSpeedAndPlace()
        {
            lock (thisLock)
            {
                // 現在地機能が使用可能な状態か？
                if (!(Properties.Settings.Default.imakoko_genzaichi || Properties.Settings.Default.imakoko_speed || Properties.Settings.Default.imakoko_genzaichi_auto_comment) ||
                    Properties.Settings.Default.imakoko_user.Equals("") 
                    )
                {
                    return;
                }

                // 連続してリクエストを発行させない
                if (next_enter_time > DateTime.Now)
                {
                    return;
                }

                // 一度にリクエストが1回しか発行できない
                if (IsCheckOnGoing)
                {
                    return;
                }

                // チェック機能が稼動したことを記録
                IsCheckOnGoing = true;

                // 次回は、すくなくとも MINIMAM_INTERVAL 立たないと実行できない
                next_enter_time = DateTime.Now + TimeSpan.FromSeconds(MINIMAM_INTERVAL);
            }

            try
            {

                string User = Properties.Settings.Default.imakoko_user;
                string result = "";

                // HTTP用WebRequestの作成
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(GetPosURI);
                req.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
                req.Method = WebRequestMethods.Http.Post;
                req.Timeout = 6000;
                req.ContentType = "application/x-www-form-urlencoded";
                //            req.UserAgent = version + " " + Properties.Settings.Default.User;
                //if (Properties.Settings.Default.ProxyServer.Length > 0)
                //{
                //    req.Proxy = new WebProxy(string.Format("http://{0}:{1}", Properties.Settings.Default.ProxyServer, Properties.Settings.Default.ProxyPort));
                //}

                // HTTPで送信するデータ
                string body = "user=" + HttpUtility.UrlEncode(User);

                // 送信データを書き込む
                req.ContentLength = body.Length;
                using (StreamWriter sw = new StreamWriter(req.GetRequestStream()))
                {
                    sw.Write(body);
                }

                // レスポンスを取得
                WebResponse res = req.GetResponse();
                using (StreamReader sr = new StreamReader(res.GetResponseStream()))
                {
                    result = sr.ReadLine();
                    if (result == null)
                    {
                        //dSendComment("いまココ、接続し忘れたかも", true);
                        Utils.WriteLog("いまココ、接続し忘れたかも");
                        IsCheckOnGoing = false;
                        return;
                    }
                }

                mLastSpeed = mNowSpeed;
                mNowSpeed = -1;
                if (Reg_Speed.IsMatch(result))
                {
                    MatchCollection matchCol = Reg_Speed.Matches(result);
                    mNowSpeed = double.Parse(matchCol[0].Groups[1].Value);
                    //dSendComment("時速" + ((int)mNowSpeed).ToString() + "kmです", true);
                }

                mLastAltitude = mNowAltitude;
                mNowAltitude = 0;
                                
                if (Reg_Altitude.IsMatch(result))
                {
                    MatchCollection matchCol = Reg_Altitude.Matches(result);
                    mNowAltitude = double.Parse(matchCol[0].Groups[1].Value);

                    
                }

                mAltitudeEGM96 = 0;

                mNowPlace = "不明";

                if (Reg_Lon_Lat.IsMatch(result) && Properties.Settings.Default.imakoko_genzaichi)
                {
                    MatchCollection matchCol = Reg_Lon_Lat.Matches(result);
                    double lon = double.Parse(matchCol[0].Groups[1].Value);
                    double lat = double.Parse(matchCol[0].Groups[2].Value);

                    if(Properties.Settings.Default.use_egm96){
                        mAltitudeEGM96 = (int)mNowAltitude - calculate_efm96_geoid(lat, lon);
                    }

                    result = ReverseGeocode(lat, lon);
                    mLastPlace = mNowPlace;
                    
                    if (result != "")
                    {
                        mLastPlace = mNowPlace;
                        // 大字を取り除く
                        mNowPlace = result.Replace("（大字）", "");
                    }
                }
            }
            catch (Exception e)
            {
                Utils.WriteLog(e.Message);
            }
            finally
            {
                IsCheckOnGoing = false;
            }
        }

        static public int calculate_efm96_geoid(double lat, double lon)
        {
            //http://earth-info.nga.mil/GandG/wgs84/gravitymod/egm96/binary/WW15MGH.DAC　持ってきておく


            try
            {
                string WW15MGH = System.Windows.Forms.Application.StartupPath + "\\WW15MGH.DAC";

                if (!File.Exists(WW15MGH))
                {
                    Utils.WriteLog("calculate_efm96_geoid(): " + WW15MGH + "not found");
                }

                int offset = 0;
                int lat_offset = (int)(90 * 4 - (int)(lat / 0.25)) * 1440 * 2;
                int lon_offset = (int)(lon / 0.25) * 2;
                offset = lat_offset + lon_offset;

                FileStream fs = new FileStream(WW15MGH, FileMode.Open, FileAccess.Read);

                byte[] buf = new byte[2];
                fs.Seek(offset, SeekOrigin.Begin);
                fs.Read(buf, 0, 2);

                byte a;
                a = buf[0];
                buf[0] = buf[1];
                buf[1] = a;

                int alt = BitConverter.ToInt16(buf, 0) / 100;

                fs.Dispose();
                return alt;
            }
            catch (Exception e)
            {
                Utils.WriteLog(e.StackTrace);
            }
            return 0;
        }

    }
}
