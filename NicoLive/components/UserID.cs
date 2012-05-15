//-------------------------------------------------------------------------
// ユーザーＩＤ管理クラス
//
// Copyright (c) 金時豆(http://ch.nicovideo.jp/community/co48276)
// $Id$
//-------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Xml;
using System.Diagnostics;
using System.Threading;

//-------------------------------------------------------------------------
// クラス実装
//-------------------------------------------------------------------------
namespace NicoLive
{
	public class UserID
	{
		// コテハン用ハッシュテーブル
        private Dictionary<string, string> mNickHash = null;
        // コメビュ色ハッシュテーブル　
        private Dictionary<string, System.Drawing.Color> mColorHash= null;

        // NGユーザー用
        private List<string> mNGUserList = null;

        private ReaderWriterLock mLock = new ReaderWriterLock();

		private static UserID mInstance = null;

		//-------------------------------------------------------------------------
		// コンストラクタ
		//-------------------------------------------------------------------------
		private UserID()
		{
            mNickHash = new Dictionary<string, string>();
            mColorHash = new Dictionary<string, System.Drawing.Color>();
            mNGUserList = new List<string>();
		}

		//-------------------------------------------------------------------------
		// シングルトン用
		//-------------------------------------------------------------------------
		public static UserID Instance
		{
			get 
			{
				if (mInstance == null)
				{
					mInstance = new UserID();
				}
				return mInstance;
			}
		}

