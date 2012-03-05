using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace NicoLive
{
    public partial class oAuthDialog : Form
    {

        Twitter t;
        public oAuthDialog()
        {
            InitializeComponent();
            t = new Twitter();
        }

        private void mOKBtn_Click(object sender, EventArgs e)
        {

        }

        private void mOKBtn_Click_1(object sender, EventArgs e)
        {
            string url;
            
            url = t.GetOAuthToken();
            System.Diagnostics.Process.Start(url);

            tPin.Enabled = true;
            button1.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                t.oAuth(tPin.Text);
                label1.Text = "認証成功";
                textBox1.Enabled = true;
                button2.Enabled = true;

            }
            catch (Exception)
            {
                label1.Text = "認証失敗";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                


                Thread th = new Thread(delegate()
                {
                    string msg = string.Format(textBox1.Text + "{0:s}", DateTime.Now.ToString("HH:mm:ss"));
                    msg += " #nicolivekaiplus";
                    t.Post(msg, "");
                });
                th.Start();


            }
            catch (Exception)
            {

            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
