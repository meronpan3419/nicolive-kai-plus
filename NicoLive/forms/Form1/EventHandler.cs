//-------------------------------------------------------------------------
// Main Form
//
// Copyright (c) 金時豆(http://ch.nicovideo.jp/community/co48276)
// $Id$
//-------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;
using System.Drawing;
using System.Threading;

namespace NicoLive
{
    partial class Form1
    {

        //-------------------------------------------------------------------------
        // フォームロード
        //-------------------------------------------------------------------------
        private void Form1_Load(object sender, EventArgs e)
        {
            mNico = Nico.Instance;
            mBouyomi = new Bouyomi();

            mSpeakList = new List<string>();
            mGatherUserID = new List<string>();

            // 色
            mCommentList.BackgroundColor = Properties.Settings.Default.back_color;
            mCommentList.RowsDefaultCellStyle.ForeColor = Properties.Settings.Default.text_color;

            mUid = UserID.Instance;
            //mNG = NG.Instance;
            mUIStatus = UIStatus.Instance;
            mLiveInfo = LiveInfo.Instance;
            mRes = Response.Instance;
            mLauncher = Launcher.Instance;



            mUseHQBtn.Text = Properties.Settings.Default.use_hq ? "外部配信" : "通常配信";
            updateFMLEProfileList();
            updateBroadcastType();



            // キャプチャ突破フラグを落としておく
            //Properties.Settings.Default.auto_captcha = false;

            // ＮＧワード読み込み
            //mNG.LoadNGWord();

            // Response読み込み
            mRes.LoadResponse();

            mLauncher.LoadLauncher();

            // IDロード
            UserID uid = UserID.Instance;
            uid.LoadUserID(true);
            uid.LoadUserID(false);

            // NGユーザー読み込み
            uid.LoadNGUserID(true);
            uid.LoadNGUserID(false);

            // フォント設定
            Utils.SetCommentFont(ref mCommentList);

            // カラム幅設定
            this.mCommentList.Columns[0].Width = Properties.Settings.Default.column_width_0;
            this.mCommentList.Columns[1].Width = Properties.Settings.Default.column_width_1;
            this.mCommentList.Columns[2].Width = Properties.Settings.Default.column_width_2;
            this.mCommentList.Columns[3].Width = Properties.Settings.Default.column_width_3;
            this.mCommentList.Columns[4].Width = Properties.Settings.Default.column_width_4;
            this.mCommentList.Columns[5].Width = Properties.Settings.Default.column_width_5;

            // 最後に取得した放送ＩＤを設定
            this.mLiveID.Text = Properties.Settings.Default.last_lv;

            // バージョンチェック開始
            //CheckVersion();

            // 来場者数更新用ワーカースタート
            this.mUpdateInfoWorker.RunWorkerAsync();

            // コメント処理ワーカースタート
            this.mCommentWorker.RunWorkerAsync();

            // 自動延長ワーカースタート
            this.mAutoExtendWorker.RunWorkerAsync();

            // ユーザー名収集ワーカー
            this.mGatherUserIDWorker.RunWorkerAsync();

            mWakutoriBtn.Text = mWakutoriBtn.Text + "(F1)";
            mCopyBtn.Text = mCopyBtn.Text + "(F2)";
            mImakokoBtn.Text = mImakokoBtn.Text + "(F3)";
            mAutoExtendBtn.Text = mAutoExtendBtn.Text + "(F5)";
            mContWaku.Text = mContWaku.Text + "(F6)";
            mVisitorBtn.Text = mVisitorBtn.Text + "(F7)";
            mBouyomiBtn.Text = mBouyomiBtn.Text + "(F8)";

            // ウィンドステート復帰
            this.Width = Properties.Settings.Default.mw_size.Width;
            this.Height = Properties.Settings.Default.mw_size.Height;
            this.Top = Properties.Settings.Default.mw_pos.Y;
            this.Left = Properties.Settings.Default.mw_pos.X;
            this.WindowState = Properties.Settings.Default.mw_state;

            // 棒読みちゃん起動
            Bouyomi.Launch();

            // 今ココ起動
            ImakokoNow.Launch();


            // 迅速に配信
            if (this.mFastLive)
            {
                bool login = mNico.Login(Properties.Settings.Default.user_id,
                           Properties.Settings.Default.password);
                if (login)
                {
                    GetNextWaku();
                }
            }


        }

        //-------------------------------------------------------------------------
        // フォームクローズ
        //-------------------------------------------------------------------------
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            FMLE.Stop();
            NLE.Stop();
            XSplit.Stop();

