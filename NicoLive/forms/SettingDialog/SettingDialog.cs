//-------------------------------------------------------------------------
// �����ݒ�_�C�A���O
//
// Copyright (c) ������(http://ch.nicovideo.jp/community/co48276)
// $Id$
//-------------------------------------------------------------------------
using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Xml;
using System.Diagnostics;
using Hal.CookieGetterSharp;

//-------------------------------------------------------------------------
// �N���X����
//-------------------------------------------------------------------------
namespace NicoLive
{
	/// <summary>
	/// SettingDialog �̊T�v�̐����ł��B
	/// </summary>
	public class SettingDialog : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button mCancelBtn;
        private TabControl mTabPage8;
        private TabPage mTabPage1;
        private TabPage mTabPage2;
        private Button mSaveBtn;
        private Label mMailLabel;
        private TextBox mPasswdBox;
        private TextBox mMailBox;
        private Label mPassLabel;
        private Label mFontSizeLabel;
        private NumericUpDown mFontSize;
        private Label label1;
        private TextBox mBouyomiPort;
        private NumericUpDown mWarpCnt;
        private Label label2;
        private TabPage mTabPage3;
        private TextBox mImakokoUser;
        private Label label3;
        private CheckBox mAutoConnect;
        private TabPage mTabPage4;
        private Label label9;
        private TextBox mTwStart;
        private TextBox mTwEnd;
        private Label label10;
        private CheckBox mTalk3Min;
        private CheckBox mSaveLog;
        private CheckBox mAutoUsername;
        private CheckBox mTalkBat;
        private TabPage mTabPage5;
        private ColorDialog mColorDialog;
        private Label label12;
        private Label mTextColor;
        private Label label15;
        private Label mBackColor;
        private Label mMobileColor;
        private Label label19;
        private Label mNGColor;
        private Label label17;
        private Label mOwnerColor;
        private Label label14;
        private CheckBox mNeed184;
        private NumericUpDown mRestBatt;
        private NumericUpDown mRestTime;
        private CheckBox mUseFME;
        private Label label5;
        private Label mAuthResult;
        private Button mAuthBtn;
        private Timer mUITimer;
        private CheckBox mWakuTweet;
        private CheckBox mTwEndEnable;
        private CheckBox mTwStartEnable;
        private TabPage mTabPage6;
        private TextBox mFMLEPath;
        private Label label4;
        private Button mFMLEBtn;
        private OpenFileDialog mOpenFileDlg;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private TabPage mTabPage7;
        private DataGridView mResList;
        private Button mDelRes;
        private NumericUpDown mCmtMax;
        private Label label6;
        private CheckBox mFMEDOS;
        private CheckBox mEnableAutoRes;
        private DataGridViewCheckBoxColumn Column1;
        private DataGridViewTextBoxColumn mKeyWordCol;
        private DataGridViewTextBoxColumn mResCol;
        private CheckBox mNeedPremium;
        private TextBox mBouyomiPath;
        private Label label7;
        private CheckBox mLaunchBouyomi;
        private Button mFindBouyomi;
        private Button mFindImk;
        private TextBox mImkPath;
        private Label label8;
        private CheckBox mLaunchImk;
        private CheckBox mAutoReConnect;
        private CheckBox mSkip5min;
        private CheckBox cbGenzaichi;
        private GroupBox groupBox3;
        private NumericUpDown mFME_GUI_wait;
        private Label label11;
        private CheckBox mFME_GUI;
        private CheckBox cbSpeed;
        private NumericUpDown mSekigaeMinutes;
        private CheckBox mFMEcompact;
        private TabPage tabPage1;
        private GroupBox groupBox4;
        private CheckBox mUpLink;
        private CheckBox mBattery;
        private CheckBox mCpuInfo;
        private CheckBox mUniqCnt;
        private CheckBox mActiveCnt;
        private CheckBox mTotalCnt;
        private CheckBox m_show_fme_setting;
        private ComboBox mBrowser;
        private CheckBox mUseBrowserCookie;
        private CheckBox mTitleInc;
        private Button mFMEsessionBtn;
        private TextBox mFMEsessions;
        private Label label13;
        private OpenFileDialog mSessionFileDlg;
        private IContainer components;

		public SettingDialog()
		{
			//
			// Windows �t�H�[�� �f�U�C�i �T�|�[�g�ɕK�v�ł��B
			//
			InitializeComponent();

			//
			// TODO: InitializeComponent �Ăяo���̌�ɁA�R���X�g���N�^ �R�[�h��ǉ����Ă��������B
			//
		}

