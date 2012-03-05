﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Threading;
using System.Diagnostics;
using System.Windows.Forms;


namespace NicoLive
{
    class NLE
    {
        //
        //
        // (1). Formを作成時に、インスタンスを作成
        //      NLE mNLE = new NLE();
        //
        // (2). Timerを用いて、上記インスタンスから、
        //      HandlingStatus() を定期的に呼び出す。
        //      mNLE.HandlingStatus();
        //
        // (3). 配信をスタートする時には、
        //      mNLE.Start()
        //
        // (4). 配信を停止するときには、
        //      mNLE.Stop();
        //
        // (5). NLEが放送中かチェック
        //      mNLE.IsBroadCast でチェック可能。
        //
        // (6). NLEプロセスが存在するかチェック
        //      mNLE.IsAlive でチェック可能
        //
        //

        const string NLEName = "Nicoliveenc";
        const string NLEName2 = "VHMultiWriterExt2";

        private static bool buttonpushed = false;


        public enum NLE_Status { NLE_IDLE = 0, NLE_NEED_STOP, NLE_NEED_START, NLE_NEED_RESTART };

        private static IntPtr hNLEWnd;

        public static NLE_Status require_status = NLE_Status.NLE_IDLE;


        [DllImport("user32.dll")]
        static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, Int32 wParam, Int32 lParam);

        delegate int WNDENUMPROC(IntPtr hwnd, int lParam);

        [DllImport("user32.dll")]
        private static extern int EnumChildWindows(
            IntPtr hWndParent,
            WNDENUMPROC lpEnumFunc,
            int lParam);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern int GetWindowTextLength(IntPtr hWnd);


        static int push_start_button_proc(IntPtr hWndParent, int lParam)
        {
            int nRet;
            StringBuilder ClassName = new StringBuilder(100);
            nRet = GetClassName(hWndParent, ClassName, ClassName.Capacity);
            if (nRet != 0)
            {
                if (ClassName.ToString().Equals("Button"))
                {
                    int length = GetWindowTextLength(hWndParent);
                    StringBuilder sb = new StringBuilder(length + 1);
                    GetWindowText(hWndParent, sb, sb.Capacity);
                    if (sb.ToString().Equals("スタート"))
                    {
                        if (buttonpushed) return 1;
                        uint WM_LBUTTONDOWN = 0x201;
                        uint WM_LBUTTONUP = 0x202;
                        int _lParam = (10 & 0xffff) | (10 << 16);
                        SendMessage(hWndParent, WM_LBUTTONDOWN, _lParam, _lParam);
                        SendMessage(hWndParent, WM_LBUTTONUP, 0, _lParam);
                        buttonpushed = true;
                        return 0;
                    }
                }
            }


            EnumChildWindows(hWndParent, push_start_button_proc, 0);
            return 1;
        }


        private static void push_start_button()
        {
            EnumChildWindows(hNLEWnd, push_start_button_proc, 0);
        }
        private static string Text
        {
            get
            {
                if (hNLEWnd == IntPtr.Zero)
                {
                    FindNLEMainWindow();
                }
                return GetWindowText(hNLEWnd);
            }
        }

        public static bool IsBroadCast
        {
            get
            {
                if (IsAlive)
                {
                    int a = Process.GetProcessesByName(NLEName2).Length;
                    return a > 0;

                }
                return false;
            }
        }



        //-------------------------------------------------------------------------
        // 静的コンストラクタ
        //-------------------------------------------------------------------------
        static NLE()
        {

        }

        //-------------------------------------------------------------------------
        // 静的ディストラクタ
        //-------------------------------------------------------------------------
        public static void Dispose()
        {

        }

        public static void StartNicoLive()
        {

            buttonpushed = false;
            FindNLEMainWindow();
            push_start_button();


        }

        public static void StopNicoLive()
        {

            if (IsBroadCast)
            {
                buttonpushed = false;
                FindNLEMainWindow();
                push_start_button();
            }



        }




        //
        //  配信スタート
        //
        public static void Start()
        {
            if (IsBroadCast)
            {
                StopNicoLive();
            }
            StartNicoLive();
        }

        //
        //  配信ストップ
        //
        public static void Stop()
        {
            if (IsBroadCast)
            {
                StopNicoLive();
            }
        }


        //
        // ウインドウのキャプションをウインドウハンドルから取得
        //
        private static string GetWindowText(IntPtr hWnd, int size = 1024)
        {
            StringBuilder sb = new StringBuilder(size + 1);
            GetWindowText(hWnd, sb, sb.Capacity);
            return sb.ToString();
        }

        //
        // NLEプロセスが生きているか否かのチェック
        //
        public static bool IsAlive
        {
            get
            {
                return Process.GetProcessesByName(NLEName).Length > 0;
            }
        }



        //
        // NLEのメインウインドウを探す
        //
        private static void FindNLEMainWindow()
        {
            foreach (Process p in Process.GetProcessesByName(NLEName))
            {
                if (p.MainWindowHandle != IntPtr.Zero)
                {
                    hNLEWnd = p.MainWindowHandle;
                    return;
                }
            }
            hNLEWnd = IntPtr.Zero;
        }



    }
}
