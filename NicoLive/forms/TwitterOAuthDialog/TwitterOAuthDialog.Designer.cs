namespace NicoLive
{
    partial class TwitterOAuthDialog
    {
        /// <summary>
        /// 必要なデザイナー変数です。
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

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TwitterOAuthDialog));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.mPINCode = new System.Windows.Forms.ToolStripTextBox();
            this.btnOAuth = new System.Windows.Forms.ToolStripButton();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.mPINCode,
            this.btnOAuth});
            this.toolStrip1.Location = new System.Drawing.Point(0, 240);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(530, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(56, 22);
            this.toolStripLabel1.Text = "PINコード：";
            // 
            // mPINCode
            // 
            this.mPINCode.Name = "mPINCode";
            this.mPINCode.Size = new System.Drawing.Size(100, 25);
            this.mPINCode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.mPINCode_KeyPress);
            // 
            // btnOAuth
            // 
            this.btnOAuth.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnOAuth.Image = ((System.Drawing.Image)(resources.GetObject("btnOAuth.Image")));
            this.btnOAuth.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnOAuth.Name = "btnOAuth";
            this.btnOAuth.Size = new System.Drawing.Size(33, 22);
            this.btnOAuth.Text = "認証";
            this.btnOAuth.Click += new System.EventHandler(this.btnOAuth_Click);
            // 
            // webBrowser1
            // 
            this.webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser1.Location = new System.Drawing.Point(0, 0);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(530, 240);
            this.webBrowser1.TabIndex = 2;
            this.webBrowser1.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowser1_DocumentCompleted);
            // 
            // TwitterOAuthDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(530, 265);
            this.Controls.Add(this.webBrowser1);
            this.Controls.Add(this.toolStrip1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TwitterOAuthDialog";
            this.Text = "ツイッター認証";
            this.Load += new System.EventHandler(this.oAuthDialog_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripTextBox mPINCode;
        private System.Windows.Forms.ToolStripButton btnOAuth;
        private System.Windows.Forms.WebBrowser webBrowser1;


    }
}