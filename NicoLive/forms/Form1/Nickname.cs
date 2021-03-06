﻿//-------------------------------------------------------------------------
// Main Form
//
// Copyright (c) 金時豆(http://ch.nicovideo.jp/community/co48276)
// $Id$
//-------------------------------------------------------------------------
using System;
using System.Threading;
using System.Windows.Forms;


namespace NicoLive
{
    partial class Form1
    {
        //-------------------------------------------------------------------------
        // ユーザー名取得
        //-------------------------------------------------------------------------
        private void GetUsername(string iUserID)
        {
            if (!Properties.Settings.Default.auto_username) return;

            int id;
            if (!int.TryParse(iUserID, out id)) return;
            if (id == 900000000 || id == 394) return;    // クルーズはユーザーページ見れない

            if (!mGatherUserID.Contains(iUserID))
            {
                mGatherLock.AcquireWriterLock(Timeout.Infinite);
                mGatherUserID.Add(iUserID);
                mGatherLock.ReleaseWriterLock();
            }
        }

        //-------------------------------------------------------------------------
        // ユーザー名取得
        //-------------------------------------------------------------------------
        public void GetUsername(string iUserID, bool iReset)
        {
            if (iReset)
            {
                mUid.delNickname(iUserID);
            }
            GetUsername(iUserID);
        }




        //-------------------------------------------------------------------------
        // コテハン記憶
        //-------------------------------------------------------------------------
        public void SetNickname(string iID, string iName)
        {
            mUid.SetNickname(iID, iName);

            // コテハンを相性用のリストに追加しておく
            if (!mAishouList.Contains(iName))
            {
                mAishouList.Add(iName);
            }

            // メインのコメントリスト修正
            Utils.SetNickname(ref mCommentList, iID, iName);

            mUid.SaveUserID(false);
            mUid.SaveUserID(true);

        }

        //-------------------------------------------------------------------------
        // コテハン記憶
        //-------------------------------------------------------------------------
        private void AddNickname(string iID, string iName)
        {
            if (iName.Length <= 0) return;

            if (mUid.AddNickname(iID, iName))
            {
                // メインのコメントリスト修正
                Utils.SetNickname(ref mCommentList, iID, iName);
            }

            mUid.SaveUserID(false);
            mUid.SaveUserID(true);
        }
    }
}
//-------------------------------------------------------------------------
// Copyright (c) 金時豆(http://ch.nicovideo.jp/community/co48276)
// $Id$
//-------------------------------------------------------------------------
