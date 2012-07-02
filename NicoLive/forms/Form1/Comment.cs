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
using Microsoft.VisualBasic;

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
                        while (mLiveInfo.StartTime == 0)
                        {
                            // LiveInfo.GetInfo待ちで、ちょっとウェイト
                            Thread.Sleep(100);
                            Debug.WriteLine("ParseComment: mLiveInfo.StartTime wait");
                        }
                        cmt.ElapsedTime = (int.Parse(cmt.Date) - mLiveInfo.StartTime).ToString();

                    }
                    else
                    {
                        cmt.ElapsedTime = null;
                    }
                    if (cmt.Valid)
                    {
                        WriteLog(cmt.Xml);
                        RecvComment(cmt);
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
                Debug.WriteLine("ParseComment:" + e.Message);
            }
        }

        //-------------------------------------------------------------------------
        // コメントリストにコメントを追加
        //-------------------------------------------------------------------------
        private void RecvComment(Comment iCmt)
        {

            // 最終コメント受信時間設定
            mLastChatTime = DateTime.Now;

            // 切断
            //if (CheckDisconnect(iCmt))
            //    return;
            CheckDisconnect(iCmt);

            // アクティブ数設定
            mLiveInfo.ActivateUser(iCmt.Uid, iCmt.Date);

            // ユーザーリストにＩＤを追加
            mLiveInfo.AddUser(iCmt.Uid);

            // IDをコテハンに置換
            string nick = mUid.CheckNickname(iCmt.Uid);
            if (nick != null)
            {
                iCmt.Handle = nick;
            }
            else
            {
                // ユーザー名収集スレッドスタート
                GetUsername(iCmt.Uid);
            }

            // コテハン登録
            SaveHandle(iCmt);






            // NGコメント表示
            //if (int.Parse(iCmt.No) < mLastChatNo - Properties.Settings.Default.comment_max + 1)
            if (int.Parse(iCmt.No) < mLastChatNo - Properties.Settings.Default.comment_max + 1)
            {
                ShowNGCommentNotice(int.Parse(iCmt.No));
            }





            //過去コメントじゃない時だけ
            if (mLastChatNo < int.Parse(iCmt.No))
            {

                // 投票チェック
                if (iCmt.IsVote && iCmt.IsOwner) iCmt.ToVote(ref mVote);

                // NGユーザーを無視
                if (this.mUid.IsNGUser(iCmt.Uid))
                {
                    SetLastChatNo(int.Parse(iCmt.No));
                    return;

                }

                // 棒読みちゃん読み上げリストにコメントを追加
                if (!IsIgnoreComment(iCmt))
                {
                    lock (mSpeakLock)
                    {
                        if (iCmt.Text.StartsWith("/chukei "))
                        {
                            Regex deleteCommand = new Regex("^/chukei [0-9a-f]{5}(.*)$");
                            Match m = deleteCommand.Match(iCmt.Text);
                            if (m.Success)
                            {
                                this.mSpeakList.Add(m.Groups[1].Value);
                            }
                            else
                            {
                                this.mSpeakList.Add(iCmt.Text);
                            }
                        }
                        else if (iCmt.Text.StartsWith("/press "))
                        {
                            Regex deleteCommand = new Regex("^/press show (?:white |niconicowhite |red |pink |orange |green |cyan |blue |purple |black )?(.*)$");
                            Match m = deleteCommand.Match(iCmt.Text);
                            if (m.Success)
                            {
                                this.mSpeakList.Add(m.Groups[1].Value);
                            }
                            else
                            {
                                this.mSpeakList.Add(iCmt.Text);
                            }
                        }
                        else if (iCmt.Text.StartsWith("/telop "))
                        {
                            Regex deleteCommand = new Regex("^/telop (?:show|show0|perm) (.*)$");
                            Match m = deleteCommand.Match(iCmt.Text);
                            if (m.Success)
                            {
                                this.mSpeakList.Add(m.Groups[1].Value);
                            }
                            else
                            {
                                this.mSpeakList.Add(iCmt.Text);
                            }
                        }
                        else if (iCmt.Text.StartsWith("/info "))
                        {
                            Regex deleteCommand = new Regex("^/info [0-9]{1} \\\"(.*)\\\"?$");
                            Match m = deleteCommand.Match(iCmt.Text);
                            if (m.Success)
                            {
                                this.mSpeakList.Add(m.Groups[1].Value);
                            }
                            else
                            {
                                this.mSpeakList.Add(iCmt.Text);
                            }
                        }
                        else //通常読み上げ
                        {
                            //コテハン読み上げるかどうか
                            if (Properties.Settings.Default.speak_nick)
                            {
                                this.mSpeakList.Add(iCmt.Text + " " + nick);
                            }
                            else
                            {
                                this.mSpeakList.Add(iCmt.Text);
                            }

                        }
                    }
                }




                if (mLastChatNo < int.Parse(iCmt.No))
                {
                    // XSplitメッセージ
                    if (Properties.Settings.Default.enable_xsplit_scene_change) XSplitSceneControl(iCmt);

                    // 現在地・速度応答
                    SendCurrentPosition(iCmt);
                    SendCurrentSpeed(iCmt);
                }

                // 返信メッセージ
                AutoResponse(iCmt);

                // ようこそ！
                WelcomeMessage(iCmt);

                // NGコメント通知
                SendNGCommentNotice(int.Parse(iCmt.No));
            }

            // コメントをリストに追加
            this.Invoke((Action)delegate()
            {
                this.AddComment(iCmt);
            });

            // 最終コメントNo設定
            SetLastChatNo(int.Parse(iCmt.No));

            iCmt = null;
        }

        //-------------------------------------------------------------------------
        // NGコメント表示
        //-------------------------------------------------------------------------
        public bool IsIgnoreComment(Comment iCmt)
        {
            // 運営系スラコメはスルー
            if (iCmt.Text.StartsWith("/telop ") ||
                iCmt.Text.StartsWith("/press ") ||
                iCmt.Text.StartsWith("/info ") ||
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
            return iCmt.Text.StartsWith("■映像：");
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
                string mail = Properties.Settings.Default.imakoko_genzaichi_hidden ? "hidden" : "";
                SendComment(msg, mail, true, true);
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
                SendComment(msg, mail, true, true);
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
        // ようこそ！
        //-------------------------------------------------------------------------
        private void WelcomeMessage(Comment iCmt)
        {
            if (iCmt.IsOwner) return;

            string uid = iCmt.Uid;

            if (!mWelcomeList.Contains(uid))
            {
                mWelcomeList.Add(iCmt.Uid);

                string msg = "さんいらっしゃい！";
                //if (mUid.Contains(uid))
                //{
                    string nick = mUid.CheckNickname(iCmt.Uid);
                    if (nick != null)
                    {
                        msg = nick + msg;
                    }
                    else
                    {
                        msg = ">>" + iCmt.No + msg; 
                    }
                    this.Invoke((Action)delegate()
                    {
                        this.SendComment(msg, true);
                    });
                //}
            
            
            
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
            string tmp = Strings.StrConv(iCmt.Text, VbStrConv.Narrow, 0x0411);
            string regex = "@[0-9].*";
            Match match = Regex.Match(tmp, regex);

            if (!match.Success)
            {
                tmp = iCmt.Text.Replace("＠", "@");
                if (tmp.Contains("@"))
                {
                    int idx = tmp.LastIndexOf("@");
                    if (idx >= 0)
                    {
                        string nick = tmp.Substring(idx + 1);
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
                }
            }
        }

        //-------------------------------------------------------------------------
        // 切断チェック
        //-------------------------------------------------------------------------
        private bool CheckDisconnect(Comment iCmt)
        {
            if (!mDisconnect)
            {
                if (iCmt.IsDisconnect)
                {
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
                    if (mOwnLive && mContWaku.Checked)
                    {
                        Thread.Sleep(500);

                        // 棒読みちゃんで自動枠取り通知
                        this.mBouyomi.Talk(mMsg.GetMessage("枠取りを開始します"));

                        this.Invoke((Action)delegate()
                        {
                            GetNextWaku();
                        });
                    }
                    return true;
                }
            }
            return false;
        }

        //-------------------------------------------------------------------------
        // 次枠取り
        //-------------------------------------------------------------------------
        private void GetNextWaku()
        {

            Thread th = new Thread(delegate()
            {
                //MakeWakutori(true);

                WakuDlg dlg = new WakuDlg(LiveID, false);
                dlg.ShowDialog();

                if (dlg.mState == WakuResult.NO_ERR)
                {
                    using (Bouyomi bm = new Bouyomi())
                    {
                        bm.Talk(mMsg.GetMessage("枠が取れたよ"));
                    }

                    this.Invoke((Action)delegate()
                    {
                        this.LiveID = dlg.mLv;
                    });

                    Connect(true);
                }
                else if (dlg.mState == WakuResult.JUNBAN)
                {
                    MakeWakutori(false);
                }

            });
            th.Name = "NivoLive.Form1.Comment.GetNextWaku()";
            th.Start();


        }

        //-------------------------------------------------------------------------
        // コメントリストにコメントを追加
        //-------------------------------------------------------------------------
        private void AddComment(Comment iCmt)
        {

           
            if (mPastChat)
            {   
                // 過去コメ
                System.Drawing.Color color = this.mUid.getUserColor(iCmt.Uid);
                Utils.AddComment(ref mCommentList, iCmt, color, ref mPastChatList);
            }
            else
            {

                //mCommentListLock.AcquireWriterLock(Timeout.Infinite);
                System.Drawing.Color color = this.mUid.getUserColor(iCmt.Uid);
                Utils.AddComment(ref mCommentList, iCmt, color);
                //Utils.AddComment(ref mCommentList, iCmt);

            }




            mCommentForm.AddComment(iCmt);
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
        private void SendComment(string iComment, bool iAdmin)
        {
            if (mNico == null) return;
            if (!mNico.IsLogin) return;

            Thread th = new Thread(delegate()
            {
                if (!iAdmin)
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
        private void SendComment(string iComment, bool iAdmin, bool i184)
        {
            if (mNico == null) return;
            if (!mNico.IsLogin) return;

            Thread th = new Thread(delegate()
            {
                if (!iAdmin)
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
        private void SendComment(string iComment, string iMail, bool iAdmin, bool i184)
        {
            if (mNico == null) return;
            if (!mNico.IsLogin) return;

            Thread th = new Thread(delegate()
            {
                if (!iAdmin)
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
