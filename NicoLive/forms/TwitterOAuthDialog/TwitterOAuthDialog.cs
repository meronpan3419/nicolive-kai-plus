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
                this.DialogResult = t.GetOAuthToken(m.Groups[1].Value) ? DialogResult.OK : DialogResult.Cancel;
                this.Close();
            }
        }

        private void oAuthDialog_Load(object sender, EventArgs e)
        {
            string url;
            url = t.GetOAuthTokenURL();
            webBrowser1.Navigate(url);
        }

        private void btnOAuth_Click(object sender, EventArgs e)
        {
            this.DialogResult = t.GetOAuthToken(mPINCode.Text) ? DialogResult.OK : DialogResult.Cancel;
            this.Close();
        }

        private void mPINCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                this.DialogResult = t.GetOAuthToken(mPINCode.Text) ? DialogResult.OK : DialogResult.Cancel;
                this.Close();
            }
        }






    }
}
