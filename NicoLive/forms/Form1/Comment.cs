//-------------------------------------------------------------------------
// Main Form
//
// Copyright (c) 金時豆(http://ch.nicovideo.jp/community/co48276)
// $Id$
//-------------------------------------------------------------------------
using System;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Threading;
using System.IO;
using System.Data.SQLite;
using System.Collections.Generic;
using System.Net;


namespace NicoLive
{
    partial class Form1
    {
        //-------------------------------------------------------------------------
        // 受信コメントパーズ
        //-------------------------------------------------------------------------
        private void ParseComment()
        {
            if (mNico == null) return;

            string xml = mNico.Comment;
            mNico.Comment = "";

            if (xml.Length <= 0) return;

            //受信した文字列を表示
            char[] delimiterChars = { '\0' };
            string[] words = xml.Split(delimiterChars);

            foreach (string s in words)
            {
                if (s.Length <= 0) continue;

                // Utils.WriteLog("ParseComment(): " + s);

                //最終レス番号取得
                if (s.StartsWith("<thread "))
                {
                    System.Text.RegularExpressions.Regex r = new System.Text.RegularExpressions.Regex("last_res=\"([0-9]+)\"");
                    System.Text.RegularExpressions.Match m = r.Match(s);
                    if (m.Success)
                    {
                        mLastChatNo = int.Parse(m.Groups[1].Value);
                        if (Properties.Settings.Default.comment_max > mLastChatNo)
                        {
                            mLastChatNo2 = mLastChatNo - 50;
                        }
                        else
                        {
                            mLastChatNo2 = mLastChatNo;
                        }
                    }
                    continue;
                }



                //コメント処理
                if (s.StartsWith("<chat "))
                {
                    Comment cmt = new Comment(s);
                    if (!cmt.Date.Equals("-1"))
                    {
                        //while (mLiveInfo.StartTime == 0)
                        //{
                        //    // LiveInfo.GetInfo待ちで、ちょっとウェイト
                        //    Thread.Sleep(100);
                        //    Utils.WriteLog("ParseComment: mLiveInfo.StartTime wait");
                        //}
                        //cmt.ElapsedTime = (int.Parse(cmt.Date) - mLiveInfo.StartTime).ToString();
                        if (mLiveInfo.StartTime == 0)
                        {
                            cmt.ElapsedTime = null;
                        }
                        else
                        {
                            cmt.ElapsedTime = (int.Parse(cmt.Date) - mLiveInfo.StartTime).ToString();
                        }

                    }
                    else
                    {
                        cmt.ElapsedTime = null;
                    }
                    if (cmt.Valid)
                    {
                        WriteLog(cmt.Xml);
                        try
                        {
                            RecvComment(cmt);
                        }
                        catch (Exception e)
                        {
                            Utils.WriteLog("ParseComment(): RecvComment(cmt): " + e.Message);
                            Utils.WriteLog("ParseComment(): RecvComment(cmt): " + e.StackTrace);
                        }
                    }
                }
            }
            words = null;
        }

        //-------------------------------------------------------------------------
        // ログ書き出し
        //-------------------------------------------------------------------------
        private void WriteLog(string iStr)
        {
            if (!Properties.Settings.Default.save_log) return;

            try
            {
                mLogger.WriteLine(iStr);
                mLogger.Flush();
            }
            catch (Exception e)
            {
                Utils.WriteLog("WriteLog():" + e.Message);
            }
        }

        //-------------------------------------------------------------------------
        // コメントリストにコメントを追加
        //-------------------------------------------------------------------------
        private void RecvComment(Comment iCmt)
        {

            // 最終コメント受信時間設定
            mLastChatTime = DateTime.Now;

            // IDをコテハンに置換
            string nick = mUid.CheckNickname(iCmt.Uid);
            if (nick != null)
            {
                iCmt.Handle = nick;

                // コテハンを相性用のリストに追加しておく
                if (!mAishouList.Contains(nick))
                {
                    mAishouList.Add(nick);
                }
            }
            else
            {
                // ユーザー名収集スレッドスタート
                GetUsername(iCmt.Uid);
            }

            // コテハン登録
            SaveHandle(iCmt);

            // NGユーザーを無視
            if (this.mUid.IsNGUser(iCmt.Uid))
            {
                goto END;
            }

            try
            {
                // コメントをリストに追加
                mCommentList.Invoke((Action)delegate()
                {
                    this.AddComment(iCmt);
                });
            }
            catch (Exception e)
            {
                Utils.WriteLog("RecvComment: AddComment:" + e.Message);
                Utils.WriteLog("RecvComment: AddComment:" + e.StackTrace);
            }

            //過去コメントだったら抜ける
            if (mLastChatNo >= int.Parse(iCmt.No)) return;

            //配信終了チェック
            if (CheckDisconnect(iCmt))
            {
                goto END;
            }



            // 棒読みちゃん読み上げリストにコメントを追加
            AddSpeakText(iCmt);

            // アクティブ数設定
            mLiveInfo.ActivateUser(iCmt.Uid, iCmt.Date);

            // ユーザーリストにＩＤを追加
            mLiveInfo.AddUser(iCmt.Uid);

            // NGコメント通知
            if (int.Parse(iCmt.No) < mLastChatNo - Properties.Settings.Default.comment_max + 1)
            {
                ShowNGCommentNotice(int.Parse(iCmt.No));
            }

            // 投票チェック
            if (iCmt.IsVote && iCmt.IsOwner)
            {
                iCmt.ToVote(ref mVote);
            }

            // 棒読みタスクのクリア
            BouyomiClear(iCmt);

            // XSplitメッセージ
            if (Properties.Settings.Default.enable_xsplit_scene_change)
            {
                XSplitSceneControl(iCmt);
            }

            // 現在地・速度応答
            SendCurrentPosition(iCmt);
            SendCurrentSpeed(iCmt);

            // 返信メッセージ
            AutoResponse(iCmt);

            // ようこそ！
            WelcomeMessage(iCmt);

            // おみくじ
            Omikuji(iCmt);

            // 相性
            Aishou(iCmt);

            // 天気
            Tenki(iCmt);

            // 引き算
            Hikizan(iCmt);

            // NGコメント通知
            SendNGCommentNotice(int.Parse(iCmt.No));

            // フラッシュコメントジェネレータ
            if (Properties.Settings.Default.use_flash_comment_generator)
            {
                SendFCG(iCmt);
            }

        END:
            // 最終コメントNo設定
            SetLastChatNo(int.Parse(iCmt.No));
            iCmt = null;
            return;
        }



