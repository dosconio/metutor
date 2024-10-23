using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;
using System.Diagnostics;

namespace DemoPremissBlock
{
    public partial class TakeInExtWin : Form // 效果并不满足
    {
        [DllImport("user32.dll")]
        private static extern int SetParent(IntPtr hWndChild, IntPtr hWndParent);

        [DllImport("user32.dll")]
        private static extern bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool PostMessage(IntPtr hWnd, uint Msg, int wParam, int lParam);

        [DllImport("user32.dll", EntryPoint = "SetWindowPos")]
        private static extern bool SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

        [DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, uint Msg, int wParam, int lParam);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern uint SetWindowLong(IntPtr hwnd, int nIndex, uint newLong);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern uint GetWindowLong(IntPtr hwnd, int nIndex);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern bool ShowWindow(IntPtr hWnd, short State);

        Process p = new Process();
        private const int HWND_TOP = 0x0;
        private const int WM_COMMAND = 0x0112;
        private const int WM_QT_PAINT = 0xC2DC;
        private const int WM_PAINT = 0x000F;
        private const int WM_SIZE = 0x0005;
        private const int SWP_FRAMECHANGED = 0x0020;
        public enum ShowWindowStyles : short
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
            SW_MAX = 11
        }
        public TakeInExtWin()
        {
            InitializeComponent();
        }

        private void TakeInExtWin_Load(object sender, EventArgs e)
        {
            //Process p = new Process();
            //// 需要启动的程序
            //p.StartInfo.FileName = @"C:\Windows\System32\notepad.exe";
            //// 为了美观,启动的时候最小化程序
            //p.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
            //// 启动
            //p.Start();
            //// 这里必须等待,否则启动程序的句柄还没有创建,不能控制程序
            //Thread.Sleep(1000);
            //// 最大化启动的程序
            //ShowWindow(p.MainWindowHandle, (short)ShowWindowStyles.SW_MAXIMIZE);
            //// 设置被绑架程序的父窗口
            //SetParent(p.MainWindowHandle, this.Handle);
            //// 改变尺寸
            //ResizeControl(p);

            string fexePath = @"C:\Windows\System32\notepad.exe"; // 外部exe位置
            //fexePath = @".\DemoPremissBlock2.exe";
            


            p.StartInfo.FileName = fexePath;
            p.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
            p.Start();

            while (p.MainWindowHandle.ToInt32() == 0)
            {
                System.Threading.Thread.Sleep(100);
            }
            SetParent(p.MainWindowHandle, this.panel1.Handle);
            ShowWindow(p.MainWindowHandle, (int)ProcessWindowStyle.Maximized);


        }
        // 控制嵌入程序的位置和尺寸
        private void ResizeControl(Process p)
        {
            SendMessage(p.MainWindowHandle, WM_COMMAND, WM_PAINT, 0);
            PostMessage(p.MainWindowHandle, WM_QT_PAINT, 0, 0);

            SetWindowPos(
                p.MainWindowHandle,
                /*HWND_TOP*/0,
                0,  // 设置偏移量,把原来窗口的菜单遮住
                0,
                (int)this.panel1.Width,
                (int)this.panel1.Height,
                SWP_FRAMECHANGED);

            SendMessage(p.MainWindowHandle, WM_COMMAND, WM_SIZE, 0);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel1.Width += 10;
            ResizeControl(p);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            p.CloseMainWindow();
            Text = p.HasExited.ToString();//具有延时性
            
        }
    }
}
