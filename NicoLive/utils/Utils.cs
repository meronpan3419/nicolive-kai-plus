//-------------------------------------------------------------------------
// 雑多な関数管理クラス
//
// Copyright (c) 金時豆(http://ch.nicovideo.jp/community/co48276)
// Copyright (c) meronpan(http://ch.nicovideo.jp/community/co274186)
//-------------------------------------------------------------------------
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Drawing;
using System;
using System.IO;
using System.Collections.Generic;
using System.Net;

//-------------------------------------------------------------------------
// クラス実装
//-------------------------------------------------------------------------
namespace NicoLive
{
    public class Utils
    {
        // コメントカラム
        private enum CommentColumn : int
        {
            COLUMN_NUMBER = 0,			// 番号
            COLUMN_ID,					// ＩＤ
            COLUMN_HANDLE,				// コテハン
            COLUMN_COMMENT,				// コメント
            COLUMN_TIME,				// 時間
            COLUMN_INFO,				// 情報
        }

        //-------------------------------------------------------------------------
        // 文字コード変換
        //-------------------------------------------------------------------------
        public static string ConvertEncoding(string _src
                                      , System.Text.Encoding _srcEncoding
                                      , System.Text.Encoding _destEncoding)
        {
            byte[] srcBytes = _srcEncoding.GetBytes(_src);
            return _destEncoding.GetString(srcBytes);
        }
        public static string ToBase64(string iStr)
        {
            byte[] byteData = System.Text.Encoding.UTF8.GetBytes(iStr);
            return System.Convert.ToBase64String(byteData);
        }
        public static string FromBase64(string iStr)
        {
            byte[] byteData = System.Convert.FromBase64String(iStr);
            return System.Text.Encoding.UTF8.GetString(byteData);
        }
        //-------------------------------------------------------------------------
        // テキスト内のURLを開く
        //-------------------------------------------------------------------------
        public static void OpenURL(string iText)
        {
            string text = Regex.Replace(
                iText,
               @"(http|https):\/\/[\w\-_]+(\.[\w\-_]+)+([\w\-\.,@?^=%&amp;:/~\+#]*[\w\-\@?^=%&amp;/~\+#])",
               delegate(Match match)
               {
                   System.Diagnostics.Process.Start(match.ToString());
                   return match.ToString();
               });
        }

        ////-------------------------------------------------------------------------
        //// コメント追加
        ////-------------------------------------------------------------------------
        //public static void AddComment(ref DataGridView iView, Comment iCmt)
        //{


        //    iView.Rows.Insert(0, iView.Rows);


        //    iView.Rows[0].Cells[(int)CommentColumn.COLUMN_NUMBER].Value = iCmt.No;
        //    iView.Rows[0].Cells[(int)CommentColumn.COLUMN_ID].Value = iCmt.Uid;
        //    iView.Rows[0].Cells[(int)CommentColumn.COLUMN_HANDLE].Value = iCmt.Handle;
        //    iView.Rows[0].Cells[(int)CommentColumn.COLUMN_COMMENT].Value = iCmt.Text;


        //    if (iCmt.Premium.Equals("3"))
        //    {
        //        iView.Rows[0].Cells[(int)CommentColumn.COLUMN_COMMENT].Style.ForeColor = Properties.Settings.Default.owner_color;
        //    }
        //    //else if (iCmt.IsNG)
        //    //{
        //    //    iView.Rows[0].Cells[(int)CommentColumn.COLUMN_COMMENT].Style.ForeColor = Properties.Settings.Default.ng_color;
        //    //}
        //    else if (iCmt.Mail.Contains("docomo"))
        //    {
        //        iView.Rows[0].Cells[(int)CommentColumn.COLUMN_COMMENT].Style.ForeColor = Properties.Settings.Default.mobile_color;
        //    }

        //    // テロップの抜き出し
        //    if (iCmt.Text.StartsWith("/telop "))
        //    {
        //        // 色はパープル
        //        iView.Rows[0].Cells[(int)CommentColumn.COLUMN_COMMENT].Style.ForeColor = Color.Purple;

        //        //Regex deleteCommand = new Regex("^/telop (?:show|show0|perm) (.*)$");
        //        //Match m = deleteCommand.Match(iCmt.Text);
        //        //if (m.Success)
        //        //{
        //        //     iView.Rows[0].Cells[(int)CommentColumn.COLUMN_COMMENT].Value =m.Groups[1].Value;
        //        //}
        //    }