        //-------------------------------------------------------------------------
        // NGコメント表示
        //-------------------------------------------------------------------------
        public bool IsIgnoreComment(Comment iCmt)
        {
            // 運営系スラコメはOK
            if (iCmt.Text.StartsWith("/telop ") ||
                iCmt.Text.StartsWith("/press ") ||
                iCmt.Text.StartsWith("/info ") ||
                iCmt.Text.StartsWith("/koukoku ") ||
                iCmt.Text.StartsWith("/coupon") ||
                iCmt.Text.StartsWith("/uadpoint ") ||
                iCmt.Text.StartsWith("/chukei ")) return false;

            // 184付きのコメントを無視
            if (Properties.Settings.Default.need184 && !iCmt.IsOwner)
            {
                int id;
                if (!int.TryParse(iCmt.Uid, out id))
                {
                    return true;
                }
            }

            // プレミア以外のコメントを無視
            if (Properties.Settings.Default.need_premium && !iCmt.IsOwner)
            {
                if (!iCmt.IsPermium)
                {
                    return true;
                }
            }

            // 主コメントを読み上げるかどうか
            if (!Properties.Settings.Default.owner_comment && iCmt.IsOwner)
            {
                return true;
            }

            // スラコメを読み上げるか
            if (!Properties.Settings.Default.slash_comment && iCmt.Text.StartsWith("/"))
            {
                return true;
            }

            // #コメを読み上げるか
            if (!Properties.Settings.Default.hashamark_comment && iCmt.Text.StartsWith("#"))
            {
                return true;
            }

            //　FME設定読みあげない
            if (iCmt.Text.StartsWith("■映像："))
            {
                return true;
            }

            return false;
        }

        private void SendFCG(Comment iCmt)
        {
            StreamWriter writer = null;
            try
            {
                writer = new StreamWriter("NicoLiveComment.dat", false, System.Text.Encoding.GetEncoding("UTF-8"));

                string name = mUid.CheckNickname(iCmt.Uid);
                if (name == null)
                {
                    name = "";
                }
                string id = iCmt.Uid;
                string comment = iCmt.Text;
                string no = iCmt.No;
                string anchor = (int.Parse(no) + 50).ToString();
                string chathost = "False";
                if (iCmt.IsOwner)
                {
                    chathost = "True";
                }
                writer.WriteLine("NAME={0}_EndName\nCOMMENT={1}_EndComment\nRGB={2}_EndRGB\nANCHOR={3}_EndAnchor\nCHATNO={4}_EndChatNo\nCASTERHOST={5}_EndCasterHost",
                     name,
                     comment,
                     id,
                     anchor,
                     no,
                     chathost
                    );
            }
            finally
            {
                if (writer != null)
                {
                    writer.Close();
                }

            }


        }

