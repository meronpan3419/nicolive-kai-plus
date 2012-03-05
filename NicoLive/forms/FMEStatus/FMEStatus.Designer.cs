namespace NicoLive
{
    partial class FMEStatus
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FMEStatus));
            this.Start = new System.Windows.Forms.Button();
            this.Restart = new System.Windows.Forms.Button();
            this.Stop = new System.Windows.Forms.Button();
            this.mLabel = new System.Windows.Forms.Label();
            this.mUITimer = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.mFMLEProfileList = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // Start
            // 
            this.Start.Enabled = false;
            this.Start.Location = new System.Drawing.Point(12, 35);
            this.Start.Name = "Start";
            this.Start.Size = new System.Drawing.Size(75, 23);
            this.Start.TabIndex = 0;
            this.Start.Text = "起動";
            this.Start.UseVisualStyleBackColor = true;
            this.Start.Click += new System.EventHandler(this.Start_Click);
            // 
            // Restart
            // 
            this.Restart.Enabled = false;
            this.Restart.Location = new System.Drawing.Point(94, 35);
            this.Restart.Name = "Restart";
            this.Restart.Size = new System.Drawing.Size(75, 23);
            this.Restart.TabIndex = 1;
            this.Restart.Text = "再起動";
            this.Restart.UseVisualStyleBackColor = true;
            this.Restart.Click += new System.EventHandler(this.Restart_Click);
            // 
            // Stop
            // 
            this.Stop.Enabled = false;
            this.Stop.Location = new System.Drawing.Point(176, 35);
            this.Stop.Name = "Stop";
            this.Stop.Size = new System.Drawing.Size(75, 23);
            this.Stop.TabIndex = 2;
            this.Stop.Text = "停止";
            this.Stop.UseVisualStyleBackColor = true;
            this.Stop.Click += new System.EventHandler(this.Stop_Click);
            // 
            // mLabel
            // 
            this.mLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.mLabel.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.mLabel.Location = new System.Drawing.Point(12, 9);
            this.mLabel.Name = "mLabel";
            this.mLabel.Size = new System.Drawing.Size(238, 23);
            this.mLabel.TabIndex = 3;
            // 
            // mUITimer
            // 
            this.mUITimer.Enabled = true;
            this.mUITimer.Interval = 500;
            this.mUITimer.Tick += new System.EventHandler(this.mUITimer_Tick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 66);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "FMEプロファイル：";
            // 
            // mFMLEProfileList
            // 
            this.mFMLEProfileList.FormattingEnabled = true;
            this.mFMLEProfileList.Location = new System.Drawing.Point(94, 63);
            this.mFMLEProfileList.Name = "mFMLEProfileList";
            this.mFMLEProfileList.Size = new System.Drawing.Size(156, 20);
            this.mFMLEProfileList.TabIndex = 5;
            this.mFMLEProfileList.SelectedIndexChanged += new System.EventHandler(this.mFMLEProfileList_SelectedIndexChanged);
            // 
            // FMEStatus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(262, 91);
            this.Controls.Add(this.mFMLEProfileList);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.mLabel);
            this.Controls.Add(this.Stop);
            this.Controls.Add(this.Restart);
            this.Controls.Add(this.Start);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FMEStatus";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FMEステータス";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.FMEStatus_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Start;
        private System.Windows.Forms.Button Restart;
        private System.Windows.Forms.Button Stop;
        private System.Windows.Forms.Label mLabel;
        private System.Windows.Forms.Timer mUITimer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox mFMLEProfileList;
    }
}