            if (mLogger != null)
            {
                try
                {
                    mLogger.Close();
                    mLogger = null;
                }
                catch (Exception ex)
                {
                    Utils.WriteLog(ex.Message);
                }
            }
            // ID保存
            mUid.SaveUserID(false);
            mUid.SaveUserID(true);

            // カラム幅保存
            Properties.Settings.Default.column_width_0 = this.mCommentList.Columns[0].Width;
            Properties.Settings.Default.column_width_1 = this.mCommentList.Columns[1].Width;
            Properties.Settings.Default.column_width_2 = this.mCommentList.Columns[2].Width;
            Properties.Settings.Default.column_width_3 = this.mCommentList.Columns[3].Width;
            Properties.Settings.Default.column_width_4 = this.mCommentList.Columns[4].Width;
            Properties.Settings.Default.column_width_5 = this.mCommentList.Columns[5].Width;

            // ウィンドステート保存
            if (this.WindowState == FormWindowState.Normal)
            {
                Size size = new Size(this.Width, this.Height);
                Properties.Settings.Default.mw_size = size;
                Point pos = new Point(this.Top, this.Left);
                Properties.Settings.Default.mw_pos = pos;
            }
            Properties.Settings.Default.mw_state = this.WindowState;

            // セーブ
            Properties.Settings.Default.Save();

            // 棒読みちゃん終了
            Bouyomi.Exit();

            // 今ココ終了
            ImakokoNow.Exit();

            mNico = null;
        }

        //-------------------------------------------------------------------------
        // 初回表示時
        //-------------------------------------------------------------------------
        private void Form1_Shown(object sender, EventArgs e)
        {
            // ステータス表示設定
            UpdateStatusVisibility();

            // 自動接続
            if (mAutoConnect)
                Connect(false);
        }