        private void AddSpeakText(Comment iCmt)
        {
            // 読み上げないコメントは帰る
            if (IsIgnoreComment(iCmt)) return;

            lock (mSpeakLock)
            {
                string text = "";
                // 実況コメント
                if (iCmt.Text.StartsWith("/chukei "))
                {
                    Regex deleteCommand = new Regex("^/chukei [0-9a-f]{5}(.*)$");
                    Match m = deleteCommand.Match(iCmt.Text);
                    if (m.Success)
                    {
                        text = m.Groups[1].Value;
                    }
                    else
                    {
                        text = iCmt.Text;
                    }
                }
                // BSP
                else if (iCmt.Text.StartsWith("/press "))
                {
                    Regex deleteCommand = new Regex("^/press show (?:white |niconicowhite |red |pink |orange |green |cyan |blue |purple |black )?(.*)$");
                    Match m = deleteCommand.Match(iCmt.Text);
                    if (m.Success)
                    {
                        text = m.Groups[1].Value;
                    }
                    else
                    {
                        text = iCmt.Text;
                    }
                }
                // クルーズ
                else if (iCmt.Text.StartsWith("/telop "))
                {
                    Regex deleteCommand = new Regex("^/telop (?:show|show0|perm) (.*)$");
                    Match m = deleteCommand.Match(iCmt.Text);
                    if (m.Success)
                    {
                        text = m.Groups[1].Value;
                    }
                    else
                    {
                        text = iCmt.Text;
                    }
                }
                // インフォ
                else if (iCmt.Text.StartsWith("/info "))
                {
                    Regex infoRegex = new Regex("^/info ([0-9]+) \\\"(.*?)\\\"$");
                    Match m = infoRegex.Match(iCmt.Text);
                    if (m.Success)
                    {
                        text = m.Groups[2].Value;

                        int no = int.Parse(m.Groups[1].Value);
                        if (no == 2)    // コミュ参加
                        {

                            //  /info 2 "xxx人がコミュニティに参加しました。"     
                            //  /info 2 "xxx人（プレミアムxxx人）がコミュニティに参加しました。"

                            //if (iCmt.Text.Contains("プレミアム"))
                            //{
                            //    m = new Regex("^\\\"[0-9]+人（プレミアム[0-9]+人）がコミュニティに参加しました。\\\"$").Match(iCmt.Text);
                            //    if (m.Success)
                            //    {
                            //        text = m.Groups[0].Value;
                            //    }
                            //}
                            //else
                            //{
                            //    m = new Regex("^\\\"[0-9]+人がコミュニティに参加しました。\\\"$").Match(iCmt.Text);
                            //    if (m.Success)
                            //    {
                            //        text = m.Groups[0].Value +iCmt.Text;
                            //    }
                            //}
                        }
                        else if (no == 8)    // ランクイン
                        {
                            m = new Regex("^\\\"第([0-9]+)位にランクインしました\\\"$").Match(iCmt.Text);
                            if (m.Success)
                            {
                                text = m.Groups[1].Value;
                            }
                        }
                    }
                    else
                    {
                        text = iCmt.Text;
                    }
                }
                // 広告
                else if (iCmt.Text.StartsWith("/koukoku "))
                {

                    if (iCmt.Text.Contains("【広告設定されました】"))
                    {
                        Regex koukokuRegex = new Regex("^/koukoku show2 (.*?)【広告設定されました】(.*?)さん([0-9]+)pt「(.*?)」$");
                        Match m = koukokuRegex.Match(iCmt.Text);
                        if (m.Success)
                        {
                            text = m.Groups[2].Value + "さんに" + m.Groups[3].Value + "広告ポイントされたなう。" + m.Groups[4].Value;
                        }
                        else
                        {
                            text = iCmt.Text;
                        }
                    }
                    else
                    {   // 広告結果
                        Regex koukokuRegex = new Regex("^/koukoku show2 (.*?)【広告結果】ニコニ広告で来場者数が([0-9]+)人増えました。$");
                        Match m = koukokuRegex.Match(iCmt.Text);
                        if (m.Success)
                        {
                            text = "広告で来場者数が" + m.Groups[2].Value + "人増えたよ。";
                        }
                        else
                        {
                            text = iCmt.Text;
                        }

                    }
                }
                // クーポン
                else if (iCmt.Text.StartsWith("/coupon "))
                {
                    Regex adpointRegex = new Regex("^/coupon (.*?){\\\"required_point\\\":([0-9]+)},\\\"current_point\\\":([0-9]+)}$");
                    Match m = adpointRegex.Match(iCmt.Text);
                    if (m.Success)
                    {
                        text = m.Groups[3].Value + "広告ポイントなう。次のチケットまで" + m.Groups[2].Value + "広告ポイント。";
                    }
                    else
                    {
                        text = iCmt.Text;
                    }
                }
                // 広告ポイント
                else if (iCmt.Text.StartsWith("/uadpoint"))
                {
                    Regex adpointRegex = new Regex("^/uadpoint ([0-9]+) ([0-9]+)$");
                    Match m = adpointRegex.Match(iCmt.Text);
                    if (m.Success)
                    {
                        text = m.Groups[1].Value + "広告ポイントなう";
                    }
                    else
                    {
                        text = iCmt.Text;
                    }
                }
                else //通常読み上げ
                {

                    text = iCmt.Text;

                    if (Properties.Settings.Default.use_comment_cut &&
                        text.Length > Properties.Settings.Default.comment_cut_len)
                    {
                        text = text.Substring(0, Properties.Settings.Default.comment_cut_len) + "　以下略　";
                    }


                    //コテハン読み上げ
                    if (Properties.Settings.Default.speak_nick)
                    {
                        text = iCmt.Text + " " + iCmt.Handle;
                    }


                }

                // 読み上げ追加
                this.mSpeakList.Add(text);
            }

        }

        //-------------------------------------------------------------------------
        // NGコメント表示
        //-------------------------------------------------------------------------
        private void ShowNGCommentNotice(int no)
        {
            if (no - mLastChatNo2 > 1)
            {
                for (int i = mLastChatNo2 + 1; i < no; i++)
                {
                    // コメントをリストに追加
                    this.Invoke((Action)delegate()
                    {
                        string s = "<chat anonymity=\"1\" date=\"-1\" mail=\"184\" no=\"" + i + "\" thread=\"\" user_id=\"-\" vpos=\"0\">※NGコメント</chat>";
                        Comment cmt = new Comment(s);

                        this.AddComment(cmt);
                    });
                }
            }
        }

