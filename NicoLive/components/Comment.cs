﻿//-------------------------------------------------------------------------
// コメント管理クラス
//
// Copyright (c) 金時豆(http://ch.nicovideo.jp/community/co48276)
// $Id$
//-------------------------------------------------------------------------
using System;
using System.Text;
using System.Xml;
using System.IO;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace NicoLive
{
    public class Vote
    {
        // アンケート用
        private string[] mVoteList;
        private string mVoteTitle;

        public string[] VoteList
        {
            get { return mVoteList; }
            set { mVoteList = value; }
        }

        public string VoteTitle
        {
            get { return mVoteTitle; }
            set { mVoteTitle = value; }
        }

        public Vote()
        {
            mVoteList = new string[4];
            mVoteList[0] = "";
            mVoteList[1] = "";
            mVoteList[2] = "";
            mVoteList[3] = "";
            mVoteTitle = "";
        }
    }

    public class Comment
    {

        private bool mValid;
        private string mXml;

        // プロパティー
        private string mUid;
        private string mHandle;
        private string mNo;
        private string mDate;
        private string mPremium;
        private string mMail;
        private string mText;
        private bool mBackStage;
        private string mElapsedTime;

        // アクセッサ
        public string Xml
        {
            get { return mXml; }
            set { mXml = value; }
        }

        public string Uid
        {
            get { return mUid; }
            set { mUid = value; }
        }

        public string Handle
        {
            get { return mHandle; }
            set { mHandle = value; }
        }

        public string No
        {
            get { return mNo; }
            set { mNo = value; }
        }

        public string Date
        {
            get { return mDate; }
            set { mDate = value; }
        }

        public string Premium
        {
            get { return mPremium; }
            set { mPremium = value; }
        }

        public string Mail
        {
            get { return mMail; }
            set { mMail = value; }
        }

        public string Text
        {
            get { return mText; }
            set { mText = value; }
        }

        public bool Valid
        {
            get { return mValid; }
            set { mValid = value; }
        }

        public bool BackStage
        {
            get { return mBackStage; }
            set { mBackStage = value; }
        }

        public string ElapsedTime
        {
            get { return mElapsedTime; }
            set { mElapsedTime = value; }

        }

        //-------------------------------------------------------------------------
        // 初期化
        //-------------------------------------------------------------------------
        private void Init()
        {
            Valid = false;
        }


        //-------------------------------------------------------------------------
        // コンストラクタ
        //-------------------------------------------------------------------------
        public Comment(string iXml)
        {
            Init();

            mXml = iXml;

            using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(iXml), false))
            using (XmlTextReader reader = new XmlTextReader(ms))
            {
                try
                {
                    // コメントパーズ
                    while (reader.Read())
                    {
                        if (reader.NodeType == XmlNodeType.Element)
                        {
                            // ステータス取得
                            if (reader.LocalName.Equals("chat"))
                            {
                                string no = "", date = "", text = "", id = "", premium = "0", mail = "";

                                for (int i = 0; i < reader.AttributeCount; i++)
                                {
                                    reader.MoveToAttribute(i);
                                    if (reader.Name == "no")
                                    {
                                        no = reader.Value;
                                    }
                                    else if (reader.Name == "user_id")
                                    {
                                        id = reader.Value;
                                    }
                                    else if (reader.Name == "date")
                                    {
                                        date = reader.Value;
                                    }
                                    else if (reader.Name == "premium")
                                    {
                                        premium = reader.Value;
                                    }
                                    else if (reader.Name == "mail")
                                    {
                                        mail = reader.Value;
                                    }
                                }
                                text = reader.ReadString();

                                // メンバ設定
                                No = no;
                                Uid = id;
                                Date = date;
                                Premium = premium;
                                Mail = mail;
                                Text = text;
                                BackStage = false;

                                //// バックステージを通常コメント化する
                                //if (text.StartsWith("/press "))
                                //{
                                //    string[] arr = text.Split(' ');
                                //    int idx = text.IndexOf(arr[2]);
                                //    Text = text.Remove(0, idx + arr[2].Length+1);
                                //    BackStage = true;
                                //}

                                ////ニコ生クルースを通常コメント化する
                                //if (text.StartsWith("/telop "))
                                //{
                                //    string[] arr = text.Split(' ');
                                //    int idx = text.IndexOf(arr[3]);
                                //    Text = text.Remove(0, idx + arr[2].Length + 1);
                                //}

                                //// infoを通常コメント化する
                                //if (text.StartsWith("/info "))
                                //{
                                //    string[] arr = text.Split(' ');
                                //    int idx = text.IndexOf(arr[3]);
                                //    Text = text.Remove(0, idx + arr[2].Length + 1);
                                //}

                                Valid = true;
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLine("ParseComment:" + e.Message + "  " + iXml);
                }
            }
        }
        //-------------------------------------------------------------------------
        // NGコメントかどうか
        //-------------------------------------------------------------------------
        //public bool IsNG
        //{
        //    get
        //    {
        //        return false;
        //        /*
        //        if (!mNGChecked)
        //        {
        //            //NG ngList = NG.Instance;
        //            mNG = ngList.CheckNG(Text);
        //            mNGChecked = true;
        //        }
        //        return mNG;
        //         */
        //    }
        //}

        //-------------------------------------------------------------------------
        // スルーコメントかどうか
        //-------------------------------------------------------------------------
        //public bool IsIgnore
        //{
        //    get
        //    {
        //        if (Text.StartsWith("/telop ") ||
        //            Text.StartsWith("/press ") ||
        //            Text.StartsWith("/info ") ||
        //            Text.StartsWith("/chukei ")) return false;

        //        return (
        //            (Text.StartsWith("/")) ||
        //            (Text.StartsWith("■映像："))
        //            );
        //    }
        //}

        //-------------------------------------------------------------------------
        // 投票コメントかどうか
        //-------------------------------------------------------------------------
        public bool IsVote
        {
            get { return (Text.StartsWith("/vote")); }
        }

        //-------------------------------------------------------------------------
        // 切断コメントかどうか
        //-------------------------------------------------------------------------
        public bool IsDisconnect
        {
            get { return (Text.StartsWith("/disconnect") && (Premium.Equals("3") || Premium.Equals("2"))); }
        }

        //-------------------------------------------------------------------------
        // 運営のコメントかどうか
        //-------------------------------------------------------------------------
        public bool IsOperator
        {
            get { return (Premium.Equals("6")); }
        }

        //-------------------------------------------------------------------------
        // オーナーコメントかどうか
        //-------------------------------------------------------------------------
        public bool IsOwner
        {
            get { return (Premium.Equals("3") || Premium.Equals("2")); }
        }

        //-------------------------------------------------------------------------
        // プレミアコメントかどうか
        //-------------------------------------------------------------------------
        public bool IsPermium
        {
            get { return (Premium.Equals("1")); }
        }

        //-------------------------------------------------------------------------
        // バックステージコメントかどうか
        //-------------------------------------------------------------------------
        public bool IsBackStage
        {
            get { return BackStage; }
        }

        //-------------------------------------------------------------------------
        // 投票コメントを整形
        //-------------------------------------------------------------------------
        public void ToVote(ref Vote mVote)
        {

            string str = mText;
            if (mText.StartsWith("/vote start "))
            {


                string[] mVoteList = new string[4];
                mVoteList[0] = mVoteList[1] = mVoteList[2] = mVoteList[3] = "";

                mVote.VoteList = mVoteList;

                string pattern = " \"(.*?)\"";
                MatchCollection matches = Regex.Matches(mText, pattern);

                int i = 0;
                foreach (Match match in matches)
                {
                    if (i == 0)
                    {
                        mVote.VoteTitle = match.Groups[1].Value;
                    }
                    else
                    {
                        mVoteList[i - 1] = match.Groups[1].Value;
                    }
                    i++;
                }


                str = String.Format("アンケート開始【{0}】", mVote.VoteTitle);
                for (i = 0; i < 4; i++)
                {
                    if (mVoteList[i].Length > 0)
                    {
                        str += String.Format("\n選択肢{0}:{1} ", i + 1, mVoteList[i]);
                    }
                }


            }
            else if (mText.StartsWith("/vote showresult per "))
            {

                // /vote showresult per  0 0 0 0 0




                str = String.Format("アンケート結果【{0}】", mVote.VoteTitle);

                string pattern = "([0-9]+)";
                MatchCollection matches = Regex.Matches(mText, pattern);

                int i = 0;
                foreach (Match match in matches)
                {

                    string per = match.Groups[1].Value;
                    if (!per.Equals("0"))
                    {
                        per = per.Insert(per.Length - 1, ".");
                    }
                    if (mVote.VoteList[i].Length > 0)
                    {
                        str += String.Format("\n{0}：{1}% ", mVote.VoteList[i], per);

                    }
                    i++;
                    if (i >= 4) break;
                }


            }
            else if (mText.StartsWith("/vote stop"))
            {
                str = "アンケート終了";
            }

            Text = str;
        }
    }
}
//-------------------------------------------------------------------------
// Copyright (c) 金時豆(http://ch.nicovideo.jp/community/co48276)
// $Id$
//-------------------------------------------------------------------------
