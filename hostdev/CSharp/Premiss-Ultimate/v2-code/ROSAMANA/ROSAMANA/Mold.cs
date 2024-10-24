using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace ROSAMANA
{
    partial class MainForm
    {
        public void ExecFile(string str)
        {
            if(!File.Exists(str))return;
            /*运行这个程序*/
            int rightlocat = str.LastIndexOf('.');
            if (rightlocat < 1) Process.Start(str);
            else
            {
                switch (str.Substring(rightlocat + 1).ToUpper())
                {
                    default:
                        Process.Start(str);
                        break;
                }
            }

        }
    }
}