        //-------------------------------------------------------------------------
        // NGコメント通知
        //-------------------------------------------------------------------------
        private void SendNGCommentNotice(int no)
        {
            if (!Properties.Settings.Default.ng_notice) return;

            if (no - mLastChatNo > 1)
            {
                for (int i = mLastChatNo + 1; i < no; i++)
                {
                    SendComment(">>" + i + " NGコメントです", true);
                }
            }
        }

        //-------------------------------------------------------------------------
        // 最終コメントNo設定
        //-------------------------------------------------------------------------
        private void SetLastChatNo(int no)
        {

            if (mLastChatNo < no)
            {
                mLastChatNo = no;
            }
            if (mLastChatNo2 < no)
            {
                mLastChatNo2 = no;
            }
        }

        //-------------------------------------------------------------------------
        // 現在位置を送信する（現在位置を調べる)
        //-------------------------------------------------------------------------
        private void SendCurrentPosition(Comment iCmt)
        {
            //if (iCmt.IsOwner) return;
            if (mNico == null) return;
            if (!mNico.IsLogin) return;

            // 現在位置を聞いてきているか？
            if (!iCmt.Text.Contains("現在地は？")) return;

            // 現在地機能が使用可能な状態か？
            if (!Properties.Settings.Default.imakoko_genzaichi ||
                Properties.Settings.Default.imakoko_user == "")
            {
                return;
            }

            ImaDoko.setSendCommentDelegate(SendComment);

            // 現在地を調べる機能を駆動
            Thread th = new Thread(delegate()
            {
                ImaDoko.UpdateSpeedAndPlace();
                string msg = Properties.Settings.Default.address_template;
                msg = msg.Replace("@ADDRESS", ImaDoko.Place);
                msg = msg.Replace("@ALTITUDE_EGM96", ((int)ImaDoko.AltitudeEGM96).ToString());
                msg = msg.Replace("@ALTITUDE", ((int)ImaDoko.Altitude).ToString());
                string mail = Properties.Settings.Default.imakoko_genzaichi_hidden ? "hidden" : "";
                bool IsOwner = Properties.Settings.Default.imakoko_genzaichi_owner;
                SendComment(msg, mail, IsOwner, true);
            });
            th.Name = "NivoLive.Form1.Comment.SendCurrentPosition()";
            th.Start();
        }

        //-------------------------------------------------------------------------
        // 現在位置を送信する（現在位置を調べる)
        //-------------------------------------------------------------------------
        private void SendCurrentPosition()
        {
            if (mNico == null) return;
            if (!mNico.IsLogin) return;
            if (mDoingGetNextWaku) return;


            // 現在地機能が使用可能な状態か？
            if (Properties.Settings.Default.imakoko_user == "")
            {
                return;
            }

            ImaDoko.setSendCommentDelegate(SendComment);

            // 現在地を調べる機能を駆動
            Thread th = new Thread(delegate()
            {
                ImaDoko.UpdateSpeedAndPlace();
                string msg = Properties.Settings.Default.address_template2;
                msg = msg.Replace("@ADDRESS", ImaDoko.Place);
                msg = msg.Replace("@ALTITUDE_EGM96", ((int)ImaDoko.AltitudeEGM96).ToString());
                msg = msg.Replace("@ALTITUDE", ((int)ImaDoko.Altitude).ToString());
                string mail = Properties.Settings.Default.imakoko_genzaichi_hidden ? "hidden" : "";
                bool IsOwner = Properties.Settings.Default.imakoko_genzaichi_owner;
                SendComment(msg, mail, IsOwner, true);
            });
            th.Name = "NivoLive.Form1.Comment.SendCurrentPosition()";
            th.Start();
        }

        //-------------------------------------------------------------------------
        // 速度を送信する
        //-------------------------------------------------------------------------
        private void SendCurrentSpeed(Comment iCmt)
        {
            if (iCmt.IsOwner) return;
            if (mNico == null) return;
            if (!mNico.IsLogin) return;

            // 現在位置を聞いてきているか？
            if (!iCmt.Text.Contains("速度は？")) return;

            // 現在地機能が使用可能な状態か？
            if (!Properties.Settings.Default.imakoko_speed ||
                Properties.Settings.Default.imakoko_user == "")
            {
                return;
            }

            ImaDoko.setSendCommentDelegate(SendComment);

            // 現在地を調べる機能を駆動
            Thread th = new Thread(delegate()
            {
                ImaDoko.UpdateSpeedAndPlace();
                if (ImaDoko.Speed >= 0)
                {
                    string msg = Properties.Settings.Default.speed_template;
                    msg = msg.Replace("@SPEED", ((int)ImaDoko.Speed).ToString());
                    string mail = Properties.Settings.Default.imakoko_genzaichi_hidden ? "hidden" : "";
                    SendComment(msg, mail, true, true);
                }
                else
                {
                    string mail = Properties.Settings.Default.imakoko_genzaichi_hidden ? "hidden" : "";
                    SendComment("速度は不明です", mail, true, true);
                }
            });
            th.Name = "NivoLive.Form1.Comment.SendCurrentSpeed()";
            th.Start();
        }


