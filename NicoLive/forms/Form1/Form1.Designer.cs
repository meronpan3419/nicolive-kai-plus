﻿namespace NicoLive
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナ変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
            Properties.Settings.Default.Save();
        }

        #region Windows フォーム デザイナで生成されたコード

        /// <summary>
        /// デザイナ サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディタで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.mToolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.mLiveID = new System.Windows.Forms.ToolStripTextBox();
            this.mConnectBtn = new System.Windows.Forms.ToolStripButton();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.FMEMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewerMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.初期設定ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.nicoLiveについてToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.mBouyomiBtn = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mVisitorBtn = new System.Windows.Forms.ToolStripButton();
            this.mCaptchaSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.mContWaku = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.mAutoExtendBtn = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.mCommentBtn = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.mImakokoBtn = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.mCopyBtn = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.mWakutoriBtn = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.mStatusStrip = new System.Windows.Forms.StatusStrip();
            this.mLoginLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.mTotalCnt = new System.Windows.Forms.ToolStripStatusLabel();
            this.mActiveCnt = new System.Windows.Forms.ToolStripStatusLabel();
            this.mUniqCnt = new System.Windows.Forms.ToolStripStatusLabel();
            this.mCpuInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.mLogLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.mBattery = new System.Windows.Forms.ToolStripStatusLabel();
            this.mWakumachi = new System.Windows.Forms.ToolStripStatusLabel();
            this.mUpLink = new System.Windows.Forms.ToolStripStatusLabel();
            this.mDownLink = new System.Windows.Forms.ToolStripStatusLabel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.mFlash = new AxShockwaveFlashObjects.AxShockwaveFlash();
            this.mCommentList = new System.Windows.Forms.DataGridView();
            this.mNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mHandle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mComment = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mInfo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mCmtCxtMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mCopyComment = new System.Windows.Forms.ToolStripMenuItem();
            this.mCopyID = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.mOpenURL = new System.Windows.Forms.ToolStripMenuItem();
            this.mShowUserPage = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.mRename = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
            this.mNgID = new System.Windows.Forms.ToolStripMenuItem();
            this.miCommentColor = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.mUseNLE = new System.Windows.Forms.RadioButton();
            this.mUseXSplit = new System.Windows.Forms.RadioButton();
            this.mUseFME = new System.Windows.Forms.RadioButton();
            this.mCommandBox = new System.Windows.Forms.TextBox();
            this.mUpdateFMLEProfileListBtn = new System.Windows.Forms.Button();
            this.mResetBtn = new System.Windows.Forms.Button();
            this.mUseHQBtn = new System.Windows.Forms.Button();
            this.mFMLEProfileList = new System.Windows.Forms.ComboBox();
            this.mCommentPostBtn = new System.Windows.Forms.Button();
            this.cbCommentTwitter = new System.Windows.Forms.CheckBox();
            this.mCommentBox = new System.Windows.Forms.TextBox();
            this.cbComment184 = new System.Windows.Forms.CheckBox();
            this.cbCommentOwner = new System.Windows.Forms.CheckBox();
            this.mUITimer = new System.Windows.Forms.Timer(this.components);
            this.mUpdateInfoWorker = new System.ComponentModel.BackgroundWorker();
            this.mAutoExtendWorker = new System.ComponentModel.BackgroundWorker();
            this.mCommentWorker = new System.ComponentModel.BackgroundWorker();
            this.mLoginWorker = new System.ComponentModel.BackgroundWorker();
            this.mPerfCnt = new System.Diagnostics.PerformanceCounter();
            this.mHardInfoTimer = new System.Windows.Forms.Timer(this.components);
            this.mGatherUserIDWorker = new System.ComponentModel.BackgroundWorker();
            this.mXSplitTimer = new System.Windows.Forms.Timer(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.mToolStrip.SuspendLayout();
            this.mStatusStrip.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mFlash)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mCommentList)).BeginInit();
            this.mCmtCxtMenu.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mPerfCnt)).BeginInit();
            this.SuspendLayout();
            // 
            // mToolStrip
            // 
            this.mToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.mLiveID,
            this.mConnectBtn,
            this.toolStripDropDownButton1,
            this.toolStripSeparator8,
            this.mBouyomiBtn,
            this.toolStripSeparator1,
            this.mVisitorBtn,
            this.mCaptchaSeparator,
            this.mContWaku,
            this.toolStripSeparator7,
            this.mAutoExtendBtn,
            this.toolStripSeparator2,
            this.mCommentBtn,
            this.toolStripSeparator6,
            this.mImakokoBtn,
            this.toolStripSeparator3,
            this.mCopyBtn,
            this.toolStripSeparator4,
            this.mWakutoriBtn,
            this.toolStripSeparator9});
            this.mToolStrip.Location = new System.Drawing.Point(0, 0);
            this.mToolStrip.Name = "mToolStrip";
            this.mToolStrip.Size = new System.Drawing.Size(962, 25);
            this.mToolStrip.TabIndex = 0;
            this.mToolStrip.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(47, 22);
            this.toolStripLabel1.Text = "放送ＩＤ：";
            // 
            // mLiveID
            // 
            this.mLiveID.Name = "mLiveID";
            this.mLiveID.Size = new System.Drawing.Size(74, 25);
            this.mLiveID.ToolTipText = "lv～";
            this.mLiveID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.LiveID_KeyDown);
            // 
            // mConnectBtn
            // 
            this.mConnectBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.mConnectBtn.Image = ((System.Drawing.Image)(resources.GetObject("mConnectBtn.Image")));
            this.mConnectBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mConnectBtn.Name = "mConnectBtn";
            this.mConnectBtn.Size = new System.Drawing.Size(60, 22);
            this.mConnectBtn.Text = "接続(F12)";
            this.mConnectBtn.Click += new System.EventHandler(this.ConnectBtn_Click);
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.None;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FMEMenuItem,
            this.ViewerMenuItem,
            this.toolStripSeparator5,
            this.初期設定ToolStripMenuItem,
            this.toolStripMenuItem1,
            this.nicoLiveについてToolStripMenuItem1});
            this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(13, 22);
            this.toolStripDropDownButton1.Text = "その他";
            // 
            // FMEMenuItem
            // 
            this.FMEMenuItem.Name = "FMEMenuItem";
            this.FMEMenuItem.Size = new System.Drawing.Size(193, 22);
            this.FMEMenuItem.Text = "FMEステータス";
            this.FMEMenuItem.Click += new System.EventHandler(this.FMEMenuItem_Click);
            // 
            // ViewerMenuItem
            // 
            this.ViewerMenuItem.Name = "ViewerMenuItem";
            this.ViewerMenuItem.Size = new System.Drawing.Size(193, 22);
            this.ViewerMenuItem.Text = "簡易ビューアー";
            this.ViewerMenuItem.Click += new System.EventHandler(this.ViewerMenuItem_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(190, 6);
            // 
            // 初期設定ToolStripMenuItem
            // 
            this.初期設定ToolStripMenuItem.Name = "初期設定ToolStripMenuItem";
            this.初期設定ToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.初期設定ToolStripMenuItem.Text = "初期設定";
            this.初期設定ToolStripMenuItem.Click += new System.EventHandler(this.SettingMenu_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(190, 6);
            // 
            // nicoLiveについてToolStripMenuItem1
            // 
            this.nicoLiveについてToolStripMenuItem1.Name = "nicoLiveについてToolStripMenuItem1";
            this.nicoLiveについてToolStripMenuItem1.Size = new System.Drawing.Size(193, 22);
            this.nicoLiveについてToolStripMenuItem1.Text = "NicoLiveのバージョン情報";
            this.nicoLiveについてToolStripMenuItem1.Click += new System.EventHandler(this.AboutNicoLiveMenu_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(6, 25);
            // 
            // mBouyomiBtn
            // 
            this.mBouyomiBtn.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.mBouyomiBtn.Checked = true;
            this.mBouyomiBtn.CheckOnClick = true;
            this.mBouyomiBtn.CheckState = System.Windows.Forms.CheckState.Checked;
            this.mBouyomiBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.mBouyomiBtn.ForeColor = System.Drawing.Color.Red;
            this.mBouyomiBtn.Image = ((System.Drawing.Image)(resources.GetObject("mBouyomiBtn.Image")));
            this.mBouyomiBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mBouyomiBtn.Name = "mBouyomiBtn";
            this.mBouyomiBtn.Size = new System.Drawing.Size(58, 22);
            this.mBouyomiBtn.Text = "ｺﾒﾝﾄ読上";
            this.mBouyomiBtn.CheckStateChanged += new System.EventHandler(this.mBouyomiBtn_CheckStateChanged);
            this.mBouyomiBtn.Click += new System.EventHandler(this.BouyomiBtn_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // mVisitorBtn
            // 
            this.mVisitorBtn.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.mVisitorBtn.CheckOnClick = true;
            this.mVisitorBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.mVisitorBtn.Image = ((System.Drawing.Image)(resources.GetObject("mVisitorBtn.Image")));
            this.mVisitorBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mVisitorBtn.Name = "mVisitorBtn";
            this.mVisitorBtn.Size = new System.Drawing.Size(81, 22);
            this.mVisitorBtn.Text = "来場者数読上";
            this.mVisitorBtn.CheckStateChanged += new System.EventHandler(this.mVisitorBtn_CheckStateChanged);
            // 
            // mCaptchaSeparator
            // 
            this.mCaptchaSeparator.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.mCaptchaSeparator.Name = "mCaptchaSeparator";
            this.mCaptchaSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // mContWaku
            // 
            this.mContWaku.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.mContWaku.CheckOnClick = true;
            this.mContWaku.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.mContWaku.Image = ((System.Drawing.Image)(resources.GetObject("mContWaku.Image")));
            this.mContWaku.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mContWaku.Name = "mContWaku";
            this.mContWaku.Size = new System.Drawing.Size(57, 22);
            this.mContWaku.Text = "連続枠取";
            this.mContWaku.CheckStateChanged += new System.EventHandler(this.mContWaku_CheckStateChanged);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 25);
            // 
            // mAutoExtendBtn
            // 
            this.mAutoExtendBtn.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.mAutoExtendBtn.CheckOnClick = true;
            this.mAutoExtendBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.mAutoExtendBtn.Image = ((System.Drawing.Image)(resources.GetObject("mAutoExtendBtn.Image")));
            this.mAutoExtendBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mAutoExtendBtn.Name = "mAutoExtendBtn";
            this.mAutoExtendBtn.Size = new System.Drawing.Size(81, 22);
            this.mAutoExtendBtn.Text = "自動無料延長";
            this.mAutoExtendBtn.CheckStateChanged += new System.EventHandler(this.mAutoExtendBtn_CheckStateChanged);
            this.mAutoExtendBtn.Click += new System.EventHandler(this.AutoExtendBtn_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // mCommentBtn
            // 
            this.mCommentBtn.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.mCommentBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.mCommentBtn.Image = ((System.Drawing.Image)(resources.GetObject("mCommentBtn.Image")));
            this.mCommentBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mCommentBtn.Name = "mCommentBtn";
            this.mCommentBtn.Size = new System.Drawing.Size(34, 22);
            this.mCommentBtn.Text = "ｺﾒﾝﾄ";
            this.mCommentBtn.Click += new System.EventHandler(this.CommentBtn_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 25);
            // 
            // mImakokoBtn
            // 
            this.mImakokoBtn.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.mImakokoBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.mImakokoBtn.Image = ((System.Drawing.Image)(resources.GetObject("mImakokoBtn.Image")));
            this.mImakokoBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mImakokoBtn.Name = "mImakokoBtn";
            this.mImakokoBtn.Size = new System.Drawing.Size(33, 22);
            this.mImakokoBtn.Text = "今ｺｺ";
            this.mImakokoBtn.Click += new System.EventHandler(this.ImakokoBtn_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // mCopyBtn
            // 
            this.mCopyBtn.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.mCopyBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.mCopyBtn.Image = ((System.Drawing.Image)(resources.GetObject("mCopyBtn.Image")));
            this.mCopyBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mCopyBtn.Name = "mCopyBtn";
            this.mCopyBtn.Size = new System.Drawing.Size(54, 22);
            this.mCopyBtn.Text = "前枠続き";
            this.mCopyBtn.Click += new System.EventHandler(this.mCopyBtn_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // mWakutoriBtn
            // 
            this.mWakutoriBtn.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.mWakutoriBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.mWakutoriBtn.Image = ((System.Drawing.Image)(resources.GetObject("mWakutoriBtn.Image")));
            this.mWakutoriBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mWakutoriBtn.Name = "mWakutoriBtn";
            this.mWakutoriBtn.Size = new System.Drawing.Size(33, 22);
            this.mWakutoriBtn.Text = "枠取";
            this.mWakutoriBtn.Click += new System.EventHandler(this.WakutoriBtn_Click);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(6, 25);
            // 
            // mStatusStrip
            // 
            this.mStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mLoginLabel,
            this.mTotalCnt,
            this.mActiveCnt,
            this.mUniqCnt,
            this.mCpuInfo,
            this.mLogLabel,
            this.mBattery,
            this.mWakumachi,
            this.mUpLink,
            this.mDownLink});
            this.mStatusStrip.Location = new System.Drawing.Point(0, 411);
            this.mStatusStrip.Name = "mStatusStrip";
            this.mStatusStrip.Size = new System.Drawing.Size(962, 25);
            this.mStatusStrip.TabIndex = 1;
            this.mStatusStrip.Text = "statusStrip1";
            // 
            // mLoginLabel
            // 
            this.mLoginLabel.AutoToolTip = true;
            this.mLoginLabel.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.mLoginLabel.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.mLoginLabel.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.mLoginLabel.Name = "mLoginLabel";
            this.mLoginLabel.Size = new System.Drawing.Size(89, 20);
            this.mLoginLabel.Text = "未ログイン";
            this.mLoginLabel.ToolTipText = "ログイン状態";
            // 
            // mTotalCnt
            // 
            this.mTotalCnt.AutoToolTip = true;
            this.mTotalCnt.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.mTotalCnt.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.mTotalCnt.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.mTotalCnt.Name = "mTotalCnt";
            this.mTotalCnt.Size = new System.Drawing.Size(81, 20);
            this.mTotalCnt.Text = "来場者：0";
            this.mTotalCnt.ToolTipText = "総来場者数";
            // 
            // mActiveCnt
            // 
            this.mActiveCnt.AutoToolTip = true;
            this.mActiveCnt.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.mActiveCnt.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.mActiveCnt.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.mActiveCnt.Name = "mActiveCnt";
            this.mActiveCnt.Size = new System.Drawing.Size(117, 20);
            this.mActiveCnt.Text = "アクティブ数：0";
            this.mActiveCnt.ToolTipText = "10分以内にコメントを書き込んだアクティブリスナー数";
            // 
            // mUniqCnt
            // 
            this.mUniqCnt.AutoToolTip = true;
            this.mUniqCnt.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.mUniqCnt.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.mUniqCnt.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.mUniqCnt.Name = "mUniqCnt";
            this.mUniqCnt.Size = new System.Drawing.Size(104, 20);
            this.mUniqCnt.Text = "トータル数：0";
            this.mUniqCnt.ToolTipText = "コメントを書き込んだユニークリスナー数";
            // 
            // mCpuInfo
            // 
            this.mCpuInfo.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.mCpuInfo.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.mCpuInfo.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.mCpuInfo.Name = "mCpuInfo";
            this.mCpuInfo.Size = new System.Drawing.Size(55, 20);
            this.mCpuInfo.Text = "CPU：";
            // 
            // mLogLabel
            // 
            this.mLogLabel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.mLogLabel.Name = "mLogLabel";
            this.mLogLabel.Size = new System.Drawing.Size(0, 20);
            // 
            // mBattery
            // 
            this.mBattery.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.mBattery.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.mBattery.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.mBattery.Name = "mBattery";
            this.mBattery.Size = new System.Drawing.Size(95, 20);
            this.mBattery.Text = "バッテリー：";
            // 
            // mWakumachi
            // 
            this.mWakumachi.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.mWakumachi.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.mWakumachi.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.mWakumachi.Name = "mWakumachi";
            this.mWakumachi.Size = new System.Drawing.Size(82, 20);
            this.mWakumachi.Text = "枠数：0/0";
            this.mWakumachi.Click += new System.EventHandler(this.mWakumachi_Click);
            // 
            // mUpLink
            // 
            this.mUpLink.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.mUpLink.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.mUpLink.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.mUpLink.Name = "mUpLink";
            this.mUpLink.Size = new System.Drawing.Size(121, 20);
            this.mUpLink.Text = "上り：    0Kbps";
            // 
            // mDownLink
            // 
            this.mDownLink.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.mDownLink.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.mDownLink.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.mDownLink.Name = "mDownLink";
            this.mDownLink.Size = new System.Drawing.Size(4, 20);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Outset;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.mFlash, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.mCommentList, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 25);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 205F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 65F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(962, 383);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // mFlash
            // 
            this.mFlash.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.mFlash.Enabled = true;
            this.mFlash.Location = new System.Drawing.Point(2, 2);
            this.mFlash.Margin = new System.Windows.Forms.Padding(0);
            this.mFlash.Name = "mFlash";
            this.mFlash.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("mFlash.OcxState")));
            this.mFlash.Size = new System.Drawing.Size(958, 205);
            this.mFlash.TabIndex = 2;
            // 
            // mCommentList
            // 
            this.mCommentList.AllowUserToAddRows = false;
            this.mCommentList.AllowUserToDeleteRows = false;
            this.mCommentList.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
            this.mCommentList.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.mCommentList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.mCommentList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.mCommentList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.mNo,
            this.mID,
            this.mHandle,
            this.mComment,
            this.mDate,
            this.mInfo});
            this.mCommentList.ContextMenuStrip = this.mCmtCxtMenu;
            this.mCommentList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mCommentList.GridColor = System.Drawing.SystemColors.ActiveBorder;
            this.mCommentList.Location = new System.Drawing.Point(2, 276);
            this.mCommentList.Margin = new System.Windows.Forms.Padding(0);
            this.mCommentList.MultiSelect = false;
            this.mCommentList.Name = "mCommentList";
            this.mCommentList.ReadOnly = true;
            this.mCommentList.RowHeadersVisible = false;
            this.mCommentList.RowTemplate.Height = 21;
            this.mCommentList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.mCommentList.Size = new System.Drawing.Size(958, 130);
            this.mCommentList.TabIndex = 3;
            this.mCommentList.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.mCommentList_CellDoubleClick);
            this.mCommentList.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.mCommentList_RowsAdded);
            this.mCommentList.SortCompare += new System.Windows.Forms.DataGridViewSortCompareEventHandler(this.mCommentList_SortCompare);
            this.mCommentList.Sorted += new System.EventHandler(this.mCommentList_Sorted);
            this.mCommentList.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.mCommentList_KeyPress);
            // 
            // mNo
            // 
            this.mNo.HeaderText = "No";
            this.mNo.Name = "mNo";
            this.mNo.ReadOnly = true;
            this.mNo.Width = 50;
            // 
            // mID
            // 
            this.mID.HeaderText = "ID";
            this.mID.Name = "mID";
            this.mID.ReadOnly = true;
            this.mID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.mID.Width = 80;
            // 
            // mHandle
            // 
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.mHandle.DefaultCellStyle = dataGridViewCellStyle2;
            this.mHandle.HeaderText = "コテハン";
            this.mHandle.Name = "mHandle";
            this.mHandle.ReadOnly = true;
            this.mHandle.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.mHandle.Width = 120;
            // 
            // mComment
            // 
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.mComment.DefaultCellStyle = dataGridViewCellStyle3;
            this.mComment.HeaderText = "コメント";
            this.mComment.Name = "mComment";
            this.mComment.ReadOnly = true;
            this.mComment.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.mComment.Width = 440;
            // 
            // mDate
            // 
            this.mDate.HeaderText = "時間";
            this.mDate.Name = "mDate";
            this.mDate.ReadOnly = true;
            this.mDate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.mDate.Width = 90;
            // 
            // mInfo
            // 
            this.mInfo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.mInfo.HeaderText = "情報";
            this.mInfo.Name = "mInfo";
            this.mInfo.ReadOnly = true;
            this.mInfo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // mCmtCxtMenu
            // 
            this.mCmtCxtMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mCopyComment,
            this.mCopyID,
            this.toolStripMenuItem3,
            this.mOpenURL,
            this.mShowUserPage,
            this.toolStripMenuItem2,
            this.mRename,
            this.toolStripSeparator10,
            this.mNgID,
            this.miCommentColor});
            this.mCmtCxtMenu.Name = "mCmtCxtMenu";
            this.mCmtCxtMenu.Size = new System.Drawing.Size(178, 176);
            this.mCmtCxtMenu.Opening += new System.ComponentModel.CancelEventHandler(this.CmtCxtMenu_Opening);
            // 
            // mCopyComment
            // 
            this.mCopyComment.Name = "mCopyComment";
            this.mCopyComment.Size = new System.Drawing.Size(177, 22);
            this.mCopyComment.Text = "コメントをコピー";
            this.mCopyComment.Click += new System.EventHandler(this.CopyComment_Click);
            // 
            // mCopyID
            // 
            this.mCopyID.Name = "mCopyID";
            this.mCopyID.Size = new System.Drawing.Size(177, 22);
            this.mCopyID.Text = "ＩＤをコピー";
            this.mCopyID.Click += new System.EventHandler(this.CopyID_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(174, 6);
            // 
            // mOpenURL
            // 
            this.mOpenURL.Name = "mOpenURL";
            this.mOpenURL.Size = new System.Drawing.Size(177, 22);
            this.mOpenURL.Text = "URLを開く";
            this.mOpenURL.Click += new System.EventHandler(this.CommentList_DoubleClick);
            // 
            // mShowUserPage
            // 
            this.mShowUserPage.Name = "mShowUserPage";
            this.mShowUserPage.Size = new System.Drawing.Size(177, 22);
            this.mShowUserPage.Text = "ユーザーページを見る";
            this.mShowUserPage.Click += new System.EventHandler(this.ShowUserPage_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(174, 6);
            // 
            // mRename
            // 
            this.mRename.Name = "mRename";
            this.mRename.Size = new System.Drawing.Size(177, 22);
            this.mRename.Text = "コテハン入力・・・";
            this.mRename.Click += new System.EventHandler(this.Rename_Click);
            // 
            // toolStripSeparator10
            // 
            this.toolStripSeparator10.Name = "toolStripSeparator10";
            this.toolStripSeparator10.Size = new System.Drawing.Size(174, 6);
            // 
            // mNgID
            // 
            this.mNgID.Name = "mNgID";
            this.mNgID.Size = new System.Drawing.Size(177, 22);
            this.mNgID.Text = "このユーザーをNG登録";
            this.mNgID.Click += new System.EventHandler(this.mNgID_Click);
            // 
            // miCommentColor
            // 
            this.miCommentColor.Name = "miCommentColor";
            this.miCommentColor.Size = new System.Drawing.Size(177, 22);
            this.miCommentColor.Text = "色つけまくる";
            this.miCommentColor.Click += new System.EventHandler(this.miCommentColor_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.mUseNLE);
            this.panel1.Controls.Add(this.mUseXSplit);
            this.panel1.Controls.Add(this.mUseFME);
            this.panel1.Controls.Add(this.mCommandBox);
            this.panel1.Controls.Add(this.mUpdateFMLEProfileListBtn);
            this.panel1.Controls.Add(this.mResetBtn);
            this.panel1.Controls.Add(this.mUseHQBtn);
            this.panel1.Controls.Add(this.mFMLEProfileList);
            this.panel1.Controls.Add(this.mCommentPostBtn);
            this.panel1.Controls.Add(this.cbCommentTwitter);
            this.panel1.Controls.Add(this.mCommentBox);
            this.panel1.Controls.Add(this.cbComment184);
            this.panel1.Controls.Add(this.cbCommentOwner);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(5, 212);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(952, 59);
            this.panel1.TabIndex = 4;
            // 
            // mUseNLE
            // 
            this.mUseNLE.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.mUseNLE.AutoSize = true;
            this.mUseNLE.Location = new System.Drawing.Point(882, 10);
            this.mUseNLE.Name = "mUseNLE";
            this.mUseNLE.Size = new System.Drawing.Size(44, 16);
            this.mUseNLE.TabIndex = 15;
            this.mUseNLE.TabStop = true;
            this.mUseNLE.Text = "NLE";
            this.mUseNLE.UseVisualStyleBackColor = true;
            this.mUseNLE.CheckedChanged += new System.EventHandler(this.mUseNLE_CheckedChanged);
            // 
            // mUseXSplit
            // 
            this.mUseXSplit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.mUseXSplit.AutoSize = true;
            this.mUseXSplit.Location = new System.Drawing.Point(823, 10);
            this.mUseXSplit.Name = "mUseXSplit";
            this.mUseXSplit.Size = new System.Drawing.Size(53, 16);
            this.mUseXSplit.TabIndex = 14;
            this.mUseXSplit.TabStop = true;
            this.mUseXSplit.Text = "XSplit";
            this.mUseXSplit.UseVisualStyleBackColor = true;
            this.mUseXSplit.CheckedChanged += new System.EventHandler(this.mUseXSplit_CheckedChanged);
            // 
            // mUseFME
            // 
            this.mUseFME.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.mUseFME.AutoSize = true;
            this.mUseFME.Location = new System.Drawing.Point(776, 10);
            this.mUseFME.Name = "mUseFME";
            this.mUseFME.Size = new System.Drawing.Size(46, 16);
            this.mUseFME.TabIndex = 13;
            this.mUseFME.TabStop = true;
            this.mUseFME.Text = "FME";
            this.mUseFME.UseVisualStyleBackColor = true;
            this.mUseFME.CheckedChanged += new System.EventHandler(this.mUseFME_CheckedChanged);
            // 
            // mCommandBox
            // 
            this.mCommandBox.Location = new System.Drawing.Point(17, 37);
            this.mCommandBox.Name = "mCommandBox";
            this.mCommandBox.Size = new System.Drawing.Size(151, 19);
            this.mCommandBox.TabIndex = 12;
            // 
            // mUpdateFMLEProfileListBtn
            // 
            this.mUpdateFMLEProfileListBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.mUpdateFMLEProfileListBtn.Location = new System.Drawing.Point(882, 33);
            this.mUpdateFMLEProfileListBtn.Name = "mUpdateFMLEProfileListBtn";
            this.mUpdateFMLEProfileListBtn.Size = new System.Drawing.Size(62, 23);
            this.mUpdateFMLEProfileListBtn.TabIndex = 11;
            this.mUpdateFMLEProfileListBtn.Text = "リスト更新";
            this.mUpdateFMLEProfileListBtn.UseVisualStyleBackColor = true;
            this.mUpdateFMLEProfileListBtn.Click += new System.EventHandler(this.mUpdateFMLEProfileListBtn_Click);
            // 
            // mResetBtn
            // 
            this.mResetBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.mResetBtn.Location = new System.Drawing.Point(628, 6);
            this.mResetBtn.Name = "mResetBtn";
            this.mResetBtn.Size = new System.Drawing.Size(63, 50);
            this.mResetBtn.TabIndex = 10;
            this.mResetBtn.Text = "/reset";
            this.mResetBtn.UseVisualStyleBackColor = true;
            this.mResetBtn.Click += new System.EventHandler(this.mResetBtn_Click);
            // 
            // mUseHQBtn
            // 
            this.mUseHQBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.mUseHQBtn.Location = new System.Drawing.Point(702, 6);
            this.mUseHQBtn.Name = "mUseHQBtn";
            this.mUseHQBtn.Size = new System.Drawing.Size(68, 23);
            this.mUseHQBtn.TabIndex = 8;
            this.mUseHQBtn.Text = "通常配信";
            this.mUseHQBtn.UseVisualStyleBackColor = true;
            this.mUseHQBtn.Click += new System.EventHandler(this.mUseHQBtn_Click);
            // 
            // mFMLEProfileList
            // 
            this.mFMLEProfileList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.mFMLEProfileList.FormattingEnabled = true;
            this.mFMLEProfileList.Location = new System.Drawing.Point(702, 36);
            this.mFMLEProfileList.Name = "mFMLEProfileList";
            this.mFMLEProfileList.Size = new System.Drawing.Size(174, 20);
            this.mFMLEProfileList.TabIndex = 7;
            this.mFMLEProfileList.SelectedIndexChanged += new System.EventHandler(this.mFMLEProfileList_SelectedIndexChanged);
            // 
            // mCommentPostBtn
            // 
            this.mCommentPostBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.mCommentPostBtn.Location = new System.Drawing.Point(559, 6);
            this.mCommentPostBtn.Name = "mCommentPostBtn";
            this.mCommentPostBtn.Size = new System.Drawing.Size(63, 50);
            this.mCommentPostBtn.TabIndex = 4;
            this.mCommentPostBtn.Text = "コメント";
            this.mCommentPostBtn.UseVisualStyleBackColor = true;
            this.mCommentPostBtn.Click += new System.EventHandler(this.mCommentPostBtn_Click);
            // 
            // cbCommentTwitter
            // 
            this.cbCommentTwitter.AutoSize = true;
            this.cbCommentTwitter.Location = new System.Drawing.Point(119, 10);
            this.cbCommentTwitter.Name = "cbCommentTwitter";
            this.cbCommentTwitter.Size = new System.Drawing.Size(57, 16);
            this.cbCommentTwitter.TabIndex = 3;
            this.cbCommentTwitter.Text = "twitter";
            this.cbCommentTwitter.UseVisualStyleBackColor = true;
            // 
            // mCommentBox
            // 
            this.mCommentBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.mCommentBox.Location = new System.Drawing.Point(182, 8);
            this.mCommentBox.Multiline = true;
            this.mCommentBox.Name = "mCommentBox";
            this.mCommentBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.mCommentBox.Size = new System.Drawing.Size(371, 48);
            this.mCommentBox.TabIndex = 2;
            this.mCommentBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.mCommentBox_KeyDown);
            this.mCommentBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.mCommentBox_KeyPress);
            // 
            // cbComment184
            // 
            this.cbComment184.AutoSize = true;
            this.cbComment184.Checked = true;
            this.cbComment184.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbComment184.Enabled = false;
            this.cbComment184.Location = new System.Drawing.Point(71, 10);
            this.cbComment184.Name = "cbComment184";
            this.cbComment184.Size = new System.Drawing.Size(42, 16);
            this.cbComment184.TabIndex = 1;
            this.cbComment184.Text = "184";
            this.cbComment184.UseVisualStyleBackColor = true;
            // 
            // cbCommentOwner
            // 
            this.cbCommentOwner.AutoSize = true;
            this.cbCommentOwner.Checked = true;
            this.cbCommentOwner.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbCommentOwner.Location = new System.Drawing.Point(17, 10);
            this.cbCommentOwner.Name = "cbCommentOwner";
            this.cbCommentOwner.Size = new System.Drawing.Size(48, 16);
            this.cbCommentOwner.TabIndex = 0;
            this.cbCommentOwner.Text = "運営";
            this.cbCommentOwner.UseVisualStyleBackColor = true;
            this.cbCommentOwner.CheckedChanged += new System.EventHandler(this.cbCommentOwner_CheckedChanged);
            // 
            // mUITimer
            // 
            this.mUITimer.Enabled = true;
            this.mUITimer.Interval = 200;
            this.mUITimer.Tick += new System.EventHandler(this.UITimer_Tick);
            // 
            // mUpdateInfoWorker
            // 
            this.mUpdateInfoWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.UpdateInfoWorker_DoWork);
            // 
            // mAutoExtendWorker
            // 
            this.mAutoExtendWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.AutoExtendWorker_DoWork);
            // 
            // mCommentWorker
            // 
            this.mCommentWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.CommentWorker_DoWork);
            // 
            // mLoginWorker
            // 
            this.mLoginWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.LoginWorker_DoWork);
            // 
            // mPerfCnt
            // 
            this.mPerfCnt.CategoryName = "Processor";
            this.mPerfCnt.CounterName = "% Processor Time";
            this.mPerfCnt.InstanceName = "_Total";
            // 
            // mHardInfoTimer
            // 
            this.mHardInfoTimer.Enabled = true;
            this.mHardInfoTimer.Interval = 1000;
            this.mHardInfoTimer.Tick += new System.EventHandler(this.HardInfoTimer_Tick);
            // 
            // mGatherUserIDWorker
            // 
            this.mGatherUserIDWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.GatherUserIDWorker_DoWork);
            // 
            // mXSplitTimer
            // 
            this.mXSplitTimer.Enabled = true;
            this.mXSplitTimer.Interval = 1000;
            this.mXSplitTimer.Tick += new System.EventHandler(this.mXSplitTimer_Tick);
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(962, 436);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.mStatusStrip);
            this.Controls.Add(this.mToolStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "豆ライブ(NicoLive)";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.mToolStrip.ResumeLayout(false);
            this.mToolStrip.PerformLayout();
            this.mStatusStrip.ResumeLayout(false);
            this.mStatusStrip.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mFlash)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mCommentList)).EndInit();
            this.mCmtCxtMenu.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mPerfCnt)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip mToolStrip;
        private System.Windows.Forms.ToolStripTextBox mLiveID;
        private System.Windows.Forms.StatusStrip mStatusStrip;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ToolStripButton mConnectBtn;
        private System.Windows.Forms.ToolStripStatusLabel mLoginLabel;
        private System.Windows.Forms.Timer mUITimer;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton mBouyomiBtn;
        private System.Windows.Forms.ToolStripStatusLabel mUniqCnt;
        private System.Windows.Forms.ToolStripStatusLabel mTotalCnt;
        private System.Windows.Forms.ToolStripStatusLabel mActiveCnt;
        private System.Windows.Forms.ToolStripStatusLabel mLogLabel;
        private AxShockwaveFlashObjects.AxShockwaveFlash mFlash;
        private System.Windows.Forms.ToolStripButton mVisitorBtn;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem 初期設定ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem nicoLiveについてToolStripMenuItem1;
        private System.Windows.Forms.ContextMenuStrip mCmtCxtMenu;
        private System.Windows.Forms.ToolStripMenuItem mCopyComment;
        private System.Windows.Forms.ToolStripButton mImakokoBtn;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton mWakutoriBtn;
        private System.ComponentModel.BackgroundWorker mUpdateInfoWorker;
        private System.Windows.Forms.ToolStripButton mAutoExtendBtn;
        private System.ComponentModel.BackgroundWorker mAutoExtendWorker;
        private System.ComponentModel.BackgroundWorker mCommentWorker;
        private System.Windows.Forms.ToolStripMenuItem mRename;
        private System.ComponentModel.BackgroundWorker mLoginWorker;
        private System.Diagnostics.PerformanceCounter mPerfCnt;
        private System.Windows.Forms.ToolStripStatusLabel mCpuInfo;
        private System.Windows.Forms.Timer mHardInfoTimer;
        private System.Windows.Forms.ToolStripMenuItem mCopyID;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripSeparator mCaptchaSeparator;
        private System.Windows.Forms.ToolStripStatusLabel mBattery;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem mShowUserPage;
        private System.Windows.Forms.ToolStripButton mContWaku;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripStatusLabel mUpLink;
        private System.Windows.Forms.ToolStripStatusLabel mDownLink;
        private System.Windows.Forms.ToolStripButton mCommentBtn;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem mOpenURL;
        private System.Windows.Forms.DataGridView mCommentList;
        private System.ComponentModel.BackgroundWorker mGatherUserIDWorker;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripButton mCopyBtn;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.ToolStripMenuItem ViewerMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem FMEMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
        private System.Windows.Forms.ToolStripMenuItem mNgID;
        private System.Windows.Forms.Timer mXSplitTimer;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button mCommentPostBtn;
        private System.Windows.Forms.CheckBox cbCommentTwitter;
        private System.Windows.Forms.TextBox mCommentBox;
        private System.Windows.Forms.CheckBox cbComment184;
        private System.Windows.Forms.CheckBox cbCommentOwner;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ToolStripStatusLabel mWakumachi;
        private System.Windows.Forms.ToolStripMenuItem miCommentColor;
        private System.Windows.Forms.Button mUpdateFMLEProfileListBtn;
        private System.Windows.Forms.Button mResetBtn;
        private System.Windows.Forms.Button mUseHQBtn;
        private System.Windows.Forms.ComboBox mFMLEProfileList;
        private System.Windows.Forms.DataGridViewTextBoxColumn mNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn mID;
        private System.Windows.Forms.DataGridViewTextBoxColumn mHandle;
        private System.Windows.Forms.DataGridViewTextBoxColumn mComment;
        private System.Windows.Forms.DataGridViewTextBoxColumn mDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn mInfo;
        private System.Windows.Forms.TextBox mCommandBox;
        private System.Windows.Forms.RadioButton mUseNLE;
        private System.Windows.Forms.RadioButton mUseXSplit;
        private System.Windows.Forms.RadioButton mUseFME;
    }
}

