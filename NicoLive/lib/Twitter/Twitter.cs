//-------------------------------------------------------------------------
// Twitterアクセスクラス
//
// Copyright (c) 金時豆(http://ch.nicovideo.jp/community/co48276)
// $Id$
//-------------------------------------------------------------------------
using System;
using System.Diagnostics;
using TweetSharp;

//-------------------------------------------------------------------------
// クラス実装
//-------------------------------------------------------------------------
namespace NicoLive
{
    class Twitter :IDisposable
    {


        string mConsumerKey = "WZwUqBVTPLOn6gRXNVW9Q";
        string mConsumerSecret = "QUQS4nSTCCKRSFCAAQkCpgeo2wMaNM4GQ4FNuAr1FU";
        string mToken = "";
        string mTokenSecret = "";
        TwitterService mTwitterService;
        OAuthRequestToken mRequestToken;

        public Twitter()
        {
            mTwitterService = new TwitterService(mConsumerKey, mConsumerSecret);
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

            mRequestToken = mTwitterService.GetRequestToken();
            Uri uri = mTwitterService.GetAuthenticationUrl(mRequestToken);

            return uri.ToString();

        }


        public bool oAuth(string iPIN)
        {

            try
            {

                OAuthAccessToken access = mTwitterService.GetAccessToken(mRequestToken, iPIN);
                mTwitterService.AuthenticateWith(access.Token, access.TokenSecret);

                mToken = access.Token;
                mTokenSecret = access.TokenSecret;

                Utils.WriteLog(mToken);
                Utils.WriteLog(mTokenSecret);

                Properties.Settings.Default.tw_token = mToken;
                Properties.Settings.Default.tw_token_secret = mTokenSecret;

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

            
            try
            {
                mTwitterService.SendTweet(new SendTweetOptions { Status = iMessage });
            }
            catch (Exception)
            {
               // throw e;
               // Utils.WriteLog("twitter post :" + e.StackTrace.ToString());


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
