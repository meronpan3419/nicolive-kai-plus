using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System;
using System.Diagnostics;


namespace NicoLive
{
    class Keyboard
    {

        [DllImport("user32.dll")]
        static extern IntPtr PostMessage(IntPtr hWnd, UInt32 Msg, UInt32 wParam, UInt32 lParam);



        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern uint MapVirtualKey(uint uCode, uint uMapType);


        public static void KeyboardInput(uint inputkeys)
        {
            IntPtr hXSplitWnd = IntPtr.Zero;
            foreach (Process p in Process.GetProcessesByName("XSplit.Core"))
            {
                if (p.MainWindowHandle != IntPtr.Zero)
                {
                    hXSplitWnd = p.MainWindowHandle;
                    break;
                }
            }
            KeyboardInput((IntPtr)0x80754, inputkeys);
        }

        public static void KeyboardInput(IntPtr hWnd, uint inputkeys)
        {
            const int WM_KEYDOWN = 0x100;
            const int WM_KEYUP = 0x101;

            const int VK_SHIFT = 0x10;
            const int VK_CTRL = 0x11;
            const int VK_ALT = 0x12;

            uint scanCodeS = MapVirtualKey((uint)VK_SHIFT, 0);
            uint lParamS = (0x00000001 | (scanCodeS << 16));
            uint scanCodeC = MapVirtualKey((uint)VK_CTRL, 0);
            uint lParamC = (0x00000001 | (scanCodeC << 16));
            uint scanCodeA = MapVirtualKey((uint)VK_ALT, 0);
            uint lParamA = (0x00000001 | (scanCodeA << 16));
            uint scanCodeK = MapVirtualKey((uint)(inputkeys & 0xFFFF), 0);
            uint lParamK = (0x00000001 | (scanCodeK << 16));


            if (((inputkeys >> 16) & 0x1) == 0x1)   // shift
            {
                PostMessage(hWnd, WM_KEYDOWN, VK_SHIFT, lParamS);
                System.Threading.Thread.Sleep(250);
            }
            if (((inputkeys >> 17) & 0x1) == 0x1)   // ctrl
            {
                PostMessage(hWnd, WM_KEYDOWN, VK_CTRL, lParamC);
                System.Threading.Thread.Sleep(250);
            }
            if (((inputkeys >> 18) & 0x1) == 0x1)   // alt
            {
                PostMessage(hWnd, WM_KEYDOWN, VK_ALT, lParamA);
                System.Threading.Thread.Sleep(250);
            }

            PostMessage(hWnd, WM_KEYDOWN, (inputkeys & 0xFFFF), lParamK);

            System.Threading.Thread.Sleep(250);

            PostMessage(hWnd, WM_KEYUP, (inputkeys & 0xFFFF), lParamK | 0xC0000000);

            System.Threading.Thread.Sleep(250);

            if (((inputkeys >> 16) & 0x1) == 0x1)   // shift
            {
                PostMessage(hWnd, WM_KEYUP, VK_SHIFT, lParamS | 0xC0000000);
                System.Threading.Thread.Sleep(250);
            }
            if (((inputkeys >> 17) & 0x1) == 0x1)   // ctrl
            {
                PostMessage(hWnd, WM_KEYUP, VK_CTRL, lParamC | 0xC0000000);
                System.Threading.Thread.Sleep(250);
            }
            if (((inputkeys >> 18) & 0x1) == 0x1)   // alt
            {
                PostMessage(hWnd, WM_KEYUP, VK_ALT, lParamA | 0xC0000000);
                System.Threading.Thread.Sleep(250);
            }



        }
    }

}