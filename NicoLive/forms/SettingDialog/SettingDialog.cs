//-------------------------------------------------------------------------
// 初期設定ダイアログ
//
// Copyright (c) 金時豆(http://ch.nicovideo.jp/community/co48276)
// $Id$
//-------------------------------------------------------------------------
using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Xml;
using System.Diagnostics;
using Hal.CookieGetterSharp;
using System.Net;
using System.Threading;

//-------------------------------------------------------------------------
// クラス実装
//-------------------------------------------------------------------------
namespace NicoLive
{
    /// <summary>
    /// SettingDialog の概要の説明です。
    /// </summary>
    public class SettingDialog : System.Windows.Forms.Form
    {
        private System.Windows.Forms.Button mCancelBtn;
        private TabControl mTabPage8;
        private TabPage mTabGeneral;
        private TabPage mTabBouyomi1;
        private Button mSaveBtn;
        private Label mMailLabel;
        private TextBox mPasswdBox;
        private TextBox mMailBox;
        private Label mPassLabel;
        private TabPage mTabPlace;
        private CheckBox mAutoConnect;
        private TabPage mTabTwitter;
        private Label label9;
        private TextBox mTwStart;
        private TextBox mTwEnd;
        private Label label10;
        private CheckBox mTalk3Min;
        private CheckBox mSaveLog;
        private CheckBox mTalkBat;
        private TabPage mTabCommentList;
        private ColorDialog mColorDialog;
        private NumericUpDown mRestBatt;
        private NumericUpDown mRestTime;
        private Label label5;
        private Label mAuthResult;
        private Button mAuthBtn;
        private System.Windows.Forms.Timer mUITimer;
        private CheckBox mWakuTweet;
        private CheckBox mTwEndEnable;
        private CheckBox mTwStartEnable;
        private TabPage mTabHQ;
        private OpenFileDialog mOpenFileDlg;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private TabPage mTabAutoReply;
        private DataGridView mResList;
        private Button mDelRes;
        private CheckBox mEnableAutoRes;
        private DataGridViewCheckBoxColumn Column1;
        private DataGridViewTextBoxColumn mKeyWordCol;
        private DataGridViewTextBoxColumn mResCol;
        private CheckBox mAutoReConnect;
        private CheckBox mSkip5min;
        private CheckBox cbGenzaichi;
        private CheckBox cbSpeed;
        private NumericUpDown mSekigaeMinutes;
        private CheckBox mFMEcompact;
        private TabPage mTabStatusbar;
        private GroupBox groupBox4;
        private CheckBox mUpLink;
        private CheckBox mBattery;
        private CheckBox mCpuInfo;
        private CheckBox mUniqCnt;
        private CheckBox mActiveCnt;
        private CheckBox mTotalCnt;
        private ComboBox mBrowser;
        private CheckBox mUseBrowserCookie;
        private CheckBox mTitleInc;
        private OpenFileDialog mSessionFileDlg;
        private GroupBox groupBox5;
        private CheckBox mXSplitScene;
        private NumericUpDown mAutoGenzaichiInt;
        private CheckBox cbAutoGenzaichi;
        private CheckBox cbImkHidden;
        private CheckBox cbImkOwner;
        private CheckBox mUseHQ;
        private GroupBox groupBox6;
        private Button mFMEsessionBtn;
        private TextBox mFMEsessions;
        private Label label13;
        private ComboBox mFMLEProfileList;
        private Button button1;
        private Label label18;
        private Button mFMLEProfileBtm;
        private TextBox mFMLEProfilePath;
        private Label label16;
        private CheckBox m_show_fme_setting;
        private GroupBox groupBox3;
        private NumericUpDown mFME_GUI_wait;
        private Label label11;
        private CheckBox mFME_GUI;
        private CheckBox mFMEDOS;
        private Button mFMLEBtn;
        private TextBox mFMLEPath;
        private Label label4;
        private ToolTip toolTip1;
        private GroupBox groupBox8;
        private NumericUpDown mWarpCnt;
        private Label label2;
        private CheckBox mOwnerComment;
        private CheckBox mSpeakNick;
        private CheckBox mNeed184;
        private CheckBox mNeedPremium;
        private GroupBox groupBox7;
        private Button mFindBouyomi;
        private TextBox mBouyomiPath;
        private Label label7;
        private CheckBox mLaunchBouyomi;
        private Label label1;
        private TextBox mBouyomiPort;
        private CheckBox mWakumachi;
        private CheckBox mKeepalive;
        private CheckBox mHashmarkComment;
        private CheckBox mSlashComment;

        private System.Drawing.Font mFont;
        private TabPage mTabBouyomi2;
        private GroupBox groupBox10;
        private DataGridView dataGridView1;
        private DataGridViewTextBoxColumn yName;
        private DataGridViewTextBoxColumn yTemplate;
        private GroupBox groupBox12;
        private Label mOwnerColor;
        private Label label14;
        private Label mMobileColor;
        private Label label19;
        private Label mNGColor;
        private Label label17;
        private Label mTextColor;
        private Label label15;
        private Label mBackColor;
        private Label label12;
        private GroupBox groupBox11;
        private Button mFontSettingBtn;
        private Label mFontLabel;
        private Label label20;
        private NumericUpDown mCmtMax;
        private Label label6;
        private TabPage mTabEtc;
        private GroupBox groupBox14;
        private TextBox textBox1;
        private Label label21;
        private CheckBox checkBox1;
        private GroupBox groupBox13;
        private DataGridView mLauncherList;
        private CheckBox cbLauncher;
        private TextBox textBox2;
        private Label label22;
        private TextBox mAddressTemplate2;
        private TextBox mSpeedTemplate;
        private TextBox mAddressTemplate;
        private CheckBox mFMEDOSMIN;
        private GroupBox groupBox15;
        private RadioButton mUseNLE;
        private RadioButton mUseFME;
        private RadioButton mUseXSplit;
        private CheckBox mXSplitShortcut;
        private TextBox mTwHash;
        private Label label23;
        private CheckBox mViewerAutoBoot;
        private CheckBox mContWakuNotice;
        private DataGridViewCheckBoxColumn mLaunchEnable;
        private DataGridViewTextBoxColumn mLaunchName;
        private DataGridViewTextBoxColumn mLunchPath;
        private DataGridViewTextBoxColumn mLaunchArg;
        private CheckBox cbEGM96;
        private Label label24;
        private TextBox tbResetMessage;
        private Label label25;
        private TextBox mUserSessionBox;
        private Button mLoginTest;
        private Label mLoginResult;
        private TabPage mTabGeneral2;
        private GroupBox groupBox16;
        private GroupBox groupBox17;
        private TextBox mUserNameRegex;
        private Label lUserNameRegex;
        private CheckBox mAutoUsername;
        private CheckBox mNGNotice;
        private CheckBox mUseWelcomeMessage;
        private Label mUseNameTestResult;
        private Button button2;
        private TextBox mUserID;
        private Label label26;
        private TextBox mWelcomeMessage184;
        private Label label28;
        private TextBox mWelcomeMessage;
        private Label label27;
        private TextBox mWelcomeMessageCruise;
        private CheckBox mUseCruiseWelcomeMessage;
        private CheckBox mUseAutoNick184;
        private GroupBox groupBox9;
        private TextBox mReverseGeocodingAPI;
        private Label label29;
        private GroupBox groupBox18;
        private Button mFindImk;
        private TextBox mImkPath;
        private Label label8;
        private CheckBox mLaunchImk;
        private TextBox mImakokoUser;
        private Label label3;
        private CheckBox mUseLossTime;
        private CheckBox mUseNextLvNotice;
        private CheckBox mUseFlashCommentGenerator;
        private CheckBox mCommentCut;
        private NumericUpDown mCommentCutLen;
        private Button mTweetBtn;
        private CheckBox mUseNewConsole;


        private IContainer components;

        public SettingDialog()
        {
            //
            // Windows フォーム デザイナ サポートに必要です。
            //
            InitializeComponent();

            //
            // TODO: InitializeComponent 呼び出しの後に、コンストラクタ コードを追加してください。
            //
        }

