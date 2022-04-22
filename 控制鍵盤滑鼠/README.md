# C# 控制鍵盤滑鼠

使用windows內建user32.dll
鍵盤使用SendKeys.Send,若要送出ESC,使用SendKeys.Send("{ESC}")



```csharp
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;

namespace AutoNamespace
{
    class Auto
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool SetCursorPos(int x, int y);

        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(out POINT lpPoint);

        [DllImport("user32.dll")]
        public static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

        [DllImport("user32.dll")]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hwnd, uint wMsg, int wParam, int lParam);

        [DllImport("user32.dll")]
        static extern IntPtr FindWindowEx(IntPtr hwndParent, uint hwndChildAfter, string lpszClass, string lpszWindow);

        [DllImport("user32.dll")]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);


        [DllImport("user32")]
        public static extern int ShowWindow(int hwnd, int nCmdShow);



        private const int MOUSEEVENTF_LEFTDOWN = 0x02;
        private const int MOUSEEVENTF_LEFTUP = 0x04;
        private const int MOUSEEVENTF_RIGHTDOWN = 0x08;
        private const int MOUSEEVENTF_RIGHTUP = 0x10;

        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int X;
            public int Y;

        }

        private enum CommandShow : int
        {
            SW_HIDE = 0,
            SW_SHOWNORMAL = 1,
            SW_NORMAL = 1,
            SW_SHOWMINIMIZED = 2,
            SW_SHOWMAXIMIZED = 3,
            SW_MAXIMIZE = 3,
            SW_SHOWNOACTIVATE = 4,
            SW_SHOW = 5,
            SW_MINIMIZE = 6,
            SW_SHOWMINNOACTIVE = 7,
            SW_SHOWNA = 8,
            SW_RESTORE = 9,
            SW_SHOWDEFAULT = 10,
            SW_FORCEMINIMIZE = 11,

        }
        public static void Left()
        {
            POINT lpPoint;
            GetCursorPos(out lpPoint);
            mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, lpPoint.X, lpPoint.Y, 0, 0);
        }
        public static void Right()
        {
            POINT lpPoint;
            GetCursorPos(out lpPoint);
            mouse_event(MOUSEEVENTF_RIGHTDOWN | MOUSEEVENTF_RIGHTUP, lpPoint.X, lpPoint.Y, 0, 0);
            Thread.Sleep(20);
        }
        public static void LeftDouble()
        {
            POINT lpPoint;
            GetCursorPos(out lpPoint);
            mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, lpPoint.X, lpPoint.Y, 0, 0);
            Thread.Sleep(20);
            mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, lpPoint.X, lpPoint.Y, 0, 0);
        }

        public static void Move(int X, int Y)
        {
            SetCursorPos(X, Y);
        }
        public static void SendKey(string send)
        {
            SendKeys.Send(send);
        }

        public static void ShowWindow(string windowName)
        {
            ShowWindow(windowName, CommandShow.SW_SHOWMAXIMIZED);
        }

        public static void MiniumWindow(string windowName)
        {
            ShowWindow(windowName, CommandShow.SW_SHOWMINIMIZED);
        }

        private static void ShowWindow(string window,CommandShow cmd)
        {
            Process[] procs = Process.GetProcesses(); ;
            foreach (Process proc in procs)
            {
                if (proc.MainWindowTitle.Contains(window))
                {
                    int hwnd = proc.MainWindowHandle.ToInt32();
                    ShowWindow(hwnd, (int)cmd);
                }
            }   
        }

        public static void printScreen(string pathFile)
        {
            Image I; 
            Graphics G;
            I = new Bitmap(Screen.PrimaryScreen.Bounds.Size.Width, Screen.PrimaryScreen.Bounds.Size.Height);
            G = Graphics.FromImage(I);
            G.CopyFromScreen(new Point(0, 0), new Point(0, 0), new Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height), CopyPixelOperation.SourceCopy);
            //G.CopyFromScreen(0, 0, 0, 0, Screen.PrimaryScreen.Bounds.Size);
            I.Save(pathFile, ImageFormat.Jpeg);       

        }


    }


}

```