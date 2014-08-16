//-------------------------------------------------------------------------
// Main Form
//
// Copyright (c) 金時豆(http://ch.nicovideo.jp/community/co48276)
// $Id$
//-------------------------------------------------------------------------
using System;
using System.Windows.Forms;

namespace NicoLive
{
    partial class Form1
    {

        //-------------------------------------------------------------------------
        // 接続ボタン押し
        //-------------------------------------------------------------------------
        private void ConnectBtn_Click(object sender, EventArgs e)
        {
            Connect(false);
        }
        //-------------------------------------------------------------------------
        // 放送ＩＤでエンターキーが押された時
        //-------------------------------------------------------------------------
        private void LiveID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Enter)
            {
                Connect(false);
            }
        }
        //-------------------------------------------------------------------------
        // 自動枠取り画面起動
        //-------------------------------------------------------------------------
        private void WakutoriBtn_Click(object sender, EventArgs e)
        {
            //if (Properties.Settings.Default.UseBrowserCookie)
            //{
            //    mNico.Login(Properties.Settings.Default.user_id, Properties.Settings.Default.password);
            //}
            if (!mNico.Login(Properties.Settings.Default.user_id, Properties.Settings.Default.password))
            {
                if (Properties.Settings.Default.UseBrowserCookie)
                {
                    MessageBox.Show("Cookieによるログインに失敗しました\nブラウザ上で再ログインしてください。", "NicoLive");
                    return;
                }
            }
            
            MakeWakutori(false);
        }
        //-------------------------------------------------------------------------
        // コメント読み上げ起動
        //-------------------------------------------------------------------------
        private void BouyomiBtn_Click(object sender, EventArgs e)
        {
            if (mBouyomiBtn.Checked)
            {
                mBouyomi.Talk(mMsg.GetMessage("読み上げを開始します"));
            }
        }

        //-------------------------------------------------------------------------
        // コメント読み上げインジケーター
        //-------------------------------------------------------------------------
        private void mBouyomiBtn_CheckStateChanged(object sender, EventArgs e)
        {
            if (mBouyomiBtn.Checked)
            {
                mBouyomiBtn.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                mBouyomiBtn.ForeColor = System.Drawing.SystemColors.ControlText;
            }
        }

        //-------------------------------------------------------------------------
        // 来場者読み上げインジケーター
        //-------------------------------------------------------------------------
        private void mVisitorBtn_CheckStateChanged(object sender, EventArgs e)
        {
            if (mVisitorBtn.Checked)
            {
                mVisitorBtn.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                mVisitorBtn.ForeColor = System.Drawing.SystemColors.ControlText;
            }
        }

        //-------------------------------------------------------------------------
        // 自動延長インジケーター
        //-------------------------------------------------------------------------
        private void mAutoExtendBtn_CheckStateChanged(object sender, EventArgs e)
        {
            if (mAutoExtendBtn.Checked)
            {
                mAutoExtendBtn.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                mAutoExtendBtn.ForeColor = System.Drawing.SystemColors.ControlText;
            }
        }

        //-------------------------------------------------------------------------
        // 連続枠取りインジケーター
        //-------------------------------------------------------------------------
        private void mContWaku_CheckStateChanged(object sender, EventArgs e)
        {
            if (mContWaku.Checked)
            {
                mContWaku.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                mContWaku.ForeColor = System.Drawing.SystemColors.ControlText;
            }
        }

        //-------------------------------------------------------------------------
        // 自動無料延長
        //-------------------------------------------------------------------------
        private void AutoExtendBtn_Click(object sender, EventArgs e)
        {
            if (mAutoExtendBtn.Checked)
            {
                mBouyomi.Talk(mMsg.GetMessage("自動無料延長を開始します"));
            }
        }

        //-------------------------------------------------------------------------
        // 前枠の続き
        //-------------------------------------------------------------------------
        private void mCopyBtn_Click(object sender, EventArgs e)
        {

            WakuDlg dlg = new WakuDlg(ParseLiveID(), true);
            dlg.ShowDialog();

            if (dlg.mState == WakuResult.NO_ERR)
            {
                using (Bouyomi bm = new Bouyomi())
                {
                    bm.Talk(mMsg.GetMessage("枠が取れたよ"));
                }

                this.LiveID = dlg.mLv;
                Connect(false);
            }
            else if (dlg.mState == WakuResult.JUNBAN)
            {
                MakeWakutori(false);
            }
        }

        //-------------------------------------------------------------------------
        // 今ココ起動
        //-------------------------------------------------------------------------
        private void ImakokoBtn_Click(object sender, EventArgs e)
        {


            if (mImakoko == null || mImakoko.IsDisposed)
            {
                mImakoko = new Imakoko();
            }
            mImakoko.Show();
            mImakoko.Activate();

        }

        // NGユーザー設定画面起動
        private void NGUserStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mNico == null) return;
            //if (!mNico.IsLogin) return;
            if (mNGUser == null || mNGUser.IsDisposed)
            {
                mNGUser = new NGUser(this.LiveID);
            }
            mNGUser.Show();
            mNGUser.Activate();
        }

    }
}
//-------------------------------------------------------------------------
// Copyright (c) 金時豆(http://ch.nicovideo.jp/community/co48276)
// $Id$
//-------------------------------------------------------------------------