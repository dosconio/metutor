using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ROSAMANA
{
    public partial class TestForm : Form
    {
        public TestForm()
        {
            InitializeComponent();
        }

        private void TestForm_Load(object sender, EventArgs e)
        {
            //mediaBlk1.BackgroundImage = Image.FromFile(@"E:\PROJ\PREMISS\ROSAMANA\ICON\alicebgi.jpg");
            mediaFilesBlk1.files.Add("File1");
            mediaFilesBlk1.files.Add("File2");
            mediaFilesBlk1.files.Add("File3");
            mediaFilesBlk1.eventid.Add("str1");
            mediaFilesBlk1.eventid.Add("str2");
            mediaFilesBlk1.filesnoimg.Add(@"E:\a.jpg");
            mediaFilesBlk1.filesnoimg.Add(@"E:\b.jpg");
            mediaFilesBlk1.filesnoimg.Add(@"E:\c.jpg");
            mediaFilesBlk1.Refr(0);
            
            //table t = new table(Clk);
            //TableLayoutPanel tlp = t.GetTablexCtrl(@"E:\1.tbl");
            //tlp.Dock = DockStyle.Fill;
            //tlp.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            //Controls.Add(tlp);
        }
        private void Clk(object sender, EventArgs e, string msg)
        {
            Text = msg;
        }

        private void mediaBlk1__OnLblClick(object sender, EventArgs e, string msg)
        {
            Text = "ONCLICK";
        }

        private void mediaBlk1__OnLeftClick(object sender, EventArgs e, string msg)
        {
            Text = "TURNLEFT";
        }

        private void mediaBlk1__OnRightClick(object sender, EventArgs e, string msg)
        {
            Text = "TURNRIGHT";
        }

        private void mediaFilesBlk1__OnLblClick(object sender, EventArgs e, string msg)
        {
            Text = msg;
        }
    }
}
