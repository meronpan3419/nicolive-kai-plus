//-------------------------------------------------------------------------
// Main Form
//
// Copyright (c) 金時豆(http://ch.nicovideo.jp/community/co48276)
// $Id$
//-------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows.Forms;


//-------------------------------------------------------------------------
// クラス実装
//-------------------------------------------------------------------------
namespace NicoLive
{
    delegate void SendCommentDelegate(string iComment, bool iAdmin);
    // フォームクラス
    public partial class Form1 : Form
    {
        #region 変数
        // ニコニコアクセスクラス
        private Nico mNico = null;

        // 棒読みちゃんアクセスクラス
        private Bouyomi mBouyomi = null;

        // コテハン用クラス
        private UserID mUid = null;

        // 放送情報管理クラス
        private LiveInfo mLiveInfo = null;

        // 読み上げコメントリスト
        private List<string> mSpeakList = null;
        static readonly object mSpeakLock = new object();

        // ユーザー名取得スレッド
        private List<string> mGatherUserID = null;
        private ReaderWriterLock mGatherLock = new ReaderWriterLock();



        // 前のログイン状態
        private bool mPrevLogin = false;

        // 来場者がこれ以上になったとき読み上げる
        private int mTargetVisitorCnt = 10;

        // 来場者数
        private int mVisitorCnt = 0;

        // 枠数・枠待ち
        private string _Wakusu = "?";
        private string _Wakumachi = "?";

        // 自動接続
        public bool mAutoConnect = false;

        // 迅速に配信
        public bool mFastLive = false;

        // 自動再接続
        public bool mAutoReconnectOnGoing = false;

        // 再接続カウント
        public int mConnectCount = 0;

        // 接続中か
        public bool mLogin_cancel = false;

        // console.swfのuri
        private readonly string mConsoleUri = "http://live.nicovideo.jp/console.swf?v=";

        // Twitterで放送開始をポストしたかどうか
        private bool mTwPost = false;
        private string mTwPostedLv = "";

        // 自分の配信かどうか
        private bool mOwnLive = false;

        // 切断済みかどうか
        private bool mDisconnect = true;

        // 最後にコメントを読み上げた時間
        //private DateTime mSpeakTime;

        // 次枠取得中か
        private bool mDoingGetNextWaku = false;

        // 残り3分通知したかどうか
        private bool mTalkLimit = false;

        // ロガー
        private StreamWriter mLogger = null;

        // 配信開始ボタンがおされた
        private bool mPushStart = false;

        // 延長ボタンがおされた
        private bool mPushExtend = false;

        // 延長されたかどうか
        private bool mIsExtend = false;

        // NetworkSpeed計測用
        private UIStatus mUIStatus;

        // メッセージ設定
        private MessageSettings mMsg;

        // 返信クラス
        private Response mRes;

        private Launcher mLauncher;

        // keepaliveコメントを送信するタイミング（5分おき）
        private readonly int KEEP_ALIVE_TIME = 5;

        // 最後にコメントを受信した時間
        private DateTime mLastChatTime;

        //最後に現在地コメントした時間
        private DateTime mLastGenzaichiCommentTime;

        // 現在地をコメントするタイミング(1分おき)
        private int GENZAICHI_TIME = 1;

        //最後にコネクションチェックした時間
        private DateTime mLastConnectionCheckTime;

        // 最後に枠数取得した時間
        private DateTime mLastWakusuTime;

        // /compact を送信するタイミング(11分おき)
        private int COMPACT_TIME = 11;

        // compact 予告送信フラグ
        private bool mCompactForcast = false;

        // 最後にコンパクトを送信した時刻
        private DateTime mLastCompctTime;

        // 外部配信開始フラグ
        private bool mStartHQ = false;
        private bool _boot = true;


        private bool mSkipLogin;

        private UInt32 mNextGC;

        private Viewer mViewer = null;
        private LiveConsole mLiveConsole = null;

        private bool mSkipBouyomi = true;

        private string mCurrentLiveID = "";

