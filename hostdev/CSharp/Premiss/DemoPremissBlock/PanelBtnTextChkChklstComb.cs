using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DemoPremissBlock
{
    public partial class PanelBtnTextChkChklstComb : Form
    {
        static int i=0;
        public PanelBtnTextChkChklstComb()
        {
            InitializeComponent();
            
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //textBox1.Capture = true;
            //timer1.Start();


        }
        private void ShowMessage(string msg)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new CSRunCmdReIO.ShowMessageHandler(ShowMessage), new object[] { msg });
            }
            else
            {
                if (msg != null)
                {
                    textBox2.AppendText(msg + "\r\n");
                }
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Text =(++i).ToString()+( (this.textBox1.Capture)?"t":"f");
            
        }

        private void PanelBtnTextChkChklstComb_Load(object sender, EventArgs e)
        {
            CSRunCmdReIO csr = new CSRunCmdReIO();
            csr.LoadShowMessage(this.ShowMessage);
            csr.Exe("cmd.exe","cmd /c echo \"Hello,World!\"");
            this.FormBorderStyle = FormBorderStyle.None;
        }
    }
}