		/// <summary>
		/// �g�p����Ă��郊�\�[�X�Ɍ㏈�������s���܂��B
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows �t�H�[�� �f�U�C�i�Ő������ꂽ�R�[�h 
		/// <summary>
		/// �f�U�C�i �T�|�[�g�ɕK�v�ȃ��\�b�h�ł��B���̃��\�b�h�̓��e��
		/// �R�[�h �G�f�B�^�ŕύX���Ȃ��ł��������B
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            this.mCancelBtn = new System.Windows.Forms.Button();
            this.mTabPage8 = new System.Windows.Forms.TabControl();
            this.mTabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.mMailBox = new System.Windows.Forms.TextBox();
            this.mMailLabel = new System.Windows.Forms.Label();
            this.mBrowser = new System.Windows.Forms.ComboBox();
            this.mUseBrowserCookie = new System.Windows.Forms.CheckBox();
            this.mPassLabel = new System.Windows.Forms.Label();
            this.mPasswdBox = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.mTitleInc = new System.Windows.Forms.CheckBox();
            this.mRestTime = new System.Windows.Forms.NumericUpDown();
            this.mSekigaeMinutes = new System.Windows.Forms.NumericUpDown();
            this.mFontSize = new System.Windows.Forms.NumericUpDown();
            this.mFMEcompact = new System.Windows.Forms.CheckBox();
            this.mFontSizeLabel = new System.Windows.Forms.Label();
            this.mSkip5min = new System.Windows.Forms.CheckBox();
            this.mRestBatt = new System.Windows.Forms.NumericUpDown();
            this.mAutoConnect = new System.Windows.Forms.CheckBox();
            this.mTalk3Min = new System.Windows.Forms.CheckBox();
            this.mSaveLog = new System.Windows.Forms.CheckBox();
            this.mAutoUsername = new System.Windows.Forms.CheckBox();
            this.mUseFME = new System.Windows.Forms.CheckBox();
            this.mAutoReConnect = new System.Windows.Forms.CheckBox();
            this.mTalkBat = new System.Windows.Forms.CheckBox();
            this.mNeed184 = new System.Windows.Forms.CheckBox();
            this.mNeedPremium = new System.Windows.Forms.CheckBox();
            this.mCmtMax = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.mTabPage2 = new System.Windows.Forms.TabPage();
            this.mFindBouyomi = new System.Windows.Forms.Button();
            this.mBouyomiPath = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.mLaunchBouyomi = new System.Windows.Forms.CheckBox();
            this.mWarpCnt = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.mBouyomiPort = new System.Windows.Forms.TextBox();
            this.mTabPage3 = new System.Windows.Forms.TabPage();
            this.cbSpeed = new System.Windows.Forms.CheckBox();
            this.cbGenzaichi = new System.Windows.Forms.CheckBox();
            this.mFindImk = new System.Windows.Forms.Button();
            this.mImkPath = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.mLaunchImk = new System.Windows.Forms.CheckBox();
            this.mImakokoUser = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.mTabPage4 = new System.Windows.Forms.TabPage();
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
            this.mTabPage5 = new System.Windows.Forms.TabPage();
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
            this.mTabPage6 = new System.Windows.Forms.TabPage();
            this.m_show_fme_setting = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.mFME_GUI_wait = new System.Windows.Forms.NumericUpDown();
            this.label11 = new System.Windows.Forms.Label();
            this.mFME_GUI = new System.Windows.Forms.CheckBox();
            this.mFMEDOS = new System.Windows.Forms.CheckBox();
            this.mFMLEBtn = new System.Windows.Forms.Button();
            this.mFMLEPath = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.mTabPage7 = new System.Windows.Forms.TabPage();
            this.mEnableAutoRes = new System.Windows.Forms.CheckBox();
            this.mDelRes = new System.Windows.Forms.Button();
            this.mResList = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.mKeyWordCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mResCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.mUpLink = new System.Windows.Forms.CheckBox();
            this.mBattery = new System.Windows.Forms.CheckBox();
            this.mCpuInfo = new System.Windows.Forms.CheckBox();
            this.mUniqCnt = new System.Windows.Forms.CheckBox();
            this.mActiveCnt = new System.Windows.Forms.CheckBox();
            this.mTotalCnt = new System.Windows.Forms.CheckBox();
            this.mSaveBtn = new System.Windows.Forms.Button();
            this.mColorDialog = new System.Windows.Forms.ColorDialog();
            this.mUITimer = new System.Windows.Forms.Timer(this.components);
            this.mOpenFileDlg = new System.Windows.Forms.OpenFileDialog();
            this.mSessionFileDlg = new System.Windows.Forms.OpenFileDialog();
            this.mFMEsessionBtn = new System.Windows.Forms.Button();
            this.mFMEsessions = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.mTabPage8.SuspendLayout();
            this.mTabPage1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mRestTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mSekigaeMinutes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mFontSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mRestBatt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mCmtMax)).BeginInit();
            this.mTabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mWarpCnt)).BeginInit();
            this.mTabPage3.SuspendLayout();
            this.mTabPage4.SuspendLayout();
            this.mTabPage5.SuspendLayout();
            this.mTabPage6.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mFME_GUI_wait)).BeginInit();
            this.mTabPage7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mResList)).BeginInit();
            this.tabPage1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // mCancelBtn
            // 
            this.mCancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.mCancelBtn.Location = new System.Drawing.Point(310, 488);
            this.mCancelBtn.Name = "mCancelBtn";
            this.mCancelBtn.Size = new System.Drawing.Size(75, 23);
            this.mCancelBtn.TabIndex = 2;
            this.mCancelBtn.Text = "�L�����Z��";
            this.mCancelBtn.Click += new System.EventHandler(this.cancel_Click);
            // 
            // mTabPage8
            // 
            this.mTabPage8.Controls.Add(this.mTabPage1);
            this.mTabPage8.Controls.Add(this.mTabPage2);
            this.mTabPage8.Controls.Add(this.mTabPage3);
            this.mTabPage8.Controls.Add(this.mTabPage4);
            this.mTabPage8.Controls.Add(this.mTabPage5);
            this.mTabPage8.Controls.Add(this.mTabPage6);
            this.mTabPage8.Controls.Add(this.mTabPage7);
            this.mTabPage8.Controls.Add(this.tabPage1);
            this.mTabPage8.Location = new System.Drawing.Point(1, 1);
            this.mTabPage8.Name = "mTabPage8";
            this.mTabPage8.SelectedIndex = 0;
            this.mTabPage8.Size = new System.Drawing.Size(485, 481);
            this.mTabPage8.TabIndex = 0;
            // 
            // mTabPage1
            // 
            this.mTabPage1.Controls.Add(this.groupBox1);
            this.mTabPage1.Controls.Add(this.groupBox2);
            this.mTabPage1.Location = new System.Drawing.Point(4, 22);
            this.mTabPage1.Name = "mTabPage1";
            this.mTabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.mTabPage1.Size = new System.Drawing.Size(477, 455);
            this.mTabPage1.TabIndex = 0;
            this.mTabPage1.Text = "���";
            this.mTabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.mMailBox);
            this.groupBox1.Controls.Add(this.mMailLabel);
            this.groupBox1.Controls.Add(this.mBrowser);
            this.groupBox1.Controls.Add(this.mUseBrowserCookie);
            this.groupBox1.Controls.Add(this.mPassLabel);
            this.groupBox1.Controls.Add(this.mPasswdBox);
            this.groupBox1.Location = new System.Drawing.Point(7, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(461, 95);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "�j�R�����O�C�����";
            // 
            // mMailBox
            // 
            this.mMailBox.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.mMailBox.Location = new System.Drawing.Point(102, 12);
            this.mMailBox.Name = "mMailBox";
            this.mMailBox.Size = new System.Drawing.Size(229, 19);
            this.mMailBox.TabIndex = 2;
            // 
            // mMailLabel
            // 
            this.mMailLabel.AutoSize = true;
            this.mMailLabel.Location = new System.Drawing.Point(21, 15);
            this.mMailLabel.Name = "mMailLabel";
            this.mMailLabel.Size = new System.Drawing.Size(75, 12);
            this.mMailLabel.TabIndex = 1;
            this.mMailLabel.Text = "���[���A�h���X�F";
            // 
            // mBrowser
            // 
            this.mBrowser.FormattingEnabled = true;
            this.mBrowser.Location = new System.Drawing.Point(167, 62);
            this.mBrowser.Name = "mBrowser";
            this.mBrowser.Size = new System.Drawing.Size(163, 20);
            this.mBrowser.TabIndex = 5;
            // 
            // mUseBrowserCookie
            // 
            this.mUseBrowserCookie.AutoSize = true;
            this.mUseBrowserCookie.Location = new System.Drawing.Point(21, 66);
            this.mUseBrowserCookie.Name = "mUseBrowserCookie";
            this.mUseBrowserCookie.Size = new System.Drawing.Size(138, 16);
            this.mUseBrowserCookie.TabIndex = 4;
            this.mUseBrowserCookie.Text = "�u���E�U�̃N�b�L�[�𗘗p";
            this.mUseBrowserCookie.UseVisualStyleBackColor = true;
            this.mUseBrowserCookie.CheckedChanged += new System.EventHandler(this.mUseBrowserCookie_CheckedChanged);
            // 
            // mPassLabel
            // 
            this.mPassLabel.AutoSize = true;
            this.mPassLabel.Location = new System.Drawing.Point(38, 44);
            this.mPassLabel.Name = "mPassLabel";
            this.mPassLabel.Size = new System.Drawing.Size(58, 12);
            this.mPassLabel.TabIndex = 3;
            this.mPassLabel.Text = "�p�X���[�h�F";
            // 
            // mPasswdBox
            // 
            this.mPasswdBox.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.mPasswdBox.Location = new System.Drawing.Point(102, 37);
            this.mPasswdBox.Name = "mPasswdBox";
            this.mPasswdBox.PasswordChar = '*';
            this.mPasswdBox.Size = new System.Drawing.Size(229, 19);
            this.mPasswdBox.TabIndex = 4;
            this.mPasswdBox.UseSystemPasswordChar = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.mTitleInc);
            this.groupBox2.Controls.Add(this.mRestTime);
            this.groupBox2.Controls.Add(this.mSekigaeMinutes);
            this.groupBox2.Controls.Add(this.mFontSize);
            this.groupBox2.Controls.Add(this.mFMEcompact);
            this.groupBox2.Controls.Add(this.mFontSizeLabel);
            this.groupBox2.Controls.Add(this.mSkip5min);
            this.groupBox2.Controls.Add(this.mRestBatt);
            this.groupBox2.Controls.Add(this.mAutoConnect);
            this.groupBox2.Controls.Add(this.mTalk3Min);
            this.groupBox2.Controls.Add(this.mSaveLog);
            this.groupBox2.Controls.Add(this.mAutoUsername);
            this.groupBox2.Controls.Add(this.mUseFME);
            this.groupBox2.Controls.Add(this.mAutoReConnect);
            this.groupBox2.Controls.Add(this.mTalkBat);
            this.groupBox2.Controls.Add(this.mNeed184);
            this.groupBox2.Controls.Add(this.mNeedPremium);
            this.groupBox2.Controls.Add(this.mCmtMax);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Location = new System.Drawing.Point(9, 107);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(462, 337);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "���̑�";
            // 
            // mTitleInc
            // 
            this.mTitleInc.AutoSize = true;
            this.mTitleInc.Location = new System.Drawing.Point(99, 286);
            this.mTitleInc.Name = "mTitleInc";
            this.mTitleInc.Size = new System.Drawing.Size(177, 16);
            this.mTitleInc.TabIndex = 20;
            this.mTitleInc.Text = "�^�C�g�����ԍ������C���N�������g";
            this.mTitleInc.UseVisualStyleBackColor = true;
            // 
            // mRestTime
            // 
            this.mRestTime.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.mRestTime.Location = new System.Drawing.Point(190, 84);
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
            this.mSekigaeMinutes.Location = new System.Drawing.Point(198, 261);
            this.mSekigaeMinutes.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.mSekigaeMinutes.Minimum = new decimal(new int[] {
            5,
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
            // mFontSize
            // 
            this.mFontSize.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.mFontSize.Location = new System.Drawing.Point(99, 18);
            this.mFontSize.Name = "mFontSize";
            this.mFontSize.Size = new System.Drawing.Size(64, 19);
            this.mFontSize.TabIndex = 7;
            this.mFontSize.Value = new decimal(new int[] {
            11,
            0,
            0,
            0});
            // 
            // mFMEcompact
            // 
            this.mFMEcompact.AutoSize = true;
            this.mFMEcompact.Location = new System.Drawing.Point(100, 263);
            this.mFMEcompact.Name = "mFMEcompact";
            this.mFMEcompact.Size = new System.Drawing.Size(247, 16);
            this.mFMEcompact.TabIndex = 18;
            this.mFMEcompact.Text = "���O�΍�̂��߁A�@�@�@�@�@�@�����Ƃɋ��������[�h";
            this.mFMEcompact.UseVisualStyleBackColor = true;
            // 
            // mFontSizeLabel
            // 
            this.mFontSizeLabel.AutoSize = true;
            this.mFontSizeLabel.Location = new System.Drawing.Point(17, 20);
            this.mFontSizeLabel.Name = "mFontSizeLabel";
            this.mFontSizeLabel.Size = new System.Drawing.Size(73, 12);
            this.mFontSizeLabel.TabIndex = 6;
            this.mFontSizeLabel.Text = "�t�H���g�T�C�Y�F";
            // 
            // mSkip5min
            // 
            this.mSkip5min.AutoSize = true;
            this.mSkip5min.Location = new System.Drawing.Point(100, 43);
            this.mSkip5min.Name = "mSkip5min";
            this.mSkip5min.Size = new System.Drawing.Size(113, 16);
            this.mSkip5min.TabIndex = 17;
            this.mSkip5min.Text = "�J���҂����X�L�b�v";
            this.mSkip5min.UseVisualStyleBackColor = true;
            // 
            // mRestBatt
            // 
            this.mRestBatt.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.mRestBatt.Location = new System.Drawing.Point(200, 150);
            this.mRestBatt.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.mRestBatt.Name = "mRestBatt";
            this.mRestBatt.Size = new System.Drawing.Size(49, 19);
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
            this.mAutoConnect.Location = new System.Drawing.Point(100, 65);
            this.mAutoConnect.Name = "mAutoConnect";
            this.mAutoConnect.Size = new System.Drawing.Size(203, 16);
            this.mAutoConnect.TabIndex = 8;
            this.mAutoConnect.Text = "�u�z�M�J�n�v�{�^���������ŃN���b�N����";
            this.mAutoConnect.UseVisualStyleBackColor = true;
            // 
            // mTalk3Min
            // 
            this.mTalk3Min.AutoSize = true;
            this.mTalk3Min.Location = new System.Drawing.Point(100, 87);
            this.mTalk3Min.Name = "mTalk3Min";
            this.mTalk3Min.Size = new System.Drawing.Size(284, 16);
            this.mTalk3Min.TabIndex = 9;
            this.mTalk3Min.Text = "�z�M���Ԏc��@�@�@�@�@�@�@����_�ǂ݂����Œʒm����";
            this.mTalk3Min.UseVisualStyleBackColor = true;
            // 
            // mSaveLog
            // 
            this.mSaveLog.AutoSize = true;
            this.mSaveLog.Location = new System.Drawing.Point(100, 109);
            this.mSaveLog.Name = "mSaveLog";
            this.mSaveLog.Size = new System.Drawing.Size(171, 16);
            this.mSaveLog.TabIndex = 11;
            this.mSaveLog.Text = "�����ŃR�����g�̃��O��ۑ�����";
            this.mSaveLog.UseVisualStyleBackColor = true;
            // 
            // mAutoUsername
            // 
            this.mAutoUsername.AutoSize = true;
            this.mAutoUsername.Location = new System.Drawing.Point(100, 131);
            this.mAutoUsername.Name = "mAutoUsername";
            this.mAutoUsername.Size = new System.Drawing.Size(272, 16);
            this.mAutoUsername.TabIndex = 12;
            this.mAutoUsername.Text = "�u184�v���O���Ă��郆�[�U�[�̖��O�������Ŏ擾����";
            this.mAutoUsername.UseVisualStyleBackColor = true;
            // 
            // mUseFME
            // 
            this.mUseFME.AutoSize = true;
            this.mUseFME.Location = new System.Drawing.Point(100, 219);
            this.mUseFME.Name = "mUseFME";
            this.mUseFME.Size = new System.Drawing.Size(155, 16);
            this.mUseFME.TabIndex = 16;
            this.mUseFME.Text = "�O���G���R�[�_�[�𗘗p����";
            this.mUseFME.UseVisualStyleBackColor = true;
            // 
            // mAutoReConnect
            // 
            this.mAutoReConnect.AutoSize = true;
            this.mAutoReConnect.Location = new System.Drawing.Point(100, 241);
            this.mAutoReConnect.Name = "mAutoReConnect";
            this.mAutoReConnect.Size = new System.Drawing.Size(84, 16);
            this.mAutoReConnect.TabIndex = 3;
            this.mAutoReConnect.Text = "�����Đڑ�";
            this.mAutoReConnect.UseVisualStyleBackColor = true;
            // 
            // mTalkBat
            // 
            this.mTalkBat.AutoSize = true;
            this.mTalkBat.Location = new System.Drawing.Point(100, 153);
            this.mTalkBat.Name = "mTalkBat";
            this.mTalkBat.Size = new System.Drawing.Size(346, 16);
            this.mTalkBat.TabIndex = 13;
            this.mTalkBat.Text = "�o�b�e���[�c�ʂ��@�@�@�@�@�@�@%�ȉ��ɂȂ�����_�ǂ݂����Œʒm����";
            this.mTalkBat.UseVisualStyleBackColor = true;
            // 
            // mNeed184
            // 
            this.mNeed184.AutoSize = true;
            this.mNeed184.Location = new System.Drawing.Point(100, 175);
            this.mNeed184.Name = "mNeed184";
            this.mNeed184.Size = new System.Drawing.Size(170, 16);
            this.mNeed184.TabIndex = 15;
            this.mNeed184.Text = "�u184�v�t���̃R�����g�𖳎�����";
            this.mNeed184.UseVisualStyleBackColor = true;
            // 
            // mNeedPremium
            // 
            this.mNeedPremium.AutoSize = true;
            this.mNeedPremium.Location = new System.Drawing.Point(100, 197);
            this.mNeedPremium.Name = "mNeedPremium";
            this.mNeedPremium.Size = new System.Drawing.Size(211, 16);
            this.mNeedPremium.TabIndex = 2;
            this.mNeedPremium.Text = "�v���~�A������ȊO�̃R�����g�𖳎�����";
            this.mNeedPremium.UseVisualStyleBackColor = true;
            // 
            // mCmtMax
            // 
            this.mCmtMax.Location = new System.Drawing.Point(99, 308);
            this.mCmtMax.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.mCmtMax.Name = "mCmtMax";
            this.mCmtMax.Size = new System.Drawing.Size(64, 19);
            this.mCmtMax.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(17, 310);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 12);
            this.label6.TabIndex = 0;
            this.label6.Text = "�R�����g�\�����F";
            // 
            // mTabPage2
            // 
            this.mTabPage2.Controls.Add(this.mFindBouyomi);
            this.mTabPage2.Controls.Add(this.mBouyomiPath);
            this.mTabPage2.Controls.Add(this.label7);
            this.mTabPage2.Controls.Add(this.mLaunchBouyomi);
            this.mTabPage2.Controls.Add(this.mWarpCnt);
            this.mTabPage2.Controls.Add(this.label2);
            this.mTabPage2.Controls.Add(this.label1);
            this.mTabPage2.Controls.Add(this.mBouyomiPort);
            this.mTabPage2.Location = new System.Drawing.Point(4, 22);
            this.mTabPage2.Name = "mTabPage2";
            this.mTabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.mTabPage2.Size = new System.Drawing.Size(477, 455);
            this.mTabPage2.TabIndex = 1;
            this.mTabPage2.Text = "�_�ǂ݂����";
            this.mTabPage2.UseVisualStyleBackColor = true;
            // 
            // mFindBouyomi
            // 
            this.mFindBouyomi.Location = new System.Drawing.Point(380, 130);
            this.mFindBouyomi.Name = "mFindBouyomi";
            this.mFindBouyomi.Size = new System.Drawing.Size(75, 23);
            this.mFindBouyomi.TabIndex = 7;
            this.mFindBouyomi.Text = "�Q��";
            this.mFindBouyomi.UseVisualStyleBackColor = true;
            this.mFindBouyomi.Click += new System.EventHandler(this.mFindBouyomi_Click);
            // 
            // mBouyomiPath
            // 
            this.mBouyomiPath.Location = new System.Drawing.Point(151, 132);
            this.mBouyomiPath.Name = "mBouyomiPath";
            this.mBouyomiPath.Size = new System.Drawing.Size(223, 19);
            this.mBouyomiPath.TabIndex = 6;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(43, 136);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(102, 12);
            this.label7.TabIndex = 5;
            this.label7.Text = "�_�ǂ݂����̃p�X�F";
            // 
            // mLaunchBouyomi
            // 
            this.mLaunchBouyomi.AutoSize = true;
            this.mLaunchBouyomi.Location = new System.Drawing.Point(151, 95);
            this.mLaunchBouyomi.Name = "mLaunchBouyomi";
            this.mLaunchBouyomi.Size = new System.Drawing.Size(285, 16);
            this.mLaunchBouyomi.TabIndex = 4;
            this.mLaunchBouyomi.Text = "�����C�u�N���A�I�����ɖ_�ǂ݂������N���A�I������";
            this.mLaunchBouyomi.UseVisualStyleBackColor = true;
            this.mLaunchBouyomi.CheckedChanged += new System.EventHandler(this.mLaunchBouyomi_CheckedChanged);
            // 
            // mWarpCnt
            // 
            this.mWarpCnt.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.mWarpCnt.Location = new System.Drawing.Point(153, 55);
            this.mWarpCnt.Name = "mWarpCnt";
            this.mWarpCnt.Size = new System.Drawing.Size(63, 19);
            this.mWarpCnt.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(136, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "�R�����g���[�v����R�����g���F";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(73, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "�\�P�b�g�|�[�g�F";
            // 
            // mBouyomiPort
            // 
            this.mBouyomiPort.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.mBouyomiPort.Location = new System.Drawing.Point(153, 18);
            this.mBouyomiPort.Name = "mBouyomiPort";
            this.mBouyomiPort.Size = new System.Drawing.Size(63, 19);
            this.mBouyomiPort.TabIndex = 1;
            // 
            // mTabPage3
            // 
            this.mTabPage3.Controls.Add(this.cbSpeed);
            this.mTabPage3.Controls.Add(this.cbGenzaichi);
            this.mTabPage3.Controls.Add(this.mFindImk);
            this.mTabPage3.Controls.Add(this.mImkPath);
            this.mTabPage3.Controls.Add(this.label8);
            this.mTabPage3.Controls.Add(this.mLaunchImk);
            this.mTabPage3.Controls.Add(this.mImakokoUser);
            this.mTabPage3.Controls.Add(this.label3);
            this.mTabPage3.Location = new System.Drawing.Point(4, 22);
            this.mTabPage3.Name = "mTabPage3";
            this.mTabPage3.Size = new System.Drawing.Size(477, 455);
            this.mTabPage3.TabIndex = 2;
            this.mTabPage3.Text = "���R�R�Ȃ��I";
            this.mTabPage3.UseVisualStyleBackColor = true;
            // 
            // cbSpeed
            // 
            this.cbSpeed.AutoSize = true;
            this.cbSpeed.Location = new System.Drawing.Point(127, 74);
            this.cbSpeed.Name = "cbSpeed";
            this.cbSpeed.Size = new System.Drawing.Size(145, 16);
            this.cbSpeed.TabIndex = 12;
            this.cbSpeed.Text = "�u���x�́H�v���x�𓚂���";
            this.cbSpeed.UseVisualStyleBackColor = true;
            // 
            // cbGenzaichi
            // 
            this.cbGenzaichi.AutoSize = true;
            this.cbGenzaichi.Location = new System.Drawing.Point(127, 52);
            this.cbGenzaichi.Name = "cbGenzaichi";
            this.cbGenzaichi.Size = new System.Drawing.Size(220, 16);
            this.cbGenzaichi.TabIndex = 11;
            this.cbGenzaichi.Text = "�u���ݒn�́H�v�ɑ΂��Č��݈ʒu�𓚂���";
            this.cbGenzaichi.UseVisualStyleBackColor = true;
            // 
            // mFindImk
            // 
            this.mFindImk.Location = new System.Drawing.Point(356, 137);
            this.mFindImk.Name = "mFindImk";
            this.mFindImk.Size = new System.Drawing.Size(75, 23);
            this.mFindImk.TabIndex = 10;
            this.mFindImk.Text = "�Q��";
            this.mFindImk.UseVisualStyleBackColor = true;
            this.mFindImk.Click += new System.EventHandler(this.mFindImkBtn_Click);
            // 
            // mImkPath
            // 
            this.mImkPath.Location = new System.Drawing.Point(127, 139);
            this.mImkPath.Name = "mImkPath";
            this.mImkPath.Size = new System.Drawing.Size(223, 19);
            this.mImkPath.TabIndex = 9;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(19, 143);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(97, 12);
            this.label8.TabIndex = 8;
            this.label8.Text = "���R�R�Ȃ��I�̃p�X�F";
            // 
            // mLaunchImk
            // 
            this.mLaunchImk.AutoSize = true;
            this.mLaunchImk.Location = new System.Drawing.Point(127, 106);
            this.mLaunchImk.Name = "mLaunchImk";
            this.mLaunchImk.Size = new System.Drawing.Size(280, 16);
            this.mLaunchImk.TabIndex = 5;
            this.mLaunchImk.Text = "�����C�u�N���A�I�����ɍ��R�R�Ȃ��I���N���A�I������";
            this.mLaunchImk.UseVisualStyleBackColor = true;
            this.mLaunchImk.CheckedChanged += new System.EventHandler(this.mLaunchImk_CheckedChanged);
            // 
            // mImakokoUser
            // 
            this.mImakokoUser.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.mImakokoUser.Location = new System.Drawing.Point(127, 22);
            this.mImakokoUser.Name = "mImakokoUser";
            this.mImakokoUser.Size = new System.Drawing.Size(146, 19);
            this.mImakokoUser.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(58, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "���[�U�[�h�c�F";
            // 
            // mTabPage4
            // 
            this.mTabPage4.Controls.Add(this.mTwEndEnable);
            this.mTabPage4.Controls.Add(this.mTwStartEnable);
            this.mTabPage4.Controls.Add(this.mWakuTweet);
            this.mTabPage4.Controls.Add(this.label5);
            this.mTabPage4.Controls.Add(this.mAuthResult);
            this.mTabPage4.Controls.Add(this.mAuthBtn);
            this.mTabPage4.Controls.Add(this.label10);
            this.mTabPage4.Controls.Add(this.mTwEnd);
            this.mTabPage4.Controls.Add(this.label9);
            this.mTabPage4.Controls.Add(this.mTwStart);
            this.mTabPage4.Location = new System.Drawing.Point(4, 22);
            this.mTabPage4.Name = "mTabPage4";
            this.mTabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.mTabPage4.Size = new System.Drawing.Size(477, 455);
            this.mTabPage4.TabIndex = 3;
            this.mTabPage4.Text = "Twitter";
            this.mTabPage4.UseVisualStyleBackColor = true;
            // 
            // mTwEndEnable
            // 
            this.mTwEndEnable.AutoSize = true;
            this.mTwEndEnable.Location = new System.Drawing.Point(37, 148);
            this.mTwEndEnable.Name = "mTwEndEnable";
            this.mTwEndEnable.Size = new System.Drawing.Size(143, 16);
            this.mTwEndEnable.TabIndex = 5;
            this.mTwEndEnable.Text = "�z�M�I������Tweet����";
            this.mTwEndEnable.UseVisualStyleBackColor = true;
            // 
            // mTwStartEnable
            // 
            this.mTwStartEnable.AutoSize = true;
            this.mTwStartEnable.Location = new System.Drawing.Point(37, 55);
            this.mTwStartEnable.Name = "mTwStartEnable";
            this.mTwStartEnable.Size = new System.Drawing.Size(143, 16);
            this.mTwStartEnable.TabIndex = 3;
            this.mTwStartEnable.Text = "�z�M�J�n����Tweet����";
            this.mTwStartEnable.UseVisualStyleBackColor = true;
            // 
            // mWakuTweet
            // 
            this.mWakuTweet.AutoSize = true;
            this.mWakuTweet.Location = new System.Drawing.Point(37, 297);
            this.mWakuTweet.Name = "mWakuTweet";
            this.mWakuTweet.Size = new System.Drawing.Size(259, 16);
            this.mWakuTweet.TabIndex = 9;
            this.mWakuTweet.Text = "�g��菇�ԑ҂���100�l�ȉ��ɂȂ�����Tweet����";
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
            this.mAuthResult.Text = "���F��";
            // 
            // mAuthBtn
            // 
            this.mAuthBtn.Location = new System.Drawing.Point(150, 15);
            this.mAuthBtn.Name = "mAuthBtn";
            this.mAuthBtn.Size = new System.Drawing.Size(75, 23);
            this.mAuthBtn.TabIndex = 2;
            this.mAuthBtn.Text = "�F�؂���";
            this.mAuthBtn.UseVisualStyleBackColor = true;
            this.mAuthBtn.Click += new System.EventHandler(this.AuthBtn_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(149, 256);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(318, 12);
            this.label10.TabIndex = 8;
            this.label10.Text = "�i�� @URL�͕����t�q�k�ɁA@TITLE�͕����^�C�g���ɕϊ�����܂��j";
            // 
            // mTwEnd
            // 
            this.mTwEnd.AcceptsReturn = true;
            this.mTwEnd.Location = new System.Drawing.Point(150, 170);
            this.mTwEnd.Multiline = true;
            this.mTwEnd.Name = "mTwEnd";
            this.mTwEnd.Size = new System.Drawing.Size(281, 58);
            this.mTwEnd.TabIndex = 6;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(150, 235);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(248, 12);
            this.label9.TabIndex = 7;
            this.label9.Text = "�i�����b�Z�[�W���|�X�g�������Ȃ����͋�ɂ��Ă��������j";
            // 
            // mTwStart
            // 
            this.mTwStart.AcceptsReturn = true;
            this.mTwStart.Location = new System.Drawing.Point(150, 77);
            this.mTwStart.Multiline = true;
            this.mTwStart.Name = "mTwStart";
            this.mTwStart.Size = new System.Drawing.Size(281, 58);
            this.mTwStart.TabIndex = 4;
            // 
            // mTabPage5
            // 
            this.mTabPage5.Controls.Add(this.mOwnerColor);
            this.mTabPage5.Controls.Add(this.label14);
            this.mTabPage5.Controls.Add(this.mMobileColor);
            this.mTabPage5.Controls.Add(this.label19);
            this.mTabPage5.Controls.Add(this.mNGColor);
            this.mTabPage5.Controls.Add(this.label17);
            this.mTabPage5.Controls.Add(this.mTextColor);
            this.mTabPage5.Controls.Add(this.label15);
            this.mTabPage5.Controls.Add(this.mBackColor);
            this.mTabPage5.Controls.Add(this.label12);
            this.mTabPage5.Location = new System.Drawing.Point(4, 22);
            this.mTabPage5.Name = "mTabPage5";
            this.mTabPage5.Size = new System.Drawing.Size(477, 455);
            this.mTabPage5.TabIndex = 4;
            this.mTabPage5.Text = "�F";
            this.mTabPage5.UseVisualStyleBackColor = true;
            // 
            // mOwnerColor
            // 
            this.mOwnerColor.BackColor = System.Drawing.Color.Red;
            this.mOwnerColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mOwnerColor.Location = new System.Drawing.Point(106, 103);
            this.mOwnerColor.Name = "mOwnerColor";
            this.mOwnerColor.Size = new System.Drawing.Size(64, 18);
            this.mOwnerColor.TabIndex = 5;
            this.mOwnerColor.Click += new System.EventHandler(this.Color_Click);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(23, 106);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(80, 12);
            this.label14.TabIndex = 4;
            this.label14.Text = "�^�c�R�����g�F�F";
            // 
            // mMobileColor
            // 
            this.mMobileColor.BackColor = System.Drawing.Color.Blue;
            this.mMobileColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mMobileColor.Location = new System.Drawing.Point(106, 172);
            this.mMobileColor.Name = "mMobileColor";
            this.mMobileColor.Size = new System.Drawing.Size(64, 18);
            this.mMobileColor.TabIndex = 9;
            this.mMobileColor.Click += new System.EventHandler(this.Color_Click);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(41, 175);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(61, 12);
            this.label19.TabIndex = 8;
            this.label19.Text = "���o�C���F�F";
            // 
            // mNGColor
            // 
            this.mNGColor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.mNGColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mNGColor.Location = new System.Drawing.Point(106, 137);
            this.mNGColor.Name = "mNGColor";
            this.mNGColor.Size = new System.Drawing.Size(64, 18);
            this.mNGColor.TabIndex = 7;
            this.mNGColor.Click += new System.EventHandler(this.Color_Click);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(64, 140);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(39, 12);
            this.label17.TabIndex = 6;
            this.label17.Text = "NG�F�F";
            // 
            // mTextColor
            // 
            this.mTextColor.BackColor = System.Drawing.Color.Black;
            this.mTextColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mTextColor.Location = new System.Drawing.Point(106, 70);
            this.mTextColor.Name = "mTextColor";
            this.mTextColor.Size = new System.Drawing.Size(64, 18);
            this.mTextColor.TabIndex = 3;
            this.mTextColor.Click += new System.EventHandler(this.Color_Click);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(57, 73);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(47, 12);
            this.label15.TabIndex = 2;
            this.label15.Text = "�����F�F";
            // 
            // mBackColor
            // 
            this.mBackColor.BackColor = System.Drawing.Color.White;
            this.mBackColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mBackColor.Location = new System.Drawing.Point(106, 35);
            this.mBackColor.Name = "mBackColor";
            this.mBackColor.Size = new System.Drawing.Size(64, 18);
            this.mBackColor.TabIndex = 1;
            this.mBackColor.Click += new System.EventHandler(this.Color_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(57, 38);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(47, 12);
            this.label12.TabIndex = 0;
            this.label12.Text = "�w�i�F�F";
            // 
            // mTabPage6
            // 
            this.mTabPage6.Controls.Add(this.mFMEsessionBtn);
            this.mTabPage6.Controls.Add(this.mFMEsessions);
            this.mTabPage6.Controls.Add(this.label13);
            this.mTabPage6.Controls.Add(this.m_show_fme_setting);
            this.mTabPage6.Controls.Add(this.groupBox3);
            this.mTabPage6.Controls.Add(this.mFMEDOS);
            this.mTabPage6.Controls.Add(this.mFMLEBtn);
            this.mTabPage6.Controls.Add(this.mFMLEPath);
            this.mTabPage6.Controls.Add(this.label4);
            this.mTabPage6.Location = new System.Drawing.Point(4, 22);
            this.mTabPage6.Name = "mTabPage6";
            this.mTabPage6.Padding = new System.Windows.Forms.Padding(3);
            this.mTabPage6.Size = new System.Drawing.Size(477, 455);
            this.mTabPage6.TabIndex = 5;
            this.mTabPage6.Text = "FME";
            this.mTabPage6.UseVisualStyleBackColor = true;
            // 
            // m_show_fme_setting
            // 
            this.m_show_fme_setting.AutoSize = true;
            this.m_show_fme_setting.Location = new System.Drawing.Point(106, 103);
            this.m_show_fme_setting.Name = "m_show_fme_setting";
            this.m_show_fme_setting.Size = new System.Drawing.Size(200, 16);
            this.m_show_fme_setting.TabIndex = 7;
            this.m_show_fme_setting.Text = "FME�z�M�̐ݒ���R�����g�Ō��J����";
            this.m_show_fme_setting.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.mFME_GUI_wait);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.mFME_GUI);
            this.groupBox3.Location = new System.Drawing.Point(89, 181);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(292, 100);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "FME��GUI���[�h�ݒ�";
            // 
            // mFME_GUI_wait
            // 
            this.mFME_GUI_wait.Location = new System.Drawing.Point(88, 62);
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
            this.label11.Location = new System.Drawing.Point(16, 64);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(129, 12);
            this.label11.TabIndex = 1;
            this.label11.Text = "GUI�N���҂��@�@�@�@�@�@�b";
            // 
            // mFME_GUI
            // 
            this.mFME_GUI.AutoSize = true;
            this.mFME_GUI.Location = new System.Drawing.Point(17, 31);
            this.mFME_GUI.Name = "mFME_GUI";
            this.mFME_GUI.Size = new System.Drawing.Size(156, 16);
            this.mFME_GUI.TabIndex = 0;
            this.mFME_GUI.Text = "FME��GUI���[�h�Ŏg�p����";
            this.mFME_GUI.UseVisualStyleBackColor = true;
            // 
            // mFMEDOS
            // 
            this.mFMEDOS.AutoSize = true;
            this.mFMEDOS.Location = new System.Drawing.Point(106, 80);
            this.mFMEDOS.Name = "mFMEDOS";
            this.mFMEDOS.Size = new System.Drawing.Size(143, 16);
            this.mFMEDOS.TabIndex = 3;
            this.mFMEDOS.Text = "DOS�v�����v�g��\������";
            this.mFMEDOS.UseVisualStyleBackColor = true;
            // 
            // mFMLEBtn
            // 
            this.mFMLEBtn.Location = new System.Drawing.Point(358, 30);
            this.mFMLEBtn.Name = "mFMLEBtn";
            this.mFMLEBtn.Size = new System.Drawing.Size(75, 23);
            this.mFMLEBtn.TabIndex = 2;
            this.mFMLEBtn.Text = "�Q��";
            this.mFMLEBtn.UseVisualStyleBackColor = true;
            this.mFMLEBtn.Click += new System.EventHandler(this.mFMLEBtn_Click);
            // 
            // mFMLEPath
            // 
            this.mFMLEPath.Location = new System.Drawing.Point(106, 32);
            this.mFMLEPath.Name = "mFMLEPath";
            this.mFMLEPath.Size = new System.Drawing.Size(246, 19);
            this.mFMLEPath.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(17, 35);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(92, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "FMLECmd�̃p�X�F";
            // 
            // mTabPage7
            // 
            this.mTabPage7.Controls.Add(this.mEnableAutoRes);
            this.mTabPage7.Controls.Add(this.mDelRes);
            this.mTabPage7.Controls.Add(this.mResList);
            this.mTabPage7.Location = new System.Drawing.Point(4, 22);
            this.mTabPage7.Name = "mTabPage7";
            this.mTabPage7.Padding = new System.Windows.Forms.Padding(3);
            this.mTabPage7.Size = new System.Drawing.Size(477, 455);
            this.mTabPage7.TabIndex = 6;
            this.mTabPage7.Text = "��������";
            this.mTabPage7.UseVisualStyleBackColor = true;
            // 
            // mEnableAutoRes
            // 
            this.mEnableAutoRes.AutoSize = true;
            this.mEnableAutoRes.Location = new System.Drawing.Point(20, 13);
            this.mEnableAutoRes.Name = "mEnableAutoRes";
            this.mEnableAutoRes.Size = new System.Drawing.Size(133, 16);
            this.mEnableAutoRes.TabIndex = 4;
            this.mEnableAutoRes.Text = "����������L���ɂ���";
            this.mEnableAutoRes.UseVisualStyleBackColor = true;
            // 
            // mDelRes
            // 
            this.mDelRes.Location = new System.Drawing.Point(381, 316);
            this.mDelRes.Name = "mDelRes";
            this.mDelRes.Size = new System.Drawing.Size(75, 23);
            this.mDelRes.TabIndex = 3;
            this.mDelRes.Text = "�폜";
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
            // 
            // Column1
            // 
            this.Column1.HeaderText = "�L��";
            this.Column1.Name = "Column1";
            this.Column1.Width = 50;
            // 
            // mKeyWordCol
            // 
            this.mKeyWordCol.HeaderText = "�L�[���[�h";
            this.mKeyWordCol.Name = "mKeyWordCol";
            this.mKeyWordCol.Width = 200;
            // 
            // mResCol
            // 
            this.mResCol.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.mResCol.HeaderText = "�ԓ�";
            this.mResCol.Name = "mResCol";
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox4);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(477, 455);
            this.tabPage1.TabIndex = 7;
            this.tabPage1.Text = "�\��";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.mUpLink);
            this.groupBox4.Controls.Add(this.mBattery);
            this.groupBox4.Controls.Add(this.mCpuInfo);
            this.groupBox4.Controls.Add(this.mUniqCnt);
            this.groupBox4.Controls.Add(this.mActiveCnt);
            this.groupBox4.Controls.Add(this.mTotalCnt);
            this.groupBox4.Location = new System.Drawing.Point(7, 22);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(460, 165);
            this.groupBox4.TabIndex = 0;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "�X�e�[�^�X�o�[";
            // 
            // mUpLink
            // 
            this.mUpLink.AutoSize = true;
            this.mUpLink.Location = new System.Drawing.Point(7, 134);
            this.mUpLink.Name = "mUpLink";
            this.mUpLink.Size = new System.Drawing.Size(68, 16);
            this.mUpLink.TabIndex = 5;
            this.mUpLink.Text = "���ш�";
            this.mUpLink.UseVisualStyleBackColor = true;
            // 
            // mBattery
            // 
            this.mBattery.AutoSize = true;
            this.mBattery.Location = new System.Drawing.Point(7, 111);
            this.mBattery.Name = "mBattery";
            this.mBattery.Size = new System.Drawing.Size(67, 16);
            this.mBattery.TabIndex = 4;
            this.mBattery.Text = "�o�b�e���[";
            this.mBattery.UseVisualStyleBackColor = true;
            // 
            // mCpuInfo
            // 
            this.mCpuInfo.AutoSize = true;
            this.mCpuInfo.Location = new System.Drawing.Point(7, 88);
            this.mCpuInfo.Name = "mCpuInfo";
            this.mCpuInfo.Size = new System.Drawing.Size(71, 16);
            this.mCpuInfo.TabIndex = 3;
            this.mCpuInfo.Text = "CPU����";
            this.mCpuInfo.UseVisualStyleBackColor = true;
            // 
            // mUniqCnt
            // 
            this.mUniqCnt.AutoSize = true;
            this.mUniqCnt.Location = new System.Drawing.Point(7, 65);
            this.mUniqCnt.Name = "mUniqCnt";
            this.mUniqCnt.Size = new System.Drawing.Size(72, 16);
            this.mUniqCnt.TabIndex = 2;
            this.mUniqCnt.Text = "�g�[�^����";
            this.mUniqCnt.UseVisualStyleBackColor = true;
            // 
            // mActiveCnt
            // 
            this.mActiveCnt.AutoSize = true;
            this.mActiveCnt.Location = new System.Drawing.Point(7, 42);
            this.mActiveCnt.Name = "mActiveCnt";
            this.mActiveCnt.Size = new System.Drawing.Size(78, 16);
            this.mActiveCnt.TabIndex = 1;
            this.mActiveCnt.Text = "�A�N�e�B�u��";
            this.mActiveCnt.UseVisualStyleBackColor = true;
            // 
            // mTotalCnt
            // 
            this.mTotalCnt.AutoSize = true;
            this.mTotalCnt.Location = new System.Drawing.Point(7, 19);
            this.mTotalCnt.Name = "mTotalCnt";
            this.mTotalCnt.Size = new System.Drawing.Size(72, 16);
            this.mTotalCnt.TabIndex = 0;
            this.mTotalCnt.Text = "����Ґ�";
            this.mTotalCnt.UseVisualStyleBackColor = true;
            // 
            // mSaveBtn
            // 
            this.mSaveBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.mSaveBtn.Location = new System.Drawing.Point(400, 488);
            this.mSaveBtn.Name = "mSaveBtn";
            this.mSaveBtn.Size = new System.Drawing.Size(75, 23);
            this.mSaveBtn.TabIndex = 1;
            this.mSaveBtn.Text = "�ۑ�";
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
            this.mSessionFileDlg.FileName = "mSessionFileDlg";
            this.mSessionFileDlg.Filter = "fmesessions.dat|*.dat";
            this.mSessionFileDlg.Title = "fmesessions.dat";
            // 
            // mFMEsessionBtn
            // 
            this.mFMEsessionBtn.Location = new System.Drawing.Point(358, 320);
            this.mFMEsessionBtn.Name = "mFMEsessionBtn";
            this.mFMEsessionBtn.Size = new System.Drawing.Size(75, 23);
            this.mFMEsessionBtn.TabIndex = 11;
            this.mFMEsessionBtn.Text = "�Q��";
            this.mFMEsessionBtn.UseVisualStyleBackColor = true;
            this.mFMEsessionBtn.Click += new System.EventHandler(this.mFMEsessionBtn_Click);
            // 
            // mFMEsessions
            // 
            this.mFMEsessions.Location = new System.Drawing.Point(106, 320);
            this.mFMEsessions.Name = "mFMEsessions";
            this.mFMEsessions.Size = new System.Drawing.Size(246, 19);
            this.mFMEsessions.TabIndex = 10;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(17, 305);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(122, 12);
            this.label13.TabIndex = 9;
            this.label13.Text = "fmesessions.dat�̃p�X�F";
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
            this.Name = "SettingDialog";
            this.ShowInTaskbar = false;
            this.Text = "�����ݒ�";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.SettingDialog_Load);
            this.mTabPage8.ResumeLayout(false);
            this.mTabPage1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mRestTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mSekigaeMinutes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mFontSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mRestBatt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mCmtMax)).EndInit();
            this.mTabPage2.ResumeLayout(false);
            this.mTabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mWarpCnt)).EndInit();
            this.mTabPage3.ResumeLayout(false);
            this.mTabPage3.PerformLayout();
            this.mTabPage4.ResumeLayout(false);
            this.mTabPage4.PerformLayout();
            this.mTabPage5.ResumeLayout(false);
            this.mTabPage5.PerformLayout();
            this.mTabPage6.ResumeLayout(false);
            this.mTabPage6.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mFME_GUI_wait)).EndInit();
            this.mTabPage7.ResumeLayout(false);
            this.mTabPage7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mResList)).EndInit();
            this.tabPage1.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

		}
		#endregion

        //-------------------------------------------------------------------------
        // �L�����Z��
        //-------------------------------------------------------------------------
        private void cancel_Click(object sender, EventArgs e)
        {
			//this.DialogResult = DialogResult.OK;
			this.Close();
		}

        //-------------------------------------------------------------------------
        // �ۑ�
        //-------------------------------------------------------------------------
        private void ok_Click(object sender, EventArgs e)
        {
            if (this.mMailBox.Text.Length <= 0)
            {
                MessageBox.Show("���[���A�h���X��ݒ肵�Ă�������");
                return;
            }
            if (this.mPasswdBox.Text.Length <= 0)
            {
                MessageBox.Show("�p�X���[�h��ݒ肵�Ă�������");
                return;
            }
            this.SaveSettings();

            // �\���̍X�V
            (Owner as Form1).Invoke((Action)delegate()
            {
                (Owner as Form1).UpdateStatusVisibility();
            });

            this.Close();
        }

        //-------------------------------------------------------------------------
        // �쐬��
        //-------------------------------------------------------------------------
        private void SettingDialog_Load(object sender, EventArgs e)
        {
            this.LoadSettings();

            // �Z���^�����O
            this.Top = (Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2;
            this.Left = (Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2;
        }

        //-------------------------------------------------------------------------
        // �ݒ�t�@�C�����[�h
        //-------------------------------------------------------------------------
        private void LoadSettings()
        {
            // ���
            this.mMailBox.Text = Properties.Settings.Default.user_id;
            this.mPasswdBox.Text = Properties.Settings.Default.password;
            this.mFontSize.Value = Properties.Settings.Default.font_size;
            this.mAutoConnect.Checked = Properties.Settings.Default.auto_connect;
            this.mTalk3Min.Checked = Properties.Settings.Default.talk_3min;
            this.mSaveLog.Checked = Properties.Settings.Default.save_log;
            this.mAutoUsername.Checked = Properties.Settings.Default.auto_username;
            this.mWakuTweet.Checked = Properties.Settings.Default.tweet_wait;
            this.mTalkBat.Checked = Properties.Settings.Default.talk_bat;
            this.mNeed184.Checked = Properties.Settings.Default.need184;
            this.mRestBatt.Value = Properties.Settings.Default.rest_batt;
            this.mRestTime.Value = Properties.Settings.Default.rest_time;
            this.mUseFME.Checked = Properties.Settings.Default.use_fme;
            this.mCmtMax.Value = Properties.Settings.Default.comment_max;
            this.mNeedPremium.Checked = Properties.Settings.Default.need_premium;
            this.mTitleInc.Checked = Properties.Settings.Default.title_auto_inc;

            // �N�b�L�[
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
            mBrowser.Enabled = mUseBrowserCookie.Checked;
            mMailBox.Enabled = !mUseBrowserCookie.Checked;
            mPasswdBox.Enabled = !mUseBrowserCookie.Checked;

            // �_�ǂ݂����
            this.mBouyomiPort.Text = Properties.Settings.Default.bouyomi_port.ToString();
            this.mWarpCnt.Value = Properties.Settings.Default.warp_cnt;
            this.mLaunchBouyomi.Checked = Properties.Settings.Default.launch_bouyomi;
            this.mBouyomiPath.Text = Properties.Settings.Default.bouyomi_path;
            mBouyomiPath.Enabled = mLaunchBouyomi.Checked;
            mFindBouyomi.Enabled = mLaunchBouyomi.Checked;
            //this.mCmtInterval.Text = Properties.Settings.Default.comment_interval.ToString();

            // ���R�R
            this.mImakokoUser.Text = Properties.Settings.Default.imakoko_user;
            this.mLaunchImk.Checked = Properties.Settings.Default.launch_imk;
            this.mImkPath.Text = Properties.Settings.Default.imk_path;
            mImkPath.Enabled = mLaunchImk.Checked;
            mFindImk.Enabled = mLaunchImk.Checked;
            cbGenzaichi.Checked = Properties.Settings.Default.imakoko_genzaichi;
            cbSpeed.Checked=Properties.Settings.Default.imakoko_speed;

            // Twitter
            this.mTwStart.Text = Properties.Settings.Default.tw_start;
            this.mTwStartEnable.Checked = Properties.Settings.Default.tw_start_enable;
            this.mTwEnd.Text = Properties.Settings.Default.tw_end;
            this.mTwEndEnable.Checked = Properties.Settings.Default.tw_end_enable;
            this.mAuthResult.Text = (Properties.Settings.Default.tw_token.Length == 0) ? "���F��" : "�F�؍ς�";

            // �F
            this.mBackColor.BackColor = Properties.Settings.Default.back_color;
            this.mTextColor.BackColor = Properties.Settings.Default.text_color;
            this.mOwnerColor.BackColor = Properties.Settings.Default.owner_color;
            this.mNGColor.BackColor = Properties.Settings.Default.ng_color;
            this.mMobileColor.BackColor = Properties.Settings.Default.mobile_color;

            // FME
            mFMLEPath.Text = Properties.Settings.Default.fmle_path;
            mFMEDOS.Checked = Properties.Settings.Default.fme_dos;
            mFMEcompact.Checked = Properties.Settings.Default.fme_compact;
            mSekigaeMinutes.Value = Properties.Settings.Default.sekigaeMinutes;
            mFME_GUI.Checked = Properties.Settings.Default.fme_gui;
            mFME_GUI_wait.Value = Properties.Settings.Default.fme_wait;
            m_show_fme_setting.Checked = Properties.Settings.Default.show_fme_setting;
            mFMEsessions.Text = Properties.Settings.Default.fme_session;

            // �\��
            mTotalCnt.Checked = Properties.Settings.Default.TotalCnt_view;
            mActiveCnt.Checked = Properties.Settings.Default.ActiveCnt_view;
            mUniqCnt.Checked = Properties.Settings.Default.UniqCnt_view;
            mCpuInfo.Checked = Properties.Settings.Default.CpuInfo_view;
            mBattery.Checked = Properties.Settings.Default.Battery_view;
            mUpLink.Checked = Properties.Settings.Default.UpLink_view;

            // ��������
            mEnableAutoRes.Checked = Properties.Settings.Default.auto_res;

            // �����Đڑ�
            mAutoReConnect.Checked = Properties.Settings.Default.auto_reconnect;

            // �J���҂��X�L�b�v
            mSkip5min.Checked = Properties.Settings.Default.skip5min;

            LoadResponse();
        }

        //-------------------------------------------------------------------------
        // �ݒ�t�@�C���Z�[�u
        //-------------------------------------------------------------------------
        private void SaveSettings()
        {
            // ���
            Properties.Settings.Default.user_id = this.mMailBox.Text;
            Properties.Settings.Default.password = this.mPasswdBox.Text;
            Properties.Settings.Default.font_size = (int)this.mFontSize.Value;
            Properties.Settings.Default.auto_connect = this.mAutoConnect.Checked;
            Properties.Settings.Default.talk_3min = this.mTalk3Min.Checked;
            Properties.Settings.Default.save_log = this.mSaveLog.Checked;
            Properties.Settings.Default.auto_username = this.mAutoUsername.Checked;
            Properties.Settings.Default.tweet_wait = this.mWakuTweet.Checked;
            Properties.Settings.Default.talk_bat = this.mTalkBat.Checked;
            Properties.Settings.Default.need184 = this.mNeed184.Checked;
            Properties.Settings.Default.rest_batt = (int)this.mRestBatt.Value;
            Properties.Settings.Default.rest_time = (int)this.mRestTime.Value;
            Properties.Settings.Default.use_fme = this.mUseFME.Checked;
            Properties.Settings.Default.comment_max = (int)this.mCmtMax.Value;
            Properties.Settings.Default.need_premium = this.mNeedPremium.Checked;
            Properties.Settings.Default.title_auto_inc = this.mTitleInc.Checked;

            // �N�b�L�[
            Properties.Settings.Default.UseBrowserCookie = this.mUseBrowserCookie.Checked;
            Properties.Settings.Default.Browser = this.mBrowser.SelectedItem.ToString();

            // �_�ǂ݂����
            Properties.Settings.Default.bouyomi_port= int.Parse(this.mBouyomiPort.Text);
            Properties.Settings.Default.warp_cnt = (uint)this.mWarpCnt.Value;
            Properties.Settings.Default.launch_imk = this.mLaunchImk.Checked;
            Properties.Settings.Default.imk_path = this.mImkPath.Text;
            //Properties.Settings.Default.comment_interval = float.Parse( this.mCmtInterval.Text );

            // ���R�R
            Properties.Settings.Default.imakoko_user = this.mImakokoUser.Text;
            Properties.Settings.Default.launch_bouyomi = this.mLaunchBouyomi.Checked;
            Properties.Settings.Default.bouyomi_path = this.mBouyomiPath.Text;
            Properties.Settings.Default.imakoko_genzaichi = cbGenzaichi.Checked;
            Properties.Settings.Default.imakoko_speed = cbSpeed.Checked;

            // Twitter
            Properties.Settings.Default.tw_start = this.mTwStart.Text;
            Properties.Settings.Default.tw_end = this.mTwEnd.Text;
            Properties.Settings.Default.tw_start_enable = this.mTwStartEnable.Checked;
            Properties.Settings.Default.tw_end_enable = this.mTwEndEnable.Checked;

            // �F
            Properties.Settings.Default.back_color = this.mBackColor.BackColor;
            Properties.Settings.Default.text_color = this.mTextColor.BackColor;
            Properties.Settings.Default.owner_color = this.mOwnerColor.BackColor;
            Properties.Settings.Default.ng_color = this.mNGColor.BackColor;
            Properties.Settings.Default.mobile_color = this.mMobileColor.BackColor;

            // FME
            Properties.Settings.Default.fmle_path = mFMLEPath.Text;
            Properties.Settings.Default.fme_dos = mFMEDOS.Checked;
            Properties.Settings.Default.fme_compact = mFMEcompact.Checked;
            Properties.Settings.Default.sekigaeMinutes = (int)mSekigaeMinutes.Value;
            Properties.Settings.Default.fme_gui = mFME_GUI.Checked;
            Properties.Settings.Default.fme_wait = (int)mFME_GUI_wait.Value;
            Properties.Settings.Default.show_fme_setting = m_show_fme_setting.Checked;
            Properties.Settings.Default.fme_session = mFMEsessions.Text;

            // �\��
            Properties.Settings.Default.TotalCnt_view = mTotalCnt.Checked;
            Properties.Settings.Default.ActiveCnt_view = mActiveCnt.Checked;
            Properties.Settings.Default.UniqCnt_view = mUniqCnt.Checked;
            Properties.Settings.Default.CpuInfo_view = mCpuInfo.Checked;
            Properties.Settings.Default.Battery_view = mBattery.Checked;
            Properties.Settings.Default.UpLink_view = mUpLink.Checked;

            // ��������
            Properties.Settings.Default.auto_res = mEnableAutoRes.Checked;

            // �����Đڑ�
             Properties.Settings.Default.auto_reconnect = mAutoReConnect.Checked;

            // �J���҂��X�L�b�v
            Properties.Settings.Default.skip5min = mSkip5min.Checked;

            Response res = Response.Instance;
            SaveResponse();
            res.Reload();

            Properties.Settings.Default.Save();
        }
        #region �C�x���g�n���h��
        //-------------------------------------------------------------------------
        // �F�I���_�C�A���O�\��
        //-------------------------------------------------------------------------
        private void ShowColorDialog(ref Label iLabel)
        {
            if (mColorDialog.ShowDialog() == DialogResult.OK)
            {
                iLabel.BackColor = mColorDialog.Color;
            }
        }
        //-------------------------------------------------------------------------
        // �F�N���b�N
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
                xAuthDialog dlg = new xAuthDialog();
                dlg.ShowDialog();
            }
            else
            {
                Properties.Settings.Default.tw_token = "";
                Properties.Settings.Default.tw_token_secret = "";
            }
        }

        private void mUITimer_Tick(object sender, EventArgs e)
        {
            this.mAuthResult.Text = (Properties.Settings.Default.tw_token.Length == 0) ? "���F��" : "�F�؍ς�";
            this.mAuthBtn.Text = (Properties.Settings.Default.tw_token.Length == 0) ? "�F�؂���" : "�F�؉���";
        }

        //-------------------------------------------------------------------------
        // FME�p�X�ύX
        //-------------------------------------------------------------------------
        private void mFMLEBtn_Click(object sender, EventArgs e)
        {
            if (mOpenFileDlg.ShowDialog() == DialogResult.OK)
            {
                mFMLEPath.Text = mOpenFileDlg.FileName;
            }
        }

        //-------------------------------------------------------------------------
        // ���������폜
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
                    Debug.WriteLine(file + "��������܂���");
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

                                mResList.Rows.Add(enabled, msg,res);

                                //Debug.WriteLine( String.Format("res: {0}  msg: {1}", res, msg));
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

                        // �쐬������������
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
            mBrowser.Enabled = mUseBrowserCookie.Checked;
            mMailBox.Enabled = !mUseBrowserCookie.Checked;
            mPasswdBox.Enabled = !mUseBrowserCookie.Checked;
        }

        private void mFMEsessionBtn_Click(object sender, EventArgs e)
        {
            if (mSessionFileDlg.ShowDialog() == DialogResult.OK)
            {
                mFMEsessions.Text = mSessionFileDlg.FileName;
            }
        }
    }
}
//-------------------------------------------------------------------------
// �����ݒ�_�C�A���O
//
// Copyright (c) ������(http://ch.nicovideo.jp/community/co48276)
//-------------------------------------------------------------------------