        // 無料延長用
        private SaleList mFreeExtendItem;

        // 最終コメントレス番号　（コメントサーバーでの）
        private int mLastChatNo;

        // 最終コメントレス番号　（豆ライブが処理した分）
        private int mLastChatNo2;

        // 過去コメ読み込み中かどうか
        private bool mPastChat;

        // コメントバッファ
        List<DataGridViewRow> mPastChatList = new List<DataGridViewRow>();

        // ようこそ
        private List<string> mWelcomeList = new List<string>();

        // アンケート用
        private Vote mVote;

        #endregion

        #region 列挙列
        // コメントカラム
        private enum CommentColumn : int
        {
            COLUMN_NUMBER = 0,			// 番号
            COLUMN_ID,					// ＩＤ
            COLUMN_HANDLE,				// コテハン
            COLUMN_COMMENT,				// コメント
        }
        #endregion

        //-------------------------------------------------------------------------
        // 放送ＩＤ
        //-------------------------------------------------------------------------
        public string LiveID
        {
            set { this.mLiveID.Text = value; }
            get { return this.mCurrentLiveID; }
        }

        //-------------------------------------------------------------------------
        // コンポーネント初期化
        //-------------------------------------------------------------------------
        public Form1()
        {
            InitializeComponent();

            Properties.Settings.Default.auto_wakutori = true;

            if (Properties.Settings.Default.tw_token.Length == 0 ||
                    Properties.Settings.Default.tw_token_secret.Length == 0)
            {
                cbCommentTwitter.Enabled = false;

            }
            if (Properties.Settings.Default.comment_sort_desc)
            {
                this.mCommentList.Columns[0].HeaderCell.SortGlyphDirection = SortOrder.Descending;
            }
            else
            {
                this.mCommentList.Columns[0].HeaderCell.SortGlyphDirection = SortOrder.Ascending;
            }

            // 枠数
            UpdateWakumachi();
            mLastWakusuTime = DateTime.Now;

            // メッセージロード
            mMsg = MessageSettings.Instance;
            mMsg.Load();
        }

        //-------------------------------------------------------------------------
        // ステータス表示設定
        //-------------------------------------------------------------------------
        public void UpdateStatusVisibility()
        {
            mTotalCnt.Visible = Properties.Settings.Default.TotalCnt_view;
            mActiveCnt.Visible = Properties.Settings.Default.ActiveCnt_view;
            mUniqCnt.Visible = Properties.Settings.Default.UniqCnt_view;
            mCpuInfo.Visible = Properties.Settings.Default.CpuInfo_view;
            mBattery.Visible = Properties.Settings.Default.Battery_view;
            mUpLink.Visible = Properties.Settings.Default.UpLink_view;
            mWakumachi.Visible = Properties.Settings.Default.wakumachi_view;

        }

        //-------------------------------------------------------------------------
        // コメビュ色設定
        //-------------------------------------------------------------------------
        public void UpdateCommentColor()
        {
            if (mCommentList.RowsDefaultCellStyle.BackColor != Properties.Settings.Default.back_color)
            {
                mCommentList.RowsDefaultCellStyle.BackColor = Properties.Settings.Default.back_color;
                mCommentList.BackgroundColor = Properties.Settings.Default.back_color;
            }

            if (mCommentList.RowsDefaultCellStyle.ForeColor != Properties.Settings.Default.text_color)
            {
                mCommentList.RowsDefaultCellStyle.ForeColor = Properties.Settings.Default.text_color;
            }
        }

