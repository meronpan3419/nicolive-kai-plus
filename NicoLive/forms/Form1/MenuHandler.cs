//-------------------------------------------------------------------------
// Main Form
//
// Copyright (c) 金時豆(http://ch.nicovideo.jp/community/co48276)
// $Id$
//-------------------------------------------------------------------------
using System;

namespace NicoLive
{
    partial class Form1
    {
        //-------------------------------------------------------------------------
        // Aboutメニュー選択時
        //-------------------------------------------------------------------------
        private void AboutNicoLiveMenu_Click(object sender, EventArgs e)
        {
            VerInfoDialog dlg = new VerInfoDialog();
            dlg.Show();
        }

        //-------------------------------------------------------------------------
        // 初期設定メニュー選択時
        //-------------------------------------------------------------------------
        private void SettingMenu_Click(object sender, EventArgs e)
        {
            SettingDialog dlg = new SettingDialog();
            dlg.Owner = this;
            dlg.Show();
        }

        //-------------------------------------------------------------------------
        // 簡易ビュアーメニュー選択時
        //-------------------------------------------------------------------------
        private void ViewerMenuItem_Click(object sender, EventArgs e)
        {
            if (mViewer == null || mViewer.IsDisposed)
            {
                mViewer = new Viewer(LiveID);
            }
            mViewer.Show();
            mViewer.Activate();
        }

        private void LiveConsoleStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mLiveConsole == null || mLiveConsole.IsDisposed)
            {
                mLiveConsole = new LiveConsole(LiveID);
            }
            mLiveConsole.Show();
            mLiveConsole.Activate();
        }
    }
}
//-------------------------------------------------------------------------
// Copyright (c) 金時豆(http://ch.nicovideo.jp/community/co48276)
// $Id$
//-------------------------------------------------------------------------