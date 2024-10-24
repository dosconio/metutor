using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ROSAMANA
{
    public  class TableData
    {
        public static ListView MakeTable(string str)
        {
            if (!File.Exists(str)) return null;
            rose r = new rose(str,null);
            if (r.rbase.type != 6) return null;
            ListView lv = new ListView();
            int row = 0, col = 0;
            List<string> colname=null, rowname=null;
            string[] vals={};
            foreach (var v in r.ExProperty)
                //不考虑Trim()
            {
                string[] tmp = v.Split(new char[] { '#' });
                if (tmp.Length < 2) return null;
                switch (tmp[0].ToUpper())
                {
                    case "COL":
                        col = Convert.ToInt32(tmp[1]);
                        if (col <= 0) return null;
                        colname = new List<string>();
                        for (int i = 0; i < col; i++)
                        {
                            if (tmp.Length > i + 2)
                            {
                                colname.Add(tmp[2 + i]);
                            }
                            else
                                colname.Add("-");
                        }
                            break;
                    case "ROW":
                            row = Convert.ToInt32(tmp[1]);
                            if (row <= 0) return null;
                            rowname = new List<string>();
                            for (int i = 0; i < row; i++)
                            {
                                if (tmp.Length > i + 2)
                                {
                                    rowname.Add(tmp[2 + i]);
                                }
                                else
                                    rowname.Add("");
                            }
                                break;

                    case "DATA":
                            //    vals = tmp;
                                vals = tmp[1].Split(new char[] { '|' });
                            break;
                    default:
                        break;
                }
            

            }
            if (row <= 0 || col <= 0 || vals.Length < 1) return null;
            //
            for (int i = 0; i < col+1; i++)
            {

                ColumnHeader ch = new ColumnHeader();
                if (i > 0)
                    ch.Text = colname[i - 1];
                else
                    ch.Text = "";
                lv.Columns.Add(ch);
            }
            for (int i = 0; i < row; i++)
            {
                ListViewItem lvi = new ListViewItem(rowname[i]);
                for (int ii = 0; ii < col; ii++)
                {
                    if(vals.Length>i*col+ii)
                    lvi.SubItems.Add(vals[i * col + ii]);

                }
                lv.Items.Add(lvi);
            }
            lv.GridLines = true;
            lv.View = View.Details;
            lv.Dock = DockStyle.Fill;
            return lv;
        }
        public static Panel MakeTableInPanel(string str)
        {
            Panel p = new Panel();
            ListView lv = MakeTable(str);
            if (lv == null) return null;
            p.Dock = DockStyle.Fill;
            p.Controls.Add(lv);

            return p;

        }
    }
}
