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
    public partial class TwitterOAuthDialog : Form
    {

        Twitter t;
        public TwitterOAuthDialog()
        {
            InitializeComponent();
            t = new Twitter();
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            System.Text.RegularExpressions.Match m = System.Text.RegularExpressions.Regex.Match(webBrowser1.DocumentText, "<code>(.*?)</code>");
            if (m.Success)
            {
                t.GetOAuthToken(m.Groups[1].Value);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void oAuthDialog_Load(object sender, EventArgs e)
        {



            string url;
            url = t.GetOAuthTokenURL();
            webBrowser1.Navigate(url);
        }


    }
}
