using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ROSAMANA
    //ver=3
{
    public struct tabpage
    {
        public string tabname;
        public Panel panel;
    }
    public struct rose_base
    {
        public string oname;
        public string parent;
        public byte type;
        public string author;
        public string source;
        public string lastdo_datetime;
        public string create_datetime;
        public List<string> children;
        public List<string> iname;
        //---
        public List<tabpage> tabs;
        public List<string> labels;
        public Color BackgroundColor;
        public int TransparentPer;//背景色透明度
        public string BackgroundImagePath;
        public Color LabelBackColor;
        public int LabelTrandPer;
        public string LabelBgIP;
        public string FontName;
        //注意是否为NULL
    }
    class rose
    //each rose item own one :
        //ONAME: C:\ROSE\ONAME
        //INAME: C:\INDX\INAME
        //OROSE: C:\LIBR\RESO\ONAME\
        //-----
        //FINTFILE: \LIBR\FONT\FONTNAME
    {
        public bool IsLoaded = false;//是否可用
        public List<string> ExProperty;//except from base
        public rose_base rbase;
        public /*TableLabel.ClickHandle*/ TableText.ClickHandle Clk;
        public rose(string url, /*TableLabel.ClickHandle*/TableText.ClickHandle clk)
        {
            Clk = clk;
            ReloadRose(url);
        }
        public rose(char RPart, string OName, /*TableLabel.ClickHandle*/TableText.ClickHandle clk)
        {
            Clk = clk;
            ReloadRose(RPart.ToString() + @":\ROSE\" + OName);
        }

        public static string CnrvLocation(string str)
            ///返回的都是存在的
        {
            if (File.Exists(str)) return str;
            if(!resomana.IsInit) return "";
            if (str == null) return "";
            string _str = str.Replace('/', '\\');
            if (str == ""||str.Length<=4||(_str[3]!='\\')) return "";
            switch (_str.Substring(0, 3).ToUpper())//no trim
            {
                case "URL":
                    if (_str[11] != '\\' ) return "";
                    //按照ASCII尺寸进行计算！
                    string si = str.Substring(4, 7).ToUpper();
                    foreach (var v in resomana.RPARTITIONS)
                    {
                        string di = v.ToUpper().Substring(1,7);

                        if (si == di && File.Exists(v[0]+@":\"+_str.Substring(12)))
                        {
                            return v[0] + @":\" + _str.Substring(12);
                        }
                    }
                    return "";
                case "ROS":
                    foreach (var v in resomana.RPARTITIONS)
                    {
                        //if (File.Exists(v[0] + @":\ROSE\" + _str.Substring(4)))
                        if (File.Exists(v[0] + @":\ROSE\" + _str.Substring(4) + ".R")) 
                        {
                            return v[0] + @":\ROSE\" + _str.Substring(4)+".R";
                            //return v[0] + @":\ROSE\" + _str.Substring(4);
                        }
                    }
                    return "";
                default:
                    return str;
            }

            
        }


        public void ReloadRose(string url)
        {
            if (!File.Exists(url)) return;
            rbase = new rose_base();
            ExProperty = new List<string>();
            rbase.tabs = new List<tabpage>();
            //关于词条文件版本与格式的判断。
            {
                //清空控件

                string tmp = File.ReadAllText(url);
                if (tmp.Length <= 5 || tmp[0]!='R') return;
                string[] tmp_split = tmp.Split(new char[] { '\n' });
                if (tmp_split.Length <= 1) return;

                #region read property and judge version.
                foreach (var v in tmp_split)
                {
                    if (v.Trim().Length == 0||
                        v.Trim()[0]==';') continue;
                    string[] tmp_s_s=v.Trim().Split(new char[]{'#'});
                    if (tmp_s_s.Length < 2) continue;
                    switch (tmp_s_s[0].ToUpper())
                        //注意大小写
                    {
                        case "VER":
                            if (tmp_s_s[1] != "R3")
                                return;
                            break;
                                //这句只是用来安慰编译器
                        case "NAM":
                            rbase.oname = tmp_s_s[1];
                            rbase.iname = new List<string>();//【INIT】
                            for (int i = 1; i < (tmp_s_s.Length); i++)
                            {
                                if (tmp_s_s[i].Trim() != "")
                                    rbase.iname.Add(tmp_s_s[i].Trim());
                            }
                                break;
                        case "NOD":
                                rbase.parent = tmp_s_s[1];
                                rbase.children = new List<string>();
                                for (int i = 2; i < tmp_s_s.Length; i++)
                                {
                                    if (tmp_s_s[i].Trim() != "") 
                                        rbase.children.Add(tmp_s_s[i]);
                                }
                                break;
                        case "TYP":
                                try 
                                {
                                    rbase.type = Convert.ToByte(tmp_s_s[1]);
                                }
                                catch
                                {

                                }
                                break;
                        case "AUT":
                                rbase.author = tmp_s_s[1].Trim();
                                break;
                        case "DTM":
                                rbase.create_datetime = tmp_s_s[1].Trim();
                                if (tmp_s_s.Length > 2)
                                    rbase.lastdo_datetime = tmp_s_s[2].Trim();
                                break;
                        case "REF":
                                rbase.source = tmp_s_s[1];
                                break;
                        case "LBL":
                                rbase.labels = new List<string>();
                                for (int i = 1; i < tmp_s_s.Length; i++)
                                {
                                    if (tmp_s_s[i].Trim() != "") 
                                    rbase.labels.Add(tmp_s_s[i].Trim());
                                    // 不理会去重
                                }
                                break;
                        

                        case "BGI":
                                rbase.BackgroundImagePath = CnrvLocation(tmp_s_s[1]);
                                break;
                        case "BGC":
                                rbase.BackgroundColor = MainForm.RGB(Convert.ToInt32(tmp_s_s[1]));
                                if (tmp_s_s.Length >= 3) 
                                rbase.TransparentPer = Convert.ToInt32(tmp_s_s[2]);
                                break;
                        case "LBC":
                                rbase.LabelBackColor = MainForm.RGB(Convert.ToInt32(tmp_s_s[1]));
                                break;
                        case "TAB":
                                if (tmp_s_s.Length < 4) break;
                                string con_type = tmp_s_s[1], con_name = tmp_s_s[2];
                                if (con_type == "" || con_name == "") break;
                                //if (rbase.tabs == null) rbase.tabs = new List<tabpage>();
                                tabpage tpage = new tabpage();
                                tpage.tabname = con_name;

                                switch (con_type.ToUpper())
                                {
                                    case "TXT":
                                        Panel con_p = new Panel();
                                        Label con_lbl = new Label();
                                        int AutoLine = 0;
                                        for (int i = 3; i < tmp_s_s.Length; i++)
                                        {
                                            if (tmp_s_s[i].Trim() == "") continue;
                                            int location2 = tmp_s_s[i].Trim().IndexOf('=');
                                            if (location2 < 1) continue;
                                            string itm = tmp_s_s[i].Trim().Substring(0, location2),
                                                val = tmp_s_s[i].Trim().Substring(location2 + 1);
                                            switch (itm.ToUpper())
                                            {
                                                case "FONT":
                                                    int location3 = val.IndexOf(',');
                                                    if (location3 < 1) continue;
                                                    con_lbl.Font = new Font(val.Substring(location3 + 1), Convert.ToSingle(val.Substring(0, location3)));
                                                    break;
                                                case "TEXT":
                                                    //foreach (var vvvv in ExProperty)
                                                    for (int ii = 0; ii < ExProperty.Count; ii++)
                                                    {
                                                        if (val.ToUpper() == ExProperty[ii].Substring(0, ExProperty[ii].IndexOf('#')).ToUpper())
                                                        {
                                                            con_lbl.Text = ExProperty[ii].Substring(ExProperty[ii].IndexOf('#') + 1).Replace("<br>", "\n");
                                                            break;
                                                        }
                                                    }
                                                    break;
                                            }
                                        }
                                        con_p.AutoScroll = true;
                                        con_p.Dock = DockStyle.Fill;
                                        con_p.BackgroundImageLayout = ImageLayout.Stretch;
                                        con_p.BackColor = Color.FromArgb(200, Color.White);
                                        //con_lbl.BackColor = Color.FromArgb(200, Color.White);
                                        if (AutoLine > 0)
                                        {
                                            //con_lbl.Dock = DockStyle.Fill;
                                            con_lbl.Top = con_lbl.Left = 0;
                                            con_lbl.Width = con_p.Width;//暂时不行
                                            con_lbl.AutoSize = false;
                                        }
                                        else
                                        {
                                            //con_lbl.AutoSize = false;
                                            con_lbl.Top = con_lbl.Left = 0;
                                            con_lbl.AutoSize = true;
                                            //con_lbl.Dock = DockStyle.Fill;
                                        }

                                        con_p.Controls.Add(con_lbl);

                                        //con_p.BackColor = Color.White;
                                        tpage.panel = con_p;
                                        //tpage.ctrls = con_lbl;

                                        rbase.tabs.Add(tpage);
                                        // 需要在对应Form中实现
                                        break;
                                    case "TBL":
                                        if (tmp_s_s.Length <= 3) break;
                                        Panel p = TableData.MakeTableInPanel(rose.CnrvLocation(tmp_s_s[3]));
                                        if (p == null) break;
                                        p.BackColor = Color.Transparent;
                                        //p.Controls[0].BackColor = Color.Transparent;
                                        tabpage tpage2 = new tabpage();
                                        tpage2.panel = p;
                                        tpage2.tabname = tmp_s_s[2];

                                        //tpage.ctrls = p.Controls[0];//【该版本不断定了】
                                        rbase.tabs.Add(tpage2);
                                        break;
                                    case "TABLE":
                                        if (tmp_s_s.Length < 4) break;

                                        table t = new table(Clk);
                                        TableLayoutPanel tlp = t.GetTablexCtrl(rose.CnrvLocation(tmp_s_s[3]));
                                        //tlp.Dock = DockStyle.Fill;
                                        if (tlp == null) break;
                                        tlp.AutoSize = true;
                                        tlp.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
                                        Panel p2 = new Panel();
                                        p2.Dock = DockStyle.Fill;
                                        p2.AutoScroll = true;
                                        p2.Controls.Add(tlp);
                                        p2.BackColor = Color.Transparent;

                                        tabpage tpage3 = new tabpage();
                                        tpage3.panel = p2;
                                        tpage3.tabname = tmp_s_s[2];

                                        rbase.tabs.Add(tpage3);
                                        break;

                                    case "MED":
                                        if (tmp_s_s.Length < 3) break;
                                        MediaFilesBlk media = new MediaFilesBlk();
                                        for (int i = 3; i < tmp_s_s.Length; i++)
                                        {
                                            if (tmp_s_s[i].Trim() == "") break;
                                            string[] elms = tmp_s_s[i].Split(new char[] { '|' });
                                            if (elms.Length != 3) break;
                                            media.eventid.Add(CnrvLocation(elms[1]));
                                            media.files.Add(elms[0]);
                                            media.filesnoimg.Add(CnrvLocation(elms[2]));
                                        }
                                        media._OnLblClick += Clk;
                                        media.Refr(0);
                                        media.Dock = DockStyle.Fill;
                                        Panel p3 = new Panel();
                                        p3.Dock = DockStyle.Fill;
                                        p3.Controls.Add(media);

                                        tabpage tpage4 = new tabpage();
                                        tpage4.panel = p3;
                                        tpage4.tabname = tmp_s_s[2];
                                        rbase.tabs.Add(tpage4);


                                        break;
                                    default: break;
                                }
                                break;


                        default:
                            ExProperty.Add(v.Trim());
                            break;
                    }
                }





                #endregion
                {
                    tmp="";
                    tmp_split=null;
                }
            }

            this.IsLoaded = true;
        }


        

    }
    static class resomana
        //【约定】盘符[1]+标识[7]
    {
        public static List<string> RPARTITIONS;
        public static bool IsInit = false;
        public static void LoadRPartition()
        {
            if (RPARTITIONS == null) RPARTITIONS = new List<string>();
            RPARTITIONS.Clear();
            string[] logicdrives = System.IO.Directory.GetLogicalDrives();
            // C:\
            for (int i = 0; i < logicdrives.Length; i++)
            {
                if(!File.Exists(logicdrives[i]+"R")) continue;
                string RPID = File.ReadAllText(logicdrives[i] + "R");
                if (RPID.Trim().Length != 7) continue;

                RPARTITIONS.Add(logicdrives[i][0].ToString() + RPID.Trim());
            }
            IsInit = true;
        }

    }
}
