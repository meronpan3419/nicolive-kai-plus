//-------------------------------------------------------------------------
// コテハンリネームダイアログ
//
// Copyright (c) 金時豆(http://ch.nicovideo.jp/community/co48276)
// $Id$
//-------------------------------------------------------------------------
using System;
using System.Windows.Forms;
using System.Threading;

//-------------------------------------------------------------------------
// クラス実装
//-------------------------------------------------------------------------
namespace NicoLive
{
    public partial class Rename : Form
    {
        private Form1 mOwner = null;
        private string mID = null;
        private string mUserName = null;

        public Form1 MyOwner
        {
            set { mOwner = value; }
            get { return mOwner; }
        }

        public string ID
        {
            set { mID = value; }
            get { return mID; }
        }


        public string UserName
        {
            set { mUserName = value; }
            get { return mUserName; }
        }
        
        //-------------------------------------------------------------------------
        // コンストラクタ
        //-------------------------------------------------------------------------
        public Rename()
        {
            InitializeComponent();
        }

        //-------------------------------------------------------------------------
        // フォームロード時
        //-------------------------------------------------------------------------
        private void Rename_Load(object sender, EventArgs e)
        {
            // センタリング
            this.Top = (this.mOwner.Height - this.Height) / 2;
            this.Left = (this.mOwner.Width - this.Width) / 2;
            this.mName.Text = mUserName;
            this.lID.Text = mID;
            int r;
            mGetUserName.Enabled = int.TryParse(mID, out r); // 184じゃない時だけ
        }
        //-------------------------------------------------------------------------
        // ＯＫボタン
        //-------------------------------------------------------------------------
        private void mOK_Click(object sender, EventArgs e)
        {
                if (mOwner != null)
                {
                    mOwner.SetNickname(this.mID, mName.Text);
                }
                Close();
        }

        //-------------------------------------------------------------------------
        // キャンセルボタン
        //-------------------------------------------------------------------------
        private void mCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void mGetUserName_Click(object sender, EventArgs e)
        {
            Thread th = new Thread(delegate()
            {
                this.Invoke((Action)delegate()
                {
                    mGetUserName.Enabled = false;
                    mGetUserName.Text = "取得中…";
                });
                Nico mNico = Nico.Instance;
                string name = mNico.GetUsername(mID);
                

                this.Invoke((Action)delegate()
                {
                    this.mName.Text = name;
                    mGetUserName.Enabled = true;
                    mGetUserName.Text = "自動取得";


                });


            });
            th.Name = "rename.mGetUserName()";
            th.Start();






        }

 
    }
}
//-------------------------------------------------------------------------
// コテハンリネームダイアログ
//
// Copyright (c) 金時豆(http://ch.nicovideo.jp/community/co48276)
//-------------------------------------------------------------------------