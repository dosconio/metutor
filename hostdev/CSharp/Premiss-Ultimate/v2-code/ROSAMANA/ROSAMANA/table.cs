using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ROSAMANA
{
    struct table_item
    {
        public int top;
        public int left;
        public int width;
        public int height;
        public Color ForeColor;
        public string Text;
        public string Link;
        public string FontName;//yoin libr/font
    }
//;[A1]{haha][lele][lolo}{qw]
//;[B1]{gaga][qeqe}{kiki][qw]
//;[C1]{momo][keke][lili][qw]
//;[D1][lulu][dede}[riri}[qw}
    /// <summary>
    /// <author>Arina yoon REE18</author>
    /// </summary>
    class table
    {
        public delegate void ClickHandle(object sender, EventArgs e, string msg);
        //public TableLabel.ClickHandle OnTableItmClick;
        public TableText.ClickHandle OnTableItmClick;
        public table(/*TableLabel.ClickHandle*/ TableText.ClickHandle ch)
        {
            OnTableItmClick = ch;
        }
        public /*static*/ TableLayoutPanel GetTablexCtrl(string url)
        {
            if (!File.Exists(url)) return null;
            int cols, rows;
            string[] txt = File.ReadAllText(url).Split(new char[]{'\n'});
            List<string> data = new List<string>();
            foreach (var v in txt)
            {
                if (v.Trim() == "") continue;
                if (v.Substring(0, 1) == ";") continue;
                data.Add(v.Trim());
            }
            rows = data.Count;
            if (rows < 1) return null;
            if (data[0][0] != '[' && data[0][0] != '{') return null;
            List<table_item> items = new List<table_item>();
            List<List<string>> predata = new List<List<string>>();//
            int tmp_lastsym = -1;
            foreach (var v in data)
            {
                List<string> subpredata = new List<string>();
                for (int i = 0; i < v.Length; i++)
                {
                    if (v[i] == '[' || v[i] == '{')
                    {
                        tmp_lastsym = i;
                    }
                    else if (v[i] == ']' || v[i] == '}')
                    {
                        subpredata.Add(v.Substring(tmp_lastsym, i - tmp_lastsym + 1));
                    }
                }
                predata.Add(subpredata);
            }
            data.Clear();
            cols = predata[0].Count;
            if (cols < 1) return null;
            bool[] syms = new bool[rows * cols];
            bool OnBlk = false; int BlkTop = -1, BlkLeft = -1;
            for (int ro = 0; ro <rows; ro++)
            {
                for (int cl = 0; cl < cols; cl++)
                {
                    if (predata[ro][cl][0] == '{' && predata[ro][cl][predata[ro][cl].Length - 1] == ']' && syms[cols * ro + cl] == false)//块开始
                    {
                        OnBlk = true;
                        BlkTop = ro;//from 0
                        BlkLeft = cl;//from 0
                        //syms[cols * ro + cl] = true;
                        for (int roro = BlkTop; roro < rows; roro++)
                        {
                            for (int clcl = BlkLeft; clcl < cols; clcl++)
                            {
                                if (predata[roro][clcl][0] == '[' && predata[roro][clcl][predata[roro][clcl].Length - 1] == '}' && syms[cols * roro + clcl] == false)
                                {
                                    OnBlk = false;
                                    for (int finrow = BlkTop; finrow <= roro; finrow++)
                                    {
                                        for (int fincol = BlkLeft; fincol <= clcl; fincol++)
                                        {
                                            syms[cols * finrow + fincol] = true;
                                        }
                                    }
                                    table_item ti = new table_item();
                                    ti.top = BlkTop;
                                    ti.left = BlkLeft;
                                    ti.width = clcl - BlkLeft + 1;
                                    ti.height = roro - BlkTop + 1;
                                    /*
                                     样式解析
                                     */
                                    string[] divi = predata[BlkTop][BlkLeft].Substring(1, predata[BlkTop][BlkLeft].Length - 2).Split(new char[] { '|' });
                                    ti.Text = divi[0];
                                    for (int i = 0; i < divi.Length; i++)
                                    {
                                        int location = divi[i].IndexOf('=');
                                        if (location < 1) continue;
                                        switch (divi[i].Substring(0, location).ToUpper())
                                        {
                                            case "LINK":
                                                ti.Link = divi[i].Substring(location + 1);
                                                break;
                                            case "COLOR":
                                                string[] divicol = divi[i].Substring(location + 1).Split(new char[] { '=' });
                                                if (divicol.Length < 3) continue;
                                                ti.ForeColor = Color.FromArgb(Convert.ToInt32(divicol[0]), Convert.ToInt32(divicol[1]), Convert.ToInt32(divicol[2]));
                                                break;
                                            default:
                                                break;
                                        }
                                    }
                                        items.Add(ti);
                                    //这里用goto的话就不用OnBlk了
                                    break;
                                }

                            } if (OnBlk == false) break;

                        }

                    }
                    else
                    if (predata[ro][cl][0] == '[' && predata[ro][cl][predata[ro][cl].Length - 1] == ']' && syms[cols * ro + cl] == false)
                    {
                        /*...*/
                        table_item ti = new table_item();
                        ti.top = BlkTop = ro;
                        ti.left = BlkLeft = cl;
                        ti.width = 1;
                        ti.height = 1;
                        /*
                         样式解析
                         */
                        string[] divi = predata[BlkTop][BlkLeft].Substring(1, predata[BlkTop][BlkLeft].Length - 2).Split(new char[] { '|' });
                        ti.Text = divi[0];
                        for (int i = 0; i < divi.Length; i++)
                        {
                            int location = divi[i].IndexOf('=');
                            if (location < 1) continue;
                            switch (divi[i].Substring(0, location).ToUpper())
                            {
                                case "LINK":
                                    ti.Link = divi[i].Substring(location + 1);
                                    break;
                                case "COLOR":
                                    string[] divicol = divi[i].Substring(location + 1).Split(new char[] { '=' });
                                    if (divicol.Length < 3) continue;
                                    ti.ForeColor = Color.FromArgb(Convert.ToInt32(divicol[0]), Convert.ToInt32(divicol[1]), Convert.ToInt32(divicol[2]));
                                    break;
                                default:
                                    break;
                            }
                        }
                        items.Add(ti);
                        syms[cols * ro + cl] = true;
                    }
                }
            }


            // cols*rows
            TableLayoutPanel table = new TableLayoutPanel();
            table.ColumnCount = cols;
            table.RowCount = rows;
            for (int i = 0; i < cols; i++)
            {

                if (table.ColumnStyles.Count <= i)
                {
                    ColumnStyle cstyle = new ColumnStyle(SizeType.Percent, 100 / cols);
                    table.ColumnStyles.Add(cstyle);
                }else
                {
                    table.ColumnStyles[i] = new ColumnStyle(SizeType.Percent, 100 / cols);
                }
            }
            
            for (int i = 0; i < rows; i++)
            {
                if (table.RowStyles.Count <= i)
                {
                    RowStyle rstyle = new RowStyle();//(SizeType.Percent, 100 / rows);
                    table.RowStyles.Add(rstyle);
                }
                else
                {
                    //table.RowStyles[i] = new RowStyle(SizeType.Percent, 100 / rows);
                }
            }
            foreach (var v in items)
            {
                //TableLabel tl = new TableLabel();
                TableText tl = new TableText();
                tl.Text = v.Text.Replace("<br>","\r\n");
                //对于TextBox用\r\n，对于label用\n.
                tl._EVENTID = v.Link;
                tl._OnLblClick += OnTableItmClick;
                tl._LblForeColor = v.ForeColor;
                tl.Dock = DockStyle.Fill;
                if (v.Link!=null&&v.Link.Trim() != "")
                {
                    tl._FONT = new Font("微软雅黑", (float)12.0, FontStyle.Bold|FontStyle.Underline);
                }
                else
                {
                    tl._FONT = new Font("微软雅黑", (float)12.0, FontStyle.Bold);
                }
                //tl._FONT=new Font("微软雅黑",(float)12.0,FontStyle.Bold);
                table.Controls.Add(tl);
                table.SetRow(tl, v.top);
                table.SetColumn(tl, v.left);
                table.SetRowSpan(tl, v.height);
                table.SetColumnSpan(tl, v.width);
                //table.AutoSize = false;
                //table.AutoSizeMode = AutoSizeMode.GrowOnly;
                //int id = table.Controls.IndexOf(tl);
                
            }



                return table;
        }
    }
}