        //-------------------------------------------------------------------------
        // 接続
        //-------------------------------------------------------------------------
        public void Connect(bool iSkipLogin)
        {
            if (mLoginWorker.IsBusy)
                return;
            mSkipBouyomi = true;
            mSkipLogin = iSkipLogin;
            mPushStart = false;
            mPushExtend = false;
            mIsExtend = false;
            mStartHQ = false;
            mPrevLogin = false;
            mTwPost = false;
            mOwnLive = false;
            mDisconnect = true;
            mTalkLimit = false;
            mDoingGetNextWaku = false;
            mNextGC = 0;
            mNico.Comment = "";
            mTargetVisitorCnt = 10;

            mSpeakList.Clear();
            mLiveInfo.Clear();

            mCurrentLiveID = "";
            // 放送ＩＤをフォーマット
            mLiveID.Text = ParseLiveID();

            // HQチェック
            if (HQ.hasHQ())
            {
                HQ.Stop();
            }

            // 切断
            mNico.Close();

            if (mLogger != null)
            {
                mLogger.Close();
                mLogger = null;
            }

            // 放送IDが空
            if (this.mLiveID.Text.Length == 0)
            {
                MessageBox.Show("放送ＩＤが空です。", "NicoLive");
                return;
            }


            this.Invoke((Action)delegate()
            {
                this.mCommentList.Rows.Clear();

                this.mConnectBtn.Enabled = false;
            });

            mLastChatTime = DateTime.Now;
            mLastCompctTime = DateTime.Now;
            mLastGenzaichiCommentTime = DateTime.Now;
            mCompactForcast = false;

            mVote = new Vote();

            GC.Collect();

            // ログインワーカースタート
            mLoginWorker.RunWorkerAsync();
        }

        //-------------------------------------------------------------------------
        // 放送ＩＤ取得
        //-------------------------------------------------------------------------
        public string ParseLiveID()
        {
            string live_id = "lv";
            if (this.mLiveID == null || this.mLiveID.Text == null) return "";

            string tmp = this.mLiveID.Text;

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
        // 現在放送中のLV取得
        //-------------------------------------------------------------------------
        private string GetCurrentLive()
        {
            Nico nico = Nico.Instance;

            string result = nico.GetCurrentLive(
                                Properties.Settings.Default.user_id,
                                Properties.Settings.Default.password
                             );

            nico = null;
            return result;
        }
        //-------------------------------------------------------------------------
        // 配信プレイヤー取得
        //-------------------------------------------------------------------------
        private void GetPlayer()
        {
            //string uri = mConsoleUri + LiveID + "&_ut=" + Utils.GetUnixTime(DateTime.Now);
            ////if (uri.Equals(mFlash.Movie))
            ////{
            ////    mFlash.LoadMovie(0, mConsoleUri);
            ////}
            //mFlash.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top);
            //mFlash.Size = new System.Drawing.Size(958, 205);
            //mFlash.LoadMovie(0, uri);


        }

        //-------------------------------------------------------------------------
        // 来場者数通知
        //-------------------------------------------------------------------------
        private void SpeakVisitor()
        {
            // 読み上げが有効になってない
            if (!this.mVisitorBtn.Checked)
                return;

            // 読み上げる
            int cnt = mVisitorCnt;
            if (cnt <= 0) return;

            if (mTargetVisitorCnt <= cnt)
            {
                string str = String.Format(mMsg.GetMessage("只今の来場者すう、{0}人です"), cnt);
                mBouyomi.Talk(str);

                mTargetVisitorCnt = cnt + 10;
                mTargetVisitorCnt /= 10;
                mTargetVisitorCnt *= 10;
            }
        }

        //-------------------------------------------------------------------------
        // 自動枠取り画面起動
        //-------------------------------------------------------------------------
        private void MakeWakutori(bool iAuto)
        {
            Wakutori mk = new Wakutori();
            mk.MyOwner = this;
            mk.AutoWaku = iAuto;
            mk.Show();
        }




        private void CommentPost()
        {
            if (cbCommentOwner.Checked)
            {
                //運営コメント
                SendComment(mCommentBox.Text, mCommandBox.Text, true, true);
            }
            else
            {
                if (cbComment184.Checked)
                {
                    SendComment(mCommentBox.Text, false, true);
                }
                else
                {
                    SendComment(mCommentBox.Text, false, false);
                }

            }

            if (cbCommentTwitter.Checked || Properties.Settings.Default.tw_token.Length == 0 ||
              Properties.Settings.Default.tw_token_secret.Length == 0)
            {


                Twitter t = new Twitter(Properties.Settings.Default.tw_token, Properties.Settings.Default.tw_token_secret);
                string comment = mCommentBox.Text;
                string msg = comment + " (" + mLiveInfo.Title + " http://nico.ms/" + LiveID + " ) " + Properties.Settings.Default.tw_hash;
                Utils.WriteLog("msg:" + msg);
                Utils.WriteLog("msg length:" + msg.Length);

                try
                {
                    Thread th = new Thread(delegate()
                    {

                        t.Post(msg, "");
                    });
                    th.Name = "NivoLive.Form1.CommentPost()";
                    th.Start();



                }
                catch (Exception)
                {

                }
            }

            mCommentBox.Text = "";
        }



        private void updateFMLEProfileList()
        {
            string path = Properties.Settings.Default.fmle_profile_path;
            if (Directory.Exists(path))
            {
                string[] xmls = System.IO.Directory.GetFiles(path);
                mFMLEProfileList.Items.Clear();
                foreach (string xml in xmls)
                {
                    mFMLEProfileList.Items.Add(System.IO.Path.GetFileName(xml));
                }
                mFMLEProfileList.SelectedItem = Properties.Settings.Default.fmle_default_profile;
                _boot = false;
            }
        }

        private void updateBroadcastType()
        {
            if (Properties.Settings.Default.use_nle)
            {
                mUseNLE.Checked = true;
            }
            else if (Properties.Settings.Default.use_xsplit)
            {
                mUseXSplit.Checked = true;
            }
            else if (Properties.Settings.Default.use_fme)
            {
                mUseFME.Checked = true;
            }


        }

        private void mUseFME_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.use_fme = true;
            Properties.Settings.Default.use_xsplit = false;
            Properties.Settings.Default.use_nle = false;
            if (Properties.Settings.Default.use_hq && mNico.IsLogin)
            {
                HQ.Restart(LiveID);
            }


        }

