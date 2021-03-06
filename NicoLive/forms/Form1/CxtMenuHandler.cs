﻿//-------------------------------------------------------------------------
// Main Form
//
// Copyright (c) 金時豆(http://ch.nicovideo.jp/community/co48276)
// $Id$
//-------------------------------------------------------------------------
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace NicoLive
{
    partial class Form1
    {
        //-------------------------------------------------------------------------
        // コメントリストのコンテクストメニュー表示開始時
        //-------------------------------------------------------------------------
        private void CmtCxtMenu_Opening(object sender, CancelEventArgs e)
        {

            this.mCopyComment.Enabled = (this.mCommentList.SelectedRows.Count > 0);
            this.mCopyID.Enabled = (this.mCommentList.SelectedRows.Count > 0);
            this.mRename.Enabled = (this.mCommentList.SelectedRows.Count > 0);
            this.mNgID.Enabled = (this.mCommentList.SelectedRows.Count > 0);
            if (this.mNgID.Enabled)
            {
                string id = this.mCommentList.SelectedRows[0].Cells[(int)CommentColumn.COLUMN_ID].Value.ToString();
                Utils.WriteLog("CmtCxtMenu_Opening: id:" + id);
                if (this.mUid.IsNGUser(id))
                {
                    this.mNgID.Text = "このユーザーをNG解除";
                } else {
                    this.mNgID.Text = "このユーザーをNG登録";
                }
            }

            int r;
            if (this.mCommentList.SelectedRows.Count > 0 &&
                int.TryParse(mCommentList.SelectedRows[0].Cells[(int)CommentColumn.COLUMN_ID].Value.ToString(), out r))
            {
                this.mShowUserPage.Enabled = true;
            }
            else
            {
                this.mShowUserPage.Enabled = false;
            }
        }

        //-------------------------------------------------------------------------
        // コテハンリネーム
        //-------------------------------------------------------------------------
        private void Rename_Click(object sender, EventArgs e)
        {
            if (this.mCommentList.SelectedRows.Count > 0)
            {
                Rename rm = new Rename();
                rm.MyOwner = this;
                rm.ID = this.mCommentList.SelectedRows[0].Cells[(int)CommentColumn.COLUMN_ID].Value.ToString();
                if(this.mCommentList.SelectedRows[0].Cells[(int)CommentColumn.COLUMN_HANDLE].Value == null){
                    rm.UserName = "";
                } else {
                    rm.UserName = this.mCommentList.SelectedRows[0].Cells[(int)CommentColumn.COLUMN_HANDLE].Value.ToString();
                }
                rm.Show();
            }
        }

        //-------------------------------------------------------------------------
        // コメントのコピー
        //-------------------------------------------------------------------------
        private void CopyComment_Click(object sender, EventArgs e)
        {
            if (this.mCommentList.SelectedRows.Count > 0)
            {
                string cmt = this.mCommentList.SelectedRows[0].Cells[(int)CommentColumn.COLUMN_COMMENT].Value.ToString();
                Clipboard.SetDataObject(cmt, true);
            }
        }

        //-------------------------------------------------------------------------
        // IDのコピー
        //-------------------------------------------------------------------------
        private void CopyID_Click(object sender, EventArgs e)
        {
            if (this.mCommentList.SelectedRows.Count > 0)
            {
                string id = this.mCommentList.SelectedRows[0].Cells[(int)CommentColumn.COLUMN_ID].Value.ToString();
                Clipboard.SetDataObject(id, true);
            }
        }

        //-------------------------------------------------------------------------
        // ユーザーページを開く
        //-------------------------------------------------------------------------
        private void ShowUserPage_Click(object sender, EventArgs e)
        {
            if (this.mCommentList.SelectedRows.Count > 0)
            {
                string id = this.mCommentList.SelectedRows[0].Cells[(int)CommentColumn.COLUMN_ID].Value.ToString();
                string uri = "http://www.nicovideo.jp/user/" + id;
                System.Diagnostics.Process.Start(uri);
            }
        }
        //-------------------------------------------------------------------------
        // URLを開く
        //-------------------------------------------------------------------------
        private void CommentList_DoubleClick(object sender, EventArgs e)
        {
            if (this.mCommentList.SelectedRows.Count > 0)
            {
                string msg = this.mCommentList.SelectedRows[0].Cells[(int)CommentColumn.COLUMN_COMMENT].Value.ToString();

                if (msg.Length == 0) return;

                Utils.OpenURL(msg);
            }
        }
        //-------------------------------------------------------------------------
        // NG ID　登録・解除
        //-------------------------------------------------------------------------
        private void mNgID_Click(object sender, EventArgs e)
        {
            if (this.mCommentList.SelectedRows.Count > 0)
            {
                string id = this.mCommentList.SelectedRows[0].Cells[(int)CommentColumn.COLUMN_ID].Value.ToString();
                if (this.mUid.IsNGUser(id))
                {
                    delNgID(id);
                }
                else
                {
                    addNgID(id);
                }
            }
        }

        //-------------------------------------------------------------------------
        // NG ID 登録の実際の処理
        //-------------------------------------------------------------------------
        public void addNgID(string id)
        {
            if (MessageBox.Show(id + "\nをNG登録します", "NicoLive", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == DialogResult.OK)
            {
                // 旧NG処理 
                //if (mNico != null && mNico.IsLogin && !mDisconnect)
                //{
                //    SendComment("/ngadd ID \"" + id + "\" 0 0", true);
                //}

                // NGユーザなんたら
                if (mNico != null /*&& mNico.IsLogin && !mDisconnect*/)
                {
                    this.mUid.AddNGUser(id);
                }


            }
        }

        //-------------------------------------------------------------------------
        // NG ID 解除の実際の処理
        //-------------------------------------------------------------------------
        public void delNgID(string id)
        {
            if (MessageBox.Show(id + "\nをNG解除します", "NicoLive", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == DialogResult.OK)
            {
                // 旧NG処理 
                //if (mNico != null && mNico.IsLogin && !mDisconnect)
                //{
                //    SendComment("/ngadd ID \"" + id + "\" 0 0", true);
                //}

                // NGユーザなんたら
                if (mNico != null /*&& mNico.IsLogin && !mDisconnect*/)
                {
                    this.mUid.delNGUser(id);
                }


            }
        }
    }
}
//-------------------------------------------------------------------------
// Copyright (c) 金時豆(http://ch.nicovideo.jp/community/co48276)
// $Id$
//-------------------------------------------------------------------------