//-------------------------------------------------------------------------
// ニコニコアクセスクラス
//
// Copyright (c) 金時豆(http://ch.nicovideo.jp/community/co48276)
// $Id$
//-------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Xml;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Web;
using Hal.CookieGetterSharp;

//-------------------------------------------------------------------------
// クラス実装
//-------------------------------------------------------------------------
namespace NicoLive
{
    //-------------------------------------------------------------------------
    // エラーステータス
    //-------------------------------------------------------------------------
    public enum NicoErr
    {
        ERR_NO_ERR = 0,                             // エラー無し
        ERR_COULD_NOT_CONNECT_COMMENT_SERVER,       // コメントサーバーにログインできない
        ERR_COMMENT_SERVER_IS_FULL,                 // コメントサーバーが満員
        ERR_NOT_LIVE,                               // 放送中じゃない
        ERR_COMMUNITY_ONLY,                         // コミュ限
        ERR_CLOSED                              // 既に放送終了
    };

    public enum WakuErr
    {
        ERR_NO_ERR = 0,
        ERR_JUNBAN,                                 // 順番待ち
        ERR_KONZATU,                                // 混雑中
        ERR_TAJU,                                   // 多重投稿
        ERR_MAINTE,                                 // メンテ中
        ERR_UNKOWN,                                 // ハンドルしてないエラー
        ERR_ALREADY_LIVE,                           // 既に放送中
        ERR_KIYAKU,                                 // 規約確認
        ERR_LOGIN,                                  // ログインしてない
        ERR_JUNBAN_ALREADY,                         // 既に順番待ち
        ERR_JUNBAN_WAIT,                            // 順番待ち
        ERR_MOJI,									// 文字数制限エラー
        ERR_TAG,                                    // タグエラー
        ERR_WEB_PAGE_SESSION,                         // WEBページの有効期限エラー
        ERR_DESCRIPTION_EMPTY,                         //番組説明文入力してください
    };

    struct SaleList
    {
        public string mCode;
        public string mPrice;
        public string mNum;
        public string mItem;
        public string mLabel;
    }

    //-------------------------------------------------------------------------
    // ニコ生アクセスクラス
    //-------------------------------------------------------------------------
    class Nico
    {
        // for IE component cookie
        [DllImport("wininet.dll", EntryPoint = "InternetSetCookie", ExactSpelling = false, CharSet = CharSet.Unicode, SetLastError = true)]
        static extern bool InternetSetCookie(string lpszUrl, string lpszCookieName, string lpszCookieData);

        // ie保護モードか判断
        [DllImport("ieframe.dll")]
        public static extern int IEIsProtectedModeProcess(ref bool pbResult);