        private void mUseXSplit_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.use_fme = false;
            Properties.Settings.Default.use_xsplit = true;
            Properties.Settings.Default.use_nle = false;
            if (Properties.Settings.Default.use_hq && mNico.IsLogin)
            {
                HQ.Restart(LiveID);
            }

        }

        private void mUseNLE_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.use_fme = false;
            Properties.Settings.Default.use_xsplit = false;
            Properties.Settings.Default.use_nle = true;
            if (Properties.Settings.Default.use_hq && mNico.IsLogin)
            {
                HQ.Restart(LiveID);
            }

        }

        private void mNextWakuFastBtn_Click(object sender, EventArgs e)
        {


            //次枠通知
            if (Properties.Settings.Default.use_loss_time && Properties.Settings.Default.use_next_lv_notice)
            {
                if (!mDoingGetNextWaku)
                {
                    // 枠取り画面へ 
                    if (mOwnLive)
                    {
                        mDoingGetNextWaku = true;
                        Thread.Sleep(500);

                        // 棒読みちゃんで自動枠取り通知
                        this.mBouyomi.Talk(mMsg.GetMessage("枠取りを開始します"));

                        mNico.LiveStop(LiveID, mLiveInfo.Token);

                        this.Invoke((Action)delegate()
                        {
                            GetNextWaku2();
                        });
                    }
                }
            }
        }





        private void mHQRestartBtn_Click(object sender, EventArgs e)
        {
            HQ.Restart(LiveID);
        }

        private void mHQStartBtn_Click(object sender, EventArgs e)
        {
            HQ.Exec(LiveID);
        }

        private void mHQStopBtn_Click(object sender, EventArgs e)
        {
            HQ.Stop();
        }



        private void mRemainingTime_Click(object sender, EventArgs e)
        {
            if (LiveID.Length != 0)
            {
                System.Diagnostics.Process.Start("http://live.nicovideo.jp/watch/" + LiveID);
            }
        }



































    }
}
//-------------------------------------------------------------------------
// Main Form
//
// Copyright (c) 金時豆(http://ch.nicovideo.jp/community/co48276)
//-------------------------------------------------------------------------