        //    // change color for /press コマンド
        //    if (iCmt.Text.StartsWith("/press"))
        //    {
        //        Regex deleteCommand = new Regex("^/press show (white|red|pink|orange|green|cyan|blue|purple|black) ");
        //        Match m = deleteCommand.Match(iCmt.Text);
        //        if (m.Success)
        //        {
        //            if (m.Groups[1].Value.Contains("red"))
        //            {
        //                iView.Rows[0].Cells[(int)CommentColumn.COLUMN_COMMENT].Style.ForeColor = Color.Red;
        //            }
        //            else if (m.Groups[1].Value.Contains("pink"))
        //            {
        //                iView.Rows[0].Cells[(int)CommentColumn.COLUMN_COMMENT].Style.ForeColor = Color.Pink;
        //            }
        //            else if (m.Groups[1].Value.Contains("orange"))
        //            {
        //                iView.Rows[0].Cells[(int)CommentColumn.COLUMN_COMMENT].Style.ForeColor = Color.Orange;
        //            }
        //            else if (m.Groups[1].Value.Contains("green"))
        //            {
        //                iView.Rows[0].Cells[(int)CommentColumn.COLUMN_COMMENT].Style.ForeColor = Color.Green;
        //            }
        //            else if (m.Groups[1].Value.Contains("cyan"))
        //            {
        //                iView.Rows[0].Cells[(int)CommentColumn.COLUMN_COMMENT].Style.ForeColor = Color.Cyan;
        //            }
        //            else if (m.Groups[1].Value.Contains("blue"))
        //            {
        //                iView.Rows[0].Cells[(int)CommentColumn.COLUMN_COMMENT].Style.ForeColor = Color.Blue;
        //            }
        //            else if (m.Groups[1].Value.Contains("purple"))
        //            {
        //                iView.Rows[0].Cells[(int)CommentColumn.COLUMN_COMMENT].Style.ForeColor = Color.Purple;
        //            }
        //        }

        //        //deleteCommand = new Regex("^/press show (?:white |niconicowhite |red |pink |orange |green |cyan |blue |purple |black )?(.*)$");
        //        //m = deleteCommand.Match(iCmt.Text);
        //        //if (m.Success)
        //        //{
        //        //    iView.Rows[0].Cells[(int)CommentColumn.COLUMN_COMMENT].Value = m.Groups[1].Value;
        //        //}
        //    }

        //    // /chukei の色制御
        //    if (iCmt.Text.StartsWith("/chukei"))
        //    {
        //        Regex deleteCommand = new Regex("^/chukei ([0-9a-f]{3})[0-9a-f]{2}");
        //        Match m = deleteCommand.Match(iCmt.Text);
        //        if (m.Success)
        //        {
        //            int color = Convert.ToInt32(m.Groups[1].Value, 16);
        //            int r = (color >> 8) * 17;
        //            int g = ((color >> 4) & 15) * 17;
        //            int b = (color & 15) * 17;
        //            iView.Rows[0].Cells[(int)CommentColumn.COLUMN_COMMENT].Style.ForeColor = Color.FromArgb(r, g, b);
        //        }
        //    }

        //    int max_item = Properties.Settings.Default.comment_max;
        //    if (iView.Rows.Count > max_item)
        //    {
        //        iView.Rows.RemoveAt(max_item);
        //    }

        //}