        //-------------------------------------------------------------------------
        // 自動返信
        //-------------------------------------------------------------------------
        private void AutoResponse(Comment iCmt)
        {
            if (iCmt.IsOwner) return;

            string res = mRes.GetResponse(iCmt.Text);
            if (res.Length > 0)
            {
                this.Invoke((Action)delegate()
                {
                    this.SendComment(res, true);
                });
            }
        }


        //-------------------------------------------------------------------------
        // 棒読みタスクのクリア
        //-------------------------------------------------------------------------
        private void BouyomiClear(Comment iCmt)
        {
            List<string> clear_keyword = new List<string>();
            clear_keyword.Add("ザ・ワールド");
            clear_keyword.Add("棒読み");

            foreach (string keyword in clear_keyword)
            {
                if (iCmt.Text.StartsWith(keyword))
                {
                    using (Bouyomi bm = new Bouyomi())
                    {
                        bm.Clear();
                    }
                }
            }

        }

        //-------------------------------------------------------------------------
        // 引き算
        //-------------------------------------------------------------------------
        private void Hikizan(Comment iCmt)
        {
            if (iCmt.IsOwner) return;
            string post_msg = "";
            string pattern = @"([0-9]+)([-ー－―‐])([0-9]+)";
            Match match = Regex.Match(iCmt.Text, pattern);
            if (!match.Success) return;
            int i = 0;
            try
            {
                i = int.Parse(match.Groups[1].Value) - int.Parse(match.Groups[3].Value);
                post_msg = match.Groups[1].Value + "-" + match.Groups[3].Value + "は、" + i + "です";
            }
            catch
            {
                post_msg = match.Groups[1].Value + "-" + match.Groups[3].Value + "は、わかりませんでした。";
            }
            this.Invoke((Action)delegate()
            {
                this.SendComment(post_msg, true);
            });


        }

        //-------------------------------------------------------------------------
        // 天気
        //-------------------------------------------------------------------------
        private void Tenki(Comment iCmt)
        {
            //if (iCmt.IsOwner) return;
            if (!iCmt.Text.Contains("天気は？")) return;

            Thread t = new Thread(delegate()
            {
                string post_msg = "";

                string pattern = "(?<keyword>.*?)の天気は";
                Match match = Regex.Match(iCmt.Text, pattern);
                if (!match.Success) return;

                string keyword = match.Groups["keyword"].Value;
                keyword = keyword.Replace("/", "");
                string url = "http://weather.yahoo.co.jp/weather/search/?p=";

                CookieContainer cc = new CookieContainer();
                Utils.HTTP_GET("http://weather.yahoo.co.jp/", ref cc); //クッキーを食わせる

                url = url + System.Web.HttpUtility.UrlEncode(keyword);

                string html = Utils.HTTP_GET(url, ref cc);
                if (html == "") return;

                string place_url = "";
                string place_name = "";
                string place_weather = "";

                pattern = @"<td><a href=""(?<url>http://weather.yahoo.co.jp/weather/.*?\.html)"">(?<place>.*?)</a></td>";
                match = Regex.Match(html, pattern);
                if (!match.Success)
                {
                    post_msg = keyword + "の天気は、わかりません。";
                    goto POST_MESSAGE;
                }

                place_url = match.Groups["url"].Value;
                place_name = match.Groups["place"].Value;

                html = Utils.HTTP_GET(place_url, ref cc);
                if (html == "")
                {
                    post_msg = place_name + "の天気が取得できませんでした。";
                    goto POST_MESSAGE;
                }

                pattern = @"<td><img src=""([^_]*?).gif"" border=0 width=40 height=40 alt=""(?<weather>.*?)""><br>";
                match = Regex.Match(html, pattern);
                if (!match.Success)
                {
                    post_msg = place_name + "の天気が取得できませんでした。";
                    goto POST_MESSAGE;
                }

                place_weather = match.Groups["weather"].Value;


                post_msg = keyword + "(" + place_name + ")の天気は" + place_weather + "です";

            POST_MESSAGE:
                this.Invoke((Action)delegate()
                {
                    this.SendComment(post_msg, true);
                });
            });
            t.Name = "Tenki";
            t.Start();



        }


        //-------------------------------------------------------------------------
        // 相性
        //-------------------------------------------------------------------------
        private void Aishou(Comment iCmt)
        {
            if (mAishouList.Count < 2) return;
            if (iCmt.IsOwner) return;

            if (!iCmt.Text.Contains("相性")) return;

            string name;
            if (mUid.Contains(iCmt.Uid))
            {
                name = mUid.CheckNickname(iCmt.Uid);
                name = name.Replace("/", "");
            }
            else
            {
                name = ">>" + iCmt.No;
            }

            mAishouList.Remove(name);
            string name2;
            int i = new Random().Next(mAishouList.Count);
            name2 = mAishouList[i];
            mAishouList.Add(name);


            string post_msg;
            int s = new Random().Next(400);
            post_msg = name + "さんと" + name2 + "さんの相性は、" + s + "%です！";
            
            Thread t = new Thread(delegate()
            {
                this.Invoke((Action)delegate()
                {
                    this.SendComment(post_msg, true);
                });
            });
            t.Name = "Aisho";
            t.Start();


        }

