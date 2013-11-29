namespace NicoLive
{
    partial class LiveConsole
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LiveConsole));
            this.mFlash = new AxShockwaveFlashObjects.AxShockwaveFlash();
            ((System.ComponentModel.ISupportInitialize)(this.mFlash)).BeginInit();
            this.SuspendLayout();
            // 
            // mFlash
            // 
            this.mFlash.Enabled = true;
            this.mFlash.Location = new System.Drawing.Point(0, 0);
            this.mFlash.Name = "mFlash";
            this.mFlash.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("mFlash.OcxState")));
            this.mFlash.Size = new System.Drawing.Size(952, 256);
            this.mFlash.TabIndex = 0;
            // 
            // LiveConsole
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(953, 257);
            this.Controls.Add(this.mFlash);
            this.Name = "LiveConsole";
            this.Text = "配信コンソール";
            this.Load += new System.EventHandler(this.LiveConsole_Load);
            ((System.ComponentModel.ISupportInitialize)(this.mFlash)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private AxShockwaveFlashObjects.AxShockwaveFlash mFlash;
    }
}