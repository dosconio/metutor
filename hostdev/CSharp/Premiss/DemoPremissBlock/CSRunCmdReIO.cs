using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DemoPremissBlock
{
    class CSRunCmdReIO
    {
        public delegate void ShowMessageHandler(string msg);
        ShowMessageHandler ShowMessage ;
        public void LoadShowMessage(ShowMessageHandler smh){
            this.ShowMessage=smh;
        }
        //调用函数,
        public void Exe(string filename, string args)
        {
            Process cmd = new Process();
            cmd.StartInfo.FileName = filename;
            cmd.StartInfo.Arguments = args;
            //将cmd的标准输入和输出全部重定向到.NET的程序里 
            cmd.StartInfo.RedirectStandardInput = true;//标准输入 
            cmd.StartInfo.RedirectStandardOutput = true;//标准输出 
            cmd.StartInfo.UseShellExecute = false;//此处必须为false否则引发异常 
            //
            cmd.StartInfo.CreateNoWindow = true;//不显示命令行窗口
            cmd.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            //
            ThreadPool.QueueUserWorkItem(new WaitCallback(ExeThread), cmd);
        }
        private  void ExeThread(object obj)
        {
            Process cmd = obj as Process;
            cmd.Start();
            cmd.OutputDataReceived += new DataReceivedEventHandler(cmd_OutputDataReceived);
            cmd.BeginOutputReadLine();
            //
            Application.DoEvents();
            cmd.WaitForExit();
            if (cmd.ExitCode != 0)
            {
                ShowMessage(cmd.StandardOutput.ReadToEnd());
            }
            cmd.Close();
        }
        public  void cmd_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            ShowMessage(e.Data);
        }
        
        ///public ShowMessageHandler ShowMessage(string msg);

        //private void ShowMessage(string msg)
        //{
        //    if (this.InvokeRequired)
        //    {
        //        this.BeginInvoke(new ShowMessageHandler(ShowMessage), new object[] { msg });
        //    }
        //    else
        //    {
        //        if (msg != null)
        //        {
        //            textBox1.AppendText(msg + "/r/n");
        //        }
        //    }
        //}
    }
}