        //-------------------------------------------------------------------------
        // おみくじ
        //-------------------------------------------------------------------------
        private void Omikuji(Comment iCmt)
        {
            // 主コメはおみくじしない
            if (iCmt.IsOwner) return;

            if (iCmt.Text.Contains("おみくじ") && !iCmt.Text.Contains("ありません"))
            {
                string name;
                if (mUid.Contains(iCmt.Uid))
                {
                    name = mUid.CheckNickname(iCmt.Uid);
                    name = name.Replace("/", "");
                }
                else
                {
                    name = ">>" + iCmt.No;
                }

                string post_msg = "";
                if (mOmikujiList.Contains(iCmt.Uid))
                {
                    post_msg = name + "さんは、おみくじをもう使えません。";
                }
                else
                {
                    mOmikujiList.Add(iCmt.Uid);

                    int o = new System.Random().Next(1, 100);
                    string[] unsei = new string[7] { "大吉", "吉", "半吉", "小吉", "末小吉", "末吉", "凶" };
                    int[] ratio = new int[7] { 17, 35, 5, 4, 3, 6, 30 };
                    int r = 0;
                    for (int i = 0; i < 7; i++)
                    {
                        r += ratio[i];
                        if (o <= r)
                        {
                            post_msg = name + "さんの運勢は、" + unsei[i] + "です！";
                            break;
                        }
                    }

                }
                Thread t = new Thread(delegate()
                {
                    this.Invoke((Action)delegate()
                    {
                        this.SendComment(post_msg, true);
                    });
                });
                t.Name = "Omikuji";
                t.Start();


            }
            if (iCmt.Text.Contains("おみくじありません"))
            {
                string name;
                if (mUid.Contains(iCmt.Uid))
                {
                    name = mUid.CheckNickname(iCmt.Uid);
                }
                else
                {
                    name = ">>" + iCmt.No;
                }

                string post_msg;
                post_msg = name + "さんは、おみくじはあります！";
                Thread t = new Thread(delegate()
                {
                    this.Invoke((Action)delegate()
                    {
                        this.SendComment(post_msg, true);
                    });
                });
                t.Name = "Omikuji";
                t.Start();

                if (mOmikujiList.Contains(iCmt.Uid))
                {
                    mOmikujiList.Remove(iCmt.Uid);
                }

            }
        }