        private static DataGridViewRow makeCommentRow(ref DataGridView iView, Comment iCmt, System.Drawing.Color iColor)
        {
            DataGridViewRow row = new DataGridViewRow();

            row.CreateCells(iView);

            row.Cells[(int)CommentColumn.COLUMN_NUMBER].Style.BackColor = iColor;
            row.Cells[(int)CommentColumn.COLUMN_ID].Style.BackColor = iColor;
            row.Cells[(int)CommentColumn.COLUMN_HANDLE].Style.BackColor = iColor;
            row.Cells[(int)CommentColumn.COLUMN_COMMENT].Style.BackColor = iColor;
            row.Cells[(int)CommentColumn.COLUMN_INFO].Style.BackColor = iColor;
            row.Cells[(int)CommentColumn.COLUMN_TIME].Style.BackColor = iColor;

            row.Cells[(int)CommentColumn.COLUMN_NUMBER].Value = iCmt.No;
            row.Cells[(int)CommentColumn.COLUMN_ID].Value = iCmt.Uid;
            row.Cells[(int)CommentColumn.COLUMN_HANDLE].Value = iCmt.Handle;
            row.Cells[(int)CommentColumn.COLUMN_COMMENT].Value = iCmt.Text;

            if (iCmt.ElapsedTime != null)
            {
                row.Cells[(int)CommentColumn.COLUMN_TIME].Value = UNIX_EPOCH.AddSeconds(int.Parse(iCmt.ElapsedTime)).ToString("HH:mm:ss");
            }
            else
            {
                row.Cells[(int)CommentColumn.COLUMN_TIME].Value = "";

            }
            //1	プレミアム会員
            //2	運営のシステム
            //3	放送主
            //6	バックステージパス及び公式生の運営コメント
            //7	バックステージパス
            //8	新iPhoneアプリからのコメント
            //9	新iPhoneアプリからのコメント

            string info = "";

            // 情報設定
            if (iCmt.Premium.Equals("1"))
            {
                //プレミアム会員
                info += "P ";

            }
            else if (iCmt.Premium.Equals("2"))
            {
                //運営
                info += "運 ";
            }
            else if (iCmt.Premium.Equals("3"))
            {
                //主
                info += "主 ";
                row.Cells[(int)CommentColumn.COLUMN_COMMENT].Style.ForeColor = Properties.Settings.Default.owner_color;
            }
            else if (iCmt.Premium.Equals("6"))
            {
                // BSP, 公式
                info += "BSP ";
            }
            else if (iCmt.Premium.Equals("7"))
            {
                // BSP
                info += "BSP ";
            }
            else if (iCmt.Premium.Equals("8"))
            {
                // BSP
                info += "iPhone ";
            }
            else if (iCmt.Premium.Equals("9"))
            {
                // BSP
                info += "iPhone ";
            }
            if (iCmt.Mail.Contains("docomo"))
            {
                // BSP
                info += "docomo ";
                row.Cells[(int)CommentColumn.COLUMN_COMMENT].Style.ForeColor = Properties.Settings.Default.mobile_color;
            }

            // NGスコア
            info += iCmt.Score + " ";


            row.Cells[(int)CommentColumn.COLUMN_INFO].Value = info;

            // NGコメントは送られてこない
            //else if (iCmt.IsNG) 
            //{
            //    iView.Rows[0].Cells[(int)CommentColumn.COLUMN_COMMENT].Style.ForeColor = Properties.Settings.Default.ng_color;
            //}



            // テロップの抜き出し
            if (iCmt.Text.StartsWith("/telop "))
            {
                // 色はパープル
                row.Cells[(int)CommentColumn.COLUMN_COMMENT].Style.ForeColor = Color.Purple;



                //Regex deleteCommand = new Regex("^/telop (?:show|show0|perm) (.*)$");
                //Match m = deleteCommand.Match(iCmt.Text);
                //if (m.Success)
                //{
                //     iView.Rows[i].Cells[(int)CommentColumn.COLUMN_COMMENT].Value =m.Groups[1].Value;
                //}
            }

            // change color for /press コマンド
            if (iCmt.Text.StartsWith("/press"))
            {
                Regex deleteCommand = new Regex("^/press show (white|red|pink|orange|green|cyan|blue|purple|black) ");
                Match m = deleteCommand.Match(iCmt.Text);
                if (m.Success)
                {
                    if (m.Groups[1].Value.Contains("red"))
                    {
                        row.Cells[(int)CommentColumn.COLUMN_COMMENT].Style.ForeColor = Color.Red;
                    }
                    else if (m.Groups[1].Value.Contains("pink"))
                    {
                        row.Cells[(int)CommentColumn.COLUMN_COMMENT].Style.ForeColor = Color.Pink;
                    }
                    else if (m.Groups[1].Value.Contains("orange"))
                    {
                        row.Cells[(int)CommentColumn.COLUMN_COMMENT].Style.ForeColor = Color.Orange;
                    }
                    else if (m.Groups[1].Value.Contains("green"))
                    {
                        row.Cells[(int)CommentColumn.COLUMN_COMMENT].Style.ForeColor = Color.Green;
                    }
                    else if (m.Groups[1].Value.Contains("cyan"))
                    {
                        row.Cells[(int)CommentColumn.COLUMN_COMMENT].Style.ForeColor = Color.Cyan;
                    }
                    else if (m.Groups[1].Value.Contains("blue"))
                    {
                        row.Cells[(int)CommentColumn.COLUMN_COMMENT].Style.ForeColor = Color.Blue;
                    }
                    else if (m.Groups[1].Value.Contains("purple"))
                    {
                        row.Cells[(int)CommentColumn.COLUMN_COMMENT].Style.ForeColor = Color.Purple;
                    }
                }

                //deleteCommand = new Regex("^/press show (?:white |niconicowhite |red |pink |orange |green |cyan |blue |purple |black )?(.*)$");
                //m = deleteCommand.Match(iCmt.Text);
                //if (m.Success)
                //{
                //    iView.Rows[0].Cells[(int)CommentColumn.COLUMN_COMMENT].Value = m.Groups[1].Value;
                //}
            }

            // /chukei の色制御
            if (iCmt.Text.StartsWith("/chukei"))
            {
                Regex deleteCommand = new Regex("^/chukei ([0-9a-f]{3})[0-9a-f]{2}");
                Match m = deleteCommand.Match(iCmt.Text);
                if (m.Success)
                {
                    int color = Convert.ToInt32(m.Groups[1].Value, 16);
                    int r = (color >> 8) * 17;
                    int g = ((color >> 4) & 15) * 17;
                    int b = (color & 15) * 17;
                    row.Cells[(int)CommentColumn.COLUMN_COMMENT].Style.ForeColor = Color.FromArgb(r, g, b);
                }
            }
            return row;
        }

