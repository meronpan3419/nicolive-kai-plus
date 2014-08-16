using System;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Drawing.Imaging;
using System.Diagnostics;
using System.Windows.Forms;

namespace NicoLive
{
    class Input
    {
        //-------------------------------------------------------------------------

        [DllImport("user32.dll")]
        static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, Int32 wParam, Int32 lParam);


        [DllImport("user32.dll")]
        extern static uint SendInput(
            uint nInputs,
            INPUT[] pInputs,
            int cbSize
            );

        [StructLayout(LayoutKind.Sequential)]
        struct INPUT
        {
            public int type;
            public MOUSEINPUT mi;
        }

        [StructLayout(LayoutKind.Sequential)]
        struct MOUSEINPUT
        {
            public int dx;
            public int dy;
            public int mouseData;
            public int dwFlags;
            public int time;
            public IntPtr dwExtraInfo;
        }
        // dwFlags
        const int MOUSEEVENTF_MOVED = 0x0001;
        const int MOUSEEVENTF_LEFTDOWN = 0x0002;  // 左ボタン Down
        const int MOUSEEVENTF_LEFTUP = 0x0004;  // 左ボタン Up
        const int MOUSEEVENTF_RIGHTDOWN = 0x0008;  // 右ボタン Down
        const int MOUSEEVENTF_RIGHTUP = 0x0010;  // 右ボタン Up
        const int MOUSEEVENTF_MIDDLEDOWN = 0x0020;  // 中ボタン Down
        const int MOUSEEVENTF_MIDDLEUP = 0x0040;  // 中ボタン Up
        const int MOUSEEVENTF_WHEEL = 0x0080;
        const int MOUSEEVENTF_XDOWN = 0x0100;
        const int MOUSEEVENTF_XUP = 0x0200;
        const int MOUSEEVENTF_ABSOLUTE = 0x8000;

        //-------------------------------------------------------------------------
        // 指定位置をマウスでクリック
        //-------------------------------------------------------------------------
        public static void MouseClick(int iX, int iY)
        {
            Cursor.Position = new Point(iX, iY);

            INPUT[] input = new INPUT[2];  // 計3イベントを格納

            input[0].mi.dwFlags = MOUSEEVENTF_LEFTDOWN;
            input[1].mi.dwFlags = MOUSEEVENTF_LEFTUP;

            //input[2].mi.dx = 0;
            //input[2].mi.dy = 0;
            //input[2].mi.dwFlags = MOUSEEVENTF_MOVED | MOUSEEVENTF_ABSOLUTE;

            // 操作の実行
            SendInput(2, input, Marshal.SizeOf(input[0]));
        }

        //-------------------------------------------------------------------------
        // 指定位置をマウスでクリック(send message)
        //-------------------------------------------------------------------------
        public static void MouseClickHWnd(IntPtr hWnd, int iX, int iY)
        {
            //IntPtr hWnd = System.Diagnostics.Process.GetCurrentProcess().MainWindowHandle;
            uint WM_LBUTTONDOWN = 0x201;
            uint WM_LBUTTONUP = 0x202;
            int MK_LBUTTON = 0x1;
            int _lParam = (iX & 0xffff) | (iY << 16);

            SendMessage(hWnd, WM_LBUTTONDOWN, MK_LBUTTON, _lParam);
            SendMessage(hWnd, WM_LBUTTONUP, MK_LBUTTON, _lParam);
        }
    }
}
