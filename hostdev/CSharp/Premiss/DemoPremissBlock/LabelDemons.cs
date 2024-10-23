using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace DemoPremissBlock
{
    /// <summary>
    /// <date>REW 09</date>
    /// </summary>
    class LabelDemons:Form
    {
        /*新建项目为类库时，项目不会自动引用System.Windows.Forms
         * 当我们使用命名空间System.Windows.Forms时，就会报这个错误
         * 解决方法：
         * 右键引用——添加引用——框架——搜索System.Windows.Forms添加即可*/
        private Label LabelDem;
        
        public  LabelDemons()
        {
            LabelDem = new Label();

            this.SuspendLayout();

            this.Controls.Add(LabelDem);
            this.LabelLayout();
            this.ResumeLayout();
            this.SizeChanged+=Resize;
            Resize(null, null);
            //MessageBox.Show(@"Test.");
        }
        private void LabelLayout()
        {

            LabelDem.Top = 0;
            LabelDem.Left = 0;
            LabelDem.Text = "Hello, World !";
            LabelDem.AutoSize = false;

            FontDialog fontDialog= new FontDialog();
            fontDialog.ShowColor=true;
            fontDialog.ShowDialog();
            LabelDem.Font = fontDialog.Font;

            Graphics g = LabelDem.CreateGraphics();
            SizeF StrSize = g.MeasureString(LabelDem.Text, LabelDem.Font);
            Size FixedSize = new Size(Width - ClientSize.Width, Height - ClientSize.Height);
            // 没有考虑间隙
            this.MinimumSize= new Size(FixedSize.Width+(int)StrSize.Width, FixedSize.Height+(int)StrSize.Height);
            LabelDem.RightToLeft = RightToLeft.Yes;
        }

        private void Resize(object sender, EventArgs e)
        {
            // Anchor and Dock
            LabelDem.Height = ClientSize.Height;
            LabelDem.Width = ClientSize.Width;
        }

    }
}