        //for IE component cookie（ie保護モード）
        [DllImport("ieframe.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool IESetProtectedModeCookie(string url, string name, string data, int flags);

        [DllImport("kernel32.dll")]
        public static extern Int32 GetLastError();

        [DllImport("msvcrt.dll", CharSet = CharSet.Auto)]
        public static extern long time(IntPtr tm);

        // Cookie
        private CookieContainer mCookieLogin = null;

        // uri of api to logging in
        private readonly string URI_LOGIN = "https://secure.nicovideo.jp/secure/login?site=niconico";
        //private readonly string URI_LOGIN = "https://secure.nicovideo.jp/secure/login?site=nicolivespweb";


        // getplayerstatus uri
        private readonly string URI_GETPLAYERSTATUS = "http://live.nicovideo.jp/api/getplayerstatus?v=";

        // getpublishstatus uri
        private readonly string URI_GETPUBLISHSTATUS = "http://live.nicovideo.jp/api/getpublishstatus?version=2&v=";


        // profile uri
        private readonly string URI_GETFMEPROFILE = "http://live.nicovideo.jp/api/getfmeprofile?v=";

        // fme uri
        private readonly string URI_STARTFME = "http://live.nicovideo.jp/api/configurestream/";

        private readonly string URI_STARTFME2 = "http://live.nicovideo.jp/api/configurestream?version=2&v=";

        private readonly string URI_WATCH_URL = "http://live.nicovideo.jp/watch";

        private readonly string URI_NG_URL = "http://watch.live.nicovideo.jp/api/configurengword?";

        // コメントサーバーへのTCPソケット
        private TcpClient mTcp = null;

        // 取得したコメント
        private static string mComment = "";

        // コメント取得用バッファ
        private static byte[] mTmpBuffer = null;

        // ログイン済みかどうか
        private static bool mIsLogin = false;

        // コメント送信用パラメータ
        private static string mTicket = "";
        private static string mLastRes = "0";

        private UInt32 mBaseTime = 0;
        private string mUserID = "";
        private string mThread = "";
        private UInt16 mPremium = 0;
        //
        private static Nico mInstance = null;
        private bool mWakutoriMode = false;
        //

        public bool WakutoriMode
        {
            get { return mWakutoriMode; }
            set { mWakutoriMode = value; }
        }

        //-------------------------------------------------------------------------
        // LiveStart
        //-------------------------------------------------------------------------
        public bool LiveStart(string iLV, string iToken)
        {
            if (iLV.Length <= 2) return false;
            if (iToken.Length <= 0) return false;

            // 配信の種別セット、value(0: 通常配信, 1: 外部配信) 
            string url = URI_STARTFME2 + iLV + "&key=hq&value=1&token=" + iToken;
            string xml = HttpGet(url, ref this.mCookieLogin);
            if (!xml.Contains("status=\"ok\"")) return false;

            // 配信開始コマンド exclude
            url = URI_STARTFME2 + iLV + "&key=exclude&value=0&token=" + iToken;
            xml = HttpGet(url, ref this.mCookieLogin);
            if (!xml.Contains("status=\"ok\"")) return false;

            //
            // ついでにstart_timeとend_timeのセット
            //
            Regex r = new Regex("<start_time>(.*?)</start_time>");
            Match m = r.Match(xml);
            LiveInfo info = LiveInfo.Instance;
            if (m.Success)
            {
                info.StartTime = Convert.ToUInt32(m.Groups[1].Value);
            }
            else
            {
                info.StartTime = 0;
            }
            r = new Regex("<end_time>(.*?)</end_time>");
            m = r.Match(xml);
            if (m.Success)
            {
                info.EndTime = Convert.ToUInt32(m.Groups[1].Value);
            }
            else
            {
                //info.EndTime = 0;
            }

            return true;
        }

        //-------------------------------------------------------------------------
        // LiveStop
        //-------------------------------------------------------------------------
        public bool LiveStop(string iLV, string iToken)
        {
            if (iLV.Length <= 2) return false;
            if (iToken.Length <= 0) return false;

            string url = URI_STARTFME2 + iLV + "&key=end_now&token=" + iToken;
            string xml = HttpGet(url, ref this.mCookieLogin);

            return (xml.Contains("status=\"ok\""));
        }

        //-------------------------------------------------------------------------
        // コンストラクタ
        //-------------------------------------------------------------------------
        private Nico()
        {

        }

        //-------------------------------------------------------------------------
        // シングルトン用
        //-------------------------------------------------------------------------
        public static Nico Instance
        {
            get
            {
                if (mInstance == null)
                {
                    mInstance = new Nico();
                }
                return mInstance;
            }
        }

        //-------------------------------------------------------------------------
        // コメント
        //-------------------------------------------------------------------------
        public string Comment
        {
            set { mComment = value; }
            get { return mComment; }
        }

        //-------------------------------------------------------------------------
        // 切断
        //-------------------------------------------------------------------------
        public void Close()
        {
            try
            {
                if (mTcp != null && mTcp.Connected)
                {
                    mTcp.Client.Disconnect(false);
                    //mTcp.Close();                    
                }

            }
            catch (Exception e)
            {
                Utils.WriteLog("Close:" + e.Message);
            }
            IsLogin = false;
            WakutoriMode = false;
        }

        //-------------------------------------------------------------------------
        //  現在の放送ＩＤ取得
        //-------------------------------------------------------------------------
        public string GetCurrentLive(string username, string password)
        {
            const string uri = "http://live.nicovideo.jp/my";
            const string REGEX_pat = "<a href=\"http://live.nicovideo.jp/watch/lv(?<videoid>[0-9]+)\\?ref=my_live\" title=\"生放送ページへ戻る\" class=\"nml\">";
            //const string REGEX_pat = "http://live.nicovideo.jp/watch/lv(?<videoid>[0-9]+)\" class=\"now\"";
            //const string REGEX_pat = "immendStream\\('http://live.nicovideo.jp/','(?<videoid>[0-9]+)'";
            string result = "";

            if (Login(username, password))
            {
                string html = HttpGet(uri, ref this.mCookieLogin);
                Regex regex = new Regex(REGEX_pat);
                MatchCollection match = regex.Matches(html);

                if (match.Count > 0)
                {
                    result = "lv" + match[0].Groups["videoid"].Value;
                    Utils.WriteLog(result);
                }

            }
            return result;
        }

        //-------------------------------------------------------------------------
        //  ログイン
        //
        //  ログイン処理の流れ
        //  1.設定ファイルのuser_sessionでログイン
        //  2.ブラウザのクッキーでログイン
        //  3.ID/PASSでログイン
        //-------------------------------------------------------------------------
        public bool Login(string username, string password)
        {

            // hashtable to hold the arguments of POST request.
            Dictionary<string, string> post_arg = new Dictionary<string, string>(3);

            post_arg["mail"] = username;
            post_arg["password"] = password;
            post_arg["next_url"] = "";

            mUserID = "";
            mThread = "";
            mBaseTime = 0;

            // create cookie-container
            this.mCookieLogin = new CookieContainer();

            string ret = "ログインエラー";

            if (LoginTest(Properties.Settings.Default.user_session))
            {
                Utils.WriteLog("Nico: Login() LoginTest() local user_session OK");
                Cookie c = new Cookie("user_session", Properties.Settings.Default.user_session, "/", ".nicovideo.jp");
                c.Expires = DateTime.Now.AddDays(30);
                this.mCookieLogin.Add(c);
                // IEのCookieを書き換える

                OverrideIECookie(addSessionidExpires(Properties.Settings.Default.user_session));

                mIsLogin = true;
                return true;

            }
            else
            {
                Utils.WriteLog("Nico: Login() local user_session failed");
            }



            // ブラウザのクッキーを用いてログインを試みる
            if (Properties.Settings.Default.UseBrowserCookie)
            {

                Utils.WriteLog("Nico: Login() Login by Cookie");

                ICookieGetter[] cookieGetters = CookieGetter.CreateInstances(true);

                ICookieGetter s = null;
                foreach (ICookieGetter es in cookieGetters)
                {
                    if (es.ToString().Equals(Properties.Settings.Default.Browser))
                    {
                        s = es;
                        break;
                    }
                }
                if (s != null)
                {
                    Utils.WriteLog("Nico: Login() has Cookie");
                    try
                    {
                        //System.Net.Cookie cookie = s.GetCookie(new Uri("http://live.nicovideo.jp/"), "user_session");

                        System.Net.CookieCollection collection = s.GetCookieCollection(new Uri("http://live.nicovideo.jp/"));

                        Utils.WriteLog("Nico: Login() user_session: " + collection["user_session"].Value);

                        if (collection["user_session"] != null)
                        {
                            this.mCookieLogin.Add(new Cookie("user_session", collection["user_session"].Value, "/", ".nicovideo.jp"));
                            if (LoginTest(collection["user_session"].Value))
                            {
                                Utils.WriteLog("Nico: Login() LoginTest() OK");

                                // IEのCookieを書き換える
                                OverrideIECookie(addSessionidExpires(collection["user_session"].Value));
                                Properties.Settings.Default.user_session = collection["user_session"].Value;
                                mIsLogin = true;
                                return true;

                            }
                            else
                            {
                                Utils.WriteLog("Nico: Login() has Cookie, user_session null");
                            }
                        }
                        else
                        {
                            Utils.WriteLog("Nico: Login() LoginTest() NG");
                        }

                    }
                    catch (Exception ex)
                    {
                        //System.Windows.Forms.MessageBox.Show(ex.Message);
                        Utils.WriteLog("Nico: Login() has Cookie: " + ex.Message);
                    }
                }
                else
                {
                    Utils.WriteLog("Nico: Login() not has Cookie");
                }
            }

            Utils.WriteLog("Nico: Login() Login by ID-PASS");

            // send POST request
            ret = HttpPost(URI_LOGIN, post_arg, ref this.mCookieLogin /*, Properties.Settings.Default.nicolive_login_user_agent*/);
            if (ret == null)
            {
                Utils.WriteLog("Nico: Login() Login by ID-PASS, ret == null");
                this.mCookieLogin = null;
                mIsLogin = false;
                return false;
            }


            // check if result contains "ログインエラー"
            if (ret.IndexOf("ログインエラー") != -1)
            {
                Utils.WriteLog("Nico: Login() Login by ID-PASS, ログインエラー");
                this.mCookieLogin = null;
                mIsLogin = false;
                return false;
            }

            string user_session = "";

            //CookieからセッションＩＤ取得
            Uri uri = new Uri(URI_LOGIN);
            CookieCollection cc = this.mCookieLogin.GetCookies(uri);
            user_session = cc["user_session"].ToString();
            string user_session2 = addSessionidExpires(user_session);
            if (user_session.Equals(""))
            {
                Utils.WriteLog("Nico: Login()  Login by ID-PASS, user_session is null ");
                mIsLogin = false;
                return false;
            }

            // IEのCookieを書き換える
            OverrideIECookie(user_session2);
            Properties.Settings.Default.user_session = user_session.Replace("user_session=", "");
            Properties.Settings.Default.Save();

            // ログイン済みフラグを立てる
            Utils.WriteLog("Nico: Login()  Login by ID-PASS, piiiiii");
            mIsLogin = true;
            return true;
        }

        private string addSessionidExpires(string user_session)
        {
            DateTime expires = DateTime.Now.AddDays(30);

            System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("en-US");
            return user_session + "; path=/; domain=.nicovideo.jp; expires=" + expires.ToString("ddd, d-MMM-yyyy HH:mm:ss ", culture) + "GMT;";

        }

        private void OverrideIECookie(string user_session)
        {


            bool isIEProtectedMode = false;
            IEIsProtectedModeProcess(ref isIEProtectedMode);

            Utils.WriteLog("OverrideIECookie: " + user_session);
            Utils.WriteLog("IEIsProtectedModeProcess: " + isIEProtectedMode.ToString());
            if (isIEProtectedMode)
            {
                //IE保護モードON, win vista/7
                if (!IESetProtectedModeCookie(URI_LOGIN, null,
                    user_session, 0x10))
                {
                    Utils.WriteLog("Cound not write cookie (isIEProtectedMode: " + isIEProtectedMode + ")");
                    Utils.WriteLog(GetLastError().ToString());
                }
            }
            else
            {
                //IE保護モードOFF, winXP
                if (!InternetSetCookie(URI_LOGIN, null,
                    user_session))
                {
                    Utils.WriteLog("Cound not write cookie (isIEProtectedMode: " + isIEProtectedMode + ")");
                    Utils.WriteLog(GetLastError().ToString());
                }
            }
        }

        public bool LoginTest(string iUserSession)
        {
            try
            {
                CookieContainer user_session = new CookieContainer();
                user_session.Add(new Cookie("user_session", iUserSession, "/", ".nicovideo.jp"));
                if (HttpGet("http://live.nicovideo.jp/my", ref user_session).Contains("<title>マイページ - ニコニコ生放送</title>"))
                {
                    return true;
                }
            }
            catch (Exception)
            { }
            return false;

        }

        private void IEIsProtectedModeProcess()
        {
            throw new NotImplementedException();
        }

        //-------------------------------------------------------------------------
        // ログインしてるかどうか
        //-------------------------------------------------------------------------
        public bool IsLogin
        {
            get
            {
                return mIsLogin;
            }
            set { mIsLogin = value; }
        }

        //-------------------------------------------------------------------------
        // ユーザーIDからユーザー名取得
        //-------------------------------------------------------------------------
        public string GetUsername(string iUserID)
        {
            if (iUserID.Length <= 0) return "";

            string name = "";
            string uri = "http://www.nicovideo.jp/user/" + iUserID;
            string regex = Properties.Settings.Default.user_name_regex;

            string res = HttpGet(uri, ref this.mCookieLogin);


            if (res != null)
            {
                Match match = Regex.Match(res, regex);
                if (match.Success)
                {
                    name = match.Groups[1].Value;
                }
                else
                {
                    Utils.WriteLog("GetUsername ng1 : " + res);
                }
            }
            else
            {
                Utils.WriteLog("GetUsername ng2: " + res);
            }

            return name;
        }

        public string GetUsername(string iUserID, string iRegex, string iUserSession)
        {
            if (iUserID.Length <= 0) return "";

            string name = "";
            string uri = "http://www.nicovideo.jp/user/" + iUserID;
            CookieContainer user_session = new CookieContainer();
            user_session.Add(new Cookie("user_session", iUserSession, "/", ".nicovideo.jp"));
            string res = HttpGet(uri, ref user_session);


            if (res != null)
            {
                Match match = Regex.Match(res, iRegex);
                if (match.Success)
                {
                    name = match.Groups[1].Value;
                }
            }
            return name;
        }



        //-------------------------------------------------------------------------
        // 動画情報取得
        //-------------------------------------------------------------------------
        public Dictionary<string, string> GetPublishStatus(string iLiveID)
        {
            // check already logged in or not
            if (mIsLogin == false)
                return null;
            // check have a valid cookie or not
            if (this.mCookieLogin == null)
                return null;

            Dictionary<string, string> ret = new Dictionary<string, string>();

            // send request (GET)
            string uri = URI_GETPUBLISHSTATUS + iLiveID;
            string response = HttpGet(uri, ref this.mCookieLogin);

            if (response == null)
                return null;

            using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(response), false))
            using (XmlTextReader reader = new XmlTextReader(ms))
            {
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        // ステータス取得
                        if (reader.LocalName.Equals("getpublishstatus"))
                        {
                            for (int i = 0; i < reader.AttributeCount; i++)
                            {
                                reader.MoveToAttribute(i);
                                if (reader.Name == "status")
                                {
                                    ret["status"] = reader.Value;
                                }
                                else if (reader.Name == "time")
                                {
                                    ret["time"] = reader.Value;
                                }
                            }

                        }
                        // ベースタイム取得
                        else if (reader.LocalName.Equals("base_time"))
                        {
                            ret["base_time"] = reader.ReadString();
                        }
                        // 開始時間取得
                        else if (reader.LocalName.Equals("start_time"))
                        {
                            ret["start_time"] = reader.ReadString();
                        }
                        // 終了時間取得
                        else if (reader.LocalName.Equals("end_time"))
                        {
                            ret["end_time"] = reader.ReadString();
                        }
                        // コメントサーバーアドレス取得
                        else if (reader.LocalName.Equals("addr"))
                        {
                            ret["addr"] = reader.ReadString();
                        }
                        // コメントサーバーポート取得
                        else if (reader.LocalName.Equals("port"))
                        {
                            ret["port"] = reader.ReadString();
                        }
                        // スレッド取得
                        else if (reader.LocalName.Equals("thread"))
                        {
                            ret["thread"] = reader.ReadString();
                        }
                        // 来場者数取得
                        else if (reader.LocalName.Equals("watch_count"))
                        {
                            ret["watch_count"] = reader.ReadString();
                        }
                        // 来場者数取得
                        else if (reader.LocalName.Equals("code"))
                        {
                            ret["code"] = reader.ReadString();
                        }
                        // コミュニティー
                        else if (reader.LocalName.Equals("room_label"))
                        {
                            ret["room_label"] = reader.ReadString();
                        }
                        // ユーザーＩＤ
                        else if (reader.LocalName.Equals("user_id"))
                        {
                            ret["user_id"] = reader.ReadString();
                        }
                        // プレミアム
                        else if (reader.LocalName.Equals("is_premium"))
                        {
                            ret["is_premium"] = reader.ReadString();
                        }
                        // 名前
                        else if (reader.LocalName.Equals("nickname"))
                        {
                            ret["nickname"] = reader.ReadString();
                        }
                        // token
                        else if (reader.LocalName.Equals("token"))
                        {
                            ret["token"] = reader.ReadString();
                        }
                    }
                }
            }