        private void mUseHQBtn_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.use_hq)
            {
                Properties.Settings.Default.use_hq = false;
                mUseHQBtn.Text = "通常配信";
                Thread th = new Thread(delegate()
                {
                    HQ.Stop();
                });
                th.Name = "NivoLive.Form1.EventHandler.mUseHQBtn_Click()";
                th.Start();

                //this.Invoke((Action)delegate()
                //{
                //    GetPlayer();
                //});
                

            }
            else
            {
                Properties.Settings.Default.use_hq = true;
                mUseHQBtn.Text = "外部配信";





            }


        }

        private void mResetBtn_Click(object sender, EventArgs e)
        {
            SendComment("/reset", true);
        }

        private void mCommentList_SortCompare(object sender, DataGridViewSortCompareEventArgs e)
        {

            int no1 = (e.CellValue1 == null ? 0 : int.Parse(mCommentList.Rows[e.RowIndex1].Cells[0].Value.ToString()));
            int no2 = (e.CellValue2 == null ? 0 : int.Parse(mCommentList.Rows[e.RowIndex2].Cells[0].Value.ToString()));

            //結果を代入
            e.SortResult = no1 - no2;
            //処理したことを知らせる
            e.Handled = true;
        }

        private void mCommentList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return; //ヘッダーをクリック

            Random rnd = new Random(Environment.TickCount);

            //int a = rnd.Next(0, 255);
            int r = rnd.Next(100, 255);
            int g = rnd.Next(100, 255);
            int b = rnd.Next(100, 255);
            //System.Drawing.Color color = System.Drawing.Color.FromArgb(a, r, g, b);
            System.Drawing.Color color = System.Drawing.Color.FromArgb(r, g, b);

            string id = "";

            try
            {

                id = this.mCommentList.SelectedRows[0].Cells[(int)CommentColumn.COLUMN_ID].Value.ToString();
                this.mUid.AddUserColor(id, color);

                //this.mCommentList.SelectedRows[0].Cells[(int)CommentColumn.COLUMN_NUMBER].Style.BackColor = color;
                //this.mCommentList.SelectedRows[0].Cells[(int)CommentColumn.COLUMN_ID].Style.BackColor = color;
                //this.mCommentList.SelectedRows[0].Cells[(int)CommentColumn.COLUMN_HANDLE].Style.BackColor = color;
                //this.mCommentList.SelectedRows[0].Cells[(int)CommentColumn.COLUMN_COMMENT].Style.BackColor = color;

                this.mCommentList.ClearSelection();

                Utils.updateColor(ref mCommentList, id, color);
            }
            catch (Exception ex)
            {
                Utils.WriteLog(ex.Message);
            }
        }

        private void mCommentList_Sorted(object sender, EventArgs e)
        {
            if (this.mCommentList.Columns[0].HeaderCell.SortGlyphDirection.Equals(SortOrder.Descending))
            {
                Properties.Settings.Default.comment_sort_desc = true;
            }
            else
            {
                Properties.Settings.Default.comment_sort_desc = false;
            }
        }

        private void mCommentList_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            if (e.RowCount == 0)
            {
                return;
            }

            //mCommentList.FirstDisplayedScrollingRowIndex = e.RowIndex;

            if (!this.mCommentList.Columns[0].HeaderCell.SortGlyphDirection.Equals(SortOrder.Descending))
            {
                mCommentList.FirstDisplayedScrollingRowIndex = mCommentList.Rows.Count - 1;
            }
            else
            {
                mCommentList.FirstDisplayedScrollingRowIndex = 0;
            }
        }

        private void mUpdateFMLEProfileListBtn_Click(object sender, EventArgs e)
        {
            updateFMLEProfileList();
        }

        private void mFMLEProfileList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_boot && Properties.Settings.Default.use_fme && mNico != null && mNico.IsLogin)
            {
                Properties.Settings.Default.fmle_default_profile = mFMLEProfileList.SelectedItem.ToString();

                HQ.Restart(LiveID);

            }
        }

        private void cbCommentOwner_CheckedChanged(object sender, EventArgs e)
        {
            if (cbCommentOwner.Checked)
            {
                cbComment184.Checked = true;
                cbComment184.Enabled = false;
            }
            else
            {
                cbComment184.Checked = false;
                cbComment184.Enabled = true;
            }
        }

        private void mCommentBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (!mCommentBox.Text.Equals(""))
            {
                if (e.KeyCode == System.Windows.Forms.Keys.Enter)
                {
                    CommentPost();
                    e.Handled = true;
                    return;
                }
            }

        }


        private void mCommentBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            //EnterやEscapeキーでビープ音が鳴らないようにする
            if (e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Escape)
            {
                e.Handled = true;
            }
        }

        private void mCommentList_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Char.ToUpper(e.KeyChar);
            if (e.KeyChar == (char)Keys.R)
            {
                if (this.mCommentList.SelectedRows.Count > 0)
                {
                    string no = this.mCommentList.SelectedRows[0].Cells[(int)CommentColumn.COLUMN_NUMBER].Value.ToString();
                    mCommentBox.Text = ">>" + no + " ";
                    mCommentBox.Focus();
                    mCommentBox.Select(mCommentBox.Text.Length, 0);
                }
            }
        }


        private void mWakumachi_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://nicolive_wakusu.b72.in/");

        }

        private void miCommentColor_Click(object sender, EventArgs e)
        {

            string id = "";

            try
            {
                id = this.mCommentList.SelectedRows[0].Cells[(int)CommentColumn.COLUMN_ID].Value.ToString();
                System.Drawing.Color color;

                //ColorDialogクラスのインスタンスを作成
                ColorDialog cd = new ColorDialog();

                //はじめに選択されている色を設定
                cd.Color = System.Drawing.Color.Pink;
                //色の作成部分を表示可能にする
                //デフォルトがTrueのため必要はない
                cd.AllowFullOpen = true;
                //純色だけに制限しない
                //デフォルトがFalseのため必要はない
                cd.SolidColorOnly = false;
                //[作成した色]に指定した色（RGB値）を表示する
                cd.CustomColors = new int[] {
    0x33, 0x66, 0x99, 0xCC, 0x3300, 0x3333,
    0x3366, 0x3399, 0x33CC, 0x6600, 0x6633,
    0x6666, 0x6699, 0x66CC, 0x9900, 0x9933};

                //ダイアログを表示する
                if (cd.ShowDialog() != DialogResult.OK)
                {
                    return;


                }
                //選択された色の取得
                color = cd.Color;

                this.mCommentList.ClearSelection();

                this.mUid.updateUserColor(id, color);
                Utils.updateColor(ref mCommentList, id, color);
            }
            catch (Exception ex)
            {
                Utils.WriteLog(ex.Message);
            }


        }


        private void mXSplitTimer_Tick(object sender, EventArgs e)
        {
            XSplit.HandlingStatus(LiveID, this);
        }

        private void mCommentPostBtn_Click(object sender, EventArgs e)
        {
            CommentPost();
        }
        
        private void mDisconnectBtn_Click(object sender, EventArgs e)
        {
            mNico.SendOwnerComment(LiveID, "/disconnect", "", mLiveInfo.Token);
            mNico.LiveStop(LiveID, mLiveInfo.Token);

        }
    }
}
//-------------------------------------------------------------------------
// Copyright (c) 金時豆(http://ch.nicovideo.jp/community/co48276)
// $Id$
//-------------------------------------------------------------------------
