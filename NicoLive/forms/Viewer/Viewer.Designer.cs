namespace NicoLive
{
    partial class Viewer
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
        }

        #region Windows フォーム デザイナで生成されたコード

        /// <summary>
        /// デザイナ サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディタで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Viewer));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.mLiveID = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mSmallBtn = new System.Windows.Forms.ToolStripButton();
            this.mBigBtn = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.mTop = new System.Windows.Forms.ToolStripButton();
            this.mAutoBoot = new System.Windows.Forms.ToolStripButton();
            this.mFlash = new AxShockwaveFlashObjects.AxShockwaveFlash();
            this.UI_Timer = new System.Windows.Forms.Timer(this.components);
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mFlash)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.mLiveID,
            this.toolStripSeparator1,
            this.mSmallBtn,
            this.mBigBtn,
            this.toolStripSeparator2,
            this.mTop,
            this.mAutoBoot});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(552, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            this.toolStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.toolStrip1_ItemClicked);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(58, 22);
            this.toolStripLabel1.Text = "放送ID：";
            // 
            // mLiveID
            // 
            this.mLiveID.Name = "mLiveID";
            this.mLiveID.Size = new System.Drawing.Size(100, 25);
            this.mLiveID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.mLiveID_KeyDown);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // mSmallBtn
            // 
            this.mSmallBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.mSmallBtn.Image = ((System.Drawing.Image)(resources.GetObject("mSmallBtn.Image")));
            this.mSmallBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mSmallBtn.Name = "mSmallBtn";
            this.mSmallBtn.Size = new System.Drawing.Size(24, 22);
            this.mSmallBtn.Text = "小";
            this.mSmallBtn.Click += new System.EventHandler(this.mSmallBtn_Click);
            // 
            // mBigBtn
            // 
            this.mBigBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.mBigBtn.Image = ((System.Drawing.Image)(resources.GetObject("mBigBtn.Image")));
            this.mBigBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mBigBtn.Name = "mBigBtn";
            this.mBigBtn.Size = new System.Drawing.Size(24, 22);
            this.mBigBtn.Text = "大";
            this.mBigBtn.Click += new System.EventHandler(this.mBigBtn_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // mTop
            // 
            this.mTop.BackColor = System.Drawing.SystemColors.Control;
            this.mTop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.mTop.Image = ((System.Drawing.Image)(resources.GetObject("mTop.Image")));
            this.mTop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mTop.Name = "mTop";
            this.mTop.Size = new System.Drawing.Size(48, 22);
            this.mTop.Text = "最前面";
            this.mTop.Click += new System.EventHandler(this.mTop_Click);
            // 
            // mAutoBoot
            // 
            this.mAutoBoot.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.mAutoBoot.Image = ((System.Drawing.Image)(resources.GetObject("mAutoBoot.Image")));
            this.mAutoBoot.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mAutoBoot.Name = "mAutoBoot";
            this.mAutoBoot.Size = new System.Drawing.Size(60, 22);
            this.mAutoBoot.Text = "自動起動";
            this.mAutoBoot.Click += new System.EventHandler(this.mAutoBoot_Click);
            // 
            // mFlash
            // 
            this.mFlash.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.mFlash.Enabled = true;
            this.mFlash.Location = new System.Drawing.Point(0, 25);
            this.mFlash.Margin = new System.Windows.Forms.Padding(0);
            this.mFlash.Name = "mFlash";
            this.mFlash.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("mFlash.OcxState")));
            this.mFlash.Size = new System.Drawing.Size(1312, 701);
            this.mFlash.TabIndex = 3;
            // 
            // UI_Timer
            // 
            this.UI_Timer.Enabled = true;
            this.UI_Timer.Interval = 500;
            // 
            // Viewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(552, 511);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.mFlash);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Viewer";
            this.Text = "簡易ビューアー";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Viewer_FormClosing);
            this.Load += new System.EventHandler(this.Viewer_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mFlash)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripTextBox mLiveID;
        private AxShockwaveFlashObjects.AxShockwaveFlash mFlash;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton mSmallBtn;
        private System.Windows.Forms.ToolStripButton mBigBtn;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton mTop;
        private System.Windows.Forms.ToolStripButton mAutoBoot;
        private System.Windows.Forms.Timer UI_Timer;
    }
}