//-------------------------------------------------------------------------
// Main Form
//
// Copyright (c) 金時豆(http://ch.nicovideo.jp/community/co48276)
// $Id$
//-------------------------------------------------------------------------
using System.Windows.Forms;

namespace NicoLive
{
    partial class Form1
    {
        //-------------------------------------------------------------------------
        // ショートカットキー
        //-------------------------------------------------------------------------
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {

            if ((int)keyData == (int)Keys.F8)
            {
                this.mBouyomiBtn.PerformClick();
                return true;
            }
            if ((int)keyData == (int)Keys.F7)
            {
                this.mVisitorBtn.PerformClick();
                return true;
            }
            if ((int)keyData == (int)Keys.F6)
            {
                this.mContWaku.PerformClick();
                return true;
            }
            if ((int)keyData == (int)Keys.F5)
            {
                this.mAutoExtendBtn.PerformClick();
                return true;
            }
            if ((int)keyData == (int)Keys.F3)
            {
                this.mImakokoBtn.PerformClick();
                return true;
            }
            if ((int)keyData == (int)(Keys.F2 | Keys.Control))
            {
                if (mDoingGetNextWaku) return true;

                GetNextWaku(false);

                return true;
            }
            if ((int)keyData == (int)Keys.F2)
            {
                this.mCopyBtn.PerformClick();
                return true;
            }
            if ((int)keyData == (int)(Keys.F1 | Keys.Control))
            {
                this.mWakutoriBtn.PerformClick();
                return true;
            }
            if ((int)keyData == (int)Keys.F1)
            {
                this.mWakutoriBtn.PerformClick();
                return true;
            }

            if ((int)keyData == (int)Keys.F12)
            {
                this.mConnectBtn.PerformClick();
                return true;
            }

            // 接続中止
            if (keyData == (Keys.F12 | Keys.Control))
            {
                if (this.mLoginWorker.IsBusy)
                {
                    this.mLogin_cancel = true;
                    this.mAutoReconnectOnGoing = false;
                    this.mConnectBtn.Enabled = true;
                }
                return true;
            }

            // コメントパクる
            if (keyData == (Keys.W | Keys.Control))
            {
                if (this.mCommentList.SelectedRows.Count > 0)
                {
                    string comment = this.mCommentList.SelectedRows[0].Cells[(int)CommentColumn.COLUMN_COMMENT].Value.ToString();
                    this.SendComment(comment, true);
                }

            }

            // アンカーつけてコメントパクる
            if (keyData == (Keys.E | Keys.Control))
            {
                if (this.mCommentList.SelectedRows.Count > 0)
                {
                    string no = this.mCommentList.SelectedRows[0].Cells[(int)CommentColumn.COLUMN_NUMBER].Value.ToString();
                    string comment = this.mCommentList.SelectedRows[0].Cells[(int)CommentColumn.COLUMN_COMMENT].Value.ToString();
                    this.SendComment(">>" + no + " " + comment, true);
                }

            }

            // コメントツイート
            if (keyData == (Keys.T | Keys.Control))
            {
                if (this.mCommentList.SelectedRows.Count > 0)
                {
                    string no = this.mCommentList.SelectedRows[0].Cells[(int)CommentColumn.COLUMN_NUMBER].Value.ToString();
                    string comment = this.mCommentList.SelectedRows[0].Cells[(int)CommentColumn.COLUMN_COMMENT].Value.ToString();
                    //string name = this.mCommentList.SelectedRows[0].Cells[(int)CommentColumn.COLUMN_HANDLE].Value.ToString();
                    //this.SendComment(">>" + no + " " + comment, true);
                    string liveid = this.LiveID;
                    System.Threading.Thread th = new System.Threading.Thread(delegate()
                    {
                        Utils.Tweet(comment + "(>>" + no + " http://nico.ms/" + liveid + " #" + liveid + ")");
                    });
                    th.Name = "ShortcutKey: tweet comment";
                    th.Start();


                }

            }

            // 上に
            if (keyData == (Keys.K | Keys.Control))
            {
                if (this.mCommentList.SelectedRows.Count > 0)
                {
                    int index = this.mCommentList.SelectedRows[0].Index;
                    if (index != 0)
                    {
                        this.mCommentList.Rows[index].Selected = false;
                        this.mCommentList.Rows[index - 1].Selected = true;
                    }

                }

            }

            // 下に
            if (keyData == (Keys.J | Keys.Control))
            {
                if (this.mCommentList.SelectedRows.Count > 0)
                {
                    int index = this.mCommentList.SelectedRows[0].Index;
                    if (index < mCommentList.Rows.Count - 1)
                    {
                        this.mCommentList.Rows[index].Selected = false;
                        this.mCommentList.Rows[index + 1].Selected = true;
                    }

                }

            }

            // 一番新しいの
            if (keyData == (Keys.N | Keys.Control))
            {
                if (this.mCommentList.SelectedRows.Count > 0)
                {
                    int index = this.mCommentList.SelectedRows[0].Index;

                    this.mCommentList.Rows[index].Selected = false;
                    this.mCommentList.Rows[0].Selected = true;


                }

            }

            // コメント打つ
            if (keyData == (Keys.I | Keys.Control))
            {
                if (!this.mCommentBox.Focused)
                {
                    this.mCommentBox.Clear();
                    this.mCommentBox.Focus();
                }
            }

            // 棒読みちゃん静かに
            if (keyData == (Keys.Q | Keys.Control))
            {
                using (Bouyomi bm = new Bouyomi())
                {
                    bm.Clear();
                }
            }

            if (keyData == (Keys.Enter | Keys.Control))
            {
                if (this.mCommentBox.Focused)
                {
                    if (!mCommentBox.Text.Equals(""))
                    {
					
                        return true;

                    }
                }
            }



            ContainerControl containerControl;
            if (keyData == (Keys.I | Keys.Control) ||
                keyData == (Keys.N | Keys.Control) ||
                keyData == (Keys.W | Keys.Control) ||
                keyData == (Keys.J | Keys.Control) ||
                keyData == (Keys.K | Keys.Control))
            {
                if (this.ActiveControl is TextBox)
                {
                    return true;
                }
            }


            if (keyData == (Keys.A | Keys.Control))
            {
                if (this.ActiveControl is TextBox)
                {
                    ((TextBox)this.ActiveControl).SelectAll();
                    return true;
                }
                else if (this.ActiveControl is ContainerControl)
                {
                    containerControl = (ContainerControl)this.ActiveControl;
                    while (containerControl.ActiveControl is ContainerControl)
                    {
                        containerControl = containerControl.ActiveControl as ContainerControl;
                    }

                    if (containerControl.ActiveControl is TextBox)
                    {
                        ((TextBox)containerControl.ActiveControl).SelectAll();
                        return true;
                    }
                }
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
//-------------------------------------------------------------------------
// Copyright (c) 金時豆(http://ch.nicovideo.jp/community/co48276)
// $Id$
//-------------------------------------------------------------------------