        //-------------------------------------------------------------------------
        // コメント追加
        //-------------------------------------------------------------------------
        public static void AddComment(ref DataGridView iView, Comment iCmt, System.Drawing.Color iColor)
        {
            try
            {
                DataGridViewRow row = makeCommentRow(ref iView, iCmt, iColor);

                int i = 0;
                if (!Properties.Settings.Default.comment_sort_desc)
                {
                    i = iView.Rows.Count;
                }


                iView.Rows.Insert(i, row);

                int max_item = Properties.Settings.Default.comment_max;
                if (iView.Rows.Count > max_item)
                {
                    iView.Rows.RemoveAt(max_item);
                }
            }
            catch (Exception e)
            {
                Utils.WriteLog("AddComment() :" + e.Message);
            }

            //iView.Refresh();

        }

        //-------------------------------------------------------------------------
        // コメント追加
        //-------------------------------------------------------------------------
        public static void AddComment(ref DataGridView iView, Comment iCmt, System.Drawing.Color iColor, ref List<DataGridViewRow> iPastChatList)
        {


            DataGridViewRow row = makeCommentRow(ref iView, iCmt, iColor);

            if (Properties.Settings.Default.comment_sort_desc)
            {
                iPastChatList.Insert(0, row);
            }
            else
            {
                iPastChatList.Add(row);
            }

            //int max_item = Properties.Settings.Default.comment_max;
            //if (iView.Rows.Count > max_item)
            //{
            //    iView.Rows.RemoveAt(max_item);
            //}

            //iView.Refresh();

        }

        //-------------------------------------------------------------------------
        // リスト中のニックネームを変更
        //-------------------------------------------------------------------------
        public static void SetNickname(ref DataGridView iView, string iID, string iName)
        {
            // 既存アイテムのIDをコテハンに書き換え
            int cnt = iView.Rows.Count;
            for (int i = 0; i < cnt; i++)
            {
                DataGridViewRow item = iView.Rows[i];
                if (item != null)
                {
                    string id = item.Cells[(int)CommentColumn.COLUMN_ID].Value.ToString();
                    if (id.Equals(iID))
                    {
                        item.Cells[(int)CommentColumn.COLUMN_HANDLE].Value = iName;
                    }
                }
            }
        }