        //-------------------------------------------------------------------------
        // USER ID読み込み
        //-------------------------------------------------------------------------
        public void LoadUserID(bool iAnon)
        {
            string file = (iAnon) ? "anonymous.xml" : "userid.xml";

            using (XmlTextReader xml = new XmlTextReader(file))
            {
                if (xml == null)
                {
                    Debug.WriteLine(file + "が見つかりません");
                    return;
                }


                string id = "";
                string nick = "";
                System.Drawing.Color c = System.Drawing.Color.White;
                string color = String.Format("{0:x2}", c.A) + String.Format("{0:x2}", c.R) + String.Format("{0:x2}", c.G) + String.Format("{0:x2}", c.B);


                try
                {
                    while (xml.Read())
                    {
                        if (xml.NodeType == XmlNodeType.Element)
                        {
                            // 古いXMLファイルは削除
                            if (iAnon)
                            {
                                if (xml.LocalName.Equals("date"))
                                {
                                    string d = xml.ReadString();
                                    long create = long.Parse(d);

                                    TimeSpan ts = new TimeSpan(DateTime.Now.Ticks - create);

                                    // 七日以上経過は削除
                                    if (ts.Days > 7)
                                    {
                                        return;
                                    }

                                    // 木曜11時以降は作成日が木曜じゃない設定は削除
                                    DayOfWeek week = DateTime.Now.DayOfWeek;
                                    if (week == DayOfWeek.Thursday && DateTime.Now.Hour >= 11)
                                    {
                                        DateTime dt = new DateTime(create);
                                        DayOfWeek w = dt.DayOfWeek;
                                        if (w != DayOfWeek.Thursday || (w == DayOfWeek.Thursday && dt.Hour < 11))
                                        {
                                            Debug.WriteLine("REFRESH ID");
                                            return;
                                        }
                                    }

                                }
                            }

                            if (xml.LocalName.Equals("user"))
                            {
                                for (int i = 0; i < xml.AttributeCount; i++)
                                {
                                    xml.MoveToAttribute(i);
                                    if (xml.Name == "id")
                                    {
                                        id = xml.Value;
                                    } else if(xml.Name == "color")
                                    {
                                        color = xml.Value;
                                    }
                                }
                                nick = xml.ReadString();
                                string a = color.Substring(0, 2);
                                string r = color.Substring(2, 2);
                                string g = color.Substring(4, 2);
                                string b = color.Substring(6, 2);

                                mNickHash[id] = nick;
                                mColorHash[id] = System.Drawing.Color.FromArgb(Convert.ToInt32(a, 16), Convert.ToInt32(r, 16), Convert.ToInt32(b, 16), Convert.ToInt32(b, 16));
                                //Debug.WriteLine(id + "   " + nick);
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLine("LoadUser:" + e.Message);
                }
            }
        }

        //-------------------------------------------------------------------------
        // ユーザーＩＤセーブ
        //-------------------------------------------------------------------------
        public void SaveUserID(bool iAnon)
        {
            string file = (iAnon) ? "anonymous.xml" : "userid.xml";

            using (XmlTextWriter writer = new XmlTextWriter(file, null))
            {
                writer.Formatting = Formatting.Indented;

                writer.WriteStartDocument();

                long t = DateTime.Now.Ticks;
                string time = t.ToString();
                int r;

                writer.WriteStartElement("nick");
                // 作成時刻書き込み
                writer.WriteElementString("date", time);

                foreach (string id in mNickHash.Keys)
                {
                    if (iAnon != int.TryParse(id, out r))
                    {
                        if (mNickHash[id].ToString().Length > 0)
                        {
                            writer.WriteStartElement("user");
                            writer.WriteAttributeString("id", id);
                            if (mColorHash.ContainsKey(id))
                            {
                                System.Drawing.Color c = mColorHash[id];
                                string cstring = String.Format("{0:x2}", c.A) + String.Format("{0:x2}", c.R) + String.Format("{0:x2}", c.G) + String.Format("{0:x2}", c.B);
                                writer.WriteAttributeString("color", cstring);
                            }
                            else
                            {
                                System.Drawing.Color c = System.Drawing.Color.White;
                                string cstring = String.Format("{0:x2}", c.A) + String.Format("{0:x2}", c.R) + String.Format("{0:x2}", c.G) + String.Format("{0:x2}", c.B);
                                writer.WriteAttributeString("color", cstring);
                            }
                            
                            writer.WriteString(mNickHash[id]);
                            writer.WriteEndElement();
                        }
                    }
                }

                writer.WriteEndElement();
                writer.WriteEndDocument();

                writer.Flush();
                writer.Close();
            }
        }

        //-------------------------------------------------------------------------
        // NG USER ID読み込み
        //-------------------------------------------------------------------------
        public void LoadNGUserID(bool iAnon)
        {
            string file = (iAnon) ? "ng_anonymous.xml" : "ng_userid.xml";

            using (XmlTextReader xml = new XmlTextReader(file))
            {
                if (xml == null)
                {
                    Debug.WriteLine(file + "が見つかりません");
                    return;
                }

                string id = "";

                try
                {
                    while (xml.Read())
                    {
                        if (xml.NodeType == XmlNodeType.Element)
                        {
                            // 古いXMLファイルは削除
                            if (iAnon)
                            {
                                if (xml.LocalName.Equals("date"))
                                {
                                    string d = xml.ReadString();
                                    long create = long.Parse(d);

                                    TimeSpan ts = new TimeSpan(DateTime.Now.Ticks - create);

                                    // 七日以上経過は削除
                                    if (ts.Days > 7)
                                    {
                                        return;
                                    }

                                    // 木曜11時以降は作成日が木曜じゃない設定は削除
                                    DayOfWeek week = DateTime.Now.DayOfWeek;
                                    if (week == DayOfWeek.Thursday && DateTime.Now.Hour >= 11)
                                    {
                                        DateTime dt = new DateTime(create);
                                        DayOfWeek w = dt.DayOfWeek;
                                        if (w != DayOfWeek.Thursday || (w == DayOfWeek.Thursday && dt.Hour < 11))
                                        {
                                            Debug.WriteLine("REFRESH ID");
                                            return;
                                        }
                                    }

                                }
                            }


                            if (xml.LocalName.Equals("user"))
                            {
                                for (int i = 0; i < xml.AttributeCount; i++)
                                {
                                    xml.MoveToAttribute(i);
                                    if (xml.Name == "id")
                                    {
                                        id = xml.Value;
                                    }
                                }

                                mNGUserList.Add(id);
                                Debug.WriteLine("read ng user:  id:" + id);
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLine("LoadNGUser:" + e.Message);
                }
            }
        }

        //-------------------------------------------------------------------------
        // NGユーザーＩＤセーブ
        //-------------------------------------------------------------------------
        public void SaveNGUserID(bool iAnon)
        {
            string file = (iAnon) ? "ng_anonymous.xml" : "ng_userid.xml";

            using (XmlTextWriter writer = new XmlTextWriter(file, null))
            {
                writer.Formatting = Formatting.Indented;

                writer.WriteStartDocument();

                long t = DateTime.Now.Ticks;
                string time = t.ToString();
                int r;

                writer.WriteStartElement("ng_user");
                // 作成時刻書き込み
                writer.WriteElementString("date", time);

                foreach (string id in mNGUserList)
                {
                    if (iAnon != int.TryParse(id, out r))
                    {
                            writer.WriteStartElement("user");
                            writer.WriteAttributeString("id", id);
                            writer.WriteEndElement();
                    }
                }

                writer.WriteEndElement();
                writer.WriteEndDocument();

                writer.Flush();
                writer.Close();
            }
        }


        //-------------------------------------------------------------------------
        // ユーザーＩＤに対応するニックネームがあるかどうかチェック
        //-------------------------------------------------------------------------
		public bool Contains( string iID)
		{
            return mNickHash.ContainsKey(iID);
		}
	    //-------------------------------------------------------------------------
        // コテハンチェック
        //------------------------------------------------------------------------
        public string CheckNickname(string iID)
        {
            if (!mNickHash.ContainsKey(iID))
                return null;

            return mNickHash[iID];
        }
	    //-------------------------------------------------------------------------
        // コテハン記憶
        //-------------------------------------------------------------------------
        public void SetNickname(string iID, string iName)
        {
            try
            {
                mLock.AcquireWriterLock(Timeout.Infinite);

                if (mNickHash.ContainsKey(iID)) {

                    mNickHash.Remove(iID);
				}

                mNickHash[iID] = iName;
            }
            finally
            {
                mLock.ReleaseWriterLock();
            }
        }

        //-------------------------------------------------------------------------
        // コテハン記憶
        //-------------------------------------------------------------------------
        public bool AddNickname(string iID, string iName)
        {
            if (mNickHash.ContainsKey(iID)) return false;

            try
            {
                mLock.AcquireWriterLock(Timeout.Infinite);
                mNickHash[iID] = iName;
            }
            finally
            {
                mLock.ReleaseWriterLock();
            }
            return true;
        }

        //-------------------------------------------------------------------------
        // コテハン削除
        //-------------------------------------------------------------------------
        public bool delNickname(string iID)
        {
            if (!mNickHash.ContainsKey(iID)) return false;

            try
            {
                mLock.AcquireWriterLock(Timeout.Infinite);
                mNickHash.Remove(iID);
            }
            finally
            {
                mLock.ReleaseWriterLock();
            }
            return true;
        }

        //-------------------------------------------------------------------------
        // NGユーザー追加
        //-------------------------------------------------------------------------
        public bool AddNGUser(string iID)
        {
            if (mNGUserList.Contains(iID)) return false;

            try
            {
                mLock.AcquireWriterLock(Timeout.Infinite);
                mNGUserList.Add(iID);
            }
            finally
            {
                mLock.ReleaseWriterLock();
            }

            SaveNGUserID(true);
            SaveNGUserID(false);
            return true;
        }


        //-------------------------------------------------------------------------
        // NGユーザー削除
        //-------------------------------------------------------------------------
        public bool delNGUser(string iID)
        {
            if (!mNGUserList.Contains(iID)) return false;

            try
            {
                mLock.AcquireWriterLock(Timeout.Infinite);
                mNGUserList.RemoveAll(id => id == iID);
            }
            finally
            {
                mLock.ReleaseWriterLock();
            }

            SaveNGUserID(true);
            SaveNGUserID(false);
            return true;
        }

        //-------------------------------------------------------------------------
        // NGユーザーかどうか
        //-------------------------------------------------------------------------
        public bool IsNGUser(string iID)
        {
            return mNGUserList.Contains(iID);
        }

        //-------------------------------------------------------------------------
        // コメビュ色追加
        //-------------------------------------------------------------------------
        public bool AddUserColor(string iID, System.Drawing.Color iColor)
        {
            try
            {
                mLock.AcquireWriterLock(Timeout.Infinite);

                if (mColorHash.ContainsKey(iID))
                {

                    mColorHash.Remove(iID);
                }

                mColorHash[iID] = iColor;
            }
            finally
            {
                mLock.ReleaseWriterLock();
            }
            return true;
        }

        //-------------------------------------------------------------------------
        // コメビュ色取得
        //-------------------------------------------------------------------------
        public System.Drawing.Color getUserColor(string iID)
        {
            System.Drawing.Color color = Properties.Settings.Default.back_color;
            try
            {
                mLock.AcquireWriterLock(Timeout.Infinite);

                if (mColorHash.ContainsKey(iID))
                {

                    color = mColorHash[iID];
                }
            }
            finally
            {
                mLock.ReleaseWriterLock();
            }
            return color;
        }

        //-------------------------------------------------------------------------
        // コメビュ色更新
        //-------------------------------------------------------------------------
        public bool updateUserColor(string iID, System.Drawing.Color iColor)
        {
            try
            {
                mLock.AcquireWriterLock(Timeout.Infinite);

                if (mColorHash.ContainsKey(iID))
                {

                    mColorHash.Remove(iID);
                }

                mColorHash[iID] = iColor;
            }
            finally
            {
                mLock.ReleaseWriterLock();
            }
            return true;
        }

	}
}

//-------------------------------------------------------------------------
// ユーザーＩＤ管理クラス
//
// Copyright (c) 金時豆(http://ch.nicovideo.jp/community/co48276)
//-------------------------------------------------------------------------
