//-------------------------------------------------------------------------
// 今ココなう！用ウィンド
//
// Copyright (c) 金時豆(http://ch.nicovideo.jp/community/co48276)
// $Id$
//-------------------------------------------------------------------------
using System;
using System.Windows.Forms;


//-------------------------------------------------------------------------
// クラス実装
//-------------------------------------------------------------------------
namespace NicoLive
{
    public partial class Imakoko : Form
    {
        // オーナーフォーム
        private Form mOwner = null;

        public Form MyOwner
        {
            get { return mOwner; }
            set { mOwner = value; }
        }

        //-------------------------------------------------------------------------
        // 初期化
        //-------------------------------------------------------------------------
        public Imakoko()
        {
            InitializeComponent();
        }

        //-------------------------------------------------------------------------
        // ロード時
        //-------------------------------------------------------------------------
        private void Imakoko_Load(object sender, EventArgs e)
        {
            string uri = String.Format("http://proxy.imacoconow.com/v3/view?trace={0}",
                                        Properties.Settings.Default.imakoko_user);

            if (mOwner != null)
            {
                this.Left = mOwner.Left + 400;
                this.Top = mOwner.Top + 60;
            }
            mBrowser.Navigate(uri);
            //System.Diagnostics.Process.Start(uri);
        }

        private void mBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            //if (!IsIE8Fixed)
            //{
            //    string html = mBrowser.DocumentText.Replace("<head>", "<head><meta http-equiv=\"X-UA-Compatible\" content=\"IE=7\">");
            //    html = html.Replace("script src=\"/static/", "script src=\"http://imakoko-gps.appspot.com/static/");
            //    mBrowser.DocumentText = html;
            //    IsIE8Fixed = true;
            // }
        }

    }
}
//-------------------------------------------------------------------------
// 今ココなう！用ウィンド
//
// Copyright (c) 金時豆(http://ch.nicovideo.jp/community/co48276)
//-------------------------------------------------------------------------
