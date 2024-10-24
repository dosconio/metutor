using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ROSEITEM
{
    public partial class FormMain : Form
    {
        rose _r = null;
        List<string> RoseEvolv;
        List<string> ONameForListItems;
        public FormMain(string[] args)
        {
            RoseEvolv = new List<string>();
            InitializeComponent();
            if (args.Length > 0 && args[0].Length > 0) 
            {
                OpenRoseItem(args[0]);
                RoseEvolv.Add(args[0]);
            }
            Refr();
        }
        private void FormMain_Load(object sender, EventArgs e)
        {
            //webBrowser1.Url = new Uri(@"E:/Premiss2021/tmp02.html");
            
        }
        private void Refr()
        {
            SuspendLayout();
            //ListAlbum
            listAlb.Items.Clear();
            string ThisPath = Application.StartupPath;//only add \ in root path
            if (ThisPath[ThisPath.Length - 1] != '\\') ThisPath += "\\";
            if (File.Exists(ThisPath + "ALBUM")) 
            {
                string[] TmpAlbums = File.ReadAllText(ThisPath + "ALBUM").Split(new char[]{'\n'});
                for (int i = 0; i < TmpAlbums.Length; i++)
                {
                    if (TmpAlbums[i].Trim() != "")
                    {
                        listAlb.Items.Add(TmpAlbums[i].Trim());
                    }
                }
            }
            

            ResumeLayout();
        }
        private void OpenRoseItem(string onlyname)
        {
            string TmpFile = rose.CnrvLocation("ROS\\"+onlyname);
            if (TmpFile == null || TmpFile == "") return;
            _r = new rose(TmpFile);
            string ThisPath = Application.StartupPath;//only add \ in root path
            if (ThisPath[ThisPath.Length - 1] != '\\') ThisPath += "\\";
            if (!File.Exists(ThisPath + "TEMP\\" + onlyname + ".html"))
                RemakeLastHTML(TmpFile, ThisPath + "TEMP\\" + onlyname + ".html");
            //------------------
            webBrowser1.Url = new Uri("File:\\\\" + ThisPath + "TEMP\\" + onlyname + ".html");
            this.Text = onlyname;
        }
        private void RemakeLastHTML(string FromRealPath,string DestRealPath)
        {
            File.WriteAllText(DestRealPath, CompRoseItem(FromRealPath), Encoding.UTF8);
        }
        private string CompRoseItem(string RealPath)
        {
            _r = new rose(RealPath);
            string HTML = "<!DOCTYPE html><html lang=\"en\"><head><meta charset=\"utf-8\"><title>" + _r.rbase.oname + "</title></head><body>";

            foreach (var v in _r.subitems)
            {
                HTML += v + "\n";
            }
            HTML += "<br><hr>";

            HTML += "<table style=\"font-family:\'Consolas\';font-size: 16px;\"><tr><td>[Name]</td><td>";
            if (_r.rbase.iname.Count == 0) HTML += _r.rbase.oname + "; ";
            foreach (var v in _r.rbase.iname)
            {
                HTML += v + "; ";
            }
            HTML += "</td></tr><tr><td>";

            if (File.Exists(rose.CnrvLocation("ros\\" + _r.rbase.parent)))
            {
                HTML += "[Class]</td><td>" + "<a href=\"file:\\\\" + rose.CnrvLocation("ros\\" + _r.rbase.parent) + "\">";
                HTML += _r.rbase.parent + "</a>";
            }
            else
            {
                HTML += "[Class]</td><td>" + _r.rbase.parent;
            }

            if (_r.rbase.children.Count > 0)
                HTML += "</td></tr><tr><td rowspan=\"" + _r.rbase.children.Count.ToString() + "\">[Include]</td><td>";
            else HTML += "</td></tr><tr><td>";
            for (int i=0; i < _r.rbase.children.Count; i++)
            {
                if (File.Exists(rose.CnrvLocation("ros\\" + _r.rbase.children[i])))
                    HTML += "<a href=\"file:\\\\" + rose.CnrvLocation("ros\\" + _r.rbase.children[i]) + "\">" + _r.rbase.children[i] + "</a></td></tr><tr><td>";
                else
                    HTML += _r.rbase.children[i] + "</td></tr><tr><td>";
            }

            HTML += "[Creator]</td><td>" + _r.rbase.author;
            HTML += "</td></tr><tr><td>";
            HTML += "[Source]</td><td>" + _r.rbase.source;
            HTML += "</td></tr><tr><td>";
            HTML += "[LastMake]</td><td>" + DateTime.Now.ToString();
            HTML += "</td></tr><tr><td>";
            //display properties
            if (_r.PropName.Count == _r.PropVal.Count)
            {
                for (int i = 0; i < _r.PropName.Count; i++)
                {
                    HTML += "[" + _r.PropName[i] + "]</td><td>" + _r.PropVal[i] + "</td></tr><tr><td>";
                }
            }


                HTML += "</td></tr></table>";




            HTML += "</body></html>";
            return HTML;
        }

        private void RefButton_Click(object sender, EventArgs e)
        {
            Refr();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (_r != null && _r.rbase.oname != "") 
            {
                string ThisPath = Application.StartupPath;//only add \ in root path
                if (ThisPath[ThisPath.Length - 1] != '\\') ThisPath += "\\";
                RemakeLastHTML(rose.CnrvLocation("ROS\\" + _r.rbase.oname), ThisPath + "TEMP\\" + _r.rbase.oname + ".html");
                OpenRoseItem(_r.rbase.oname);
            }
        }


        private void webBrowser1_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            string u=e.Url.ToString();
            if (u[u.Length - 1] == 'R' && u[u.Length - 2] == '.')
            {
                int p=u.LastIndexOf('/');
                if (p > 3)
                {
                    e.Cancel = true;
                    OpenRoseItem(u.Substring(p + 1, u.Length - 3 - p));
                    if (RoseEvolv.Count > 0)
                        RoseEvolv.Add(u.Substring(p + 1, u.Length - 3 - p));
                }
            }
        }

        private void toolStripTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 0x0D)
            {
                OnSearch();
            }
        }

        private void listAlb_DoubleClick(object sender, EventArgs e)
        {
            if (listAlb.SelectedIndex >= 0)
            {
                tabControl1.SelectedIndex = 1;
                string ThisPath = Application.StartupPath;//only add \ in root path
                if (ThisPath[ThisPath.Length - 1] != '\\') ThisPath += "\\";
                listItems.Items.Clear();
                ONameForListItems=new List<string>();
                string[] TmpListItems = File.ReadAllText(ThisPath+listAlb.SelectedItems[0]).Split(new char[]{'\n'});
                for (int i = 0; i < TmpListItems.Length; i++)
                {
                    string _t = TmpListItems[i].Trim();
                    int ptr = _t.IndexOf(":");
                    if (ptr > 0)
                    {
                        listItems.Items.Add(_t.Substring(0, ptr));
                        ONameForListItems.Add(_t.Substring(ptr + 1));
                    }
                }
            }
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            if (RoseEvolv.Count > 1)
            {
                OpenRoseItem(RoseEvolv[RoseEvolv.Count - 2]);
                RoseEvolv.RemoveAt(RoseEvolv.Count - 1);
            }
        }

        private void listItems_DoubleClick(object sender, EventArgs e)
        {
            if (listItems.SelectedIndex >= 0 && ONameForListItems.Count == listItems.Items.Count)
            {
                string dest = rose.CnrvLocation("ROS\\" + ONameForListItems[listItems.SelectedIndex]);
                if (dest == "")
                {
                    MessageBox.Show("O.O Lost.");
                }
                else
                {
                    OpenRoseItem(ONameForListItems[listItems.SelectedIndex]);
                    RoseEvolv.Add(ONameForListItems[listItems.SelectedIndex]);
                }
            }
        }

        private void GotoButton_Click(object sender, EventArgs e)
        {
            OnSearch();
        }
        private void OnSearch()
        {
            if (toolStripTextBox1.Text.Trim() != "")
            {
                string TmpIndxPath = rose.CnrvLocation("IDX\\" + toolStripTextBox1.Text.Trim());
                if (TmpIndxPath != "")
                {
                    listItems.Items.Clear();
                    ONameForListItems = new List<string>();
                    string[] TmpIndxItems = File.ReadAllText(TmpIndxPath).Split(new char[] { '\n' });
                    for (int i = 0; i < TmpIndxItems.Length; i++)
                    {
                        string _t = TmpIndxItems[i].Trim();
                        int ptr = _t.IndexOf(":");
                        if (ptr > 0)
                        {
                            listItems.Items.Add(_t.Substring(0, ptr));
                            ONameForListItems.Add(_t.Substring(ptr + 1));
                        }
                    }
                }
                else
                {
                    listItems.Items.Clear();
                    //ONameForListItems.Clear();
                    ONameForListItems = new List<string>();
                }
                tabControl1.SelectedIndex = 1;
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            //Application.Run(new AlbumMana());
            AlbumMana alm = new AlbumMana();
            alm.ShowDialog();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            IndxMana idm = new IndxMana();
            idm.ShowDialog();
        }
    }
}