        //-------------------------------------------------------------------------
        // ようこそ！
        //-------------------------------------------------------------------------
        private void WelcomeMessage(Comment iCmt)
        {
            if (iCmt.IsOwner) return;

            // クルーズいらっしゃい
            if (Properties.Settings.Default.use_welcome_cruise_message &&
                iCmt.Text.StartsWith("/telop show ") && iCmt.Text.Contains("到着しました"))
            {
                Thread t = new Thread(delegate()
                    {
                        string post_msg = "";
                        post_msg = Properties.Settings.Default.welcome_cruise_message;
                        Thread.Sleep(1000 /* ms */);
                        this.Invoke((Action)delegate()
                        {
                            this.SendComment(post_msg, true);
                        });
                    });
                t.Start();
            }

            if (!Properties.Settings.Default.use_welcome_message) return;

            string uid = iCmt.Uid;

            if (!mWelcomeList.Contains(uid))
            {

                Thread th = new Thread(delegate()
                {
                    try
                    {
                        string post_msg = "";

                        mWelcomeList.Add(iCmt.Uid);

                        string msg = Properties.Settings.Default.welcome_message;
                        string msg184 = Properties.Settings.Default.welcome_message184;


                        string nick = null;
                        int wait = 5;


                        // 184は勝手に名前をつける
                        int id;
                        if (!int.TryParse(iCmt.Uid, out id))
                        {
                            string name = "";

                            // コテハンついてない時
                            if (Properties.Settings.Default.auto_nick_184 && !mUid.Contains(iCmt.Uid))
                            {

                                string file = System.Windows.Forms.Application.StartupPath + "\\name.db";

                                // 名前DBがあったら勝手に名前をつける
                                if (File.Exists(file))
                                {
                                    try
                                    {
                                        using (var conn = new SQLiteConnection("Data Source=name.db"))
                                        {
                                            using (SQLiteCommand cmd = conn.CreateCommand())
                                            {
                                                conn.Open();
                                                // SELECT文の実行
                                                cmd.CommandText = "select * from name  order by random() limit 1;";
                                                using (SQLiteDataReader reader = cmd.ExecuteReader())
                                                {
                                                    while (reader.Read())
                                                    {
                                                        Utils.WriteLog((string)reader[0]);
                                                        name = "." + (string)reader[0];
                                                    }
                                                }
                                                conn.Close();
                                            }
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        Utils.WriteLog("WelcomeMessage(): SQLite error " + ex.Message);
                                    }

                                    // コテハン上書き
                                    this.Invoke((Action)delegate()
                                    {
                                        SetNickname(iCmt.Uid, name);
                                        iCmt.Handle = nick;
                                    });


                                }
                                else
                                {
                                    Utils.WriteLog("WelcomeMessage(): " + file + "not found");
                                }
                            }
                            else
                            {   //コテハンついてる時
                                name = mUid.CheckNickname(iCmt.Uid);
                            }

                            msg = msg184.Replace("@name", name);
                            msg = msg.Replace("@id", iCmt.Uid.Substring(0, 4));
                            msg = msg.Replace("@no", iCmt.No);
                        }
                        else    //生ID
                        {

                            // コテハン自動取得の為にwait秒間待ってみる
                            while (wait > 0)
                            {
                                nick = mUid.CheckNickname(iCmt.Uid);
                                if (nick != null)
                                {
                                    // 取得できたら抜ける
                                    break;
                                }
                                Thread.Sleep(500);
                                wait--;
                            }

                            if (nick != null)
                            {
                                nick = nick.Replace("/", "");
                                msg = msg.Replace("@name", nick);
                            }
                            else
                            {
                                msg = msg.Replace("@name", iCmt.No);
                            }
                        }

                        post_msg = msg;



                        this.Invoke((Action)delegate()
                        {
                            this.SendComment(post_msg, true);
                        });


                    }
                    catch (Exception e)
                    {
                        Utils.WriteLog("WelcomeMessage:" + e.Message);

                    }
                });
                th.Start();


            }



        }

        //-------------------------------------------------------------------------
        // XSplit シーン切り替え
        //-------------------------------------------------------------------------
        private void XSplitSceneControl(Comment iCmt)
        {
            if (!Properties.Settings.Default.enable_xsplit_scene_change) return;

            if (iCmt.IsOwner) return;

            Regex regex = new System.Text.RegularExpressions.Regex(@"^S(\d+)");
            // 演奏する曲を選ぶ
            if (regex.IsMatch(iCmt.Text))
            {
                Match m = regex.Match(iCmt.Text);
                XSplit.CONTROL_numPad(int.Parse(m.Groups[1].Captures[0].Value));
            }
        }

        //-------------------------------------------------------------------------
        // コテハン記憶
        //-------------------------------------------------------------------------
        private void SaveHandle(Comment iCmt)
        {

            if (iCmt.IsOwner) return;
            if (iCmt.IsOperator) return;
            if (!iCmt.Text.Contains("@")) return;
            if (!iCmt.Text.Contains("＠")) return;

            string comment = iCmt.Text.Replace("＠", "@");

            int idx = iCmt.Text.LastIndexOf("@");
            if (idx < 0) return;

            string nick = iCmt.Text.Substring(idx + 1);

            if (!mUid.Contains(iCmt.Uid))
            {
                // ユーザー名が登録されてない
                this.Invoke((Action)delegate()
                {
                    AddNickname(iCmt.Uid, nick);
                    iCmt.Handle = nick;
                });
            }
            else
            {
                // すでに登録されているユーザー名を変更
                this.Invoke((Action)delegate()
                {
                    SetNickname(iCmt.Uid, nick);
                    iCmt.Handle = nick;
                });
            }



        }

        //-------------------------------------------------------------------------
        // 切断チェック
        //-------------------------------------------------------------------------
        private bool CheckDisconnect(Comment iCmt)
        {
            if (mDisconnect) return false;

            if (!iCmt.IsDisconnect) return false;

            if (mOwnLive)
            {
                // Twitterにポスト
                if (Properties.Settings.Default.tw_end_enable)
                    TwitterPoster(false);

                // 棒読みちゃんで配信終了通知
                this.mBouyomi.Talk(mMsg.GetMessage("配信が終了しました"));

                //FMLE.Stop();
                HQ.Stop();
                mStartHQ = false;
            }

            // ログクローズ
            if (Properties.Settings.Default.save_log)
            {
                mLogger.Close();
                mLogger = null;
            }

            mDisconnect = true;

            // 枠取り画面へ 
            if (/*mOwnLive && */mContWaku.Checked && !mDoingGetNextWaku)
            {
                mDoingGetNextWaku = true;

                Thread.Sleep(500);

                // 棒読みちゃんで自動枠取り通知
                this.mBouyomi.Talk(mMsg.GetMessage("枠取りを開始します"));

                bool lv_notice = mUseHQ.Checked;
                GetNextWaku(lv_notice);

            }
            return true;


        }

        //-------------------------------------------------------------------------
        // 次枠取り
        //-------------------------------------------------------------------------
        private void GetNextWaku(bool iUseLvNotice)
        {

            Thread th = new Thread(delegate()
            {

                WakuResult result = WakuResult.ERR;
                string new_lv = "";

                if (Properties.Settings.Default.use_auto_wakutiri_dialog)
                {
                    WakuDlg dlg = new WakuDlg(LiveID, false);
                    dlg.ShowDialog();

                    if (dlg.mState != WakuResult.NO_ERR) return;
                    result = dlg.mState;
                    new_lv = dlg.mLv;
                }
                else
                {
                    Wakutori mk = new Wakutori();
                    mk.MyOwner = this;
                    mk.AutoWaku = true;
                    mk.ReuseLv = this.LiveID;

                    mk.ShowDialog();

                    if (mk.mState != WakuResult.NO_ERR) return;
                    result = mk.mState;
                    new_lv = mk.mLv;
                }

                if (result != WakuResult.NO_ERR) return;

                if (iUseLvNotice)
                {
                    try
                    {

                        mNico.SendOwnerComment(LiveID, "/cls", "", mLiveInfo.Token);

                        // 配信開始コマンド撃っとく
                        Utils.WriteLog("GetNextWaku(): LiveStart():");
                        string token = "";
                        Dictionary<string, string> info_PublishStatus = mNico.GetPublishStatus(new_lv);
                        if (info_PublishStatus != null && info_PublishStatus.ContainsKey("token"))
                        {
                            token = info_PublishStatus["token"];
                            mNico.LiveStart(new_lv, token, true);
                        }

                        mNico.SendOwnerComment(LiveID, "/perm 次枠こちら：http://nico.ms/" + new_lv, "", mLiveInfo.Token);
                        mNico.SendOwnerComment(LiveID, "/disconnect", "", mLiveInfo.Token);
                    }
                    catch (Exception e)
                    {
                        Utils.WriteLog("GetNextWaku:" + e.Message);
                    }

                    Thread.Sleep(1000);
                }

                try
                {
                    this.Invoke((Action)delegate()
                    {
                        this.LiveID = new_lv;
                    });
                }
                catch (Exception e)
                {
                    Utils.WriteLog("GetNextWaku:" + e.Message);
                }


                Connect(true);


                //else if (dlg.mState == WakuResult.JUNBAN)
                //{
                //    MakeWakutori(true);
                //}

            });
            th.Name = "NivoLive.Form1.Comment.GetNextWaku()";
            th.SetApartmentState(ApartmentState.STA);
            th.Start();


        }


        //-------------------------------------------------------------------------
        // コメントリストにコメントを追加
        //-------------------------------------------------------------------------
        private void AddComment(Comment iCmt)
        {
            try
            {
                if (mPastChat)
                {
                    // 過去コメ
                    System.Drawing.Color color = this.mUid.getUserColor(iCmt.Uid);
                    Utils.AddComment(ref mCommentList, iCmt, color, ref mPastChatList);
                    //Utils.AddComment(ref mCommentList, iCmt, color);
                }
                else
                {
                    System.Drawing.Color color = this.mUid.getUserColor(iCmt.Uid);
                    Utils.AddComment(ref mCommentList, iCmt, color);
                }
            }
            catch (Exception e)
            {
                Utils.WriteLog("AddComment(): " + e.StackTrace);
                Utils.WriteLog("AddComment(): " + e.Message);
            }
        }

        //-------------------------------------------------------------------------
        // コメント読み上げ
        //------------------------------------------------------------------------
        private void SpeakComment()
        {
            lock (mSpeakLock)
            {
                // 読み上げが有効になってない
                if (!this.mBouyomiBtn.Checked)
                {
                    this.mSpeakList.Clear();
                    return;
                }

                // 読み上げる
                int cnt = this.mSpeakList.Count;
                if (cnt <= 0) return;

                // コメントワープ
                if (Properties.Settings.Default.warp_cnt < cnt)
                {
                    this.mSpeakList.Clear();
                    if (mSkipBouyomi)
                    {
                        mSkipBouyomi = false;
                        return;
                    }
                    mBouyomi.Talk("コメントワープ");
                    return;
                }

                string str = this.mSpeakList[0];
                this.mSpeakList.RemoveAt(0);

                if (mSkipBouyomi)
                {
                    mSkipBouyomi = false;
                    if (cnt == 1)
                        mBouyomi.Talk(str);
                    return;
                }
                mBouyomi.Talk(str);
            }
        }

        //-------------------------------------------------------------------------
        // コメント送信
        //-------------------------------------------------------------------------
        private void SendComment(string iComment, bool Owner)
        {
            if (mNico == null) return;
            if (!mNico.IsLogin) return;

            Thread th = new Thread(delegate()
            {
                if (!Owner)
                    mNico.SendComment(LiveID, iComment, true);
                else
                    mNico.SendOwnerComment(LiveID, iComment, mLiveInfo.Nickname, mLiveInfo.Token);
            });
            th.Name = "NivoLive.Form1.Comment.SendComment()";
            th.Start();
        }

        //-------------------------------------------------------------------------
        // コメント送信
        //-------------------------------------------------------------------------
        private void SendComment(string iComment, bool Owner, bool i184)
        {
            if (mNico == null) return;
            if (!mNico.IsLogin) return;

            Thread th = new Thread(delegate()
            {
                if (!Owner)
                    mNico.SendComment(LiveID, iComment, i184);
                else
                    mNico.SendOwnerComment(LiveID, iComment, mLiveInfo.Nickname, mLiveInfo.Token);
            });
            th.Name = "NivoLive.Form1.Comment.SendComment()";
            th.Start();
        }

        //-------------------------------------------------------------------------
        // コメント送信
        //-------------------------------------------------------------------------
        private void SendComment(string iComment, string iMail, bool Owner, bool i184)
        {
            if (mNico == null) return;
            if (!mNico.IsLogin) return;

            Thread th = new Thread(delegate()
            {
                if (!Owner)
                    mNico.SendComment(LiveID, iComment, i184);
                else
                    mNico.SendOwnerComment(LiveID, iComment, iMail, mLiveInfo.Nickname, mLiveInfo.Token);
            });
            th.Name = "NivoLive.Form1.Comment.SendComment()";
            th.Start();
        }
    }
}
//-------------------------------------------------------------------------
// Copyright (c) 金時豆(http://ch.nicovideo.jp/community/co48276)
// $Id$
//-------------------------------------------------------------------------
