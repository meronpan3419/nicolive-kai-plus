//-------------------------------------------------------------------------
// Viewer
//
// Copyright (c) 金時豆(http://ch.nicovideo.jp/community/co48276)
// $Id: Nickname.cs 697 2010-07-05 06:13:32Z kintoki $
//-------------------------------------------------------------------------
using System;
using System.Windows.Forms;

namespace NicoLive
{
    public partial class Viewer : Form
    {
        string mLv;

        //-------------------------------------------------------------------------
        //　コンストラクタ
        //-------------------------------------------------------------------------
        public Viewer(string iLv)
        {
            mLv = iLv;



            InitializeComponent();

        }

        //-------------------------------------------------------------------------
        //　SWFロード
        //-------------------------------------------------------------------------
        private void LoadMovie()
        {
            Nico nico = Nico.Instance;
            if (!nico.IsLogin)
            {
                string user_id = Properties.Settings.Default.user_id;
                string passwd = Properties.Settings.Default.password;
                nico.Login(user_id, passwd);
            }

            //string url = "http://live.nicovideo.jp/liveplayer.swf?110519134121&allowscale=FALSE&v=" + mLiveID.Text + "&watchVideoID" + mLiveID.Text;
            //string url = "http://live.nicovideo.jp/liveplayer.swf?v=" + mLiveID.Text + "&watchVideoID" + mLiveID.Text;
            //string url = "http://nl.nimg.jp/public/swf/liveplayer11.swf?v=" + mLiveID.Text + "&watchVideoID" + mLiveID.Text;
            string url = "http://live.nicovideo.jp/nicoliveplayer.swf?v=" + mLiveID.Text + "&_utime=" + Utils.GetUnixTime(DateTime.Now); ;

            //mFlash.Size = new System.Drawing.Size(950, 520);
            mFlash.LoadMovie(0, url);

        }

        //-------------------------------------------------------------------------
        //　Formロード
        //-------------------------------------------------------------------------
        private void Viewer_Load(object sender, System.EventArgs e)
        {
            updateUI();
            SetLiveID(mLv);
            this.Location = Properties.Settings.Default.viewer_pos;
        }



        //-------------------------------------------------------------------------
        //　視聴する放送ＩＤ設定
        //-------------------------------------------------------------------------
        public void SetLiveID(string iLv)
        {
            this.Invoke((Action)delegate()
            {
                mLiveID.Text = iLv;
                LoadMovie();
            });
        }

        //-------------------------------------------------------------------------
        //　視聴する放送ＩＤ変更
        //-------------------------------------------------------------------------
        private void mLiveID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Enter)
            {
                string tmp = mLiveID.Text;
                string live_id = "lv";

                int idx = tmp.LastIndexOf("lv");
                int len = tmp.Length;

                for (int i = idx + 2; i < len; i++)
                {
                    if ('0' <= tmp[i] && tmp[i] <= '9')
                        live_id += tmp[i].ToString();
                    else
                        break;
                }

                mLiveID.Text = live_id;

                LoadMovie();
            }
        }



        private void mSmallBtn_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.viewer_big = false;
            updateUI();
        }

        private void mBigBtn_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.viewer_big = true;
            updateUI();
        }

        private void mTop_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.viewer_top = !Properties.Settings.Default.viewer_top;
            updateUI();
        }

        private void updateUI()
        {
            if (!Properties.Settings.Default.viewer_top)
            {
                this.TopMost = true;
                mTop.BackColor = System.Drawing.Color.Orange;

            }
            else
            {
                this.TopMost = false;
                mTop.BackColor = System.Drawing.SystemColors.Control;
            }

            if (Properties.Settings.Default.viewer_big)
            {
                mBigBtn.BackColor = System.Drawing.Color.Orange;
                mSmallBtn.BackColor = System.Drawing.SystemColors.Control;
                this.Size = new System.Drawing.Size(690, 555);
                mFlash.Size = new System.Drawing.Size(960, 960);
                mTop.Text = "最前面";
                mAutoBoot.Text = "自動起動";
            }
            else
            {
                mBigBtn.BackColor = System.Drawing.SystemColors.Control;
                mSmallBtn.BackColor = System.Drawing.Color.Orange;
                this.Size = new System.Drawing.Size(350, 310);
                mFlash.Size = new System.Drawing.Size(480, 480);
                mTop.Text = "最";
                mAutoBoot.Text = "自";
            }

            if (Properties.Settings.Default.viewer_auto_boot)
            {
                mAutoBoot.BackColor = System.Drawing.Color.Orange;
            }
            else
            {

                mAutoBoot.BackColor = System.Drawing.SystemColors.Control;
            }
        }

        private void Viewer_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.viewer_pos = this.Location;
        }

        private void mAutoBoot_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.viewer_auto_boot = !Properties.Settings.Default.viewer_auto_boot;
            updateUI();
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }


    }
}
//-------------------------------------------------------------------------
// Copyright (c) 金時豆(http://ch.nicovideo.jp/community/co48276)
// $Id: Nickname.cs 697 2010-07-05 06:13:32Z kintoki $
//-------------------------------------------------------------------------