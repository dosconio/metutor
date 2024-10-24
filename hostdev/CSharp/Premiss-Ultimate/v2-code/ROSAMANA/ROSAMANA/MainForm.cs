using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ROSAMANA
{
    public partial class MainForm : Form
    {
        rose r;
        public MainForm(string[] args)
        {
            InitializeComponent();
            Init();
            if (args.Length > 0 && args[0].Length > 3) //"E:/1"
            {
                string lefts = (args[0].Substring(0, 3).ToUpper());
                if (lefts == "ROS" || lefts == "URL")
                {
                    Refr(rose.CnrvLocation(args[0]));
                }
                else Refr(args[0]);
            }
            RefrProperty_RPart();

        }
        private void RefrProperty_RPart()
        {
            string rplst="";
            foreach (var v in resomana.RPARTITIONS)
            {
                rplst += v[0] + ":" + v.Substring(1)+"\n";
            }
            pgRPlabel.Text = rplst;
        }
        private void Refr(string url)
        {
            if (!File.Exists(url)) return;
            r = new rose(url, this.Clkx);
            Label.Text = r.rbase.oname;

            {
                PropertyListView.Items.Clear();

                string namestr = "";
                if (r.rbase.iname != null) 
                foreach (var v in r.rbase.iname)
                {
                    namestr += v + ";";
                }
                PropertyListView.Items.Add(new ListViewItem(new string[] { Language.Lang("Name"), namestr }));

                PropertyListView.Items.Add(new ListViewItem(new string[] { Language.Lang("Author"), r.rbase.author }));
                PropertyListView.Items.Add(new ListViewItem(new string[] { Language.Lang("CreateDateTime"), r.rbase.create_datetime }));
                PropertyListView.Items.Add(new ListViewItem(new string[] { Language.Lang("LastDateTime"), r.rbase.lastdo_datetime }));
                PropertyListView.Items.Add(new ListViewItem(new string[] { Language.Lang("Source"), r.rbase.source }));


                string extext = "";
                foreach (var v in r.ExProperty)
                {
                    if (v[0] == '*')
                        extext += v + "\n";
                }
                MenuLabel.Text = extext;

            }

            for (int i = 0; i < r.rbase.tabs.Count; i++)
            {
                this.ComboBox.Items.Add(r.rbase.tabs[i].tabname);
                r.rbase.tabs[i].panel.Visible = false;
                splitContainer1.Panel2.Controls.Add(r.rbase.tabs[i].panel);
            }
            toolStrip.BackColor = r.rbase.LabelBackColor;
            if (File.Exists(r.rbase.BackgroundImagePath))
            splitContainer1.Panel2.BackgroundImage = (System.Drawing.Image.FromFile(r.rbase.BackgroundImagePath));

            if (splitContainer1.Panel2.Controls.Count > 0)
            {
                ComboBox.SelectedIndex = 0;
                RefrTab(0);
            }
        }
        private void RefrTab(int id)
        {
            
            //if (r.rbase.tabs.Count <= id) return;
            if (splitContainer1.Panel2.Controls.Count <= id) return;
            for (int i = 0; i < splitContainer1.Panel2.Controls.Count; i++) 
            {
                if   (i == id)
                     splitContainer1.Panel2.Controls[i].Visible = true;
                else splitContainer1.Panel2.Controls[i].Visible = false;
            }
            Text = Label.Text + " - " + ComboBox.Items[ComboBox.SelectedIndex];
        }
        private void Init()
        {
            SuspendLayout();
            // load language mold

            JP_LANG:
            //donot change to EN(USA),it zo original language!
            //【NEXT VERSION】PUT LANGUAGE PACK SUPPORT
            {
                Language.mode = (1);
                menuStrip1.Items[0].Text = Language.Lang("Program");
                    loadToolStripMenuItem.Text = Language.Lang("Load");
                    newToolStripMenuItem.Text = Language.Lang("New");
                    refreshToolStripMenuItem.Text = Language.Lang("Refresh");
                    saveToolStripMenuItem.Text = Language.Lang("Save");
                    cosaveToolStripMenuItem.Text = Language.Lang("CoSave");
                    closeToolStripMenuItem.Text = Language.Lang("Close");
                    exitToolStripMenuItem.Text = Language.Lang("Exit");
                menuStrip1.Items[1].Text = Language.Lang("Setting");
                menuStrip1.Items[2].Text = Language.Lang("Expack");
                    addToolStripMenuItem.Text = Language.Lang("add");
                    listToolStripMenuItem.Text = Language.Lang("list");
                menuStrip1.Items[3].Text = Language.Lang("View");
                    codeViewToolStripMenuItem.Text = Language.Lang("codeview");
                menuStrip1.Items[4].Text = Language.Lang("Rose");
                    bindFolderToolStripMenuItem.Text = Language.Lang("bindFolder");
                    newElmToolStripMenuItem.Text = Language.Lang("AddElm");
                //ToolStrip Omit
                    tabControl1.TabPages[0].Text = Language.Lang("property");
                    tabControl1.TabPages[1].Text = Language.Lang("menu");
                    tabControl1.TabPages[2].Text = Language.Lang("partition");
            }

            ResumeLayout();
        }
        private void MainForm_SizeChanged(object sender, EventArgs e)
        {

        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            debugf();
        }
        public void Clkx(object sender, EventArgs e, string msg)
        {
            //MessageBox.Show(msg);
            ExecFile(rose.CnrvLocation(msg));
        }
        void debugf()
        {
            //;动态改变MenuStrip内容
            //menuStrip1.Items[0].Text = "&Test";

        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd=new OpenFileDialog();
            ofd.ShowDialog();
            if (File.Exists(ofd.FileName))
            {
                Refr(ofd.FileName);
            }
        }

        private void cosaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("...");
        }

        private void settingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(this.Size.ToString());
        }

        private void tableLabel1__OnLblClick(object sender, EventArgs e, string msg)
        {
            MessageBox.Show(msg);
        }

        private void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefrTab(ComboBox.SelectedIndex);
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("...");
        }

        private void PropertyListView_DoubleClick(object sender, EventArgs e)
        {
            if (PropertyListView.SelectedItems.Count > 0) 
            MessageBox.Show(PropertyListView.Items[PropertyListView.SelectedItems[0].Index].SubItems[1].ToString());
        }

        private void codeViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("The Version Don't Clude the Function.");
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Text = "";
            Label.Text = "";
            splitContainer1.Panel2.Controls.Clear();
            ComboBox.Items.Clear();
            PropertyListView.Items.Clear();
            MenuLabel.Text = "";
            pgRPlabel.Text = "";
            r = null;
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("...");
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("...");
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("...");
        }

        private void listToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("...");
        }

        private void bindFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("...");
        }

        private void newElmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("...");
        }
    }
}
