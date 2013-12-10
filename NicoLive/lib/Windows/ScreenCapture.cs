//-------------------------------------------------------------------------
// スクリーンキャプチャクラス
//
// Copyright (c) 金時豆(http://ch.nicovideo.jp/community/co48276)
// $Id$
//-------------------------------------------------------------------------
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Diagnostics;
using System.Runtime.InteropServices;

//-------------------------------------------------------------------------
// クラス実装
//-------------------------------------------------------------------------
namespace NicoLive
{
    class ScreenCapture : IDisposable
    {
        // ウィンドウハンドルでウィンドウキャプチャ
        [DllImport("User32.dll")]        
        private extern static bool PrintWindow(IntPtr hwnd, IntPtr hDC, uint nFlags);

        // ウィンドウハンドルでクライアント領域サイズ取得
        [DllImport("User32.dll")]
        private extern static bool GetClientRect(IntPtr hWnd, ref RECT rect);

        public struct RECT
        {

            public int left;
            public int top;
            public int right;
            public int bottom;

            public RECT(int l, int t, int r, int b)
            {
                left = l;
                top = t;
                right = r;
                bottom = b;
            }
        }

        //-------------------------------------------------------------------------
        // スクリーンショット取得
        //-------------------------------------------------------------------------
        public Bitmap Capture(Rectangle iBounds)
        {
            Rectangle rc = iBounds;

            Bitmap bmp = new Bitmap(rc.Width, rc.Height, PixelFormat.Format24bppRgb);

            using (Graphics g = Graphics.FromImage(bmp))
            {
                try
                {
                    g.CopyFromScreen(rc.X, rc.Y, 0, 0, rc.Size, CopyPixelOperation.SourceCopy);
                }
                catch (Exception e)
                {
                    Utils.WriteLog(e.Message);
                }

            }

            //--------------- DEBUG --------------------
            //string filePath = @"C:\screen.bmp";
            //bmp.Save(filePath, ImageFormat.Bmp);
            //Process.Start(filePath);
            //------------------------------------------

            return bmp;
        }



        //-------------------------------------------------------------------------
        // スクリーンショット取得
        //-------------------------------------------------------------------------
        public Bitmap Capture2(IntPtr iHwnd)
        {

            if (iHwnd == IntPtr.Zero)
            {
                return null;
            }
            RECT _rect = new RECT();


            GetClientRect(iHwnd, ref _rect);

            Rectangle rc = new System.Drawing.Rectangle(0, 0, _rect.right, _rect.bottom);

            Bitmap bmp = new Bitmap(rc.Width, rc.Height, PixelFormat.Format24bppRgb);

            using (Graphics g = Graphics.FromImage(bmp))
            {
                try
                {
                    //g.CopyFromScreen(rc.X, rc.Y, 0, 0, rc.Size, CopyPixelOperation.SourceCopy);
                    IntPtr dc = g.GetHdc();
                    PrintWindow(iHwnd, dc, 0);
                }
                catch (Exception e)
                {
                    Utils.WriteLog(e.Message);
                }

            }




            //--------------- DEBUG --------------------
            string filePath = @"C:\screen.bmp";
            bmp.Save(filePath, ImageFormat.Bmp);
            //Process.Start(filePath);
            //------------------------------------------

            return bmp;
        }

        //-------------------------------------------------------------------------
        // 解放
        //-------------------------------------------------------------------------
        public void Dispose()
        {
        }
    }
}
//-------------------------------------------------------------------------
// スクリーンキャプチャクラス
//
// Copyright (c) 金時豆(http://ch.nicovideo.jp/community/co48276)
//-------------------------------------------------------------------------