            return ret;
        }

        //-------------------------------------------------------------------------
        // 動画情報取得
        //-------------------------------------------------------------------------
        public Dictionary<string, string> GetPlayerStatus(string iLiveID)
        {
            // check already logged in or not
            if (mIsLogin == false)
                return null;
            // check have a valid cookie or not
            if (this.mCookieLogin == null)
                return null;

            Dictionary<string, string> ret = new Dictionary<string, string>();

            // send request (GET)
            string uri = URI_GETPLAYERSTATUS + iLiveID;
            string response = HttpGet(uri, ref this.mCookieLogin);

            if (response == null)
                return null;

            //Utils.WriteLog(response);

            using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(response), false))
            using (XmlTextReader reader = new XmlTextReader(ms))
            {
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        // ステータス取得
                        if (reader.LocalName.Equals("getplayerstatus"))
                        {
                            for (int i = 0; i < reader.AttributeCount; i++)
                            {
                                reader.MoveToAttribute(i);
                                if (reader.Name == "status")
                                {
                                    ret["status"] = reader.Value;
                                }
                                else if (reader.Name == "time")
                                {
                                    ret["time"] = reader.Value;
                                }
                            }

                        }
                        // ベースタイム取得
                        else if (reader.LocalName.Equals("base_time"))
                        {
                            ret["base_time"] = reader.ReadString();
                        }
                        // 開始時間取得
                        else if (reader.LocalName.Equals("start_time"))
                        {
                            ret["start_time"] = reader.ReadString();
                        }
                        // コメントサーバーアドレス取得
                        else if (reader.LocalName.Equals("addr"))
                        {
                            ret["addr"] = reader.ReadString();
                        }
                        // コメントサーバーポート取得
                        else if (reader.LocalName.Equals("port"))
                        {
                            ret["port"] = reader.ReadString();
                        }
                        // スレッド取得
                        else if (reader.LocalName.Equals("thread"))
                        {
                            ret["thread"] = reader.ReadString();
                        }
                        // 来場者数取得
                        else if (reader.LocalName.Equals("watch_count"))
                        {
                            ret["watch_count"] = reader.ReadString();
                        }
                        // 来場者数取得
                        else if (reader.LocalName.Equals("code"))
                        {
                            ret["code"] = reader.ReadString();
                        }
                        // コミュニティー
                        else if (reader.LocalName.Equals("room_label"))
                        {
                            ret["room_label"] = reader.ReadString();
                        }
                        // ユーザーＩＤ
                        else if (reader.LocalName.Equals("user_id"))
                        {
                            ret["user_id"] = reader.ReadString();
                        }
                        // プレミアム
                        else if (reader.LocalName.Equals("is_premium"))
                        {
                            ret["is_premium"] = reader.ReadString();
                        }
                        // 名前
                        else if (reader.LocalName.Equals("nickname"))
                        {
                            ret["nickname"] = reader.ReadString();
                        }
                        // 名前
                        else if (reader.LocalName.Equals("title"))
                        {
                            ret["title"] = reader.ReadString();
                        }
                    }
                }
            }

            return ret;
        }

        //-------------------------------------------------------------------------
        // コミュ限か
        //-------------------------------------------------------------------------
        public bool IsMemberOnly(string iLiveID)
        {
            // check already logged in or not
            if (mIsLogin == false)
                return false;
            // check have a valid cookie or not
            if (this.mCookieLogin == null)
                return false;

            Dictionary<string, string> ret = new Dictionary<string, string>();

            // send request (GET)
            string uri = URI_WATCH_URL + "/" + iLiveID;
            string response = HttpGet(uri, ref this.mCookieLogin);

            if (response == null)
                return false;

            //Utils.WriteLog(response);

            string regex = "<ul id=\"livetags\"(.*?)>メンバー限定</a>(.*?)</li>";
            Regex reg = new Regex(regex, RegexOptions.Singleline);
            Match match = reg.Match(response);
            if (match.Success)
            {
                return true;
            }
            else
            {
                return false;
            }



        }

        //-------------------------------------------------------------------------
        // 主コメント送信
        //-------------------------------------------------------------------------
        public bool SendOwnerComment(string iLiveID, string iComment, string iName, string iToken)
        {
            return SendOwnerComment(iLiveID, iComment, "", iName, iToken);
        }


        //-------------------------------------------------------------------------
        // 主コメント送信
        //-------------------------------------------------------------------------
        public bool SendOwnerComment(string iLiveID, string iComment, string iMail, string iName, string iToken)
        {
            if (!IsLogin)
                return false;

            string uri = "http://watch.live.nicovideo.jp/api/broadcast/" + iLiveID;

            Dictionary<string, string> post_arg = new Dictionary<string, string>();

            post_arg["mail"] = iMail;
            post_arg["is184"] = "true";
            post_arg["token"] = iToken;
            post_arg["body"] = iComment;
            if (iName.Length > 0)
                post_arg["name"] = iName;

            Utils.WriteLog("SendOwnerComment:" + iComment);

            // send POST request
            string ret = HttpPost(uri, post_arg, ref this.mCookieLogin);
            Utils.WriteLog("SendOwnerComment ret = " + ret);
            post_arg = null;
            return true;
        }

        //-------------------------------------------------------------------------
        // PINGコメント送信
        //-------------------------------------------------------------------------
        public bool SendPING()
        {
            // 送信
            try
            {
                if (mTcp.Client.Connected)
                {
                    this.Send(mTcp.Client, "<ping>EOT</ping>\0");
                }
            }
            catch (Exception e)
            {
                Utils.WriteLog("SendPING(): " + e.StackTrace);
                Utils.WriteLog("SendPING()", e.Message);
                Utils.WriteLog("SendPING()", e.StackTrace);
            }

            return true;
        }

        //-------------------------------------------------------------------------
        // コメント送信
        //-------------------------------------------------------------------------
        public bool SendComment(string iLiveID, string iComment, bool i184)
        {
            Int32 block_no = 0;

            if (!IsLogin)
                return false;

            if (iComment.Length <= 0)
            {
                Utils.WriteLog("コメントが空");
                return false;
            }

            Utils.WriteLog("COMMENT:" + iComment);

            // MovieInfo取得
            if (mThread.Length <= 0 || mUserID.Length <= 0 || mBaseTime == 0)
            {
                Dictionary<string, string> h = GetPlayerStatus(iLiveID);
                //Dictionary<string,string> h = GetPublishStatus(iLiveID);

                if (h == null)
                {
                    Utils.WriteLog("unable get movieinfo");
                    return false;
                }
                mUserID = h["user_id"];
                mThread = h["thread"];
                UInt32.TryParse(h["base_time"], out mBaseTime);
                UInt16.TryParse(h["is_premium"], out mPremium);
            }

            // block_no
            Int32.TryParse(mLastRes, out block_no);
            block_no /= 100;

            // postkey取得
            string url = string.Format(
                            "http://live.nicovideo.jp/api/getpostkey?thread={0}&block_no={1}",
                            mThread,
                            block_no);

            string result = HttpGet(url, ref this.mCookieLogin);

            string postkey = "";
            Match match = Regex.Match(result, "postkey=(.+)");
            if (match.Success)
                postkey = match.Groups[1].Value;

            if (postkey.Length <= 0)
            {
                Utils.WriteLog("unable get postkey");
                return false;
            }

            // vpos計算time_tに変換
            UInt32 ret = (UInt32)time(IntPtr.Zero);
            UInt32 vpos = (ret - mBaseTime) * 100;

            string mail = i184 ? "184" : "";

            // コメント送信リクエスト作成
            string req = String.Format(
                    "<chat thread=\"{0}\" ticket=\"{2}\" postkey=\"{4}\" vpos=\"{1}\" mail=\" " + mail + "\" user_id=\"{3}\" premium=\"{5}\">{6}</chat>\0",
                    mThread,
                    vpos,
                    mTicket,
                    mUserID,
                    postkey,
                    mPremium,
                    iComment);

            Utils.WriteLog(req);

            // 送信
            this.Send(mTcp.Client, req);

            return true;
        }

        //-------------------------------------------------------------------------
        // コメントサーバーに接続
        //-------------------------------------------------------------------------
        public NicoErr ConnectToCommentServer(string iLiveID, int commentCount)
        {
            Utils.WriteLog("ConnectToCommentServer(): GetPlayerStatus()");
            Dictionary<string, string> minfo = GetPlayerStatus(iLiveID);

            if (minfo == null)
            {
                mIsLogin = false;
                return NicoErr.ERR_COULD_NOT_CONNECT_COMMENT_SERVER;
            }
            if (minfo["status"].ToString().CompareTo("ok") != 0)
            {
                mIsLogin = false;

                if (minfo["code"].ToString().CompareTo("full") == 0)
                {
                    return NicoErr.ERR_COMMENT_SERVER_IS_FULL;
                }
                else if (minfo["code"].ToString().CompareTo("closed") == 0)
                {
                    return NicoErr.ERR_CLOSED;
                }
                else if (minfo["code"].ToString().CompareTo("require_community_member") == 0)
                {
                    return NicoErr.ERR_COMMUNITY_ONLY;
                }
                return NicoErr.ERR_COULD_NOT_CONNECT_COMMENT_SERVER;
            }

            // uri of message server
            string uri = minfo["addr"] as string;
            if (uri == null)
                return NicoErr.ERR_COULD_NOT_CONNECT_COMMENT_SERVER;

            // thread
            string tid = minfo["thread"] as string;
            if (tid == null)
                return NicoErr.ERR_COULD_NOT_CONNECT_COMMENT_SERVER;

            // port
            string port = minfo["port"] as string;
            if (port == null)
                return NicoErr.ERR_COULD_NOT_CONNECT_COMMENT_SERVER;

            // request argument
            string req = string.Format(
                "<thread thread=\"{1}\" version=\"20061206\" res_from=\"-{0}\"/>\0",
                commentCount, tid
                );

            // send request
            try
            {
                Utils.WriteLog("Addr: " + uri + "    Port: " + port);
                if (mTcp != null && mTcp.Connected)
                {
                    mTcp.Client.Disconnect(false);
                    //mTcp.Close();                    
                }

                mTcp = new TcpClient(uri, int.Parse(port));
                Utils.WriteLog("ConnectToCommentServer(): サーバーと接続しました。");

                //NetworkStreamを取得する
                NetworkStream ns = mTcp.GetStream();

                // データ送信
                System.Text.Encoding enc = System.Text.Encoding.UTF8;
                byte[] sendBytes = enc.GetBytes(req);

                //データを送信する
                ns.Write(sendBytes, 0, sendBytes.Length);

                // 非同期受信開始
                StartReceive(mTcp.Client);

            }
            catch (Exception e)
            {
                Utils.WriteLog("ConnectToCommentServer(): send request: " + e.Message);
                mIsLogin = false;
                return NicoErr.ERR_COULD_NOT_CONNECT_COMMENT_SERVER;
            }

            minfo = null;
            return NicoErr.ERR_NO_ERR;
        }

        //-------------------------------------------------------------------------
        //非同期データ受信のための状態オブジェクト
        //-------------------------------------------------------------------------
        private class AsyncStateObject
        {
            public System.Net.Sockets.Socket Socket;
            public byte[] ReceiveBuffer;

            public AsyncStateObject(System.Net.Sockets.Socket soc)
            {
                this.Socket = soc;
                this.ReceiveBuffer = new byte[1024 * 4];
            }
        }

        //-------------------------------------------------------------------------
        //データ受信スタート
        //-------------------------------------------------------------------------
        private static void StartReceive(System.Net.Sockets.Socket soc)
        {
            AsyncStateObject so = new AsyncStateObject(soc);
            //非同期受信を開始
            soc.BeginReceive(so.ReceiveBuffer,
                0,
                so.ReceiveBuffer.Length,
                System.Net.Sockets.SocketFlags.None,
                new System.AsyncCallback(ReceiveDataCallback),
                so);
            mTmpBuffer = new byte[0];
        }

        //-------------------------------------------------------------------------
        //BeginReceiveのコールバック
        //-------------------------------------------------------------------------
        private static void ReceiveDataCallback(System.IAsyncResult ar)
        {
            //読み込んだ長さを取得
            int len = 0;
            AsyncStateObject so;
            Socket socket;

            try
            {
                //状態オブジェクトの取得
                so = (AsyncStateObject)ar.AsyncState;
                socket = so.Socket;




                len = socket.EndReceive(ar);
            }
            catch (System.ObjectDisposedException e)
            {
                //閉じた時
                Utils.WriteLog("ReceiveDataCallback(): ObjectDisposedException 閉じました。");
                Utils.WriteLog("ReceiveDataCallback()" + e.Message);
                //Utils.WriteLog("ReceiveDataCallback()" + e.StackTrace);
                mIsLogin = false;
                return;
            }
            catch (SocketException e)
            {
                //閉じた時
                Utils.WriteLog("ReceiveDataCallback(): SocketException 閉じました。");
                Utils.WriteLog("ReceiveDataCallback()" + e.Message);
                //Utils.WriteLog("ReceiveDataCallback()" + e.StackTrace);
                mIsLogin = false;
                return;
            }
            catch (Exception e)
            {
                //閉じた時
                Utils.WriteLog("ReceiveDataCallback(): Exception 閉じました。");
                Utils.WriteLog("ReceiveDataCallback()" + e.Message);
                Utils.WriteLog("ReceiveDataCallback()" + e.StackTrace);
                mIsLogin = false;
                return;
            }


            //切断されたか調べる
            if (len <= 0)
            {
                Utils.WriteLog("ReceiveDataCallback(): 切断されました。");
                so.Socket.Close();
                mIsLogin = false;
                //mComment = "<chat date=\"\" no=\"\" premium=\"2\" thread=\"\" user_id=\"\" vpos=\"\">コメントサーバーから切断されました</chat>";
                return;
            }

            //受信したデータを蓄積する
            int org = mTmpBuffer.Length;
            int sz = len + org;
            Array.Resize<byte>(ref mTmpBuffer, sz);
            Buffer.BlockCopy(so.ReceiveBuffer, 0, mTmpBuffer, org, len);

            //最後まで受信した時
            //受信したデータを文字列に変換       
            if (so.Socket.Available == 0)
            {
                string str = System.Text.Encoding.UTF8.GetString(mTmpBuffer, 0, mTmpBuffer.Length);
                //Utils.WriteLog("ReceiveDataCallback(): " + str );

                if (str.Contains("<thread"))
                {
                    string thread;
                    //<thread resultcode="0" thread="1143681983" ticket="0xd15f360" revision="1" server_time="1325401550"/>
                    Match match = Regex.Match(str, "thread=\"(.*?)\"");
                    if (match.Success)
                    {
                        thread = match.Groups[1].Value;

                    }
                    match = Regex.Match(str, "Last_res=\"(.*?)\"");
                    if (match.Success)
                    {
                        mLastRes = match.Groups[1].Value;

                    }
                    else
                    {

                        mLastRes = "0";
                    }
                    match = Regex.Match(str, "ticket=\"(.*?)\"");
                    if (match.Success)
                    {
                        mTicket = match.Groups[1].Value;

                    }


                }

                if (str.EndsWith("</chat>\0") || str.EndsWith("/>\0") || str.EndsWith("</ping>\0"))
                {
                    Array.Resize<byte>(ref mTmpBuffer, 0);
                    mComment += str;


                }
                if (str.Contains("<chat_result"))
                {
                    Utils.WriteLog(str);
                }
            }

            try
            {
                //再び受信開始
                so.Socket.BeginReceive(so.ReceiveBuffer,
                    0,
                    so.ReceiveBuffer.Length,
                    System.Net.Sockets.SocketFlags.None,
                    new System.AsyncCallback(ReceiveDataCallback),
                    so);
            }
            catch (Exception e)
            {
                //閉じた時
                Utils.WriteLog("再受信開始失敗");
                Utils.WriteLog("ReceiveDataCallback()" + e.Message);
                Utils.WriteLog("ReceiveDataCallback()" + e.StackTrace);
                mIsLogin = false;
                return;
            }
        }

        //-------------------------------------------------------------------------
        // 非同期送信
        //-------------------------------------------------------------------------
        private void Send(Socket client, String data)
        {
            // Convert the string data to byte data using ASCII encoding.
            byte[] byteData = Encoding.UTF8.GetBytes(data);
            try
            {
                // Begin sending the data to the remote device.
                client.BeginSend(byteData, 0, byteData.Length, SocketFlags.None,
                    new AsyncCallback(SendCallback), client);
            }
            catch (Exception e)
            {
                Utils.WriteLog("Send(): " + e.ToString());
            }

        }

        //-------------------------------------------------------------------------
        // 送信コールバック
        //-------------------------------------------------------------------------
        private static void SendCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.
                Socket client = (Socket)ar.AsyncState;

                // Complete sending the data to the remote device.
                int bytesSent = client.EndSend(ar);
                Utils.WriteLog(String.Format("Sent {0} bytes to server.", bytesSent));
            }
            catch (Exception e)
            {
                Utils.WriteLog("SendCallback(): " + e.ToString());
            }
        }

        //-------------------------------------------------------------------------
        // FMEプロファイルの取得
        //-------------------------------------------------------------------------
        public Dictionary<string, string> GetFMEProfile(string iLV)
        {
            string api_url = URI_GETFMEPROFILE + iLV;
            string xml = HttpGet(api_url, ref this.mCookieLogin);

            Dictionary<string, string> ret = new Dictionary<string, string>();

            ret["status"] = "ok";

            using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(xml), false))
            using (XmlTextReader reader = new XmlTextReader(ms))
            {
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        if (reader.LocalName.Equals("getfmeprofile"))
                        {
                            ret["status"] = "failed";
                            return ret;
                        }
                        else if (reader.LocalName.Equals("url"))
                            ret["url"] = reader.ReadString();
                        else if (reader.LocalName.Equals("stream"))
                            ret["stream"] = reader.ReadString();
                    }
                }
            }

            return ret;
        }

        //-------------------------------------------------------------------------
        // FMEプロファイルの取得
        //-------------------------------------------------------------------------
        public bool StartFME(string iLV, string iToken)
        {
            if (iLV.Length <= 2) return false;
            if (iToken.Length <= 0) return false;

            string url = URI_STARTFME + iLV + "?key=exclude&value=0&token=" + iToken;
            string xml = HttpGet(url, ref this.mCookieLogin);

            return (xml.Contains("status=\"ok\""));
        }

        //-------------------------------------------------------------------------
        // 過去の放送情報の取得
        //-------------------------------------------------------------------------
        public string GetAlreadtLive()
        {
            string url = "http://live.nicovideo.jp/my";
            string res = HttpGet(url, ref mCookieLogin);
            string lv = "";

            if (res == null) return lv;

            Match match = Regex.Match(res, "<a href=\"http://live.nicovideo.jp/watch/(lv[0-9]+)\" title=\"生放送ページへ戻る\"");

            if (match.Success)
                lv = match.Groups[1].Value;
            return lv;
        }

        //-------------------------------------------------------------------------
        // 過去の放送情報の取得
        //-------------------------------------------------------------------------
        public bool GetOldLiveInfo(string iLv, ref Dictionary<string, string> iInfo, ref Dictionary<string, string> iCom, ref Dictionary<string, string> iTag, ref Dictionary<string, string> iTaglock)
        {
            string lv = iLv.Replace("lv", "");
            string url = "http://live.nicovideo.jp/editstream?reuseid=" + lv;
            string res = HttpGet(url, ref mCookieLogin);

            if (res == null) return false;

            Match match = Regex.Match(res, "<input type=\"text\" name=\"title\" style=\"width:400px\" value=\"(.*?)\">");
            if (match.Success)
                iInfo["title"] = match.Groups[1].Value;
            else
                iInfo["title"] = "^-^";

            match = Regex.Match(res, "<textarea name=\"description\"(.*?)>(.*?)</textarea>", RegexOptions.Singleline);
            if (match.Success)
                iInfo["description"] = HttpUtility.HtmlDecode(match.Groups[2].Value);
            else
                iInfo["description"] = "^-^";

            // 広告設定取得
            match = Regex.Match(res, "<input type=\"radio\" name=\"ad_enable\" value=\"0\" checked", RegexOptions.Singleline);
            if (match.Success)
                iInfo["ad_enable"] = "0";
            else
                iInfo["ad_enable"] = "1";

            match = Regex.Match(res, "<option value=\"co(.*?)\" style=\"\" class=\".*\" selected >(.*?)</option>");
            if (match.Success)
            {
                iInfo["default_community"] = "co" + match.Groups[1].Value;
                iCom["co" + match.Groups[1].Value] = match.Groups[2].Value.Replace("<wbr />&#8203;", "");
            }
            MatchCollection mc = Regex.Matches(res, "<option value=\"co(.*?)\" style=\"\" class=.*?>(.*?)</option>");
            if (mc.Count > 0)
            {
                foreach (Match m in mc)
                {
                    if (!iInfo.ContainsKey("default_community"))
                    {
                        iInfo["default_community"] = "co" + m.Groups[1].Value;
                    }
                    iCom["co" + m.Groups[1].Value] = m.Groups[2].Value.Replace("<wbr />&#8203;", "");
                }
            }

            // タグ ----------------------------------------------------------------
            List<string> livetags = new List<string>();
            List<string> taglock = new List<string>();

            mc = Regex.Matches(res, @"<input type=""text"" value=""(.*?)""\s+?style=""width: 100px;"" name=""livetags(.*?)""", RegexOptions.Multiline);
            if (mc.Count > 0)
            {
                foreach (Match m in mc)
                {
                    livetags.Add(m.Groups[1].Value);
                }
            }

            mc = Regex.Matches(res, @"<input type=""checkbox"" value=""ロックする"" name=""taglock.*?"" id=""taglock.*?"".*?>", RegexOptions.Multiline);
            if (mc.Count > 0)
            {
                foreach (Match m in mc)
                {
                    if (m.Groups[1].Value.ToString().Contains("checked"))
                        taglock.Add("ロックする");
                    else
                        taglock.Add("");
                }
            }

            int i = 0;
            foreach (string s in livetags)
            {
                string tagkey = "livetags" + (i + 1).ToString();
                iTag[tagkey] = s;
                tagkey = "taglock" + (i + 1).ToString();
                iTaglock[tagkey] = taglock[i];
                i++;
            }

            mc = Regex.Matches(res, @"<input type=""checkbox"" value=""ロックする"" name=""livetaglockall"" id=""livetaglockall""\s+?(\w*?)\s+?>", RegexOptions.Multiline);
            if (mc.Count > 0)
            {
                foreach (Match m in mc)
                {
                    Utils.WriteLog(m.Groups[1].Value);
                    if (m.Groups[1].Value.ToString().Contains("checked"))
                        iInfo["livetaglockall"] = "ロックする";
                    else
                        iInfo["livetaglockall"] = "";
                }
            }

            //---------------------------------------------------------------------

            // カテゴリ
            string[] category = new string[] { "一般(その他)", "政治", "動物", "料理", "演奏してみた", "歌ってみた", "踊ってみた", "講座", "ゲーム", "動画紹介", "R18" };
            foreach (string s in category)
            {
                if (res.Contains("<option value=\"" + s + "\" selected"))
                {
                    iInfo["tags[0]"] = s;
                }
            }
            if (!iInfo.ContainsKey("tags[0]"))
            {
                iInfo["tags[0]"] = "一般(その他)";
            }

            if (res.Contains("input id=\"face\" name=\"tags[]\" type=\"checkbox\" value=\"顔出し\" checked>"))
            {
                iInfo["tags[1]"] = "顔出し";
            }
            else
            {
                iInfo["tags[1]"] = "";
            }
            if (res.Contains("input id=\"totu\" name=\"tags[]\" type=\"checkbox\" value=\"凸待ち\" checked>"))
            {
                iInfo["tags[2]"] = "凸待ち";
            }
            else
            {
                iInfo["tags[2]"] = "";
            }
            if (res.Contains("input id=\"cruise\" name=\"tags[]\" type=\"checkbox\" value=\"クルーズ待ち\"checked>"))
            {
                iInfo["tags[3]"] = "クルーズ待ち";
            }
            else
            {
                iInfo["tags[3]"] = "";
            }

            if (res.Contains("<input id=\"community_only\" type=\"checkbox\" value=\"2\" name=\"public_status\" checked>"))
            {
                iInfo["public_status"] = "2";
            }
            else
            {
                iInfo["public_status"] = "";
            }

            if (res.Contains("<input type=\"radio\" value=\"0\" id=\"timeshift_disabled\" name=\"timeshift_enabled\"  checked >"))
            {
                iInfo["timeshift_enabled"] = "0";
            }
            else
            {
                iInfo["timeshift_enabled"] = "1";
            }


            iInfo["is_charge"] = "false";
            iInfo["usecoupon"] = "";
            iInfo["back"] = "false";
            iInfo["is_wait"] = "";

            match = Regex.Match(res, "<input type=\"hidden\" name=\"confirm\" value=\"(.*?)\">");

            if (match.Success)
            {
                Utils.WriteLog(match.Groups[1].Value);
                iInfo["confirm"] = match.Groups[1].Value;
            }

            match = Regex.Match(res, "<input type=\"hidden\" id=\"twitter_tag\" name=\"twitter_tag\" value=\"(.*?)\">");
            if (match.Success)
                iInfo["twitter_tag"] = match.Groups[1].Value;
            else
                iInfo["twitter_tag"] = "";
            return (iInfo.ContainsKey("title") && iInfo["title"].Length > 0);
        }

        //-------------------------------------------------------------------------
        // 枠取り
        //-------------------------------------------------------------------------
        public WakuErr GetWaku(ref Dictionary<string, string> iParam, ref string iLv)
        {
            string url = "http://live.nicovideo.jp/editstream";
            string location = "";
            string res = HttpPost2(url, iParam, ref mCookieLogin, out location);

            if (res == null) return WakuErr.ERR_LOGIN;

            string org_res = res;
            Match match;

            if (location != null && location.Contains("watch/"))
            {
                match = Regex.Match(res, "watch/(lv[0-9]+)");
                if (match.Success)
                {
                    iLv = match.Groups[1].Value;
                }
                return WakuErr.ERR_NO_ERR;
            }



            // 改行除去
            res = res.Replace("\n", "");
            res = res.Replace("\r", "");

            // confirmチェック
            match = Regex.Match(res, "<input type=\"hidden\" name=\"confirm\" value=\"(.*?)\">");

            if (match.Success)
            {
                Utils.WriteLog("GetWaku() confirm =" + match.Groups[1].Value);
                iParam["confirm"] = match.Groups[1].Value;
            }

            // description更新（入力時はtextareaだけど規約確認の時はinputタグでbase64してあるので）
            match = Regex.Match(res, "<input type=\"hidden\" name=\"description\"(.*?)value=\"((.*?))\"");

            if (match.Success)
            {
                Utils.WriteLog("GetWaku() description =" + Utils.FromBase64(match.Groups[2].Value));
                iParam["description"] = Utils.FromBase64(match.Groups[2].Value);
            }

            // エラーチェック error_message
            match = Regex.Match(res, "<li id=\"error_message\">(.*?)</li>");
            if (match.Success)
            {
                string err = match.Groups[1].Value;
                if (err.Contains("タグは18文字以内にして下さい。"))
                    return WakuErr.ERR_TAG;
                if (err.Contains("文字数制限"))
                    return WakuErr.ERR_MOJI;
                if (err.Contains("既にこの時間に予約をしているか"))
                {
                    match = Regex.Match(res, "gate/(lv[0-9]+)\"");
                    if (match.Success)
                    {
                        iLv = match.Groups[1].Value;
                        return WakuErr.ERR_NO_ERR;
                    }
                    return WakuErr.ERR_ALREADY_LIVE;
                }
                if (err.Contains("混み合って"))
                    return WakuErr.ERR_KONZATU;
                if (err.Contains("既に順番待ち"))
                    return WakuErr.ERR_JUNBAN_ALREADY;
                if (err.Contains("順番"))
                    return WakuErr.ERR_JUNBAN_WAIT;
                if (err.Contains("放送枠の確保が行えませんでした"))
                    return WakuErr.ERR_KONZATU;
                if (err.Contains("メンテナンス"))
                    return WakuErr.ERR_MAINTE;
                if (err.Contains("多重投稿"))
                    return WakuErr.ERR_TAJU;
                if (err.Contains("放送するコミュニティが選択されていません"))
                    return WakuErr.ERR_ALREADY_LIVE;
                if (err.Contains("WEBページの有効期限が切れています"))
                    return WakuErr.ERR_WEB_PAGE_SESSION;
                if (err.Contains("番組説明文入力してください"))
                    return WakuErr.ERR_DESCRIPTION_EMPTY;
                else
                {
                    Utils.WriteLog(match.Groups[1].Value);
                    Utils.WriteLog("Nico.GetWaku() WakuErr.ERR_UNKOWN:" + match.Groups[1].Value, org_res);
                }
            }
            if (res.Contains("<h2>メンテナンス中です</h2>"))
            {
                return WakuErr.ERR_MAINTE;
            }

            if (res.Contains("<title>ニコニコ動画　ログインフォーム</title>"))
            {
                return WakuErr.ERR_LOGIN;
            }

            if (res.Contains("番組が見つかりません"))
            {
                return WakuErr.ERR_KIYAKU;
            }
            if (res.Contains("<h2>エラーが発生しました</h2>ただいまアクセス集中、または不具合発生中のために、ニコニコ生放送に繋がりにくくなっております。"))
            {
                return WakuErr.ERR_KIYAKU;
            }
            if (res.Contains("関係者入り口</a>"))
            {
                return WakuErr.ERR_KIYAKU;
            }
            if (res.Contains("<div class=\"kiyaku_txt\">"))
            {
                return WakuErr.ERR_KIYAKU;
            }

            match = Regex.Match(res, "editstream/(lv[0-9]+)\"");
            if (match.Success)
            {
                iLv = match.Groups[1].Value;
                return (res.Contains("<td id=\"txt_wait\"")) ? WakuErr.ERR_JUNBAN : WakuErr.ERR_NO_ERR;
            }
            /*
            match = Regex.Match(res, "<a href=\"http://live.nicovideo.jp/watch/lv(.*?)\" class=\"now\" title=\"放送中の自分の番組に移動します\">");
            if (match.Success)
            {
                iLv = "lv" + match.Groups[1].Value;
                return WakuErr.ERR_NO_ERR;
            }
            */

            Utils.WriteLog("Nico.GetWaku() 想定外", org_res);

            Utils.WriteLog(org_res);
            return WakuErr.ERR_UNKOWN;
        }

        //-------------------------------------------------------------------------
        // 直近の配信を取得
        //-------------------------------------------------------------------------
        public string GetRecentLive(string username, string password)
        {

            string lv = "";
            if (Login(username, password))
            {
                string url = "http://live.nicovideo.jp/my";
                string res = HttpGet(url, ref mCookieLogin);

                Match match = Regex.Match(res, "http://live.nicovideo.jp/editstream/lv(.*?)\"");
                if (match.Success)
                {
                    lv = "lv" + match.Groups[1].Value;
                }
            }
            return lv;
        }

        //-------------------------------------------------------------------------
        // 順番待ち情報を取得
        //-------------------------------------------------------------------------
        public Dictionary<string, string> GetJunban(string iLv)
        {
            iLv.Replace("lv", "");

            Dictionary<string, string> arr = new Dictionary<string, string>();

            string url = "http://live.nicovideo.jp/api/waitinfo/" + iLv;

            string res = HttpGet(url, ref mCookieLogin);

            Utils.WriteLog(res);
            Match match;
            match = Regex.Match(res, "<count>(.*?)</count>");
            if (match.Success)
                arr["count"] = match.Groups[1].Value;
            match = Regex.Match(res, "<start_time>(.*?)</start_time>");
            if (match.Success)
                arr["start_time"] = match.Groups[1].Value;
            match = Regex.Match(res, "<stream_status>(.*?)</stream_status>");
            if (match.Success)
                arr["stream_status"] = match.Groups[1].Value;
            return arr;
        }

        //-------------------------------------------------------------------------
        // Get data using GET request
        //-------------------------------------------------------------------------
        private string HttpGet(string url, ref CookieContainer cc)
        {
            // Create HttpWebRequest
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            req.UserAgent = "Nicolive co274186 " + Program.VERSION_KAI_PLUS;
            req.CookieContainer = cc;

            try
            {
                WebResponse res = req.GetResponse();

                // read response
                Stream resStream = res.GetResponseStream();
                using (StreamReader sr = new StreamReader(resStream, Encoding.UTF8))
                {
                    string result = sr.ReadToEnd();
                    sr.Close();
                    resStream.Close();
                    return result;
                }
            }
            catch (Exception e)
            {
                Utils.WriteLog("HttpGet:" + e.Message);
            }
            return null;
        }
        //-------------------------------------------------------------------------
        // Post( multipart/form-data版）
        //-------------------------------------------------------------------------
        private string HttpPost2(string url, Dictionary<string, string> vals, ref CookieContainer cc, out string oLocation)
        {
            string boundary = System.Environment.TickCount.ToString();
            oLocation = "";

            //WebRequestの作成
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;


            //メソッドにPOSTを指定
            req.Method = "POST";
            //ContentTypeを設定
            req.ContentType = "multipart/form-data; boundary=" + boundary;

            req.CookieContainer = cc;

            req.Referer = "http://live.nicovideo.jp/editstream";
            req.ServicePoint.Expect100Continue = false;
            req.KeepAlive = true;
            req.Accept = "application/xml,application/xhtml+xml,text/html;q=0.9,text/plain;q=0.8,image/png,*/*;q=0.5";
            req.UserAgent = "Nicolive co274186 " + Program.VERSION_KAI_PLUS;
            //req.Headers["Accept-Encoding"] = "gzip,deflate,sdch";
            req.Headers["Accept-Language"] = "ja,en-US;q=0.8,en;q=0.6";
            req.Headers["Accept-Charset"] = "utf-8,Shift_JIS;q=0.7,*;q=0.3";
            req.Headers["Origin"] = "http://live.nicovideo.jp";
            req.Headers["Cache-Control"] = "max-age=0";

            System.Text.Encoding enc = System.Text.Encoding.GetEncoding("utf-8");

            //POST送信するデータを作成
            string postData = "";
            foreach (string k in vals.Keys)
            {
                postData += "--" + boundary + "\r\n";
                postData += "Content-Disposition: form-data; name=\"" + k + "\"\r\n\r\n";
                postData += vals[k] + "\r\n";
            }
            postData += "--" + boundary + "--\r\n";
            //Utils.WriteLog(postData);

            // バイト列に変換
            byte[] startData = enc.GetBytes(postData);

            // 送信
            req.ContentLength = startData.Length;
            System.IO.Stream reqStream = req.GetRequestStream();
            reqStream.Write(startData, 0, startData.Length);

            // 返答読み込み
            try
            {
                WebResponse res = req.GetResponse();

                /*
                foreach (string s in res.Headers)
                {
                    Utils.WriteLog(s + ":" + res.Headers[s]);
                }
                */

                //string location = res.Headers[HttpResponseHeader.Location];
                // z eroでlocation headerこなくなった(´・ω・｀)
                // (  Д ) ﾟ ﾟ
                string location = res.ResponseUri.ToString();
                oLocation = location;

                // read response
                Stream resStream = res.GetResponseStream();
                using (StreamReader sr = new StreamReader(resStream, Encoding.UTF8))
                {
                    string result = sr.ReadToEnd();
                    sr.Close();
                    resStream.Close();
                    reqStream.Close();
                    return result;
                }
            }
            catch (Exception e)
            {
                Utils.WriteLog("HttpPost2:" + e.Message);
            }
            reqStream.Close();
            return "";
        }

        //-------------------------------------------------------------------------
        // Get data using POST request (Hash version)（application/x-www-form-urlencoded版）
        //-------------------------------------------------------------------------
        private string HttpPost(string url, Dictionary<string, string> vals, ref CookieContainer cc)
        {
            return HttpPost(url, vals, ref  cc, "");
        }

        private string HttpPost(string url, Dictionary<string, string> vals, ref CookieContainer cc, string user_agent)
        {
            // concatinate all key-value pair
            string param = "";
            foreach (string k in vals.Keys)
            {
                param += String.Format("{0}={1}&", Uri.EscapeDataString(k), Uri.EscapeDataString(vals[k]));
            }
            Utils.WriteLog("HttpPost param = " + param);
            byte[] data = Encoding.ASCII.GetBytes(param);

            // create HttpWebRequest
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            req.Method = "POST";

            req.Proxy = null;
            req.ContentType = "application/x-www-form-urlencoded";
            if (user_agent.Equals(""))
            {
                req.UserAgent = "Nicolive co274186 " + Program.VERSION_KAI_PLUS;
            }
            else
            {
                req.UserAgent = user_agent;
            }
            req.ContentLength = data.Length;
            req.CookieContainer = cc;

            // write POST data
            try
            {
                Stream reqStream = req.GetRequestStream();
                reqStream.Write(data, 0, data.Length);
                reqStream.Close();
            }
            catch (Exception e)
            {
                Utils.WriteLog("HttpPost:" + e.Message);
                mIsLogin = false;
            }
            try
            {
                WebResponse res = req.GetResponse();

                // read response
                Stream resStream = res.GetResponseStream();
                using (StreamReader sr = new StreamReader(resStream, Encoding.UTF8))
                {
                    string result = sr.ReadToEnd();
                    sr.Close();
                    resStream.Close();
                    return result;
                }
            }
            catch (Exception e)
            {
                mIsLogin = false;
                Utils.WriteLog("HttpPost:" + e.Message);
            }
            return null;

        }

        //-------------------------------------------------------------------------
        // Get data using POST request (Plain Text Version)
        //-------------------------------------------------------------------------
        private string HttpPost(string url, string arg, ref CookieContainer cc)
        {
            byte[] data = Encoding.ASCII.GetBytes(arg);

            // create HttpWebRequest
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            req.Method = "POST";
            req.UserAgent = "Nicolive co274186 " + Program.VERSION_KAI_PLUS;
            req.ContentType = "application/x-www-form-urlencoded";
            req.ContentLength = data.Length;
            req.CookieContainer = cc;

            // write POST data
            Stream reqStream = req.GetRequestStream();
            reqStream.Write(data, 0, data.Length);
            reqStream.Close();

            try
            {
                WebResponse res = req.GetResponse();

                // read response
                Stream resStream = res.GetResponseStream();
                using (StreamReader sr = new StreamReader(resStream, Encoding.UTF8))
                {
                    string result = sr.ReadToEnd();
                    sr.Close();
                    resStream.Close();
                    return result;
                }
            }
            catch (Exception e)
            {
                mIsLogin = false;
                Utils.WriteLog("HttpPost:" + e.Message);
            }
            return null;
        }

        //-------------------------------------------------------------------------
        // GetSaleList
        //-------------------------------------------------------------------------
        public bool GetSaleList(string iLv, ref List<SaleList> iSaleList)
        {
            string url = "http://watch.live.nicovideo.jp/api/getsalelist?v=" + iLv;
            string xml = HttpGet(url, ref mCookieLogin);

            //Utils.WriteLog(xml);

            if (!xml.Contains("status=\"ok\""))
                return false;

            MatchCollection match;
            iSaleList = new List<SaleList>();
            match = Regex.Matches(xml, "<label>(.*?)</label>");
            if (match.Count > 0)
            {
                foreach (Match m in match)
                {
                    SaleList sale = new SaleList();
                    sale.mLabel = m.Groups[1].Value;
                    iSaleList.Add(sale);
                }
            }

            int n = 0;
            match = Regex.Matches(xml, "<code>(.*?)</code>");
            if (match.Count > 0)
            {
                foreach (Match m in match)
                {
                    SaleList sale = iSaleList[n];
                    sale.mCode = m.Groups[1].Value;
                    iSaleList[n++] = sale;
                }
            }

            n = 0;
            match = Regex.Matches(xml, "<num>(.*?)</num>");
            if (match.Count > 0)
            {
                foreach (Match m in match)
                {
                    SaleList sale = iSaleList[n];
                    sale.mNum = m.Groups[1].Value;
                    iSaleList[n++] = sale;
                }
            }

            n = 0;
            match = Regex.Matches(xml, "<price>(.*?)</price>");
            if (match.Count > 0)
            {
                foreach (Match m in match)
                {
                    SaleList sale = iSaleList[n];
                    sale.mPrice = m.Groups[1].Value;
                    iSaleList[n++] = sale;
                }
            }

            n = 0;
            match = Regex.Matches(xml, "</code><item>(.*?)</item>");
            if (match.Count > 0)
            {
                foreach (Match m in match)
                {
                    SaleList sale = iSaleList[n];
                    sale.mItem = m.Groups[1].Value;
                    iSaleList[n++] = sale;
                }
            }


            /*

            SaleList sale = new SaleList();
            using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(xml), false))
            using (XmlTextReader reader = new XmlTextReader(ms))
            {
                while (reader.Read())
                {
                    
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        if (reader.LocalName.Equals("code"))
                            sale.mCode = reader.ReadString();
                        else if (reader.LocalName.Equals("num"))
                            sale.mNum = reader.ReadString();
                        else if (reader.LocalName.Equals("price"))
                            sale.mPrice = reader.ReadString();
                        else if (reader.LocalName.Equals("label"))
                            sale.mLabel = reader.ReadString();
                        else if (reader.LocalName.Equals("item")) ;
                            sale.mItem = reader.ReadString();
                    }
                    else if (reader.NodeType == XmlNodeType.EndElement)
                    {
                        if (sale.mItem.Length  > 0)
                        {
                            iSaleList.Add(sale);
                            sale = new SaleList();
                        }
                    }
                }
            }
             */
            return true;
        }

        //-------------------------------------------------------------------------
        // Purchase
        //-------------------------------------------------------------------------
        public bool Purchase(string iLv, string iToken, SaleList iItem)
        {
            string url = "http://watch.live.nicovideo.jp/api/usepoint?v=" + iLv + "&code=" + iItem.mCode + "&item=" + iItem.mItem + "&token=" + iToken + "&num=" + iItem.mNum;
            string xml = HttpGet(url, ref mCookieLogin);

            Utils.WriteLog(xml);

            if (!xml.Contains("status=\"ok\""))
                return false;

            return true;
        }


        public Dictionary<string, string> GetNGUser(string iLv)
        {

            // <id, token>
            Dictionary<string, string> ng_user = new Dictionary<string, string>();

            string url = URI_NG_URL +
                        "video=" + iLv +
                        "&mode=get";

            string res = HttpGet(url, ref mCookieLogin);

            MatchCollection mc = Regex.Matches(res, @"<ngclient id=""(.*?)"" token=""(?<token>.*?)"">(.*?)<type>id</type>(.*?)<source>(?<source>.*?)</source>", RegexOptions.Multiline);
            if (mc.Count > 0)
            {
                int i = 1;
                foreach (Match m in mc)
                {
                    //Utils.WriteLog(m.Groups["token"].Value);
                    //Utils.WriteLog(m.Groups["source"].Value);
                    ng_user[m.Groups["source"].Value] = m.Groups["token"].Value;
                    i++;
                }
            }

            return ng_user;

        }

        public TcpClient getTcpClient()
        {
            return mTcp;
        }

        public void setIsLogin(bool isLogin)
        {
            mIsLogin = isLogin;
        }


    }
}
//-------------------------------------------------------------------------
// ニコニコアクセスクラス
//
// Copyright (c) 金時豆(http://ch.nicovideo.jp/community/co48276)
//-------------------------------------------------------------------------
