using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;

namespace ROSAMANA
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            resomana.LoadRPartition();
            MainForm mfrom = new MainForm(args);
            Application.Run(mfrom);
            //Application.Run(new TestForm());

        }

    }
}
