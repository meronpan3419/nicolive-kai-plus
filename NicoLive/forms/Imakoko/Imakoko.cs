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
            
            if (mOwner != null)
            {
                this.Left = mOwner.Left + 400;
                this.Top = mOwner.Top + 60;
            }
            
            navigateMap(Properties.Settings.Default.imakoko_user);
            
            mImakokoID.Text = Properties.Settings.Default.imakoko_user;

            //System.Diagnostics.Process.Start(uri);
        }

        private void navigateMap(string id)
        {
            string uri = String.Format("http://proxy.imacoconow.com/v3/view?trace={0}",
                            id);
            mBrowser.Navigate(uri);
        }

        private void mImakokoID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                navigateMap(mImakokoID.Text);
                e.Handled = true;
            }
        }






    }
}
//-------------------------------------------------------------------------
// 今ココなう！用ウィンド
//
// Copyright (c) 金時豆(http://ch.nicovideo.jp/community/co48276)
//-------------------------------------------------------------------------