        //-------------------------------------------------------------------------
        // リスト中のコメント色更新
        //-------------------------------------------------------------------------
        public static void updateColor(ref DataGridView iView, string iID, System.Drawing.Color iColor)
        {
            try
            {
                for (int i = 0; i < iView.Rows.Count; i++)
                {
                    string _id = (string)iView.Rows[i].Cells[(int)CommentColumn.COLUMN_ID].Value;
                    if (_id.Equals(iID))
                    {
                        iView.Rows[i].Cells[(int)CommentColumn.COLUMN_NUMBER].Style.BackColor = iColor;
                        iView.Rows[i].Cells[(int)CommentColumn.COLUMN_ID].Style.BackColor = iColor;
                        iView.Rows[i].Cells[(int)CommentColumn.COLUMN_HANDLE].Style.BackColor = iColor;
                        iView.Rows[i].Cells[(int)CommentColumn.COLUMN_COMMENT].Style.BackColor = iColor;
                        iView.Rows[i].Cells[(int)CommentColumn.COLUMN_INFO].Style.BackColor = iColor;
                        iView.Rows[i].Cells[(int)CommentColumn.COLUMN_TIME].Style.BackColor = iColor;

                    }
                }
            }
            catch (Exception ex)
            {
                Utils.WriteLog("updateColor() : " + ex.Message);
            }

        }

        //-------------------------------------------------------------------------
        // コメント用フォントの設定
        //-------------------------------------------------------------------------
        public static void SetCommentFont(ref DataGridView iView)
        {
            iView.Font = Properties.Settings.Default.font;

        }

        //-------------------------------------------------------------------------
        // 順番待ちが残り２桁になったらTweet
        //-------------------------------------------------------------------------
        public static void TweetWait(int iWaitCnt)
        {
            if (!Properties.Settings.Default.tweet_wait) return;

            if (iWaitCnt <= 1)
                return;

            MessageSettings msgSet = MessageSettings.Instance;

            string msg = String.Format(msgSet.GetMessage("枠取り順番待ち、残り{0}人"), iWaitCnt);
            Tweet(msg);


        }

        public static void Tweet(string iTweetString)
        {
            using (Twitter tw = new Twitter(Properties.Settings.Default.tw_token, Properties.Settings.Default.tw_token_secret))
            {
                try
                {
                    tw.Post(iTweetString, Properties.Settings.Default.tw_hash);

                }
                catch (Exception)
                {
                }
            }
        }



        //-------------------------------------------------------------------------
        // ログ書き出し
        //-------------------------------------------------------------------------
        public static void WriteLog(string iTitle, string iStr)
        {
            WriteLog(iTitle, iStr, false);
        }

        //-------------------------------------------------------------------------
        // ログ書き出し
        //-------------------------------------------------------------------------
        public static void WriteLog(string iStr)
        {
            WriteLog("DEBUG", iStr, true);
            System.Diagnostics.Debug.WriteLine(DateTime.Now + " " + iStr);

        }