        /// <summary>
        /// 使用されているリソースに後処理を実行します。
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナで生成されたコード
        /// <summary>
        /// デザイナ サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディタで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.mCancelBtn = new System.Windows.Forms.Button();
            this.mTabPage8 = new System.Windows.Forms.TabControl();
            this.mTabGeneral = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.mLoginResult = new System.Windows.Forms.Label();
            this.mLoginTest = new System.Windows.Forms.Button();
            this.label25 = new System.Windows.Forms.Label();
            this.mUserSessionBox = new System.Windows.Forms.TextBox();
            this.mMailBox = new System.Windows.Forms.TextBox();
            this.mMailLabel = new System.Windows.Forms.Label();
            this.mBrowser = new System.Windows.Forms.ComboBox();
            this.mUseBrowserCookie = new System.Windows.Forms.CheckBox();
            this.mPassLabel = new System.Windows.Forms.Label();
            this.mPasswdBox = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.mUseNewConsole = new System.Windows.Forms.CheckBox();
            this.label24 = new System.Windows.Forms.Label();
            this.tbResetMessage = new System.Windows.Forms.TextBox();
            this.mContWakuNotice = new System.Windows.Forms.CheckBox();
            this.mViewerAutoBoot = new System.Windows.Forms.CheckBox();
            this.mKeepalive = new System.Windows.Forms.CheckBox();
            this.mTitleInc = new System.Windows.Forms.CheckBox();
            this.mRestTime = new System.Windows.Forms.NumericUpDown();
            this.mSekigaeMinutes = new System.Windows.Forms.NumericUpDown();
            this.mFMEcompact = new System.Windows.Forms.CheckBox();
            this.mSkip5min = new System.Windows.Forms.CheckBox();
            this.mRestBatt = new System.Windows.Forms.NumericUpDown();
            this.mAutoConnect = new System.Windows.Forms.CheckBox();
            this.mTalk3Min = new System.Windows.Forms.CheckBox();
            this.mSaveLog = new System.Windows.Forms.CheckBox();
            this.mAutoReConnect = new System.Windows.Forms.CheckBox();
            this.mTalkBat = new System.Windows.Forms.CheckBox();
            this.mTabGeneral2 = new System.Windows.Forms.TabPage();
            this.groupBox16 = new System.Windows.Forms.GroupBox();
            this.mUseNextLvNotice = new System.Windows.Forms.CheckBox();
            this.mUseLossTime = new System.Windows.Forms.CheckBox();
            this.mTabCommentList = new System.Windows.Forms.TabPage();
            this.groupBox17 = new System.Windows.Forms.GroupBox();
            this.mUseFlashCommentGenerator = new System.Windows.Forms.CheckBox();
            this.mUseAutoNick184 = new System.Windows.Forms.CheckBox();
            this.mWelcomeMessageCruise = new System.Windows.Forms.TextBox();
            this.mUseCruiseWelcomeMessage = new System.Windows.Forms.CheckBox();
            this.mWelcomeMessage184 = new System.Windows.Forms.TextBox();
            this.label28 = new System.Windows.Forms.Label();
            this.mWelcomeMessage = new System.Windows.Forms.TextBox();
            this.label27 = new System.Windows.Forms.Label();
            this.mUserID = new System.Windows.Forms.TextBox();
            this.label26 = new System.Windows.Forms.Label();
            this.mUseNameTestResult = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.mUserNameRegex = new System.Windows.Forms.TextBox();
            this.lUserNameRegex = new System.Windows.Forms.Label();
            this.mAutoUsername = new System.Windows.Forms.CheckBox();
            this.mNGNotice = new System.Windows.Forms.CheckBox();
            this.mUseWelcomeMessage = new System.Windows.Forms.CheckBox();
            this.groupBox12 = new System.Windows.Forms.GroupBox();
            this.mOwnerColor = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.mMobileColor = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.mNGColor = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.mTextColor = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.mBackColor = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.groupBox11 = new System.Windows.Forms.GroupBox();
            this.mFontSettingBtn = new System.Windows.Forms.Button();
            this.mFontLabel = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.mCmtMax = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.mTabBouyomi1 = new System.Windows.Forms.TabPage();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.mCommentCut = new System.Windows.Forms.CheckBox();
            this.mCommentCutLen = new System.Windows.Forms.NumericUpDown();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.mHashmarkComment = new System.Windows.Forms.CheckBox();
            this.mSlashComment = new System.Windows.Forms.CheckBox();
            this.mWarpCnt = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.mOwnerComment = new System.Windows.Forms.CheckBox();
            this.mSpeakNick = new System.Windows.Forms.CheckBox();
            this.mNeed184 = new System.Windows.Forms.CheckBox();
            this.mNeedPremium = new System.Windows.Forms.CheckBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.mFindBouyomi = new System.Windows.Forms.Button();
            this.mBouyomiPath = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.mLaunchBouyomi = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.mBouyomiPort = new System.Windows.Forms.TextBox();
            this.mTabBouyomi2 = new System.Windows.Forms.TabPage();
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.yName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.yTemplate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mTabPlace = new System.Windows.Forms.TabPage();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.mReverseGeocodingAPI = new System.Windows.Forms.TextBox();
            this.label29 = new System.Windows.Forms.Label();
            this.groupBox18 = new System.Windows.Forms.GroupBox();
            this.mFindImk = new System.Windows.Forms.Button();
            this.mImkPath = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.mLaunchImk = new System.Windows.Forms.CheckBox();
            this.mImakokoUser = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbEGM96 = new System.Windows.Forms.CheckBox();
            this.mAddressTemplate2 = new System.Windows.Forms.TextBox();
            this.mSpeedTemplate = new System.Windows.Forms.TextBox();
            this.mAddressTemplate = new System.Windows.Forms.TextBox();
            this.cbImkHidden = new System.Windows.Forms.CheckBox();
            this.cbImkOwner = new System.Windows.Forms.CheckBox();
            this.mAutoGenzaichiInt = new System.Windows.Forms.NumericUpDown();
            this.cbAutoGenzaichi = new System.Windows.Forms.CheckBox();
            this.cbSpeed = new System.Windows.Forms.CheckBox();
            this.cbGenzaichi = new System.Windows.Forms.CheckBox();
            this.mTabTwitter = new System.Windows.Forms.TabPage();
            this.mTweetBtn = new System.Windows.Forms.Button();
            this.mTwHash = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.mTwEndEnable = new System.Windows.Forms.CheckBox();
            this.mTwStartEnable = new System.Windows.Forms.CheckBox();
            this.mWakuTweet = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.mAuthResult = new System.Windows.Forms.Label();
            this.mAuthBtn = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.mTwEnd = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.mTwStart = new System.Windows.Forms.TextBox();
            this.mTabHQ = new System.Windows.Forms.TabPage();
            this.mUseNLE = new System.Windows.Forms.RadioButton();
            this.groupBox15 = new System.Windows.Forms.GroupBox();
            this.mUseHQ = new System.Windows.Forms.CheckBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.mFMEDOSMIN = new System.Windows.Forms.CheckBox();
            this.mFMEsessionBtn = new System.Windows.Forms.Button();
            this.mUseFME = new System.Windows.Forms.RadioButton();
            this.mFMEsessions = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.mFMLEProfileList = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label18 = new System.Windows.Forms.Label();
            this.mFMLEProfileBtm = new System.Windows.Forms.Button();
            this.mFMLEProfilePath = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.m_show_fme_setting = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.mFME_GUI_wait = new System.Windows.Forms.NumericUpDown();
            this.label11 = new System.Windows.Forms.Label();
            this.mFME_GUI = new System.Windows.Forms.CheckBox();
            this.mFMEDOS = new System.Windows.Forms.CheckBox();
            this.mFMLEBtn = new System.Windows.Forms.Button();
            this.mFMLEPath = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.mXSplitShortcut = new System.Windows.Forms.CheckBox();
            this.mXSplitScene = new System.Windows.Forms.CheckBox();
            this.mUseXSplit = new System.Windows.Forms.RadioButton();
            this.mTabAutoReply = new System.Windows.Forms.TabPage();
            this.mEnableAutoRes = new System.Windows.Forms.CheckBox();
            this.mDelRes = new System.Windows.Forms.Button();
            this.mResList = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.mKeyWordCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mResCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mTabStatusbar = new System.Windows.Forms.TabPage();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.mWakumachi = new System.Windows.Forms.CheckBox();
            this.mUpLink = new System.Windows.Forms.CheckBox();
            this.mBattery = new System.Windows.Forms.CheckBox();
            this.mCpuInfo = new System.Windows.Forms.CheckBox();
            this.mUniqCnt = new System.Windows.Forms.CheckBox();
            this.mActiveCnt = new System.Windows.Forms.CheckBox();
            this.mTotalCnt = new System.Windows.Forms.CheckBox();
            this.mTabEtc = new System.Windows.Forms.TabPage();
            this.groupBox14 = new System.Windows.Forms.GroupBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.groupBox13 = new System.Windows.Forms.GroupBox();
            this.mLauncherList = new System.Windows.Forms.DataGridView();
            this.mLaunchEnable = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.mLaunchName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mLunchPath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mLaunchArg = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cbLauncher = new System.Windows.Forms.CheckBox();
            this.mSaveBtn = new System.Windows.Forms.Button();
            this.mColorDialog = new System.Windows.Forms.ColorDialog();
            this.mUITimer = new System.Windows.Forms.Timer(this.components);
            this.mOpenFileDlg = new System.Windows.Forms.OpenFileDialog();
            this.mSessionFileDlg = new System.Windows.Forms.OpenFileDialog();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.mTabPage8.SuspendLayout();
            this.mTabGeneral.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mRestTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mSekigaeMinutes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mRestBatt)).BeginInit();
            this.mTabGeneral2.SuspendLayout();
            this.groupBox16.SuspendLayout();
            this.mTabCommentList.SuspendLayout();
            this.groupBox17.SuspendLayout();
            this.groupBox12.SuspendLayout();
            this.groupBox11.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mCmtMax)).BeginInit();
            this.mTabBouyomi1.SuspendLayout();
            this.groupBox8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mCommentCutLen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mWarpCnt)).BeginInit();
            this.groupBox7.SuspendLayout();
            this.mTabBouyomi2.SuspendLayout();
            this.groupBox10.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.mTabPlace.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.groupBox18.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mAutoGenzaichiInt)).BeginInit();
            this.mTabTwitter.SuspendLayout();
            this.mTabHQ.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mFME_GUI_wait)).BeginInit();
            this.groupBox5.SuspendLayout();
            this.mTabAutoReply.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mResList)).BeginInit();
            this.mTabStatusbar.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.mTabEtc.SuspendLayout();
            this.groupBox14.SuspendLayout();
            this.groupBox13.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mLauncherList)).BeginInit();
            this.SuspendLayout();
            // 
            // mCancelBtn
            // 
            this.mCancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.mCancelBtn.Location = new System.Drawing.Point(310, 488);
            this.mCancelBtn.Name = "mCancelBtn";
            this.mCancelBtn.Size = new System.Drawing.Size(75, 23);
            this.mCancelBtn.TabIndex = 2;
            this.mCancelBtn.Text = "キャンセル";
            this.mCancelBtn.Click += new System.EventHandler(this.cancel_Click);
            // 
            // mTabPage8
            // 
            this.mTabPage8.Controls.Add(this.mTabGeneral);
            this.mTabPage8.Controls.Add(this.mTabGeneral2);
            this.mTabPage8.Controls.Add(this.mTabCommentList);
            this.mTabPage8.Controls.Add(this.mTabBouyomi1);
            this.mTabPage8.Controls.Add(this.mTabBouyomi2);
            this.mTabPage8.Controls.Add(this.mTabPlace);
            this.mTabPage8.Controls.Add(this.mTabTwitter);
            this.mTabPage8.Controls.Add(this.mTabHQ);
            this.mTabPage8.Controls.Add(this.mTabAutoReply);
            this.mTabPage8.Controls.Add(this.mTabStatusbar);
            this.mTabPage8.Controls.Add(this.mTabEtc);
            this.mTabPage8.Location = new System.Drawing.Point(1, 1);
            this.mTabPage8.Name = "mTabPage8";
            this.mTabPage8.SelectedIndex = 0;
            this.mTabPage8.Size = new System.Drawing.Size(485, 481);
            this.mTabPage8.TabIndex = 0;
            // 
            // mTabGeneral
            // 
            this.mTabGeneral.Controls.Add(this.groupBox1);
            this.mTabGeneral.Controls.Add(this.groupBox2);
            this.mTabGeneral.Location = new System.Drawing.Point(4, 21);
            this.mTabGeneral.Name = "mTabGeneral";
            this.mTabGeneral.Padding = new System.Windows.Forms.Padding(3);
            this.mTabGeneral.Size = new System.Drawing.Size(477, 456);
            this.mTabGeneral.TabIndex = 0;
            this.mTabGeneral.Text = "一般";
            this.mTabGeneral.UseVisualStyleBackColor = true;
            this.mTabGeneral.Click += new System.EventHandler(this.mTabGeneral_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.mLoginResult);
            this.groupBox1.Controls.Add(this.mLoginTest);
            this.groupBox1.Controls.Add(this.label25);
            this.groupBox1.Controls.Add(this.mUserSessionBox);
            this.groupBox1.Controls.Add(this.mMailBox);
            this.groupBox1.Controls.Add(this.mMailLabel);
            this.groupBox1.Controls.Add(this.mBrowser);
            this.groupBox1.Controls.Add(this.mUseBrowserCookie);
            this.groupBox1.Controls.Add(this.mPassLabel);
            this.groupBox1.Controls.Add(this.mPasswdBox);
            this.groupBox1.Location = new System.Drawing.Point(7, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(461, 110);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "ニコ生ログイン情報";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // mLoginResult
            // 
            this.mLoginResult.AutoSize = true;
            this.mLoginResult.Location = new System.Drawing.Point(335, 88);
            this.mLoginResult.Name = "mLoginResult";
            this.mLoginResult.Size = new System.Drawing.Size(11, 12);
            this.mLoginResult.TabIndex = 10;
            this.mLoginResult.Text = "-";
            // 
            // mLoginTest
            // 
            this.mLoginTest.Location = new System.Drawing.Point(334, 64);
            this.mLoginTest.Name = "mLoginTest";
            this.mLoginTest.Size = new System.Drawing.Size(76, 19);
            this.mLoginTest.TabIndex = 9;
            this.mLoginTest.Text = "ログインテスト";
            this.mLoginTest.UseVisualStyleBackColor = true;
            this.mLoginTest.Click += new System.EventHandler(this.mLoginTest_Click);
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(20, 88);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(76, 12);
            this.label25.TabIndex = 7;
            this.label25.Text = "user_session：";
            // 
            // mUserSessionBox
            // 
            this.mUserSessionBox.Enabled = false;
            this.mUserSessionBox.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.mUserSessionBox.Location = new System.Drawing.Point(100, 85);
            this.mUserSessionBox.Name = "mUserSessionBox";
            this.mUserSessionBox.Size = new System.Drawing.Size(229, 19);
            this.mUserSessionBox.TabIndex = 6;
            // 
            // mMailBox
            // 
            this.mMailBox.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.mMailBox.Location = new System.Drawing.Point(100, 12);
            this.mMailBox.Name = "mMailBox";
            this.mMailBox.Size = new System.Drawing.Size(229, 19);
            this.mMailBox.TabIndex = 2;
            // 
            // mMailLabel
            // 
            this.mMailLabel.AutoSize = true;
            this.mMailLabel.Location = new System.Drawing.Point(19, 15);
            this.mMailLabel.Name = "mMailLabel";
            this.mMailLabel.Size = new System.Drawing.Size(75, 12);
            this.mMailLabel.TabIndex = 1;
            this.mMailLabel.Text = "メールアドレス：";
            // 
            // mBrowser
            // 
            this.mBrowser.FormattingEnabled = true;
            this.mBrowser.Location = new System.Drawing.Point(181, 62);
            this.mBrowser.Name = "mBrowser";
            this.mBrowser.Size = new System.Drawing.Size(147, 20);
            this.mBrowser.TabIndex = 5;
            this.mBrowser.SelectedIndexChanged += new System.EventHandler(this.mBrowser_SelectedIndexChanged);
            // 
            // mUseBrowserCookie
            // 
            this.mUseBrowserCookie.AutoSize = true;
            this.mUseBrowserCookie.Location = new System.Drawing.Point(19, 66);
            this.mUseBrowserCookie.Name = "mUseBrowserCookie";
            this.mUseBrowserCookie.Size = new System.Drawing.Size(162, 16);
            this.mUseBrowserCookie.TabIndex = 4;
            this.mUseBrowserCookie.Text = "ブラウザのクッキーを優先利用";
            this.mUseBrowserCookie.UseVisualStyleBackColor = true;
            this.mUseBrowserCookie.CheckedChanged += new System.EventHandler(this.mUseBrowserCookie_CheckedChanged);
            // 
            // mPassLabel
            // 
            this.mPassLabel.AutoSize = true;
            this.mPassLabel.Location = new System.Drawing.Point(19, 40);
            this.mPassLabel.Name = "mPassLabel";
            this.mPassLabel.Size = new System.Drawing.Size(58, 12);
            this.mPassLabel.TabIndex = 3;
            this.mPassLabel.Text = "パスワード：";
            // 
            // mPasswdBox
            // 
            this.mPasswdBox.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.mPasswdBox.Location = new System.Drawing.Point(100, 37);
            this.mPasswdBox.Name = "mPasswdBox";
            this.mPasswdBox.PasswordChar = '*';
            this.mPasswdBox.Size = new System.Drawing.Size(229, 19);
            this.mPasswdBox.TabIndex = 4;
            this.mPasswdBox.UseSystemPasswordChar = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.mUseNewConsole);
            this.groupBox2.Controls.Add(this.label24);
            this.groupBox2.Controls.Add(this.tbResetMessage);
            this.groupBox2.Controls.Add(this.mContWakuNotice);
            this.groupBox2.Controls.Add(this.mViewerAutoBoot);
            this.groupBox2.Controls.Add(this.mKeepalive);
            this.groupBox2.Controls.Add(this.mTitleInc);
            this.groupBox2.Controls.Add(this.mRestTime);
            this.groupBox2.Controls.Add(this.mSekigaeMinutes);
            this.groupBox2.Controls.Add(this.mFMEcompact);
            this.groupBox2.Controls.Add(this.mSkip5min);
            this.groupBox2.Controls.Add(this.mRestBatt);
            this.groupBox2.Controls.Add(this.mAutoConnect);
            this.groupBox2.Controls.Add(this.mTalk3Min);
            this.groupBox2.Controls.Add(this.mSaveLog);
            this.groupBox2.Controls.Add(this.mAutoReConnect);
            this.groupBox2.Controls.Add(this.mTalkBat);
            this.groupBox2.Location = new System.Drawing.Point(9, 122);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(462, 322);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "その他";
            // 
            // mUseNewConsole
            // 
            this.mUseNewConsole.AutoSize = true;
            this.mUseNewConsole.Location = new System.Drawing.Point(17, 279);
            this.mUseNewConsole.Name = "mUseNewConsole";
            this.mUseNewConsole.Size = new System.Drawing.Size(285, 16);
            this.mUseNewConsole.TabIndex = 34;
            this.mUseNewConsole.Text = "配信接続時に配信コンソール（GINZA）を自動起動する";
            this.toolTip1.SetToolTip(this.mUseNewConsole, "フラッシュコンソール");
            this.mUseNewConsole.UseVisualStyleBackColor = true;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(32, 213);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(80, 12);
            this.label24.TabIndex = 33;
            this.label24.Text = "予告メッセージ：";
            // 
            // tbResetMessage
            // 
            this.tbResetMessage.Location = new System.Drawing.Point(118, 210);
            this.tbResetMessage.Name = "tbResetMessage";
            this.tbResetMessage.Size = new System.Drawing.Size(235, 19);
            this.tbResetMessage.TabIndex = 32;
            // 
            // mContWakuNotice
            // 
            this.mContWakuNotice.AutoSize = true;
            this.mContWakuNotice.Location = new System.Drawing.Point(17, 81);
            this.mContWakuNotice.Name = "mContWakuNotice";
            this.mContWakuNotice.Size = new System.Drawing.Size(343, 16);
            this.mContWakuNotice.TabIndex = 31;
            this.mContWakuNotice.Text = "放送時間残り通知時に連続枠取を設定しているかどうかも通知する";
            this.mContWakuNotice.UseVisualStyleBackColor = true;
            // 
            // mViewerAutoBoot
            // 
            this.mViewerAutoBoot.AutoSize = true;
            this.mViewerAutoBoot.Location = new System.Drawing.Point(17, 257);
            this.mViewerAutoBoot.Name = "mViewerAutoBoot";
            this.mViewerAutoBoot.Size = new System.Drawing.Size(228, 16);
            this.mViewerAutoBoot.TabIndex = 30;
            this.mViewerAutoBoot.Text = "配信接続時に簡易ビュアーを自動起動する";
            this.toolTip1.SetToolTip(this.mViewerAutoBoot, "配信状況確認");
            this.mViewerAutoBoot.UseVisualStyleBackColor = true;
            // 
            // mKeepalive
            // 
            this.mKeepalive.AutoSize = true;
            this.mKeepalive.Location = new System.Drawing.Point(17, 166);
            this.mKeepalive.Name = "mKeepalive";
            this.mKeepalive.Size = new System.Drawing.Size(303, 16);
            this.mKeepalive.TabIndex = 22;
            this.mKeepalive.Text = "コメントサーバーとの接続を維持するために/keepaliveを送る";
            this.mKeepalive.UseVisualStyleBackColor = true;
            // 
            // mTitleInc
            // 
            this.mTitleInc.AutoSize = true;
            this.mTitleInc.Location = new System.Drawing.Point(17, 235);
            this.mTitleInc.Name = "mTitleInc";
            this.mTitleInc.Size = new System.Drawing.Size(177, 16);
            this.mTitleInc.TabIndex = 20;
            this.mTitleInc.Text = "タイトル内番号自動インクリメント";
            this.toolTip1.SetToolTip(this.mTitleInc, "その１　その２　その４　#1　#2");
            this.mTitleInc.UseVisualStyleBackColor = true;
            // 
            // mRestTime
            // 
            this.mRestTime.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.mRestTime.Location = new System.Drawing.Point(109, 56);
            this.mRestTime.Maximum = new decimal(new int[] {
            25,
            0,
            0,
            0});
            this.mRestTime.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.mRestTime.Name = "mRestTime";
            this.mRestTime.Size = new System.Drawing.Size(46, 19);
            this.mRestTime.TabIndex = 10;
            this.mRestTime.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // mSekigaeMinutes
            // 
            this.mSekigaeMinutes.Location = new System.Drawing.Point(118, 186);
            this.mSekigaeMinutes.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.mSekigaeMinutes.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.mSekigaeMinutes.Name = "mSekigaeMinutes";
            this.mSekigaeMinutes.Size = new System.Drawing.Size(43, 19);
            this.mSekigaeMinutes.TabIndex = 19;
            this.mSekigaeMinutes.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.mSekigaeMinutes.Value = new decimal(new int[] {
            11,
            0,
            0,
            0});
            // 
            // mFMEcompact
            // 
            this.mFMEcompact.AutoSize = true;
            this.mFMEcompact.Location = new System.Drawing.Point(17, 188);
            this.mFMEcompact.Name = "mFMEcompact";
            this.mFMEcompact.Size = new System.Drawing.Size(247, 16);
            this.mFMEcompact.TabIndex = 18;
            this.mFMEcompact.Text = "ラグ対策のため、　　　　　　分ごとに強制リロード";
            this.toolTip1.SetToolTip(this.mFMEcompact, "視聴プレイヤーが再読み込みされます");
            this.mFMEcompact.UseVisualStyleBackColor = true;
            // 
            // mSkip5min
            // 
            this.mSkip5min.AutoSize = true;
            this.mSkip5min.Location = new System.Drawing.Point(17, 18);
            this.mSkip5min.Name = "mSkip5min";
            this.mSkip5min.Size = new System.Drawing.Size(113, 16);
            this.mSkip5min.TabIndex = 17;
            this.mSkip5min.Text = "開演待ちをスキップ";
            this.toolTip1.SetToolTip(this.mSkip5min, "2010年11月のメンテで配信テストモードが出来て、旧配信コンソールだと５分経たないと配信開始できなくなったやつをすっ飛ばします。");
            this.mSkip5min.UseVisualStyleBackColor = true;
            // 
            // mRestBatt
            // 
            this.mRestBatt.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.mRestBatt.Location = new System.Drawing.Point(118, 120);
            this.mRestBatt.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.mRestBatt.Name = "mRestBatt";
            this.mRestBatt.Size = new System.Drawing.Size(43, 19);
            this.mRestBatt.TabIndex = 14;
            this.mRestBatt.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // mAutoConnect
            // 
            this.mAutoConnect.AutoSize = true;
            this.mAutoConnect.Location = new System.Drawing.Point(17, 38);
            this.mAutoConnect.Name = "mAutoConnect";
            this.mAutoConnect.Size = new System.Drawing.Size(203, 16);
            this.mAutoConnect.TabIndex = 8;
            this.mAutoConnect.Text = "「配信開始」ボタンを自動でクリックする";
            this.mAutoConnect.UseVisualStyleBackColor = true;
            // 
            // mTalk3Min
            // 
            this.mTalk3Min.AutoSize = true;
            this.mTalk3Min.Location = new System.Drawing.Point(17, 59);
            this.mTalk3Min.Name = "mTalk3Min";
            this.mTalk3Min.Size = new System.Drawing.Size(284, 16);
            this.mTalk3Min.TabIndex = 9;
            this.mTalk3Min.Text = "配信時間残り　　　　　　　分を棒読みちゃんで通知する";
            this.mTalk3Min.UseVisualStyleBackColor = true;
            this.mTalk3Min.CheckedChanged += new System.EventHandler(this.mTalk3Min_CheckedChanged);
            // 
            // mSaveLog
            // 
            this.mSaveLog.AutoSize = true;
            this.mSaveLog.Location = new System.Drawing.Point(17, 101);
            this.mSaveLog.Name = "mSaveLog";
            this.mSaveLog.Size = new System.Drawing.Size(171, 16);
            this.mSaveLog.TabIndex = 11;
            this.mSaveLog.Text = "自動でコメントのログを保存する";
            this.mSaveLog.UseVisualStyleBackColor = true;
            // 
            // mAutoReConnect
            // 
            this.mAutoReConnect.AutoSize = true;
            this.mAutoReConnect.Location = new System.Drawing.Point(17, 145);
            this.mAutoReConnect.Name = "mAutoReConnect";
            this.mAutoReConnect.Size = new System.Drawing.Size(84, 16);
            this.mAutoReConnect.TabIndex = 3;
            this.mAutoReConnect.Text = "自動再接続";
            this.toolTip1.SetToolTip(this.mAutoReConnect, "サーバーとの接続が切れたら最接続します");
            this.mAutoReConnect.UseVisualStyleBackColor = true;
            // 
            // mTalkBat
            // 
            this.mTalkBat.AutoSize = true;
            this.mTalkBat.Location = new System.Drawing.Point(17, 123);
            this.mTalkBat.Name = "mTalkBat";
            this.mTalkBat.Size = new System.Drawing.Size(346, 16);
            this.mTalkBat.TabIndex = 13;
            this.mTalkBat.Text = "バッテリー残量が　　　　　　　%以下になったら棒読みちゃんで通知する";
            this.mTalkBat.UseVisualStyleBackColor = true;
            // 
            // mTabGeneral2
            // 
            this.mTabGeneral2.Controls.Add(this.groupBox16);
            this.mTabGeneral2.Location = new System.Drawing.Point(4, 21);
            this.mTabGeneral2.Name = "mTabGeneral2";
            this.mTabGeneral2.Padding = new System.Windows.Forms.Padding(3);
            this.mTabGeneral2.Size = new System.Drawing.Size(477, 456);
            this.mTabGeneral2.TabIndex = 10;
            this.mTabGeneral2.Text = "一般2";
            this.mTabGeneral2.UseVisualStyleBackColor = true;
            // 
            // groupBox16
            // 
            this.groupBox16.Controls.Add(this.mUseNextLvNotice);
            this.groupBox16.Controls.Add(this.mUseLossTime);
            this.groupBox16.Location = new System.Drawing.Point(7, 6);
            this.groupBox16.Name = "groupBox16";
            this.groupBox16.Size = new System.Drawing.Size(461, 237);
            this.groupBox16.TabIndex = 0;
            this.groupBox16.TabStop = false;
            this.groupBox16.Text = "その他";
            // 
            // mUseNextLvNotice
            // 
            this.mUseNextLvNotice.AutoSize = true;
            this.mUseNextLvNotice.Location = new System.Drawing.Point(6, 40);
            this.mUseNextLvNotice.Name = "mUseNextLvNotice";
            this.mUseNextLvNotice.Size = new System.Drawing.Size(125, 16);
            this.mUseNextLvNotice.TabIndex = 3;
            this.mUseNextLvNotice.Text = "次枠こちらを通知する";
            this.mUseNextLvNotice.UseVisualStyleBackColor = true;
            // 
            // mUseLossTime
            // 
            this.mUseLossTime.AutoSize = true;
            this.mUseLossTime.Location = new System.Drawing.Point(6, 18);
            this.mUseLossTime.Name = "mUseLossTime";
            this.mUseLossTime.Size = new System.Drawing.Size(190, 16);
            this.mUseLossTime.TabIndex = 2;
            this.mUseLossTime.Text = "外部配信時にロスタイムを利用する";
            this.mUseLossTime.UseVisualStyleBackColor = true;
            // 
            // mTabCommentList
            // 
            this.mTabCommentList.Controls.Add(this.groupBox17);
            this.mTabCommentList.Controls.Add(this.groupBox12);
            this.mTabCommentList.Controls.Add(this.groupBox11);
            this.mTabCommentList.Location = new System.Drawing.Point(4, 21);
            this.mTabCommentList.Name = "mTabCommentList";
            this.mTabCommentList.Size = new System.Drawing.Size(477, 456);
            this.mTabCommentList.TabIndex = 4;
            this.mTabCommentList.Text = "コメビュ";
            this.mTabCommentList.UseVisualStyleBackColor = true;
            // 
            // groupBox17
            // 
            this.groupBox17.Controls.Add(this.mUseFlashCommentGenerator);
            this.groupBox17.Controls.Add(this.mUseAutoNick184);
            this.groupBox17.Controls.Add(this.mWelcomeMessageCruise);
            this.groupBox17.Controls.Add(this.mUseCruiseWelcomeMessage);
            this.groupBox17.Controls.Add(this.mWelcomeMessage184);
            this.groupBox17.Controls.Add(this.label28);
            this.groupBox17.Controls.Add(this.mWelcomeMessage);
            this.groupBox17.Controls.Add(this.label27);
            this.groupBox17.Controls.Add(this.mUserID);
            this.groupBox17.Controls.Add(this.label26);
            this.groupBox17.Controls.Add(this.mUseNameTestResult);
            this.groupBox17.Controls.Add(this.button2);
            this.groupBox17.Controls.Add(this.mUserNameRegex);
            this.groupBox17.Controls.Add(this.lUserNameRegex);
            this.groupBox17.Controls.Add(this.mAutoUsername);
            this.groupBox17.Controls.Add(this.mNGNotice);
            this.groupBox17.Controls.Add(this.mUseWelcomeMessage);
            this.groupBox17.Location = new System.Drawing.Point(11, 188);
            this.groupBox17.Name = "groupBox17";
            this.groupBox17.Size = new System.Drawing.Size(457, 265);
            this.groupBox17.TabIndex = 34;
            this.groupBox17.TabStop = false;
            this.groupBox17.Text = "コメビュ設定";
            // 
            // mUseFlashCommentGenerator
            // 
            this.mUseFlashCommentGenerator.AutoSize = true;
            this.mUseFlashCommentGenerator.Location = new System.Drawing.Point(10, 243);
            this.mUseFlashCommentGenerator.Name = "mUseFlashCommentGenerator";
            this.mUseFlashCommentGenerator.Size = new System.Drawing.Size(214, 16);
            this.mUseFlashCommentGenerator.TabIndex = 52;
            this.mUseFlashCommentGenerator.Text = "フラッシュコメントジェネレーターを利用する";
            this.toolTip1.SetToolTip(this.mUseFlashCommentGenerator, "上級者向け");
            this.mUseFlashCommentGenerator.UseVisualStyleBackColor = true;
            // 
            // mUseAutoNick184
            // 
            this.mUseAutoNick184.AutoSize = true;
            this.mUseAutoNick184.Location = new System.Drawing.Point(10, 84);
            this.mUseAutoNick184.Name = "mUseAutoNick184";
            this.mUseAutoNick184.Size = new System.Drawing.Size(136, 16);
            this.mUseAutoNick184.TabIndex = 51;
            this.mUseAutoNick184.Text = "184に勝手に名前つける";
            this.mUseAutoNick184.UseVisualStyleBackColor = true;
            // 
            // mWelcomeMessageCruise
            // 
            this.mWelcomeMessageCruise.Location = new System.Drawing.Point(93, 219);
            this.mWelcomeMessageCruise.Name = "mWelcomeMessageCruise";
            this.mWelcomeMessageCruise.Size = new System.Drawing.Size(189, 19);
            this.mWelcomeMessageCruise.TabIndex = 50;
            // 
            // mUseCruiseWelcomeMessage
            // 
            this.mUseCruiseWelcomeMessage.AutoSize = true;
            this.mUseCruiseWelcomeMessage.Checked = true;
            this.mUseCruiseWelcomeMessage.CheckState = System.Windows.Forms.CheckState.Checked;
            this.mUseCruiseWelcomeMessage.Location = new System.Drawing.Point(10, 200);
            this.mUseCruiseWelcomeMessage.Name = "mUseCruiseWelcomeMessage";
            this.mUseCruiseWelcomeMessage.Size = new System.Drawing.Size(172, 16);
            this.mUseCruiseWelcomeMessage.TabIndex = 49;
            this.mUseCruiseWelcomeMessage.Text = "クルーズにいらっしゃい！って言う";
            this.toolTip1.SetToolTip(this.mUseCruiseWelcomeMessage, "海賊船へいらっしゃい！");
            this.mUseCruiseWelcomeMessage.UseVisualStyleBackColor = true;
            // 
            // mWelcomeMessage184
            // 
            this.mWelcomeMessage184.Location = new System.Drawing.Point(93, 175);
            this.mWelcomeMessage184.Name = "mWelcomeMessage184";
            this.mWelcomeMessage184.Size = new System.Drawing.Size(189, 19);
            this.mWelcomeMessage184.TabIndex = 48;
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(29, 178);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(29, 12);
            this.label28.TabIndex = 47;
            this.label28.Text = "184：";
            // 
            // mWelcomeMessage
            // 
            this.mWelcomeMessage.Location = new System.Drawing.Point(93, 150);
            this.mWelcomeMessage.Name = "mWelcomeMessage";
            this.mWelcomeMessage.Size = new System.Drawing.Size(189, 19);
            this.mWelcomeMessage.TabIndex = 46;
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(29, 153);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(34, 12);
            this.label27.TabIndex = 45;
            this.label27.Text = "生ID：";
            // 
            // mUserID
            // 
            this.mUserID.Location = new System.Drawing.Point(93, 59);
            this.mUserID.Name = "mUserID";
            this.mUserID.Size = new System.Drawing.Size(56, 19);
            this.mUserID.TabIndex = 44;
            this.mUserID.Text = "155149";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(29, 62);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(62, 12);
            this.label26.TabIndex = 43;
            this.label26.Text = "ユーザーID：";
            // 
            // mUseNameTestResult
            // 
            this.mUseNameTestResult.AutoSize = true;
            this.mUseNameTestResult.Location = new System.Drawing.Point(292, 62);
            this.mUseNameTestResult.Name = "mUseNameTestResult";
            this.mUseNameTestResult.Size = new System.Drawing.Size(11, 12);
            this.mUseNameTestResult.TabIndex = 42;
            this.mUseNameTestResult.Text = "-";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(294, 38);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(56, 19);
            this.button2.TabIndex = 41;
            this.button2.Text = "テスト";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // mUserNameRegex
            // 
            this.mUserNameRegex.Location = new System.Drawing.Point(93, 38);
            this.mUserNameRegex.Name = "mUserNameRegex";
            this.mUserNameRegex.Size = new System.Drawing.Size(189, 19);
            this.mUserNameRegex.TabIndex = 40;
            // 
            // lUserNameRegex
            // 
            this.lUserNameRegex.AutoSize = true;
            this.lUserNameRegex.Location = new System.Drawing.Point(29, 41);
            this.lUserNameRegex.Name = "lUserNameRegex";
            this.lUserNameRegex.Size = new System.Drawing.Size(59, 12);
            this.lUserNameRegex.TabIndex = 39;
            this.lUserNameRegex.Text = "正規表現：";
            // 
            // mAutoUsername
            // 
            this.mAutoUsername.AutoSize = true;
            this.mAutoUsername.Location = new System.Drawing.Point(10, 21);
            this.mAutoUsername.Name = "mAutoUsername";
            this.mAutoUsername.Size = new System.Drawing.Size(272, 16);
            this.mAutoUsername.TabIndex = 38;
            this.mAutoUsername.Text = "「184」を外しているユーザーの名前を自動で取得する";
            this.toolTip1.SetToolTip(this.mAutoUsername, "コテハン自動取得");
            this.mAutoUsername.UseVisualStyleBackColor = true;
            // 
            // mNGNotice
            // 
            this.mNGNotice.AutoSize = true;
            this.mNGNotice.Location = new System.Drawing.Point(10, 106);
            this.mNGNotice.Name = "mNGNotice";
            this.mNGNotice.Size = new System.Drawing.Size(163, 16);
            this.mNGNotice.TabIndex = 37;
            this.mNGNotice.Text = "NGコメントを主コメで通知する";
            this.toolTip1.SetToolTip(this.mNGNotice, ">>n番さんNGです！的な");
            this.mNGNotice.UseVisualStyleBackColor = true;
            // 
            // mUseWelcomeMessage
            // 
            this.mUseWelcomeMessage.AutoSize = true;
            this.mUseWelcomeMessage.Location = new System.Drawing.Point(10, 128);
            this.mUseWelcomeMessage.Name = "mUseWelcomeMessage";
            this.mUseWelcomeMessage.Size = new System.Drawing.Size(180, 16);
            this.mUseWelcomeMessage.TabIndex = 36;
            this.mUseWelcomeMessage.Text = "初コメントでいらっしゃい！って言う";
            this.toolTip1.SetToolTip(this.mUseWelcomeMessage, ">>n番さんいらっしゃい的な！");
            this.mUseWelcomeMessage.UseVisualStyleBackColor = true;
            // 
            // groupBox12
            // 
            this.groupBox12.Controls.Add(this.mOwnerColor);
            this.groupBox12.Controls.Add(this.label14);
            this.groupBox12.Controls.Add(this.mMobileColor);
            this.groupBox12.Controls.Add(this.label19);
            this.groupBox12.Controls.Add(this.mNGColor);
            this.groupBox12.Controls.Add(this.label17);
            this.groupBox12.Controls.Add(this.mTextColor);
            this.groupBox12.Controls.Add(this.label15);
            this.groupBox12.Controls.Add(this.mBackColor);
            this.groupBox12.Controls.Add(this.label12);
            this.groupBox12.Location = new System.Drawing.Point(7, 83);
            this.groupBox12.Name = "groupBox12";
            this.groupBox12.Size = new System.Drawing.Size(461, 99);
            this.groupBox12.TabIndex = 33;
            this.groupBox12.TabStop = false;
            this.groupBox12.Text = "色";
            // 
            // mOwnerColor
            // 
            this.mOwnerColor.BackColor = System.Drawing.Color.Red;
            this.mOwnerColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mOwnerColor.Location = new System.Drawing.Point(104, 68);
            this.mOwnerColor.Name = "mOwnerColor";
            this.mOwnerColor.Size = new System.Drawing.Size(64, 18);
            this.mOwnerColor.TabIndex = 15;
            this.mOwnerColor.Click += new System.EventHandler(this.Color_Click);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(12, 74);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(80, 12);
            this.label14.TabIndex = 14;
            this.label14.Text = "運営コメント色：";
            // 
            // mMobileColor
            // 
            this.mMobileColor.BackColor = System.Drawing.Color.Blue;
            this.mMobileColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mMobileColor.Location = new System.Drawing.Point(341, 42);
            this.mMobileColor.Name = "mMobileColor";
            this.mMobileColor.Size = new System.Drawing.Size(64, 18);
            this.mMobileColor.TabIndex = 19;
            this.mMobileColor.Click += new System.EventHandler(this.Color_Click);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(249, 48);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(61, 12);
            this.label19.TabIndex = 18;
            this.label19.Text = "モバイル色：";
            // 
            // mNGColor
            // 
            this.mNGColor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.mNGColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mNGColor.Location = new System.Drawing.Point(341, 17);
            this.mNGColor.Name = "mNGColor";
            this.mNGColor.Size = new System.Drawing.Size(64, 18);
            this.mNGColor.TabIndex = 17;
            this.mNGColor.Click += new System.EventHandler(this.Color_Click);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(249, 23);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(39, 12);
            this.label17.TabIndex = 16;
            this.label17.Text = "NG色：";
            // 
            // mTextColor
            // 
            this.mTextColor.BackColor = System.Drawing.Color.Black;
            this.mTextColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mTextColor.Location = new System.Drawing.Point(104, 42);
            this.mTextColor.Name = "mTextColor";
            this.mTextColor.Size = new System.Drawing.Size(64, 18);
            this.mTextColor.TabIndex = 13;
            this.mTextColor.Click += new System.EventHandler(this.Color_Click);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(12, 48);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(47, 12);
            this.label15.TabIndex = 12;
            this.label15.Text = "文字色：";
            // 
            // mBackColor
            // 
            this.mBackColor.BackColor = System.Drawing.Color.White;
            this.mBackColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mBackColor.Location = new System.Drawing.Point(104, 17);
            this.mBackColor.Name = "mBackColor";
            this.mBackColor.Size = new System.Drawing.Size(64, 18);
            this.mBackColor.TabIndex = 11;
            this.mBackColor.Click += new System.EventHandler(this.Color_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(12, 23);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(47, 12);
            this.label12.TabIndex = 10;
            this.label12.Text = "背景色：";
            // 
            // groupBox11
            // 
            this.groupBox11.Controls.Add(this.mFontSettingBtn);
            this.groupBox11.Controls.Add(this.mFontLabel);
            this.groupBox11.Controls.Add(this.label20);
            this.groupBox11.Controls.Add(this.mCmtMax);
            this.groupBox11.Controls.Add(this.label6);
            this.groupBox11.Location = new System.Drawing.Point(7, 6);
            this.groupBox11.Name = "groupBox11";
            this.groupBox11.Size = new System.Drawing.Size(461, 71);
            this.groupBox11.TabIndex = 32;
            this.groupBox11.TabStop = false;
            this.groupBox11.Text = "表示設定";
            // 
            // mFontSettingBtn
            // 
            this.mFontSettingBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.mFontSettingBtn.Location = new System.Drawing.Point(251, 15);
            this.mFontSettingBtn.Name = "mFontSettingBtn";
            this.mFontSettingBtn.Size = new System.Drawing.Size(75, 22);
            this.mFontSettingBtn.TabIndex = 38;
            this.mFontSettingBtn.Text = "フォント選択";
            this.mFontSettingBtn.UseVisualStyleBackColor = true;
            this.mFontSettingBtn.Click += new System.EventHandler(this.mFontSettingBtn_Click);
            // 
            // mFontLabel
            // 
            this.mFontLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mFontLabel.AutoSize = true;
            this.mFontLabel.Location = new System.Drawing.Point(102, 20);
            this.mFontLabel.Name = "mFontLabel";
            this.mFontLabel.Size = new System.Drawing.Size(51, 12);
            this.mFontLabel.TabIndex = 37;
            this.mFontLabel.Text = "font, size";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(12, 20);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(76, 12);
            this.label20.TabIndex = 34;
            this.label20.Text = "コメビュフォント：";
            // 
            // mCmtMax
            // 
            this.mCmtMax.Location = new System.Drawing.Point(104, 42);
            this.mCmtMax.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.mCmtMax.Name = "mCmtMax";
            this.mCmtMax.Size = new System.Drawing.Size(64, 19);
            this.mCmtMax.TabIndex = 33;
            this.toolTip1.SetToolTip(this.mCmtMax, "コメビュに表示されるコメント数です。古いコメントはコメビュで見えなくなります。大きくしすぎると再接続時の処理が重くなります");
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 44);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 12);
            this.label6.TabIndex = 32;
            this.label6.Text = "コメント表示数：";
            // 
            // mTabBouyomi1
            // 
            this.mTabBouyomi1.Controls.Add(this.groupBox8);
            this.mTabBouyomi1.Controls.Add(this.groupBox7);
            this.mTabBouyomi1.Location = new System.Drawing.Point(4, 21);
            this.mTabBouyomi1.Name = "mTabBouyomi1";
            this.mTabBouyomi1.Padding = new System.Windows.Forms.Padding(3);
            this.mTabBouyomi1.Size = new System.Drawing.Size(477, 456);
            this.mTabBouyomi1.TabIndex = 1;
            this.mTabBouyomi1.Text = "読み上げ1";
            this.mTabBouyomi1.UseVisualStyleBackColor = true;
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.mCommentCut);
            this.groupBox8.Controls.Add(this.mCommentCutLen);
            this.groupBox8.Controls.Add(this.textBox2);
            this.groupBox8.Controls.Add(this.label22);
            this.groupBox8.Controls.Add(this.mHashmarkComment);
            this.groupBox8.Controls.Add(this.mSlashComment);
            this.groupBox8.Controls.Add(this.mWarpCnt);
            this.groupBox8.Controls.Add(this.label2);
            this.groupBox8.Controls.Add(this.mOwnerComment);
            this.groupBox8.Controls.Add(this.mSpeakNick);
            this.groupBox8.Controls.Add(this.mNeed184);
            this.groupBox8.Controls.Add(this.mNeedPremium);
            this.groupBox8.Location = new System.Drawing.Point(7, 121);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(461, 292);
            this.groupBox8.TabIndex = 23;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "読み上げ設定";
            // 
            // mCommentCut
            // 
            this.mCommentCut.AutoSize = true;
            this.mCommentCut.Location = new System.Drawing.Point(22, 184);
            this.mCommentCut.Name = "mCommentCut";
            this.mCommentCut.Size = new System.Drawing.Size(132, 16);
            this.mCommentCut.TabIndex = 35;
            this.mCommentCut.Text = "長いコメントは省略する";
            this.mCommentCut.UseVisualStyleBackColor = true;
            // 
            // mCommentCutLen
            // 
            this.mCommentCutLen.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.mCommentCutLen.Location = new System.Drawing.Point(163, 181);
            this.mCommentCutLen.Name = "mCommentCutLen";
            this.mCommentCutLen.Size = new System.Drawing.Size(63, 19);
            this.mCommentCutLen.TabIndex = 34;
            // 
            // textBox2
            // 
            this.textBox2.Enabled = false;
            this.textBox2.Location = new System.Drawing.Point(22, 238);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(410, 19);
            this.textBox2.TabIndex = 32;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(20, 223);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(122, 12);
            this.label22.TabIndex = 31;
            this.label22.Text = "読みあげない正規表現：";
            // 
            // mHashmarkComment
            // 
            this.mHashmarkComment.AutoSize = true;
            this.mHashmarkComment.Location = new System.Drawing.Point(22, 162);
            this.mHashmarkComment.Name = "mHashmarkComment";
            this.mHashmarkComment.Size = new System.Drawing.Size(109, 16);
            this.mHashmarkComment.TabIndex = 30;
            this.mHashmarkComment.Text = "#コメを読み上げる";
            this.mHashmarkComment.UseVisualStyleBackColor = true;
            // 
            // mSlashComment
            // 
            this.mSlashComment.AutoSize = true;
            this.mSlashComment.Location = new System.Drawing.Point(22, 140);
            this.mSlashComment.Name = "mSlashComment";
            this.mSlashComment.Size = new System.Drawing.Size(109, 16);
            this.mSlashComment.TabIndex = 29;
            this.mSlashComment.Text = "/コメを読み上げる";
            this.mSlashComment.UseVisualStyleBackColor = true;
            // 
            // mWarpCnt
            // 
            this.mWarpCnt.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.mWarpCnt.Location = new System.Drawing.Point(163, 24);
            this.mWarpCnt.Name = "mWarpCnt";
            this.mWarpCnt.Size = new System.Drawing.Size(63, 19);
            this.mWarpCnt.TabIndex = 28;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(136, 12);
            this.label2.TabIndex = 27;
            this.label2.Text = "コメントワープするコメント数：";
            // 
            // mOwnerComment
            // 
            this.mOwnerComment.AutoSize = true;
            this.mOwnerComment.Location = new System.Drawing.Point(22, 118);
            this.mOwnerComment.Name = "mOwnerComment";
            this.mOwnerComment.Size = new System.Drawing.Size(115, 16);
            this.mOwnerComment.TabIndex = 26;
            this.mOwnerComment.Text = "主コメを読み上げる";
            this.mOwnerComment.UseVisualStyleBackColor = true;
            // 
            // mSpeakNick
            // 
            this.mSpeakNick.AutoSize = true;
            this.mSpeakNick.Location = new System.Drawing.Point(22, 96);
            this.mSpeakNick.Name = "mSpeakNick";
            this.mSpeakNick.Size = new System.Drawing.Size(123, 16);
            this.mSpeakNick.TabIndex = 25;
            this.mSpeakNick.Text = "コテハンも読み上げる";
            this.mSpeakNick.UseVisualStyleBackColor = true;
            // 
            // mNeed184
            // 
            this.mNeed184.AutoSize = true;
            this.mNeed184.Location = new System.Drawing.Point(22, 52);
            this.mNeed184.Name = "mNeed184";
            this.mNeed184.Size = new System.Drawing.Size(190, 16);
            this.mNeed184.TabIndex = 24;
            this.mNeed184.Text = "「184」付きのコメントを読みあげない";
            this.mNeed184.UseVisualStyleBackColor = true;
            // 
            // mNeedPremium
            // 
            this.mNeedPremium.AutoSize = true;
            this.mNeedPremium.Location = new System.Drawing.Point(22, 74);
            this.mNeedPremium.Name = "mNeedPremium";
            this.mNeedPremium.Size = new System.Drawing.Size(187, 16);
            this.mNeedPremium.TabIndex = 23;
            this.mNeedPremium.Text = "一般会員のコメントを読みあげない";
            this.mNeedPremium.UseVisualStyleBackColor = true;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.mFindBouyomi);
            this.groupBox7.Controls.Add(this.mBouyomiPath);
            this.groupBox7.Controls.Add(this.label7);
            this.groupBox7.Controls.Add(this.mLaunchBouyomi);
            this.groupBox7.Controls.Add(this.label1);
            this.groupBox7.Controls.Add(this.mBouyomiPort);
            this.groupBox7.Location = new System.Drawing.Point(7, 6);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(461, 109);
            this.groupBox7.TabIndex = 20;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "棒読みちゃん";
            // 
            // mFindBouyomi
            // 
            this.mFindBouyomi.Location = new System.Drawing.Point(357, 50);
            this.mFindBouyomi.Name = "mFindBouyomi";
            this.mFindBouyomi.Size = new System.Drawing.Size(75, 23);
            this.mFindBouyomi.TabIndex = 15;
            this.mFindBouyomi.Text = "参照";
            this.mFindBouyomi.UseVisualStyleBackColor = true;
            this.mFindBouyomi.Click += new System.EventHandler(this.mFindBouyomi_Click);
            // 
            // mBouyomiPath
            // 
            this.mBouyomiPath.Location = new System.Drawing.Point(128, 52);
            this.mBouyomiPath.Name = "mBouyomiPath";
            this.mBouyomiPath.Size = new System.Drawing.Size(223, 19);
            this.mBouyomiPath.TabIndex = 14;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(20, 56);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(102, 12);
            this.label7.TabIndex = 13;
            this.label7.Text = "棒読みちゃんのパス：";
            // 
            // mLaunchBouyomi
            // 
            this.mLaunchBouyomi.AutoSize = true;
            this.mLaunchBouyomi.Location = new System.Drawing.Point(22, 37);
            this.mLaunchBouyomi.Name = "mLaunchBouyomi";
            this.mLaunchBouyomi.Size = new System.Drawing.Size(285, 16);
            this.mLaunchBouyomi.TabIndex = 12;
            this.mLaunchBouyomi.Text = "豆ライブ起動、終了時に棒読みちゃんも起動、終了する";
            this.mLaunchBouyomi.UseVisualStyleBackColor = true;
            this.mLaunchBouyomi.CheckedChanged += new System.EventHandler(this.mLaunchBouyomi_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 12);
            this.label1.TabIndex = 8;
            this.label1.Text = "ソケットポート：";
            // 
            // mBouyomiPort
            // 
            this.mBouyomiPort.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.mBouyomiPort.Location = new System.Drawing.Point(128, 12);
            this.mBouyomiPort.Name = "mBouyomiPort";
            this.mBouyomiPort.Size = new System.Drawing.Size(63, 19);
            this.mBouyomiPort.TabIndex = 9;
            // 
            // mTabBouyomi2
            // 
            this.mTabBouyomi2.Controls.Add(this.groupBox10);
            this.mTabBouyomi2.Location = new System.Drawing.Point(4, 21);
            this.mTabBouyomi2.Name = "mTabBouyomi2";
            this.mTabBouyomi2.Size = new System.Drawing.Size(477, 456);
            this.mTabBouyomi2.TabIndex = 8;
            this.mTabBouyomi2.Text = "読み上げ2";
            this.mTabBouyomi2.UseVisualStyleBackColor = true;
            // 
            // groupBox10
            // 
            this.groupBox10.Controls.Add(this.dataGridView1);
            this.groupBox10.Location = new System.Drawing.Point(7, 5);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Size = new System.Drawing.Size(461, 425);
            this.groupBox10.TabIndex = 1;
            this.groupBox10.TabStop = false;
            this.groupBox10.Text = "読み上げテンプレート";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.yName,
            this.yTemplate});
            this.dataGridView1.Location = new System.Drawing.Point(6, 18);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 21;
            this.dataGridView1.Size = new System.Drawing.Size(449, 401);
            this.dataGridView1.TabIndex = 1;
            // 
            // yName
            // 
            this.yName.HeaderText = "設定名";
            this.yName.Name = "yName";
            // 
            // yTemplate
            // 
            this.yTemplate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.yTemplate.HeaderText = "テンプレート";
            this.yTemplate.Name = "yTemplate";
            // 
            // mTabPlace
            // 
            this.mTabPlace.Controls.Add(this.groupBox9);
            this.mTabPlace.Controls.Add(this.groupBox18);
            this.mTabPlace.Controls.Add(this.cbEGM96);
            this.mTabPlace.Controls.Add(this.mAddressTemplate2);
            this.mTabPlace.Controls.Add(this.mSpeedTemplate);
            this.mTabPlace.Controls.Add(this.mAddressTemplate);
            this.mTabPlace.Controls.Add(this.cbImkHidden);
            this.mTabPlace.Controls.Add(this.cbImkOwner);
            this.mTabPlace.Controls.Add(this.mAutoGenzaichiInt);
            this.mTabPlace.Controls.Add(this.cbAutoGenzaichi);
            this.mTabPlace.Controls.Add(this.cbSpeed);
            this.mTabPlace.Controls.Add(this.cbGenzaichi);
            this.mTabPlace.Location = new System.Drawing.Point(4, 21);
            this.mTabPlace.Name = "mTabPlace";
            this.mTabPlace.Size = new System.Drawing.Size(477, 456);
            this.mTabPlace.TabIndex = 2;
            this.mTabPlace.Text = "現在地";
            this.mTabPlace.UseVisualStyleBackColor = true;
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.mReverseGeocodingAPI);
            this.groupBox9.Controls.Add(this.label29);
            this.groupBox9.Location = new System.Drawing.Point(9, 364);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(460, 46);
            this.groupBox9.TabIndex = 23;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "逆ジオコーディングAPI";
            // 
            // mReverseGeocodingAPI
            // 
            this.mReverseGeocodingAPI.Location = new System.Drawing.Point(164, 17);
            this.mReverseGeocodingAPI.Name = "mReverseGeocodingAPI";
            this.mReverseGeocodingAPI.Size = new System.Drawing.Size(177, 19);
            this.mReverseGeocodingAPI.TabIndex = 18;
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(10, 20);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(113, 12);
            this.label29.TabIndex = 17;
            this.label29.Text = "逆ジオコーディングAPI：";
            // 
            // groupBox18
            // 
            this.groupBox18.Controls.Add(this.mFindImk);
            this.groupBox18.Controls.Add(this.mImkPath);
            this.groupBox18.Controls.Add(this.label8);
            this.groupBox18.Controls.Add(this.mLaunchImk);
            this.groupBox18.Controls.Add(this.mImakokoUser);
            this.groupBox18.Controls.Add(this.label3);
            this.groupBox18.Location = new System.Drawing.Point(9, 264);
            this.groupBox18.Name = "groupBox18";
            this.groupBox18.Size = new System.Drawing.Size(461, 94);
            this.groupBox18.TabIndex = 22;
            this.groupBox18.TabStop = false;
            this.groupBox18.Text = "今ココなう！";
            // 
            // mFindImk
            // 
            this.mFindImk.Location = new System.Drawing.Point(347, 58);
            this.mFindImk.Name = "mFindImk";
            this.mFindImk.Size = new System.Drawing.Size(75, 23);
            this.mFindImk.TabIndex = 18;
            this.mFindImk.Text = "参照";
            this.mFindImk.UseVisualStyleBackColor = true;
            // 
            // mImkPath
            // 
            this.mImkPath.Location = new System.Drawing.Point(164, 60);
            this.mImkPath.Name = "mImkPath";
            this.mImkPath.Size = new System.Drawing.Size(177, 19);
            this.mImkPath.TabIndex = 17;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(10, 64);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(148, 12);
            this.label8.TabIndex = 16;
            this.label8.Text = "今ココなう！クライアントのパス：";
            // 
            // mLaunchImk
            // 
            this.mLaunchImk.AutoSize = true;
            this.mLaunchImk.Location = new System.Drawing.Point(12, 40);
            this.mLaunchImk.Name = "mLaunchImk";
            this.mLaunchImk.Size = new System.Drawing.Size(280, 16);
            this.mLaunchImk.TabIndex = 15;
            this.mLaunchImk.Text = "豆ライブ起動、終了時に今ココなう！も起動、終了する";
            this.mLaunchImk.UseVisualStyleBackColor = true;
            // 
            // mImakokoUser
            // 
            this.mImakokoUser.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.mImakokoUser.Location = new System.Drawing.Point(164, 15);
            this.mImakokoUser.Name = "mImakokoUser";
            this.mImakokoUser.Size = new System.Drawing.Size(177, 19);
            this.mImakokoUser.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "ユーザーＩＤ：";
            // 
            // cbEGM96
            // 
            this.cbEGM96.AutoSize = true;
            this.cbEGM96.Location = new System.Drawing.Point(21, 228);
            this.cbEGM96.Name = "cbEGM96";
            this.cbEGM96.Size = new System.Drawing.Size(281, 16);
            this.cbEGM96.TabIndex = 21;
            this.cbEGM96.Text = "高度のジオイド補正を利用する(@ALTITUDE_EGM96)";
            this.toolTip1.SetToolTip(this.cbEGM96, "GPSの高度を96でジオイド補正してみます");
            this.cbEGM96.UseVisualStyleBackColor = true;
            // 
            // mAddressTemplate2
            // 
            this.mAddressTemplate2.Enabled = false;
            this.mAddressTemplate2.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.mAddressTemplate2.Location = new System.Drawing.Point(39, 159);
            this.mAddressTemplate2.Name = "mAddressTemplate2";
            this.mAddressTemplate2.Size = new System.Drawing.Size(313, 19);
            this.mAddressTemplate2.TabIndex = 20;
            this.mAddressTemplate2.Text = "/perm 現在地は「 @ADDRESS」です";
            // 
            // mSpeedTemplate
            // 
            this.mSpeedTemplate.Enabled = false;
            this.mSpeedTemplate.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.mSpeedTemplate.Location = new System.Drawing.Point(39, 109);
            this.mSpeedTemplate.Name = "mSpeedTemplate";
            this.mSpeedTemplate.Size = new System.Drawing.Size(313, 19);
            this.mSpeedTemplate.TabIndex = 19;
            this.mSpeedTemplate.Text = "時速 @SPEED kmです";
            // 
            // mAddressTemplate
            // 
            this.mAddressTemplate.Enabled = false;
            this.mAddressTemplate.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.mAddressTemplate.Location = new System.Drawing.Point(39, 65);
            this.mAddressTemplate.Name = "mAddressTemplate";
            this.mAddressTemplate.Size = new System.Drawing.Size(313, 19);
            this.mAddressTemplate.TabIndex = 18;
            this.mAddressTemplate.Text = "現在地は「 @ADDRESS 」です";
            // 
            // cbImkHidden
            // 
            this.cbImkHidden.AutoSize = true;
            this.cbImkHidden.Location = new System.Drawing.Point(21, 206);
            this.cbImkHidden.Name = "cbImkHidden";
            this.cbImkHidden.Size = new System.Drawing.Size(94, 16);
            this.cbImkHidden.TabIndex = 16;
            this.cbImkHidden.Text = "hiddenをつける";
            this.cbImkHidden.UseVisualStyleBackColor = true;
            // 
            // cbImkOwner
            // 
            this.cbImkOwner.AutoSize = true;
            this.cbImkOwner.Location = new System.Drawing.Point(21, 184);
            this.cbImkOwner.Name = "cbImkOwner";
            this.cbImkOwner.Size = new System.Drawing.Size(114, 16);
            this.cbImkOwner.TabIndex = 15;
            this.cbImkOwner.Text = "主コメでコメントする";
            this.cbImkOwner.UseVisualStyleBackColor = true;
            this.cbImkOwner.CheckedChanged += new System.EventHandler(this.cbImkOwner_CheckedChanged);
            // 
            // mAutoGenzaichiInt
            // 
            this.mAutoGenzaichiInt.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.mAutoGenzaichiInt.Location = new System.Drawing.Point(39, 134);
            this.mAutoGenzaichiInt.Maximum = new decimal(new int[] {
            25,
            0,
            0,
            0});
            this.mAutoGenzaichiInt.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.mAutoGenzaichiInt.Name = "mAutoGenzaichiInt";
            this.mAutoGenzaichiInt.Size = new System.Drawing.Size(46, 19);
            this.mAutoGenzaichiInt.TabIndex = 14;
            this.mAutoGenzaichiInt.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // cbAutoGenzaichi
            // 
            this.cbAutoGenzaichi.AutoSize = true;
            this.cbAutoGenzaichi.Location = new System.Drawing.Point(21, 137);
            this.cbAutoGenzaichi.Name = "cbAutoGenzaichi";
            this.cbAutoGenzaichi.Size = new System.Drawing.Size(211, 16);
            this.cbAutoGenzaichi.TabIndex = 13;
            this.cbAutoGenzaichi.Text = "             分ごとに現在地をコメントする";
            this.cbAutoGenzaichi.UseVisualStyleBackColor = true;
            this.cbAutoGenzaichi.CheckedChanged += new System.EventHandler(this.cbAutoGenzaichi_CheckedChanged);
            // 
            // cbSpeed
            // 
            this.cbSpeed.AutoSize = true;
            this.cbSpeed.Location = new System.Drawing.Point(21, 90);
            this.cbSpeed.Name = "cbSpeed";
            this.cbSpeed.Size = new System.Drawing.Size(145, 16);
            this.cbSpeed.TabIndex = 12;
            this.cbSpeed.Text = "「速度は？」速度を答える";
            this.cbSpeed.UseVisualStyleBackColor = true;
            this.cbSpeed.CheckedChanged += new System.EventHandler(this.cbSpeed_CheckedChanged);
            // 
            // cbGenzaichi
            // 
            this.cbGenzaichi.AutoSize = true;
            this.cbGenzaichi.Location = new System.Drawing.Point(21, 43);
            this.cbGenzaichi.Name = "cbGenzaichi";
            this.cbGenzaichi.Size = new System.Drawing.Size(220, 16);
            this.cbGenzaichi.TabIndex = 11;
            this.cbGenzaichi.Text = "「現在地は？」に対して現在位置を答える";
            this.cbGenzaichi.UseVisualStyleBackColor = true;
            this.cbGenzaichi.CheckedChanged += new System.EventHandler(this.cbGenzaichi_CheckedChanged);
            // 
            // mTabTwitter
            // 
            this.mTabTwitter.Controls.Add(this.mTweetBtn);
            this.mTabTwitter.Controls.Add(this.mTwHash);
            this.mTabTwitter.Controls.Add(this.label23);
            this.mTabTwitter.Controls.Add(this.mTwEndEnable);
            this.mTabTwitter.Controls.Add(this.mTwStartEnable);
            this.mTabTwitter.Controls.Add(this.mWakuTweet);
            this.mTabTwitter.Controls.Add(this.label5);
            this.mTabTwitter.Controls.Add(this.mAuthResult);
            this.mTabTwitter.Controls.Add(this.mAuthBtn);
            this.mTabTwitter.Controls.Add(this.label10);
            this.mTabTwitter.Controls.Add(this.mTwEnd);
            this.mTabTwitter.Controls.Add(this.label9);
            this.mTabTwitter.Controls.Add(this.mTwStart);
            this.mTabTwitter.Location = new System.Drawing.Point(4, 21);
            this.mTabTwitter.Name = "mTabTwitter";
            this.mTabTwitter.Padding = new System.Windows.Forms.Padding(3);
            this.mTabTwitter.Size = new System.Drawing.Size(477, 456);
            this.mTabTwitter.TabIndex = 3;
            this.mTabTwitter.Text = "Twitter";
            this.mTabTwitter.UseVisualStyleBackColor = true;
            // 
            // mTweetBtn
            // 
            this.mTweetBtn.Enabled = false;
            this.mTweetBtn.Location = new System.Drawing.Point(241, 15);
            this.mTweetBtn.Name = "mTweetBtn";
            this.mTweetBtn.Size = new System.Drawing.Size(75, 23);
            this.mTweetBtn.TabIndex = 12;
            this.mTweetBtn.Text = "ツイートテスト";
            this.mTweetBtn.UseVisualStyleBackColor = true;
            this.mTweetBtn.Click += new System.EventHandler(this.mTweetBtn_Click);
            // 
            // mTwHash
            // 
            this.mTwHash.Location = new System.Drawing.Point(150, 289);
            this.mTwHash.Name = "mTwHash";
            this.mTwHash.Size = new System.Drawing.Size(281, 19);
            this.mTwHash.TabIndex = 11;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(35, 292);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(60, 12);
            this.label23.TabIndex = 10;
            this.label23.Text = "ハッシュダグ:";
            // 
            // mTwEndEnable
            // 
            this.mTwEndEnable.AutoSize = true;
            this.mTwEndEnable.Location = new System.Drawing.Point(37, 127);
            this.mTwEndEnable.Name = "mTwEndEnable";
            this.mTwEndEnable.Size = new System.Drawing.Size(143, 16);
            this.mTwEndEnable.TabIndex = 5;
            this.mTwEndEnable.Text = "配信終了時にTweetする";
            this.mTwEndEnable.UseVisualStyleBackColor = true;
            // 
            // mTwStartEnable
            // 
            this.mTwStartEnable.AutoSize = true;
            this.mTwStartEnable.Location = new System.Drawing.Point(37, 55);
            this.mTwStartEnable.Name = "mTwStartEnable";
            this.mTwStartEnable.Size = new System.Drawing.Size(143, 16);
            this.mTwStartEnable.TabIndex = 3;
            this.mTwStartEnable.Text = "配信開始時にTweetする";
            this.mTwStartEnable.UseVisualStyleBackColor = true;
            // 
            // mWakuTweet
            // 
            this.mWakuTweet.AutoSize = true;
            this.mWakuTweet.Location = new System.Drawing.Point(37, 255);
            this.mWakuTweet.Name = "mWakuTweet";
            this.mWakuTweet.Size = new System.Drawing.Size(259, 16);
            this.mWakuTweet.TabIndex = 9;
            this.mWakuTweet.Text = "枠取り順番待ちが100人以下になったらTweetする";
            this.mWakuTweet.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(35, 20);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(43, 12);
            this.label5.TabIndex = 0;
            this.label5.Text = "Twitter:";
            // 
            // mAuthResult
            // 
            this.mAuthResult.AutoSize = true;
            this.mAuthResult.Location = new System.Drawing.Point(84, 20);
            this.mAuthResult.Name = "mAuthResult";
            this.mAuthResult.Size = new System.Drawing.Size(41, 12);
            this.mAuthResult.TabIndex = 1;
            this.mAuthResult.Text = "未認証";
            // 
            // mAuthBtn
            // 
            this.mAuthBtn.Location = new System.Drawing.Point(150, 15);
            this.mAuthBtn.Name = "mAuthBtn";
            this.mAuthBtn.Size = new System.Drawing.Size(75, 23);
            this.mAuthBtn.TabIndex = 2;
            this.mAuthBtn.Text = "認証する";
            this.mAuthBtn.UseVisualStyleBackColor = true;
            this.mAuthBtn.Click += new System.EventHandler(this.AuthBtn_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(53, 231);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(318, 12);
            this.label10.TabIndex = 8;
            this.label10.Text = "（※ @URLは放送ＵＲＬに、@TITLEは放送タイトルに変換されます）";
            // 
            // mTwEnd
            // 
            this.mTwEnd.AcceptsReturn = true;
            this.mTwEnd.Location = new System.Drawing.Point(56, 149);
            this.mTwEnd.Multiline = true;
            this.mTwEnd.Name = "mTwEnd";
            this.mTwEnd.Size = new System.Drawing.Size(375, 58);
            this.mTwEnd.TabIndex = 6;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(54, 210);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(248, 12);
            this.label9.TabIndex = 7;
            this.label9.Text = "（※メッセージをポストしたくない時は空にしてください）";
            // 
            // mTwStart
            // 
            this.mTwStart.AcceptsReturn = true;
            this.mTwStart.Location = new System.Drawing.Point(56, 77);
            this.mTwStart.Multiline = true;
            this.mTwStart.Name = "mTwStart";
            this.mTwStart.Size = new System.Drawing.Size(375, 44);
            this.mTwStart.TabIndex = 4;
            // 
            // mTabHQ
            // 
            this.mTabHQ.Controls.Add(this.mUseNLE);
            this.mTabHQ.Controls.Add(this.groupBox15);
            this.mTabHQ.Controls.Add(this.mUseHQ);
            this.mTabHQ.Controls.Add(this.groupBox6);
            this.mTabHQ.Controls.Add(this.groupBox5);
            this.mTabHQ.Location = new System.Drawing.Point(4, 21);
            this.mTabHQ.Name = "mTabHQ";
            this.mTabHQ.Padding = new System.Windows.Forms.Padding(3);
            this.mTabHQ.Size = new System.Drawing.Size(477, 456);
            this.mTabHQ.TabIndex = 5;
            this.mTabHQ.Text = "外部配信";
            this.mTabHQ.UseVisualStyleBackColor = true;
            // 
            // mUseNLE
            // 
            this.mUseNLE.AutoSize = true;
            this.mUseNLE.Location = new System.Drawing.Point(33, 418);
            this.mUseNLE.Name = "mUseNLE";
            this.mUseNLE.Size = new System.Drawing.Size(96, 16);
            this.mUseNLE.TabIndex = 41;
            this.mUseNLE.TabStop = true;
            this.mUseNLE.Text = "NLEを使用する";
            this.mUseNLE.UseVisualStyleBackColor = true;
            this.mUseNLE.CheckedChanged += new System.EventHandler(this.mUseNLE_CheckedChanged);
            // 
            // groupBox15
            // 
            this.groupBox15.Location = new System.Drawing.Point(19, 397);
            this.groupBox15.Name = "groupBox15";
            this.groupBox15.Size = new System.Drawing.Size(428, 52);
            this.groupBox15.TabIndex = 16;
            this.groupBox15.TabStop = false;
            this.groupBox15.Text = "NLE";
            // 
            // mUseHQ
            // 
            this.mUseHQ.AutoSize = true;
            this.mUseHQ.Checked = true;
            this.mUseHQ.CheckState = System.Windows.Forms.CheckState.Checked;
            this.mUseHQ.Location = new System.Drawing.Point(19, 12);
            this.mUseHQ.Name = "mUseHQ";
            this.mUseHQ.Size = new System.Drawing.Size(155, 16);
            this.mUseHQ.TabIndex = 24;
            this.mUseHQ.Text = "外部エンコーダーを利用する";
            this.mUseHQ.UseVisualStyleBackColor = true;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.mFMEDOSMIN);
            this.groupBox6.Controls.Add(this.mFMEsessionBtn);
            this.groupBox6.Controls.Add(this.mUseFME);
            this.groupBox6.Controls.Add(this.mFMEsessions);
            this.groupBox6.Controls.Add(this.label13);
            this.groupBox6.Controls.Add(this.mFMLEProfileList);
            this.groupBox6.Controls.Add(this.button1);
            this.groupBox6.Controls.Add(this.label18);
            this.groupBox6.Controls.Add(this.mFMLEProfileBtm);
            this.groupBox6.Controls.Add(this.mFMLEProfilePath);
            this.groupBox6.Controls.Add(this.label16);
            this.groupBox6.Controls.Add(this.m_show_fme_setting);
            this.groupBox6.Controls.Add(this.groupBox3);
            this.groupBox6.Controls.Add(this.mFMEDOS);
            this.groupBox6.Controls.Add(this.mFMLEBtn);
            this.groupBox6.Controls.Add(this.mFMLEPath);
            this.groupBox6.Controls.Add(this.label4);
            this.groupBox6.Location = new System.Drawing.Point(19, 34);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(428, 259);
            this.groupBox6.TabIndex = 23;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "FME";
            // 
            // mFMEDOSMIN
            // 
            this.mFMEDOSMIN.AutoSize = true;
            this.mFMEDOSMIN.Enabled = false;
            this.mFMEDOSMIN.Location = new System.Drawing.Point(163, 128);
            this.mFMEDOSMIN.Name = "mFMEDOSMIN";
            this.mFMEDOSMIN.Size = new System.Drawing.Size(132, 16);
            this.mFMEDOSMIN.TabIndex = 38;
            this.mFMEDOSMIN.Text = "タスクバーに閉まっておく";
            this.mFMEDOSMIN.UseVisualStyleBackColor = true;
            // 
            // mFMEsessionBtn
            // 
            this.mFMEsessionBtn.Location = new System.Drawing.Point(353, 221);
            this.mFMEsessionBtn.Name = "mFMEsessionBtn";
            this.mFMEsessionBtn.Size = new System.Drawing.Size(67, 23);
            this.mFMEsessionBtn.TabIndex = 37;
            this.mFMEsessionBtn.Text = "参照";
            this.mFMEsessionBtn.UseVisualStyleBackColor = true;
            this.mFMEsessionBtn.Click += new System.EventHandler(this.mFMEsessionBtn_Click);
            // 
            // mUseFME
            // 
            this.mUseFME.AutoSize = true;
            this.mUseFME.Location = new System.Drawing.Point(14, 17);
            this.mUseFME.Name = "mUseFME";
            this.mUseFME.Size = new System.Drawing.Size(98, 16);
            this.mUseFME.TabIndex = 41;
            this.mUseFME.TabStop = true;
            this.mUseFME.Text = "FMEを使用する";
            this.mUseFME.UseVisualStyleBackColor = true;
            this.mUseFME.CheckedChanged += new System.EventHandler(this.mUseFME_CheckedChanged);
            // 
            // mFMEsessions
            // 
            this.mFMEsessions.Location = new System.Drawing.Point(147, 223);
            this.mFMEsessions.Name = "mFMEsessions";
            this.mFMEsessions.Size = new System.Drawing.Size(200, 19);
            this.mFMEsessions.TabIndex = 36;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(12, 226);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(122, 12);
            this.label13.TabIndex = 35;
            this.label13.Text = "fmesessions.datのパス：";
            // 
            // mFMLEProfileList
            // 
            this.mFMLEProfileList.FormattingEnabled = true;
            this.mFMLEProfileList.Location = new System.Drawing.Point(147, 102);
            this.mFMLEProfileList.Name = "mFMLEProfileList";
            this.mFMLEProfileList.Size = new System.Drawing.Size(199, 20);
            this.mFMLEProfileList.TabIndex = 34;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(353, 99);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(66, 23);
            this.button1.TabIndex = 33;
            this.button1.Text = "リスト更新";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(12, 104);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(107, 12);
            this.label18.TabIndex = 32;
            this.label18.Text = "デフォルトプロファイル：";
            // 
            // mFMLEProfileBtm
            // 
            this.mFMLEProfileBtm.Location = new System.Drawing.Point(353, 70);
            this.mFMLEProfileBtm.Name = "mFMLEProfileBtm";
            this.mFMLEProfileBtm.Size = new System.Drawing.Size(66, 23);
            this.mFMLEProfileBtm.TabIndex = 31;
            this.mFMLEProfileBtm.Text = "参照";
            this.mFMLEProfileBtm.UseVisualStyleBackColor = true;
            this.mFMLEProfileBtm.Click += new System.EventHandler(this.mFMEProfileBtm_Click);
            // 
            // mFMLEProfilePath
            // 
            this.mFMLEProfilePath.Location = new System.Drawing.Point(147, 72);
            this.mFMLEProfilePath.Name = "mFMLEProfilePath";
            this.mFMLEProfilePath.Size = new System.Drawing.Size(200, 19);
            this.mFMLEProfilePath.TabIndex = 30;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(12, 75);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(110, 12);
            this.label16.TabIndex = 29;
            this.label16.Text = "プロファイルxmlのパス：";
            // 
            // m_show_fme_setting
            // 
            this.m_show_fme_setting.AutoSize = true;
            this.m_show_fme_setting.Location = new System.Drawing.Point(14, 150);
            this.m_show_fme_setting.Name = "m_show_fme_setting";
            this.m_show_fme_setting.Size = new System.Drawing.Size(200, 16);
            this.m_show_fme_setting.TabIndex = 28;
            this.m_show_fme_setting.Text = "FME配信の設定をコメントで公開する";
            this.m_show_fme_setting.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.mFME_GUI_wait);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.mFME_GUI);
            this.groupBox3.Location = new System.Drawing.Point(14, 172);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(405, 41);
            this.groupBox3.TabIndex = 27;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "FMEのGUIモード設定";
            // 
            // mFME_GUI_wait
            // 
            this.mFME_GUI_wait.Location = new System.Drawing.Point(291, 15);
            this.mFME_GUI_wait.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.mFME_GUI_wait.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.mFME_GUI_wait.Name = "mFME_GUI_wait";
            this.mFME_GUI_wait.Size = new System.Drawing.Size(35, 19);
            this.mFME_GUI_wait.TabIndex = 2;
            this.mFME_GUI_wait.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.mFME_GUI_wait.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(218, 19);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(129, 12);
            this.label11.TabIndex = 1;
            this.label11.Text = "GUI起動待ち　　　　　　秒";
            // 
            // mFME_GUI
            // 
            this.mFME_GUI.AutoSize = true;
            this.mFME_GUI.Location = new System.Drawing.Point(18, 18);
            this.mFME_GUI.Name = "mFME_GUI";
            this.mFME_GUI.Size = new System.Drawing.Size(156, 16);
            this.mFME_GUI.TabIndex = 0;
            this.mFME_GUI.Text = "FMEをGUIモードで使用する";
            this.mFME_GUI.UseVisualStyleBackColor = true;
            // 
            // mFMEDOS
            // 
            this.mFMEDOS.AutoSize = true;
            this.mFMEDOS.Location = new System.Drawing.Point(14, 128);
            this.mFMEDOS.Name = "mFMEDOS";
            this.mFMEDOS.Size = new System.Drawing.Size(143, 16);
            this.mFMEDOS.TabIndex = 26;
            this.mFMEDOS.Text = "DOSプロンプトを表示する";
            this.mFMEDOS.UseVisualStyleBackColor = true;
            this.mFMEDOS.CheckedChanged += new System.EventHandler(this.mFMEDOS_CheckedChanged);
            // 
            // mFMLEBtn
            // 
            this.mFMLEBtn.Location = new System.Drawing.Point(353, 40);
            this.mFMLEBtn.Name = "mFMLEBtn";
            this.mFMLEBtn.Size = new System.Drawing.Size(66, 23);
            this.mFMLEBtn.TabIndex = 25;
            this.mFMLEBtn.Text = "参照";
            this.mFMLEBtn.UseVisualStyleBackColor = true;
            this.mFMLEBtn.Click += new System.EventHandler(this.mFMLEBtn_Click);
            // 
            // mFMLEPath
            // 
            this.mFMLEPath.Location = new System.Drawing.Point(147, 42);
            this.mFMLEPath.Name = "mFMLEPath";
            this.mFMLEPath.Size = new System.Drawing.Size(200, 19);
            this.mFMLEPath.TabIndex = 24;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 45);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(92, 12);
            this.label4.TabIndex = 23;
            this.label4.Text = "FMLECmdのパス：";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.mXSplitShortcut);
            this.groupBox5.Controls.Add(this.mXSplitScene);
            this.groupBox5.Controls.Add(this.mUseXSplit);
            this.groupBox5.Location = new System.Drawing.Point(19, 299);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(428, 92);
            this.groupBox5.TabIndex = 15;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "XSplit";
            // 
            // mXSplitShortcut
            // 
            this.mXSplitShortcut.AutoSize = true;
            this.mXSplitShortcut.Location = new System.Drawing.Point(14, 67);
            this.mXSplitShortcut.Name = "mXSplitShortcut";
            this.mXSplitShortcut.Size = new System.Drawing.Size(260, 16);
            this.mXSplitShortcut.TabIndex = 42;
            this.mXSplitShortcut.Text = "ショートカットキー（crtl+shift+A）で配信を開始する";
            this.mXSplitShortcut.UseVisualStyleBackColor = true;
            // 
            // mXSplitScene
            // 
            this.mXSplitScene.AutoSize = true;
            this.mXSplitScene.Location = new System.Drawing.Point(14, 45);
            this.mXSplitScene.Name = "mXSplitScene";
            this.mXSplitScene.Size = new System.Drawing.Size(169, 16);
            this.mXSplitScene.TabIndex = 14;
            this.mXSplitScene.Text = "XSplitのシーン変更を許可する";
            this.mXSplitScene.UseVisualStyleBackColor = true;
            // 
            // mUseXSplit
            // 
            this.mUseXSplit.AutoSize = true;
            this.mUseXSplit.Location = new System.Drawing.Point(14, 19);
            this.mUseXSplit.Name = "mUseXSplit";
            this.mUseXSplit.Size = new System.Drawing.Size(105, 16);
            this.mUseXSplit.TabIndex = 41;
            this.mUseXSplit.TabStop = true;
            this.mUseXSplit.Text = "XSplitを使用する";
            this.mUseXSplit.UseVisualStyleBackColor = true;
            this.mUseXSplit.CheckedChanged += new System.EventHandler(this.mUseXSplit_CheckedChanged);
            // 
            // mTabAutoReply
            // 
            this.mTabAutoReply.Controls.Add(this.mEnableAutoRes);
            this.mTabAutoReply.Controls.Add(this.mDelRes);
            this.mTabAutoReply.Controls.Add(this.mResList);
            this.mTabAutoReply.Location = new System.Drawing.Point(4, 21);
            this.mTabAutoReply.Name = "mTabAutoReply";
            this.mTabAutoReply.Padding = new System.Windows.Forms.Padding(3);
            this.mTabAutoReply.Size = new System.Drawing.Size(477, 456);
            this.mTabAutoReply.TabIndex = 6;
            this.mTabAutoReply.Text = "自動応答";
            this.mTabAutoReply.UseVisualStyleBackColor = true;
            // 
            // mEnableAutoRes
            // 
            this.mEnableAutoRes.AutoSize = true;
            this.mEnableAutoRes.Location = new System.Drawing.Point(20, 13);
            this.mEnableAutoRes.Name = "mEnableAutoRes";
            this.mEnableAutoRes.Size = new System.Drawing.Size(133, 16);
            this.mEnableAutoRes.TabIndex = 4;
            this.mEnableAutoRes.Text = "自動応答を有効にする";
            this.mEnableAutoRes.UseVisualStyleBackColor = true;
            // 
            // mDelRes
            // 
            this.mDelRes.Location = new System.Drawing.Point(381, 316);
            this.mDelRes.Name = "mDelRes";
            this.mDelRes.Size = new System.Drawing.Size(75, 23);
            this.mDelRes.TabIndex = 3;
            this.mDelRes.Text = "削除";
            this.mDelRes.UseVisualStyleBackColor = true;
            this.mDelRes.Click += new System.EventHandler(this.mDelRes_Click);
            // 
            // mResList
            // 
            this.mResList.AllowUserToResizeRows = false;
            this.mResList.BackgroundColor = System.Drawing.SystemColors.Control;
            this.mResList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.mResList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.mKeyWordCol,
            this.mResCol});
            this.mResList.Location = new System.Drawing.Point(20, 43);
            this.mResList.MultiSelect = false;
            this.mResList.Name = "mResList";
            this.mResList.RowHeadersVisible = false;
            this.mResList.RowTemplate.Height = 21;
            this.mResList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.mResList.Size = new System.Drawing.Size(436, 265);
            this.mResList.TabIndex = 1;
            this.mResList.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.mResList_CellContentClick);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "有効";
            this.Column1.Name = "Column1";
            this.Column1.Width = 50;
            // 
            // mKeyWordCol
            // 
            this.mKeyWordCol.HeaderText = "キーワード";
            this.mKeyWordCol.Name = "mKeyWordCol";
            this.mKeyWordCol.Width = 200;
            // 
            // mResCol
            // 
            this.mResCol.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.mResCol.HeaderText = "返答";
            this.mResCol.Name = "mResCol";
            // 
            // mTabStatusbar
            // 
            this.mTabStatusbar.Controls.Add(this.groupBox4);
            this.mTabStatusbar.Location = new System.Drawing.Point(4, 21);
            this.mTabStatusbar.Name = "mTabStatusbar";
            this.mTabStatusbar.Padding = new System.Windows.Forms.Padding(3);
            this.mTabStatusbar.Size = new System.Drawing.Size(477, 456);
            this.mTabStatusbar.TabIndex = 7;
            this.mTabStatusbar.Text = "表示";
            this.mTabStatusbar.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.mWakumachi);
            this.groupBox4.Controls.Add(this.mUpLink);
            this.groupBox4.Controls.Add(this.mBattery);
            this.groupBox4.Controls.Add(this.mCpuInfo);
            this.groupBox4.Controls.Add(this.mUniqCnt);
            this.groupBox4.Controls.Add(this.mActiveCnt);
            this.groupBox4.Controls.Add(this.mTotalCnt);
            this.groupBox4.Location = new System.Drawing.Point(3, 6);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(460, 182);
            this.groupBox4.TabIndex = 0;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "ステータスバー";
            // 
            // mWakumachi
            // 
            this.mWakumachi.AutoSize = true;
            this.mWakumachi.Location = new System.Drawing.Point(6, 133);
            this.mWakumachi.Name = "mWakumachi";
            this.mWakumachi.Size = new System.Drawing.Size(78, 16);
            this.mWakumachi.TabIndex = 6;
            this.mWakumachi.Text = "枠数/枠待";
            this.mWakumachi.UseVisualStyleBackColor = true;
            // 
            // mUpLink
            // 
            this.mUpLink.AutoSize = true;
            this.mUpLink.Location = new System.Drawing.Point(6, 155);
            this.mUpLink.Name = "mUpLink";
            this.mUpLink.Size = new System.Drawing.Size(68, 16);
            this.mUpLink.TabIndex = 5;
            this.mUpLink.Text = "上り帯域";
            this.mUpLink.UseVisualStyleBackColor = true;
            // 
            // mBattery
            // 
            this.mBattery.AutoSize = true;
            this.mBattery.Location = new System.Drawing.Point(7, 111);
            this.mBattery.Name = "mBattery";
            this.mBattery.Size = new System.Drawing.Size(67, 16);
            this.mBattery.TabIndex = 4;
            this.mBattery.Text = "バッテリー";
            this.mBattery.UseVisualStyleBackColor = true;
            // 
            // mCpuInfo
            // 
            this.mCpuInfo.AutoSize = true;
            this.mCpuInfo.Location = new System.Drawing.Point(7, 88);
            this.mCpuInfo.Name = "mCpuInfo";
            this.mCpuInfo.Size = new System.Drawing.Size(71, 16);
            this.mCpuInfo.TabIndex = 3;
            this.mCpuInfo.Text = "CPU負荷";
            this.mCpuInfo.UseVisualStyleBackColor = true;
            // 
            // mUniqCnt
            // 
            this.mUniqCnt.AutoSize = true;
            this.mUniqCnt.Location = new System.Drawing.Point(7, 65);
            this.mUniqCnt.Name = "mUniqCnt";
            this.mUniqCnt.Size = new System.Drawing.Size(72, 16);
            this.mUniqCnt.TabIndex = 2;
            this.mUniqCnt.Text = "トータル数";
            this.mUniqCnt.UseVisualStyleBackColor = true;
            // 
            // mActiveCnt
            // 
            this.mActiveCnt.AutoSize = true;
            this.mActiveCnt.Location = new System.Drawing.Point(7, 42);
            this.mActiveCnt.Name = "mActiveCnt";
            this.mActiveCnt.Size = new System.Drawing.Size(78, 16);
            this.mActiveCnt.TabIndex = 1;
            this.mActiveCnt.Text = "アクティブ数";
            this.mActiveCnt.UseVisualStyleBackColor = true;
            // 
            // mTotalCnt
            // 
            this.mTotalCnt.AutoSize = true;
            this.mTotalCnt.Location = new System.Drawing.Point(7, 19);
            this.mTotalCnt.Name = "mTotalCnt";
            this.mTotalCnt.Size = new System.Drawing.Size(72, 16);
            this.mTotalCnt.TabIndex = 0;
            this.mTotalCnt.Text = "来場者数";
            this.mTotalCnt.UseVisualStyleBackColor = true;
            // 
            // mTabEtc
            // 
            this.mTabEtc.Controls.Add(this.groupBox14);
            this.mTabEtc.Controls.Add(this.groupBox13);
            this.mTabEtc.Location = new System.Drawing.Point(4, 21);
            this.mTabEtc.Name = "mTabEtc";
            this.mTabEtc.Size = new System.Drawing.Size(477, 456);
            this.mTabEtc.TabIndex = 9;
            this.mTabEtc.Text = "その他";
            this.mTabEtc.UseVisualStyleBackColor = true;
            // 
            // groupBox14
            // 
            this.groupBox14.Controls.Add(this.textBox1);
            this.groupBox14.Controls.Add(this.label21);
            this.groupBox14.Controls.Add(this.checkBox1);
            this.groupBox14.Enabled = false;
            this.groupBox14.Location = new System.Drawing.Point(3, 205);
            this.groupBox14.Name = "groupBox14";
            this.groupBox14.Size = new System.Drawing.Size(467, 78);
            this.groupBox14.TabIndex = 1;
            this.groupBox14.TabStop = false;
            this.groupBox14.Text = "API設定";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(51, 46);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(95, 19);
            this.textBox1.TabIndex = 2;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(6, 49);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(39, 12);
            this.label21.TabIndex = 1;
            this.label21.Text = "ポート：";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(6, 18);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(133, 16);
            this.checkBox1.TabIndex = 0;
            this.checkBox1.Text = "なぞのAPIを有効にする";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // groupBox13
            // 
            this.groupBox13.Controls.Add(this.mLauncherList);
            this.groupBox13.Controls.Add(this.cbLauncher);
            this.groupBox13.Location = new System.Drawing.Point(3, 3);
            this.groupBox13.Name = "groupBox13";
            this.groupBox13.Size = new System.Drawing.Size(467, 186);
            this.groupBox13.TabIndex = 0;
            this.groupBox13.TabStop = false;
            this.groupBox13.Text = "ランチャー";
            // 
            // mLauncherList
            // 
            this.mLauncherList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.mLauncherList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.mLaunchEnable,
            this.mLaunchName,
            this.mLunchPath,
            this.mLaunchArg});
            this.mLauncherList.Location = new System.Drawing.Point(4, 40);
            this.mLauncherList.Name = "mLauncherList";
            this.mLauncherList.RowHeadersVisible = false;
            this.mLauncherList.RowTemplate.Height = 21;
            this.mLauncherList.Size = new System.Drawing.Size(446, 140);
            this.mLauncherList.TabIndex = 2;
            this.mLauncherList.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.mLauncherList_CellContentClick);
            // 
            // mLaunchEnable
            // 
            this.mLaunchEnable.HeaderText = "有効";
            this.mLaunchEnable.Name = "mLaunchEnable";
            this.mLaunchEnable.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.mLaunchEnable.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.mLaunchEnable.Width = 80;
            // 
            // mLaunchName
            // 
            this.mLaunchName.HeaderText = "設定名";
            this.mLaunchName.Name = "mLaunchName";
            // 
            // mLunchPath
            // 
            this.mLunchPath.HeaderText = "パス";
            this.mLunchPath.Name = "mLunchPath";
            // 
            // mLaunchArg
            // 
            this.mLaunchArg.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.mLaunchArg.HeaderText = "コマンドラインオプション";
            this.mLaunchArg.Name = "mLaunchArg";
            // 
            // cbLauncher
            // 
            this.cbLauncher.AutoSize = true;
            this.cbLauncher.Location = new System.Drawing.Point(4, 18);
            this.cbLauncher.Name = "cbLauncher";
            this.cbLauncher.Size = new System.Drawing.Size(132, 16);
            this.cbLauncher.TabIndex = 1;
            this.cbLauncher.Text = "放接続時に起動させる";
            this.cbLauncher.UseVisualStyleBackColor = true;
            // 
            // mSaveBtn
            // 
            this.mSaveBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.mSaveBtn.Location = new System.Drawing.Point(400, 488);
            this.mSaveBtn.Name = "mSaveBtn";
            this.mSaveBtn.Size = new System.Drawing.Size(75, 23);
            this.mSaveBtn.TabIndex = 1;
            this.mSaveBtn.Text = "保存";
            this.mSaveBtn.Click += new System.EventHandler(this.ok_Click);
            // 
            // mUITimer
            // 
            this.mUITimer.Enabled = true;
            this.mUITimer.Interval = 300;
            this.mUITimer.Tick += new System.EventHandler(this.mUITimer_Tick);
            // 
            // mOpenFileDlg
            // 
            this.mOpenFileDlg.DefaultExt = "exe";
            this.mOpenFileDlg.FileName = "FMLECmd.exe";
            this.mOpenFileDlg.Filter = "FMLECmd.exe|*.exe";
            this.mOpenFileDlg.Title = "FMLECmd.exe";
            // 
            // mSessionFileDlg
            // 
            this.mSessionFileDlg.CheckFileExists = false;
            this.mSessionFileDlg.DefaultExt = "dat";
            this.mSessionFileDlg.FileName = "fmesessions.dat";
            this.mSessionFileDlg.Filter = "fmesessions.dat|*.dat";
            this.mSessionFileDlg.Title = "fmesessions.dat";
            // 
            // SettingDialog
            // 
            this.AcceptButton = this.mCancelBtn;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 12);
            this.ClientSize = new System.Drawing.Size(485, 523);
            this.Controls.Add(this.mSaveBtn);
            this.Controls.Add(this.mTabPage8);
            this.Controls.Add(this.mCancelBtn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.Name = "SettingDialog";
            this.Text = "設定";
            this.Load += new System.EventHandler(this.SettingDialog_Load);
            this.mTabPage8.ResumeLayout(false);
            this.mTabGeneral.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mRestTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mSekigaeMinutes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mRestBatt)).EndInit();
            this.mTabGeneral2.ResumeLayout(false);
            this.groupBox16.ResumeLayout(false);
            this.groupBox16.PerformLayout();
            this.mTabCommentList.ResumeLayout(false);
            this.groupBox17.ResumeLayout(false);
            this.groupBox17.PerformLayout();
            this.groupBox12.ResumeLayout(false);
            this.groupBox12.PerformLayout();
            this.groupBox11.ResumeLayout(false);
            this.groupBox11.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mCmtMax)).EndInit();
            this.mTabBouyomi1.ResumeLayout(false);
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mCommentCutLen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mWarpCnt)).EndInit();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.mTabBouyomi2.ResumeLayout(false);
            this.groupBox10.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.mTabPlace.ResumeLayout(false);
            this.mTabPlace.PerformLayout();
            this.groupBox9.ResumeLayout(false);
            this.groupBox9.PerformLayout();
            this.groupBox18.ResumeLayout(false);
            this.groupBox18.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mAutoGenzaichiInt)).EndInit();
            this.mTabTwitter.ResumeLayout(false);
            this.mTabTwitter.PerformLayout();
            this.mTabHQ.ResumeLayout(false);
            this.mTabHQ.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mFME_GUI_wait)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.mTabAutoReply.ResumeLayout(false);
            this.mTabAutoReply.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mResList)).EndInit();
            this.mTabStatusbar.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.mTabEtc.ResumeLayout(false);
            this.groupBox14.ResumeLayout(false);
            this.groupBox14.PerformLayout();
            this.groupBox13.ResumeLayout(false);
            this.groupBox13.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mLauncherList)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        //-------------------------------------------------------------------------
        // キャンセル
        //-------------------------------------------------------------------------
        private void cancel_Click(object sender, EventArgs e)
        {
            //this.DialogResult = DialogResult.OK;
            this.Close();
        }

        //-------------------------------------------------------------------------
        // 保存
        //-------------------------------------------------------------------------
        private void ok_Click(object sender, EventArgs e)
        {
            if (this.mMailBox.Text.Length <= 0)
            {
                MessageBox.Show("メールアドレスを設定してください");
                return;
            }
            if (this.mPasswdBox.Text.Length <= 0)
            {
                MessageBox.Show("パスワードを設定してください");
                return;
            }
            this.SaveSettings();

            // 表示の更新
            (Owner as Form1).Invoke((Action)delegate()
            {
                (Owner as Form1).UpdateStatusVisibility();
                (Owner as Form1).UpdateCommentColor();


            });



            this.Close();
        }

        //-------------------------------------------------------------------------
        // 作成時
        //-------------------------------------------------------------------------
        private void SettingDialog_Load(object sender, EventArgs e)
        {
            this.LoadSettings();

            // センタリング
            this.Top = (Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2;
            this.Left = (Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2;

            mLoginResult.Text = "";
        }

        //-------------------------------------------------------------------------
        // 設定ファイルロード
        //-------------------------------------------------------------------------
        private void LoadSettings()
        {
            // 一般
            this.mMailBox.Text = Properties.Settings.Default.user_id;
            this.mPasswdBox.Text = Properties.Settings.Default.password;
            this.mFontLabel.Text = Properties.Settings.Default.font.Name;
            mFont = Properties.Settings.Default.font;
            this.mAutoConnect.Checked = Properties.Settings.Default.auto_connect;
            this.mTalk3Min.Checked = Properties.Settings.Default.talk_3min;
            this.mSaveLog.Checked = Properties.Settings.Default.save_log;
            this.mAutoUsername.Checked = Properties.Settings.Default.auto_username;
            this.mWakuTweet.Checked = Properties.Settings.Default.tweet_wait;
            this.mTalkBat.Checked = Properties.Settings.Default.talk_bat;
            this.mNeed184.Checked = Properties.Settings.Default.need184;
            this.mRestBatt.Value = Properties.Settings.Default.rest_batt;
            this.mRestTime.Value = Properties.Settings.Default.rest_time;
            this.mCmtMax.Value = Properties.Settings.Default.comment_max;
            this.mTitleInc.Checked = Properties.Settings.Default.title_auto_inc;
            this.mNGNotice.Checked = Properties.Settings.Default.ng_notice;
            this.mKeepalive.Checked = Properties.Settings.Default.send_keepalive;
            this.mSekigaeMinutes.Value = Properties.Settings.Default.sekigaeMinutes;
            this.mViewerAutoBoot.Checked = Properties.Settings.Default.viewer_auto_boot;

            this.mContWakuNotice.Checked = Properties.Settings.Default.cont_waku_notice;
            this.mContWakuNotice.Enabled = Properties.Settings.Default.talk_3min;

            this.tbResetMessage.Text = Properties.Settings.Default.reset_message;



            this.mUseNewConsole.Checked = Properties.Settings.Default.use_flash_console;
            this.mUseLossTime.Checked = Properties.Settings.Default.use_loss_time;
            this.mUseNextLvNotice.Checked = Properties.Settings.Default.use_next_lv_notice;

            this.mUseWelcomeMessage.Checked = Properties.Settings.Default.use_welcome_message;
            this.mUserNameRegex.Text = Properties.Settings.Default.user_name_regex;

            this.mUseAutoNick184.Checked = Properties.Settings.Default.auto_nick_184;
            this.mUseCruiseWelcomeMessage.Checked = Properties.Settings.Default.use_welcome_cruise_message;
            this.mWelcomeMessage.Text = Properties.Settings.Default.welcome_message;
            this.mWelcomeMessage184.Text = Properties.Settings.Default.welcome_message184;
            this.mWelcomeMessageCruise.Text = Properties.Settings.Default.welcome_cruise_message;
            this.mUseFlashCommentGenerator.Checked = Properties.Settings.Default.use_flash_comment_generator;


            this.mCommentCut.Checked = Properties.Settings.Default.use_comment_cut;
            this.mCommentCutLen.Value = Properties.Settings.Default.comment_cut_len;

            // クッキー
            ICookieGetter[] cookieGetters = CookieGetter.CreateInstances(true);
            mBrowser.Items.Clear();
            mBrowser.Items.AddRange(cookieGetters);
            int sel = mBrowser.FindStringExact(Properties.Settings.Default.Browser);
            if (sel < 0)
            {
                mBrowser.SelectedIndex = 0;
            }
            else
            {
                mBrowser.SelectedIndex = sel;
            }
            mUseBrowserCookie.Checked = Properties.Settings.Default.UseBrowserCookie;
            mLoginTest.Enabled = Properties.Settings.Default.UseBrowserCookie;
            mBrowser.Enabled = mUseBrowserCookie.Checked;
            //mMailBox.Enabled = !mUseBrowserCookie.Checked;
            //mPasswdBox.Enabled = !mUseBrowserCookie.Checked;

            // 棒読みちゃん
            this.mBouyomiPort.Text = Properties.Settings.Default.bouyomi_port.ToString();
            this.mWarpCnt.Value = Properties.Settings.Default.warp_cnt;
            this.mLaunchBouyomi.Checked = Properties.Settings.Default.launch_bouyomi;
            this.mBouyomiPath.Text = Properties.Settings.Default.bouyomi_path;
            mBouyomiPath.Enabled = mLaunchBouyomi.Checked;
            mFindBouyomi.Enabled = mLaunchBouyomi.Checked;
            this.mOwnerComment.Checked = Properties.Settings.Default.owner_comment;
            this.mNeedPremium.Checked = Properties.Settings.Default.need_premium;
            this.mSpeakNick.Checked = Properties.Settings.Default.speak_nick;
            this.mSlashComment.Checked = Properties.Settings.Default.slash_comment;
            this.mHashmarkComment.Checked = Properties.Settings.Default.hashamark_comment;

            //this.mCmtInterval.Text = Properties.Settings.Default.comment_interval.ToString();

            // 今ココ
            this.mImakokoUser.Text = Properties.Settings.Default.imakoko_user;
            this.mLaunchImk.Checked = Properties.Settings.Default.launch_imk;
            this.mImkPath.Text = Properties.Settings.Default.imk_path;
            mImkPath.Enabled = mLaunchImk.Checked;
            mFindImk.Enabled = mLaunchImk.Checked;
            cbGenzaichi.Checked = Properties.Settings.Default.imakoko_genzaichi;
            cbSpeed.Checked = Properties.Settings.Default.imakoko_speed;
            cbAutoGenzaichi.Checked = Properties.Settings.Default.imakoko_genzaichi_auto_comment;
            mAutoGenzaichiInt.Value = Properties.Settings.Default.imakoko_genzaichi_auto_comment_int;
            cbImkOwner.Checked = Properties.Settings.Default.imakoko_genzaichi_owner;
            cbImkHidden.Checked = Properties.Settings.Default.imakoko_genzaichi_hidden;
            mAddressTemplate.Text = Properties.Settings.Default.address_template;
            mAddressTemplate.Enabled = Properties.Settings.Default.imakoko_genzaichi;
            mAddressTemplate2.Text = Properties.Settings.Default.address_template2;
            mAddressTemplate2.Enabled = Properties.Settings.Default.imakoko_genzaichi_auto_comment;
            mSpeedTemplate.Text = Properties.Settings.Default.speed_template;
            mSpeedTemplate.Enabled = Properties.Settings.Default.imakoko_speed;
            cbEGM96.Checked = Properties.Settings.Default.use_egm96;
            mReverseGeocodingAPI.Text = Properties.Settings.Default.geocode_address_url;

            // Twitter
            this.mTwStart.Text = Properties.Settings.Default.tw_start;
            this.mTwStartEnable.Checked = Properties.Settings.Default.tw_start_enable;
            this.mTwEnd.Text = Properties.Settings.Default.tw_end;
            this.mTwEndEnable.Checked = Properties.Settings.Default.tw_end_enable;
            this.mAuthResult.Text = (Properties.Settings.Default.tw_token.Length == 0) ? "未認証" : "認証済み";
            this.mAuthResult.BackColor = (Properties.Settings.Default.tw_token.Length == 0) ? mAuthResult.ForeColor = System.Drawing.Color.Red : System.Drawing.Color.Transparent;
            this.mTweetBtn.Enabled = !(Properties.Settings.Default.tw_token.Length == 0);


            this.mTwHash.Text = Properties.Settings.Default.tw_hash;

            // 色
            this.mBackColor.BackColor = Properties.Settings.Default.back_color;
            this.mTextColor.BackColor = Properties.Settings.Default.text_color;
            this.mOwnerColor.BackColor = Properties.Settings.Default.owner_color;
            this.mNGColor.BackColor = Properties.Settings.Default.ng_color;
            this.mMobileColor.BackColor = Properties.Settings.Default.mobile_color;

            // 外部配信
            mUseHQ.Checked = Properties.Settings.Default.use_hq;
            mUseFME.Checked = Properties.Settings.Default.use_fme;
            mUseXSplit.Checked = Properties.Settings.Default.use_xsplit;
            mUseNLE.Checked = Properties.Settings.Default.use_nle;

            mXSplitScene.Checked = Properties.Settings.Default.enable_xsplit_scene_change;
            mXSplitShortcut.Checked = Properties.Settings.Default.use_xsplit_shortcut;

            mFMLEPath.Text = Properties.Settings.Default.fmle_path;
            mFMEDOS.Checked = Properties.Settings.Default.fme_dos;
            mFMEDOSMIN.Enabled = Properties.Settings.Default.fme_dos;
            mFMEDOSMIN.Checked = Properties.Settings.Default.fme_dos_min;
            mFMEcompact.Checked = Properties.Settings.Default.fme_compact;
            mFME_GUI.Checked = Properties.Settings.Default.fme_gui;
            mFME_GUI_wait.Value = Properties.Settings.Default.fme_wait;
            m_show_fme_setting.Checked = Properties.Settings.Default.show_fme_setting;
            mFMEsessions.Text = Properties.Settings.Default.fme_session;

            mFMLEProfilePath.Text = Properties.Settings.Default.fmle_profile_path;
            mFMLEProfileList.Items.Clear();
            ShowFMLEProfileList();
            mFMLEProfileList.SelectedItem = Properties.Settings.Default.fmle_default_profile;





            // 表示
            mTotalCnt.Checked = Properties.Settings.Default.TotalCnt_view;
            mActiveCnt.Checked = Properties.Settings.Default.ActiveCnt_view;
            mUniqCnt.Checked = Properties.Settings.Default.UniqCnt_view;
            mCpuInfo.Checked = Properties.Settings.Default.CpuInfo_view;
            mBattery.Checked = Properties.Settings.Default.Battery_view;
            mUpLink.Checked = Properties.Settings.Default.UpLink_view;
            mWakumachi.Checked = Properties.Settings.Default.wakumachi_view;

            // 自動応答
            mEnableAutoRes.Checked = Properties.Settings.Default.auto_res;

            // 自動再接続
            mAutoReConnect.Checked = Properties.Settings.Default.auto_reconnect;

            // 開演待ちスキップ
            mSkip5min.Checked = Properties.Settings.Default.skip5min;

            cbLauncher.Checked = Properties.Settings.Default.use_launcher;

            LoadResponse();
            LoadLauncher();
        }

        //-------------------------------------------------------------------------
        // 設定ファイルセーブ
        //-------------------------------------------------------------------------
        private void SaveSettings()
        {
            // 一般
            Properties.Settings.Default.user_id = this.mMailBox.Text;
            Properties.Settings.Default.password = this.mPasswdBox.Text;
            Properties.Settings.Default.font = mFont;
            Properties.Settings.Default.auto_connect = this.mAutoConnect.Checked;
            Properties.Settings.Default.talk_3min = this.mTalk3Min.Checked;
            Properties.Settings.Default.save_log = this.mSaveLog.Checked;
            Properties.Settings.Default.auto_username = this.mAutoUsername.Checked;
            Properties.Settings.Default.tweet_wait = this.mWakuTweet.Checked;
            Properties.Settings.Default.talk_bat = this.mTalkBat.Checked;
            Properties.Settings.Default.need184 = this.mNeed184.Checked;
            Properties.Settings.Default.rest_batt = (int)this.mRestBatt.Value;
            Properties.Settings.Default.rest_time = (int)this.mRestTime.Value;
            Properties.Settings.Default.comment_max = (int)this.mCmtMax.Value;
            Properties.Settings.Default.title_auto_inc = this.mTitleInc.Checked;
            Properties.Settings.Default.ng_notice = mNGNotice.Checked;
            Properties.Settings.Default.send_keepalive = mKeepalive.Checked;
            Properties.Settings.Default.viewer_auto_boot = mViewerAutoBoot.Checked;
            Properties.Settings.Default.cont_waku_notice = this.mContWakuNotice.Checked;
            Properties.Settings.Default.reset_message = tbResetMessage.Text;

            Properties.Settings.Default.use_welcome_message = this.mUseWelcomeMessage.Checked;

            Properties.Settings.Default.use_flash_console = this.mUseNewConsole.Checked;
            Properties.Settings.Default.use_loss_time = this.mUseLossTime.Checked;
            Properties.Settings.Default.use_next_lv_notice = this.mUseNextLvNotice.Checked;

            Properties.Settings.Default.user_name_regex = this.mUserNameRegex.Text;

            Properties.Settings.Default.auto_nick_184 = this.mUseAutoNick184.Checked;
            Properties.Settings.Default.use_welcome_cruise_message = this.mUseCruiseWelcomeMessage.Checked;
            Properties.Settings.Default.welcome_message = this.mWelcomeMessage.Text;
            Properties.Settings.Default.welcome_message184 = this.mWelcomeMessage184.Text;
            Properties.Settings.Default.welcome_cruise_message = this.mWelcomeMessageCruise.Text;
            Properties.Settings.Default.use_flash_comment_generator = this.mUseFlashCommentGenerator.Checked;

            Properties.Settings.Default.use_comment_cut = this.mCommentCut.Checked;
            Properties.Settings.Default.comment_cut_len = (int)this.mCommentCutLen.Value;



            // クッキー
            Properties.Settings.Default.UseBrowserCookie = this.mUseBrowserCookie.Checked;
            Properties.Settings.Default.Browser = this.mBrowser.SelectedItem.ToString();

            // 棒読みちゃん
            Properties.Settings.Default.bouyomi_path = this.mBouyomiPath.Text;
            Properties.Settings.Default.bouyomi_port = int.Parse(this.mBouyomiPort.Text);
            Properties.Settings.Default.warp_cnt = (uint)this.mWarpCnt.Value;
            Properties.Settings.Default.need_premium = this.mNeedPremium.Checked;
            Properties.Settings.Default.speak_nick = this.mSpeakNick.Checked;
            Properties.Settings.Default.owner_comment = this.mOwnerComment.Checked;
            Properties.Settings.Default.slash_comment = this.mSlashComment.Checked;
            Properties.Settings.Default.hashamark_comment = this.mHashmarkComment.Checked;


            // 今ココ
            Properties.Settings.Default.launch_imk = this.mLaunchImk.Checked;
            Properties.Settings.Default.imk_path = this.mImkPath.Text;
            Properties.Settings.Default.imakoko_user = this.mImakokoUser.Text;
            Properties.Settings.Default.launch_bouyomi = this.mLaunchBouyomi.Checked;
            Properties.Settings.Default.imakoko_genzaichi = cbGenzaichi.Checked;
            Properties.Settings.Default.imakoko_speed = cbSpeed.Checked;
            Properties.Settings.Default.imakoko_genzaichi_auto_comment = cbAutoGenzaichi.Checked;
            Properties.Settings.Default.imakoko_genzaichi_auto_comment_int = (int)mAutoGenzaichiInt.Value;
            Properties.Settings.Default.imakoko_genzaichi_owner = cbImkOwner.Checked;
            Properties.Settings.Default.imakoko_genzaichi_hidden = cbImkHidden.Checked;
            Properties.Settings.Default.address_template = mAddressTemplate.Text;
            Properties.Settings.Default.address_template2 = mAddressTemplate2.Text;
            Properties.Settings.Default.speed_template = mSpeedTemplate.Text;
            Properties.Settings.Default.use_egm96 = cbEGM96.Checked;
            Properties.Settings.Default.geocode_address_url = mReverseGeocodingAPI.Text;

            // Twitter
            Properties.Settings.Default.tw_start = this.mTwStart.Text;
            Properties.Settings.Default.tw_end = this.mTwEnd.Text;
            Properties.Settings.Default.tw_start_enable = this.mTwStartEnable.Checked;
            Properties.Settings.Default.tw_end_enable = this.mTwEndEnable.Checked;
            Properties.Settings.Default.tw_hash = this.mTwHash.Text;

            // 色
            Properties.Settings.Default.back_color = this.mBackColor.BackColor;
            Properties.Settings.Default.text_color = this.mTextColor.BackColor;
            Properties.Settings.Default.owner_color = this.mOwnerColor.BackColor;
            Properties.Settings.Default.ng_color = this.mNGColor.BackColor;
            Properties.Settings.Default.mobile_color = this.mMobileColor.BackColor;

            // 外部配信
            Properties.Settings.Default.use_hq = this.mUseHQ.Checked;
            Properties.Settings.Default.use_fme = this.mUseFME.Checked;
            Properties.Settings.Default.use_xsplit = mUseXSplit.Checked;
            Properties.Settings.Default.use_nle = mUseNLE.Checked;

            Properties.Settings.Default.fmle_path = mFMLEPath.Text;
            Properties.Settings.Default.fme_dos = mFMEDOS.Checked;
            Properties.Settings.Default.fme_dos_min = mFMEDOSMIN.Checked;
            Properties.Settings.Default.fme_compact = mFMEcompact.Checked;
            Properties.Settings.Default.sekigaeMinutes = (int)mSekigaeMinutes.Value;
            Properties.Settings.Default.fme_gui = mFME_GUI.Checked;
            Properties.Settings.Default.fme_wait = (int)mFME_GUI_wait.Value;
            Properties.Settings.Default.show_fme_setting = m_show_fme_setting.Checked;
            Properties.Settings.Default.fme_session = mFMEsessions.Text;
            Properties.Settings.Default.fmle_profile_path = mFMLEProfilePath.Text;
            if (mFMLEProfileList.SelectedItem != null)
            {
                Properties.Settings.Default.fmle_default_profile = mFMLEProfileList.SelectedItem.ToString();
            }
            else
            {
                Properties.Settings.Default.fmle_default_profile = "";
            }
            if (Properties.Settings.Default.use_hq != mUseHQ.Checked)
            {
                FMLE.Stop();
            }

            Properties.Settings.Default.enable_xsplit_scene_change = mXSplitScene.Checked;
            Properties.Settings.Default.use_xsplit_shortcut = mXSplitShortcut.Checked;



            // 表示
            Properties.Settings.Default.TotalCnt_view = mTotalCnt.Checked;
            Properties.Settings.Default.ActiveCnt_view = mActiveCnt.Checked;
            Properties.Settings.Default.UniqCnt_view = mUniqCnt.Checked;
            Properties.Settings.Default.CpuInfo_view = mCpuInfo.Checked;
            Properties.Settings.Default.Battery_view = mBattery.Checked;
            Properties.Settings.Default.UpLink_view = mUpLink.Checked;
            Properties.Settings.Default.wakumachi_view = mWakumachi.Checked;

            // 自動応答
            Properties.Settings.Default.auto_res = mEnableAutoRes.Checked;

            // 自動再接続
            Properties.Settings.Default.auto_reconnect = mAutoReConnect.Checked;

            // 開演待ちスキップ
            Properties.Settings.Default.skip5min = mSkip5min.Checked;

            Properties.Settings.Default.use_launcher = cbLauncher.Checked;

            Response res = Response.Instance;
            SaveResponse();
            res.Reload();

            Launcher launcher = Launcher.Instance;
            SaveLauncher();
            launcher.Reload();

            Properties.Settings.Default.Save();
        }
        #region イベントハンドラ
        //-------------------------------------------------------------------------
        // 色選択ダイアログ表示
        //-------------------------------------------------------------------------
        private void ShowColorDialog(ref Label iLabel)
        {
            if (mColorDialog.ShowDialog() == DialogResult.OK)
            {
                iLabel.BackColor = mColorDialog.Color;
            }
        }
        //-------------------------------------------------------------------------
        // 色クリック
        //-------------------------------------------------------------------------
        private void Color_Click(object sender, EventArgs e)
        {
            Label label = sender as Label;
            ShowColorDialog(ref label);
        }


        #endregion

        private void AuthBtn_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.tw_token.Length == 0)
            {
                TwitterOAuthDialog dlg = new TwitterOAuthDialog();
                if (DialogResult.OK == dlg.ShowDialog())
                {
                    mAuthBtn.Text = "再認証";
                    mAuthResult.Text = "認証済み";
                    mAuthResult.BackColor = System.Drawing.Color.Transparent;
                    mTweetBtn.Enabled = true;
                }
            }
            else
            {
                Properties.Settings.Default.tw_token = "";
                Properties.Settings.Default.tw_token_secret = "";
                mAuthBtn.Text = "認証する";
                mAuthResult.Text = "未認証";
                mAuthResult.BackColor = System.Drawing.Color.Red;
                mTweetBtn.Enabled = false;
            }
        }

        private void mUITimer_Tick(object sender, EventArgs e)
        {
            this.mAuthResult.Text = (Properties.Settings.Default.tw_token.Length == 0) ? "未認証" : "認証済み";
            this.mAuthBtn.Text = (Properties.Settings.Default.tw_token.Length == 0) ? "認証する" : "認証解除";
        }

        //-------------------------------------------------------------------------
        // FMEパス変更
        //-------------------------------------------------------------------------
        private void mFMLEBtn_Click(object sender, EventArgs e)
        {
            if (mOpenFileDlg.ShowDialog() == DialogResult.OK)
            {
                mFMLEPath.Text = mOpenFileDlg.FileName;
            }
        }

        //-------------------------------------------------------------------------
        // 自動応答削除
        //-------------------------------------------------------------------------
        private void mDelRes_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in mResList.SelectedRows)
            {
                mResList.Rows.Remove(row);

            }
        }

        private void LoadResponse()
        {
            const string file = "response.xml";

            using (XmlTextReader xml = new XmlTextReader(file))
            {
                if (xml == null)
                {
                    Utils.WriteLog(file + "が見つかりません");
                    return;
                }


                string msg = "";
                string res = "";

                try
                {
                    while (xml.Read())
                    {
                        if (xml.NodeType == XmlNodeType.Element)
                        {
                            if (xml.LocalName.Equals("response"))
                            {
                                Int16 type = 0;
                                bool enabled = true;
                                for (int i = 0; i < xml.AttributeCount; i++)
                                {
                                    xml.MoveToAttribute(i);
                                    if (xml.Name == "message")
                                    {
                                        msg = xml.Value;
                                    }
                                    else
                                        if (xml.Name == "type")
                                        {
                                            Int16.TryParse(xml.Value, out type);
                                        }
                                        else
                                            if (xml.Name == "enabled")
                                            {
                                                enabled = xml.Value.ToString().Equals("True");
                                            }
                                }

                                res = xml.ReadString();

                                mResList.Rows.Add(enabled, msg, res);

                                //Utils.WriteLog( String.Format("res: {0}  msg: {1}", res, msg));
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Utils.WriteLog("LoadUser:" + e.Message);
                }
            }
        }

        private void SaveResponse()
        {
            using (XmlTextWriter writer = new XmlTextWriter("response.xml", null))
            {
                writer.Formatting = Formatting.Indented;

                writer.WriteStartDocument();

                writer.WriteStartElement("response_list");

                foreach (DataGridViewRow row in mResList.Rows)
                {
                    if (row.Cells[0].Value != null && row.Cells[1].Value != null)
                    {
                        string key = row.Cells[1].Value.ToString();

                        // 作成時刻書き込み
                        writer.WriteStartElement("response");
                        writer.WriteAttributeString("message", key);
                        writer.WriteAttributeString("type", "0");
                        writer.WriteAttributeString("enabled", row.Cells[0].Value.ToString());
                        writer.WriteString(row.Cells[2].Value.ToString());
                        writer.WriteEndElement();
                    }
                }

                writer.WriteEndElement();
                writer.WriteEndDocument();

                writer.Flush();
                writer.Close();
            }
        }

        private void LoadLauncher()
        {
            const string file = "Launcher.xml";

            using (XmlTextReader xml = new XmlTextReader(file))
            {
                if (xml == null)
                {
                    Utils.WriteLog(file + "が見つかりません");
                    return;
                }


                bool enabled = false;
                string name = "";
                string path = "";
                string args = "";

                try
                {
                    while (xml.Read())
                    {
                        if (xml.NodeType == XmlNodeType.Element)
                        {
                            if (xml.LocalName.Equals("command"))
                            {
                                for (int i = 0; i < xml.AttributeCount; i++)
                                {
                                    xml.MoveToAttribute(i);
                                    if (xml.Name == "enabled")
                                    {
                                        enabled = xml.Value.ToString().Equals("True");
                                    }
                                    else
                                        if (xml.Name == "name")
                                        {
                                            name = xml.Value;
                                        }
                                        else
                                            if (xml.Name == "path")
                                            {
                                                path = xml.Value;
                                            }
                                            else
                                                if (xml.Name == "args")
                                                {
                                                    args = xml.Value;
                                                }
                                }

                                mLauncherList.Rows.Add(enabled, name, path, args);


                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Utils.WriteLog("LoadLauncher:" + e.Message);
                }

            }
        }

        private void SaveLauncher()
        {
            using (XmlTextWriter writer = new XmlTextWriter("Launcher.xml", null))
            {
                writer.Formatting = Formatting.Indented;

                writer.WriteStartDocument();

                writer.WriteStartElement("Launcher");

                foreach (DataGridViewRow row in mLauncherList.Rows)
                {
                    if (row.Cells[0].Value != null && row.Cells[1].Value != null && row.Cells[2].Value != null)
                    {
                        //string key = row.Cells[1].Value.ToString();


                        // 作成時刻書き込み
                        writer.WriteStartElement("command");
                        writer.WriteAttributeString("enabled", row.Cells[0].Value.ToString());
                        writer.WriteAttributeString("name", row.Cells[1].Value.ToString());
                        writer.WriteAttributeString("path", row.Cells[2].Value.ToString());
                        writer.WriteAttributeString("args", row.Cells[3].Value == null ? "" : row.Cells[3].Value.ToString());
                        writer.WriteEndElement();
                    }
                }

                writer.WriteEndElement();
                writer.WriteEndDocument();

                writer.Flush();
                writer.Close();
            }
        }

        private void mFindBouyomi_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "BouyomiChan.exe|*.exe";
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                mBouyomiPath.Text = dlg.FileName;
            }
        }

        private void mLaunchBouyomi_CheckedChanged(object sender, EventArgs e)
        {
            mBouyomiPath.Enabled = mLaunchBouyomi.Checked;
            mFindBouyomi.Enabled = mLaunchBouyomi.Checked;
        }

        private void mFindImkBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "ImacocoNow.exe|*.exe";
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                mImkPath.Text = dlg.FileName;
            }
        }

        private void mLaunchImk_CheckedChanged(object sender, EventArgs e)
        {
            mImkPath.Enabled = mLaunchImk.Checked;
            mFindImk.Enabled = mLaunchImk.Checked;
        }

        private void mUseBrowserCookie_CheckedChanged(object sender, EventArgs e)
        {
            //mBrowser.Enabled = mUseBrowserCookie.Checked;
            //mMailBox.Enabled = !mUseBrowserCookie.Checked;
            //mPasswdBox.Enabled = !mUseBrowserCookie.Checked;
            //mLoginTest.Enabled = mUseBrowserCookie.Checked;
            mLoginResult.Text = "-";

        }

        private void mFMEsessionBtn_Click(object sender, EventArgs e)
        {
            if (mSessionFileDlg.ShowDialog() == DialogResult.OK)
            {
                mFMEsessions.Text = mSessionFileDlg.FileName;
            }
        }

        private void mFMEProfileBtm_Click(object sender, EventArgs e)
        {

            FolderBrowserDialog dialog = new FolderBrowserDialog();

            dialog.Description = "プロファイルのフォルダを指定してください。";
            dialog.RootFolder = Environment.SpecialFolder.Desktop;
            dialog.SelectedPath = System.Windows.Forms.Application.StartupPath;
            dialog.ShowNewFolderButton = true;

            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                mFMLEProfilePath.Text = dialog.SelectedPath;
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            ShowFMLEProfileList();
        }


        private void ShowFMLEProfileList()
        {
            string path = mFMLEProfilePath.Text;
            if (System.IO.Directory.Exists(path))
            {
                string[] xmls = System.IO.Directory.GetFiles(path);
                mFMLEProfileList.Items.Clear();
                foreach (string xml in xmls)
                {

                    mFMLEProfileList.Items.Add(System.IO.Path.GetFileName(xml));
                }
                mFMLEProfileList.SelectedIndex = 0;
            }
            else
            {
                mFMLEProfileList.Items.Clear();
                mFMLEProfileList.Text = "";
            }
        }



        private void mFontSettingBtn_Click(object sender, EventArgs e)
        {
            //FontDialogクラスのインスタンスを作成
            FontDialog fd = new FontDialog();

            //初期のフォントを設定
            fd.Font = Properties.Settings.Default.font;
            //初期の色を設定
            //fd.Color = TextBox1.ForeColor;
            //ユーザーが選択できるポイントサイズの最大値を設定する
            //fd.MaxSize = 15;
            //fd.MinSize = 10;
            //存在しないフォントやスタイルをユーザーが選択すると
            //エラーメッセージを表示する
            fd.FontMustExist = true;
            //横書きフォントだけを表示する
            fd.AllowVerticalFonts = true;
            //色を選択できるようにする
            fd.ShowColor = false;
            //取り消し線、下線、テキストの色などのオプションを指定可能にする
            //デフォルトがTrueのため必要はない
            fd.ShowEffects = false;

            //ダイアログを表示する
            if (fd.ShowDialog() != DialogResult.Cancel)
            {
                mFontLabel.Text = fd.Font.Name;
                mFont = fd.Font;
                Properties.Settings.Default.font = fd.Font;


            }
        }

        private void cbGenzaichi_CheckedChanged(object sender, EventArgs e)
        {
            mAddressTemplate.Enabled = cbGenzaichi.Checked;
        }

        private void cbSpeed_CheckedChanged(object sender, EventArgs e)
        {
            mSpeedTemplate.Enabled = cbSpeed.Checked;
        }

        private void cbAutoGenzaichi_CheckedChanged(object sender, EventArgs e)
        {
            mAddressTemplate2.Enabled = cbAutoGenzaichi.Enabled;
        }

        private void mFMEDOS_CheckedChanged(object sender, EventArgs e)
        {

            mFMEDOSMIN.Enabled = mFMEDOS.Checked;

        }

        private void mUseFME_CheckedChanged(object sender, EventArgs e)
        {
            if (mUseFME.Checked)
            {
                mUseXSplit.Checked = false;
                mUseNLE.Checked = false;
            }
        }

        private void mUseXSplit_CheckedChanged(object sender, EventArgs e)
        {
            if (mUseXSplit.Checked)
            {

                mUseFME.Checked = false;
                mUseNLE.Checked = false;
            }
        }

        private void mUseNLE_CheckedChanged(object sender, EventArgs e)
        {
            if (mUseNLE.Checked)
            {

                mUseFME.Checked = false;
                mUseXSplit.Checked = false;
            }
        }

        private void mTalk3Min_CheckedChanged(object sender, EventArgs e)
        {
            this.mContWakuNotice.Enabled = mTalk3Min.Enabled;
        }

        private void mResList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void mLauncherList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void mTabGeneral_Click(object sender, EventArgs e)
        {

        }

        private void btmLoasUserSession_Click(object sender, EventArgs e)
        {
            getUserSession();
        }

        private void getUserSession()
        {

            ICookieGetter[] cookieGetters = CookieGetter.CreateInstances(true);

            ICookieGetter s = null;
            foreach (ICookieGetter es in cookieGetters)
            {
                if (es.ToString().Equals(this.mBrowser.SelectedItem.ToString()))
                {
                    s = es;
                    break;
                }

            }
            System.Net.CookieCollection collection = s.GetCookieCollection(new Uri("http://live.nicovideo.jp/"));
            if (collection["user_session"] != null)
            {
                mUserSessionBox.Text = collection["user_session"].Value;
            }
            else
            {
                mUserSessionBox.Text = "";
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void mBrowser_SelectedIndexChanged(object sender, EventArgs e)
        {
            mLoginResult.Text = "-";
            getUserSession();
        }

        private void mLoginTest_Click(object sender, EventArgs e)
        {
            Thread th = new Thread(delegate()
            {
                this.Invoke((Action)delegate()
                {
                    this.mLoginResult.Text = "ログイン中…";
                });
                string msg = "ログイン失敗";
                if (Nico.Instance.Login(mMailBox.Text,mPasswdBox.Text))
                {
                    msg = "ログイン成功";
                }
                this.Invoke((Action)delegate()
                {
                    this.mLoginResult.Text = msg;
                });

            });
            th.Name = "SettingDialog.mLoginTest";
            th.Start();


        }

        private void button2_Click(object sender, EventArgs e)
        {
            Thread th = new Thread(delegate()
            {
                this.Invoke((Action)delegate()
                {
                    this.mUseNameTestResult.Text = "取得中…";
                });
                string msg = "取得失敗";
                if (!Nico.Instance.LoginTest(mUserSessionBox.Text))
                {
                    msg = "ログイン失敗";

                }
                else
                {
                    string name = Nico.Instance.GetUsername(mUserID.Text, mUserNameRegex.Text, mUserSessionBox.Text);
                    if (name.Length > 0)
                    {
                        msg = name;
                    }
                    else
                    {
                        msg = "取得失敗";
                    }
                }
                this.Invoke((Action)delegate()
                {
                    this.mUseNameTestResult.Text = "\"" + msg + "\"";
                });

            });
            th.Name = "SettingDialog.mLoginTest";
            th.Start();
        }

        private void cbImkOwner_CheckedChanged(object sender, EventArgs e)
        {
            cbImkHidden.Enabled = cbImkOwner.Checked;
            if (!cbImkOwner.Checked)
            {
                cbImkHidden.Checked = false;
            }

        }

        private void mTweetBtn_Click(object sender, EventArgs e)
        {
            System.Threading.Thread th = new System.Threading.Thread(delegate()
            {
                Utils.Tweet("ツイートテスト " + DateTime.Now);
                MessageBox.Show("ツイートテストを行いました自分のTLを確認して下さい");
            });
            th.Name = "ShortcutKey: tweet comment";
            th.Start();
        }
















    }
}
//-------------------------------------------------------------------------
// 初期設定ダイアログ
//
// Copyright (c) 金時豆(http://ch.nicovideo.jp/community/co48276)
//-------------------------------------------------------------------------