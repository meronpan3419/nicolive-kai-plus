//-------------------------------------------------------------------------
// FMEステータス
//
// Copyright (c) 金時豆(http://ch.nicovideo.jp/community/co48276)
// $Id: UserID.cs 575 2010-06-08 09:43:08Z kintoki $
//-------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;

//-------------------------------------------------------------------------
// クラス実装
//-------------------------------------------------------------------------
namespace NicoLive
{
    public partial class FMEStatus : Form
    {
        private Form1 mOwner = null;
        private bool _boot = true;

        public FMEStatus(Form1 iOwner)
        {
            InitializeComponent();
            mOwner = iOwner;
        }

        private void FMEStatus_Load(object sender, EventArgs e)
        {
            int btm = mOwner.Bottom;
            int right = mOwner.Right;

            Top = btm - 115;
            Left = right - 265;

            string path = Properties.Settings.Default.fmle_profile_path;
            if (Directory.Exists(path))
            {
                string[] xmls = System.IO.Directory.GetFiles(path);
                mFMLEProfileList.Items.Clear();
                foreach (string xml in xmls)
                {
                    mFMLEProfileList.Items.Add(System.IO.Path.GetFileName(xml));
                }
                mFMLEProfileList.SelectedItem = Properties.Settings.Default.fmle_default_profile;
                _boot = false;
            }



        }



        private void Start_Click(object sender, EventArgs e)
        {
            if (!FMLE.hasFME() || !NLE.IsBroadCast || !XSplit.IsBroadCast)
                HQ.Exec(mOwner.LiveID);

        }

        private void Restart_Click(object sender, EventArgs e)
        {
            HQ.Restart(mOwner.LiveID);
        }



        private void Stop_Click(object sender, EventArgs e)
        {
            HQ.Stop();
        }

        private void mUITimer_Tick(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.use_nle)
            {// NLE 関連設定

                Start.Enabled = NLE.IsAlive && !NLE.IsBroadCast;
                Restart.Enabled = NLE.IsBroadCast;
                Stop.Enabled = NLE.IsBroadCast;
                if (NLE.IsAlive)
                {
                    mLabel.Text = (NLE.IsBroadCast) ? "NLEが放送中です" : "NLEは停止中です";
                }
                else
                {
                    mLabel.Text = "NLEは未起動です";
                }
            }            
            else  if (Properties.Settings.Default.use_xsplit)
            {// XSplit 関連設定

                Start.Enabled = XSplit.IsAlive && !XSplit.IsBroadCast;
                Restart.Enabled = XSplit.IsBroadCast;
                Stop.Enabled = XSplit.IsBroadCast;


                if (XSplit.IsAlive)
                {
                    mLabel.Text = (XSplit.IsBroadCast) ? "XSplitが放送中です" : "XSplitは停止中です";
                }
                else
                {
                    mLabel.Text = "XSplitは未起動です";
                }

                mLabel.ForeColor = (XSplit.IsBroadCast) ? Color.Red : Color.Black;
            }


            else
            {// FME 関連設定
                Start.Enabled = !FMLE.hasFME();
                Restart.Enabled = FMLE.hasFME();
                Stop.Enabled = FMLE.hasFME();

                mLabel.Text = (FMLE.hasFME()) ? "FMLEが作動中です" : "FMLEは停止中です";
                mLabel.ForeColor = (FMLE.hasFME()) ? Color.Red : Color.Black;
            }
        }

        private void mFMLEProfileList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_boot)
            {
                Properties.Settings.Default.fmle_default_profile = mFMLEProfileList.SelectedItem.ToString();
                if (FMLE.hasFME() && Properties.Settings.Default.use_fme)
                {
                    HQ.Restart(mOwner.LiveID);
                }
            }
        }

    }
}
//-------------------------------------------------------------------------
// FMEステータス
//
// Copyright (c) 金時豆(http://ch.nicovideo.jp/community/co48276)
//-------------------------------------------------------------------------