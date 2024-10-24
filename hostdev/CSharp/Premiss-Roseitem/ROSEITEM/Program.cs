using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ROSEITEM
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            //Only accept [RoseOnlyName]
            resomana.LoadRPartition();
            //rose r = new rose("E:\\1.a");
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMain(args));
        }
    }
}
