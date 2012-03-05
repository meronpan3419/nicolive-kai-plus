//-------------------------------------------------------------------------
// Twitterアクセスクラス
//
// Copyright (c) 金時豆(http://ch.nicovideo.jp/community/co48276)
// $Id$
//-------------------------------------------------------------------------
using System;
using System.Diagnostics;
using iPentec.TwitterUtils;

//-------------------------------------------------------------------------
// クラス実装
//-------------------------------------------------------------------------
namespace NicoLive
{
    class Twitter :IDisposable
    {


        string ConsumerKey = "";
        string ConsumerSecret = "";
        string Token = "";
        string TokenSecret = "";
        TwitterUtils tu;

        public Twitter()
        {
            tu = new TwitterUtils();
        }

        //-------------------------------------------------------------------------
        // 解放
        //-------------------------------------------------------------------------
        public void Dispose()
        {
        }

        //-------------------------------------------------------------------------
        // oAuth認証
        //-------------------------------------------------------------------------
		public string GetOAuthToken()
		{
    
            string url = tu.GetOAuthToken(ConsumerKey, ConsumerSecret);

            return url;

        }


        public bool oAuth(string pin)
        {

            try
            {


                tu.GetOAuthAccessTokenWithPIN(
                      pin,
                      ConsumerKey, ConsumerSecret,
                      out Token, out TokenSecret);

                Debug.WriteLine(Token);
                Debug.WriteLine(TokenSecret);

                Properties.Settings.Default.tw_passwd = "";
                Properties.Settings.Default.tw_user = "";

                Properties.Settings.Default.tw_token = Token;
                Properties.Settings.Default.tw_token_secret = TokenSecret;

            }
            catch (Exception e)
            {
                throw e;
            }
            

            return false;
		}

        //-------------------------------------------------------------------------
        // ポスト(oAuth版)
        //-------------------------------------------------------------------------
        public bool Post(string iMessage,string iTopic)
        {
            if (Properties.Settings.Default.tw_token.Length == 0 ||
                Properties.Settings.Default.tw_token_secret.Length == 0)
                return false;


            if (iTopic.Length > 0)
                iMessage += " " + iTopic;

            TwitterUtils tu = new TwitterUtils();
            try
            {
                tu.UpdateStatusOAuth(
                    iMessage,
                      ConsumerKey, ConsumerSecret,
                      Properties.Settings.Default.tw_token, Properties.Settings.Default.tw_token_secret);
            }
            catch (Exception)
            {
               // throw e;
               // System.Diagnostics.Debug.WriteLine("twitter post :" + e.StackTrace.ToString());


            }
            return true;
		}
    }
}
//-------------------------------------------------------------------------
// Twitterアクセスクラス
//
// Copyright (c) 金時豆(http://ch.nicovideo.jp/community/co48276)
//-------------------------------------------------------------------------