        //-------------------------------------------------------------------------
        // ログ書き出し
        //-------------------------------------------------------------------------
        public static void WriteLog(string iTitle, string iStr, bool iIsInfo)
        {
            string log_name = "error_log.txt";
# if DEBUG
            if (iIsInfo)
            {
                log_name = "info_log_" + DateTime.Today.ToString("yyyy_MM_dd") + ".txt";
            }
#else
            if (iIsInfo)
            {
                return;
            }
#endif

            try
            {
                using (StreamWriter writer = new StreamWriter(log_name, true))
                {
                    writer.Write(DateTime.Now);
                    writer.Write(" 【" + iTitle + "】");
                    writer.WriteLine(iStr);
                    writer.Flush();
                    writer.Close();
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("WriteLog() :" + e.Message);
            }
        }

        //-------------------------------------------------------------------------
        // LiveID抽出
        //-------------------------------------------------------------------------
        public static string ParseLiveID(string iText)
        {
            string tmp = iText;
            string live_id = "lv";

            int idx = tmp.LastIndexOf("lv");
            int len = tmp.Length;

            for (int i = idx + 2; i < len; i++)
            {
                if ('0' <= tmp[i] && tmp[i] <= '9')
                    live_id += tmp[i].ToString();
                else
                    break;
            }

            return live_id;
        }

        //-------------------------------------------------------------------------
        // 文字列の番号のインクリメント
        //-------------------------------------------------------------------------
        public static string incString(string count)
        {
            string ret = (int.Parse(count.Replace("０", "0").Replace("１", "1").Replace("２", "2").Replace("３", "3").Replace("４", "4").Replace("５", "5").Replace("６", "6").Replace("７", "7").Replace("８", "8").Replace("９", "9")) + 1).ToString();
            if (count.Length > 0)
            {
                if ("０１２３４５６７８９０".Contains(count[0].ToString()))
                {
                    ret = ret.Replace("0", "０").Replace("1", "１").Replace("2", "２").Replace("3", "３").Replace("4", "４").Replace("5", "５").Replace("6", "６").Replace("7", "７").Replace("8", "８").Replace("9", "９");
                }
            }
            return ret;
        }

        //-------------------------------------------------------------------------
        // タイトル中の文字列のインクリメント
        //-------------------------------------------------------------------------
        public static string incTitle(string title)
        {
            Regex NumberedTitle = new Regex("^(.*?(?:＃|#|その[ |　]?|その[ |　]?\\(|その[ |　]?（|\\())([0-9０-９]+)([\\)|）]?)$");

            Match m = NumberedTitle.Match(title);
            if (m.Success)
            {
                return m.Groups[1].Value + incString(m.Groups[2].Value) + m.Groups[3].Value;
            }
            else
            {
                return title;
            }

        }

        // UNIXエポックを表すDateTimeオブジェクトを取得
        private static DateTime UNIX_EPOCH =
          new DateTime(1970, 1, 1, 0, 0, 0, 0);

        //-------------------------------------------------------------------------
        // UnixTimeを得る
        //-------------------------------------------------------------------------
        public static UInt32 GetUnixTime(DateTime targetTime)
        {
            // UTC時間に変換
            targetTime = targetTime.ToUniversalTime();

            // UNIXエポックからの経過時間を取得
            TimeSpan elapsedTime = targetTime - UNIX_EPOCH;

            // 経過秒数に変換
            return (UInt32)elapsedTime.TotalSeconds;
        }



        //-------------------------------------------------------------------------
        // 残り時間計算
        //-------------------------------------------------------------------------
        public static int CalcRemainingTime()
        {
            int sub;
            LiveInfo info = LiveInfo.Instance;

            if (info.EndTime != 0)
            {
                //if (info.StartTime > info.Time)
                //{
                //    //sub = (int)(info.Time - info.StartTime);
                //    sub = (int)(info.EndTime - Utils.GetUnixTime(DateTime.Now)) + 60 * 5;
                //}
                //else
                //{
                    sub = (int)(info.EndTime - Utils.GetUnixTime(DateTime.Now));  // 残り時間

                //}

                //UInt32 passed_sec = (Utils.GetUnixTime(DateTime.Now) - info.UnixTime);
                //if (info.StartTime > info.Time + passed_sec)
                //{
                //    sub = (int)(info.Time - info.StartTime + passed_sec);
                //}
                //else
                //{
                //    sub = (int)(info.EndTime - info.Time - passed_sec);  // 残り時間
                //    //if (info.EndTime < info.Time + passed_sec)
                //    //    sub = 0;
                //}
            }
            else
            {
                //UInt32 passed_sec = (Utils.GetUnixTime(DateTime.Now) - info.UnixTime);
                //sub = (int)(info.Time + passed_sec - info.StartTime);
                sub = (int)(info.Time  - info.StartTime);
            }
            Utils.WriteLog("CalcRemainingTime(): " + sub);
            return sub;
        }

        //-------------------------------------------------------------------------
        // 汎用HTTP GET
        //-------------------------------------------------------------------------
        public static string HTTP_GET(string url)
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            string result = "";
            try
            {
                WebResponse res = req.GetResponse();

                // read response
                Stream resStream = res.GetResponseStream();
                using (StreamReader sr = new StreamReader(resStream, System.Text.Encoding.UTF8))
                {
                    result = sr.ReadToEnd();
                    sr.Close();
                    resStream.Close();
                }
            }
            catch (Exception e)
            {
                Utils.WriteLog("HTTP_GET():" + e.Message);
            }
            return result;
        }

        //-------------------------------------------------------------------------
        // 汎用HTTP GET(クッキー食べる)
        //-------------------------------------------------------------------------
        public static string HTTP_GET(string url, ref CookieContainer cc)
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.CookieContainer = cc;
            req.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            req.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.1; WOW64; Trident/6.0;)";
            string result = "";
            try
            {
                WebResponse res = req.GetResponse();

                // read response
                Stream resStream = res.GetResponseStream();
                using (StreamReader sr = new StreamReader(resStream, System.Text.Encoding.UTF8))
                {
                    result = sr.ReadToEnd();
                    sr.Close();
                    resStream.Close();
                }
            }
            catch (Exception e)
            {
                Utils.WriteLog("HTTP_GET():" + e.Message);
            }
            return result;
        }


    }
}
