namespace NicoLive
{
    partial class Imakoko
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Imakoko));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.mImakokoID = new System.Windows.Forms.ToolStripTextBox();
            this.mBrowser = new System.Windows.Forms.WebBrowser();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.mImakokoID});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(349, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(46, 22);
            this.toolStripLabel1.Text = "今ココID:";
            // 
            // mImakokoID
            // 
            this.mImakokoID.Name = "mImakokoID";
            this.mImakokoID.Size = new System.Drawing.Size(100, 25);
            this.mImakokoID.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.mImakokoID_KeyPress);
            // 
            // mBrowser
            // 
            this.mBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mBrowser.Location = new System.Drawing.Point(0, 25);
            this.mBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.mBrowser.Name = "mBrowser";
            this.mBrowser.ScriptErrorsSuppressed = true;
            this.mBrowser.Size = new System.Drawing.Size(349, 210);
            this.mBrowser.TabIndex = 2;
            // 
            // Imakoko
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(349, 235);
            this.Controls.Add(this.mBrowser);
            this.Controls.Add(this.toolStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Imakoko";
            this.Text = "今ココなう！";
            this.Load += new System.EventHandler(this.Imakoko_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripTextBox mImakokoID;
        private System.Windows.Forms.WebBrowser mBrowser;
    }
}