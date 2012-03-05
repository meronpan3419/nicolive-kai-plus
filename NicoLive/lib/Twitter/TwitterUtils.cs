using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.IO;
using OAuth;

namespace iPentec.TwitterUtils
{
    public class TwitterUtils
    {
        const string EXP = "http://twitter.com/statuses/update.xml";
        const string TwitterUser = "(TwitterのユーザーID)";
        const string TwitterPass = "(Twitterのパスワード)";

        oAuthTwitter oAuth;
        public TwitterUtils()
        {
            oAuth = new oAuthTwitter();
        }

        public void UpdateStatus(string status)
        {
            PostData(EXP, TwitterUser, TwitterPass, status);
        }

        public void UpdateStatusOAuth(string status, string ConsumerKey, string ConsumerSecret, string AccessToken, string AccessTokenSecret)
        {
            PostDataOAuth(EXP, TwitterUser, TwitterPass, status, ConsumerKey, ConsumerSecret, AccessToken, AccessTokenSecret);
        }

        public string GetOAuthToken(string ConsumerKey, string ConsumerSecret)
        {
            //oAuthTwitter oAuth = new oAuthTwitter();
            oAuth.ConsumerKey = ConsumerKey;
            oAuth.ConsumerSecret = ConsumerSecret;

            string authurl = oAuth.AuthorizationLinkGet();// +"&oauth_callback=http://(コールバックURLを記入)";//記入しなくてもTwitterで登録した内容が反映されるためよい？
            return authurl;
        }

        public void GetOAuthAccessToken(string OAuthToken, string ConsumerKey, string ConsumerSecret,
          out string AccessToken, out string AccessTokenSecret)
        {
            //oAuthTwitter oAuth = new oAuthTwitter();
            oAuth.ConsumerKey = ConsumerKey;
            oAuth.ConsumerSecret = ConsumerSecret;

            oAuth.AccessTokenGet(OAuthToken);
            AccessToken = oAuth.Token;
            AccessTokenSecret = oAuth.TokenSecret;
        }

        public void GetOAuthAccessTokenWithPIN(string PIN, string ConsumerKey, string ConsumerSecret,
          out string AccessToken, out string AccessTokenSecret)
        {
            //oAuthTwitter oAuth = new oAuthTwitter();
            oAuth.ConsumerKey = ConsumerKey;
            oAuth.ConsumerSecret = ConsumerSecret;

            oAuth.AccessTokenGetWithPIN(PIN, oAuth.OAuthToken);
            AccessToken = oAuth.Token;
            AccessTokenSecret = oAuth.TokenSecret;
        }


        private string PostDataOAuth(string url, string user, string pass, string mes,
          string ConsumerKey, string ConsumerSecret, string AccessToken, string AccessTokenSecret)
        {
            string xml = "";
            oAuthTwitter oAuth = new oAuthTwitter();
            oAuth.ConsumerKey = ConsumerKey;
            oAuth.ConsumerSecret = ConsumerSecret;
            oAuth.Token = AccessToken;
            oAuth.TokenSecret = AccessTokenSecret;

            // 投稿内容をURLエンコードする。
            string encMes = System.Web.HttpUtility.UrlEncode(mes);
            encMes = string.Format("status={0:s}", encMes);
            xml = oAuth.oAuthWebRequest(oAuthTwitter.Method.POST, url, encMes);

            return xml;
        }

        private string PostData(string url, string user, string pass, string mes)
        {
            // 投稿内容をURLエンコードする。
            string encMes = System.Web.HttpUtility.UrlEncode(mes);

            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            request.Method = "POST";
            request.Credentials = new NetworkCredential(user, pass);                // 認証情報を設定
            // Content-typeヘッダを設定
            request.ContentType = "application/x-www-form-urlencoded";
            request.Timeout = 10000; // 規定値は100,000(100秒)

            System.Net.ServicePointManager.Expect100Continue = false;

            // 書き込む
            StreamWriter writer = new StreamWriter(request.GetRequestStream());
            writer.Write("status={0}", encMes);
            writer.Close();
            // 読み込む
            WebResponse response = request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string result = reader.ReadToEnd();
            // 後処理
            reader.Close();
            response.Close();
            return result;
        }
    }
}