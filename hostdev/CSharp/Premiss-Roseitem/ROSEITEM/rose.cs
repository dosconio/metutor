using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ROSEITEM
    //ver=REG(4)
    //node中的对象连同indexname一同放在INDX目录供搜索
{
    public struct r_item
    {
        public string oname;
        public string codeset;
        public string parent;
        public byte type;
        public string author;
        public string source;
        public string history;
        public List<string> children;
        public List<string> iname;
        public List<string> labels;
        //注意是否为NULL
    }
    public class rose
    {
        public bool IsLoaded = false;
        public List<string> subitems;//text with format (this version)
        //public List<string> subfontsFamily;// 1 font, don't use ","
        //public List<int> subfontsSize;
        public r_item rbase;
        public List<string> PropName;
        public List<string> PropVal;


        public rose(string url)
        {
            ReloadRose(url);
        }
        public rose(char RPart, string OName)
        {
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
                        if (File.Exists(v[0] + @":\ROSE\" + _str.Substring(4) + ".R")) 
                        {
                            return v[0] + @":\ROSE\" + _str.Substring(4)+".R";
                        }
                    }
                    return "";
                case "IDX":
                    foreach (var v in resomana.RPARTITIONS)
                    {
                        if (File.Exists(v[0] + @":\INDX\" + _str.Substring(4)))
                        {
                            return v[0] + @":\INDX\" + _str.Substring(4);
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
            rbase = new r_item();
            rbase.iname = new List<string>();
            rbase.children = new List<string>();
            rbase.labels = new List<string>();
            subitems = new List<string>();
            //subfontsFamily = new List<string>();
            //subfontsSize = new List<int>();
            PropName = new List<string>();
            PropVal = new List<string>();
            //关于词条文件版本与格式的判断。
            {
                string tmp = File.ReadAllText(url);
                if (tmp.Length <= 5 || tmp[0]!='R') return;
                rbase.type = Convert.ToByte(tmp[1]);
                List<char> JmpoutSym = new List<char>();
                List<int> JmpptrIndx = new List<int>();
                List<string> JmpLabel = new List<string>();///store capital words
                string TmpStringProp = null;
                //#TXT( <ffamily( 微软雅黑,Console)size(32)forecolor(FFFF00FF)>"Title",<size(16)>"char,\"")
                //#XXX()
                //后面的文本格式暂不支持未表示部分继承前者
                #region read property and judge version.

                for (int i = 1; i < tmp.Length; i++)
                {
                    switch (tmp[i])
                    {
                        //转义 \"=" \\=\
                        case '#':
                            //chk yo str
                            if (JmpLabel.Count == 0)
                            {
                                JmpoutSym.Add(')');
                                JmpptrIndx.Add(i);
                            }
                            else
                            {
                                break;
                            }
                            break;
                        case '(':
                            //after # or as property
                            //must jmps have vals
                            //not process <xxx(xxx)>
                            if (JmpoutSym.Count > 0 && JmpoutSym[JmpoutSym.Count - 1] == ')' && JmpLabel.Count == 0) 
                            {
                                string new_t_str_0 = tmp.Substring(JmpptrIndx[0] + 1, i - JmpptrIndx[0] - 1).ToUpper().Trim();
                                JmpLabel.Add(new_t_str_0);
                                JmpptrIndx.RemoveAt(0);
                                JmpptrIndx.Add(i);
                            }
                            break;
                        case ')':
                            if (JmpoutSym.Count == 0) break;
                            switch (JmpoutSym[JmpoutSym.Count - 1])
                            {
                                //if yo str, the sym is "
                                case ')':
                                    switch (JmpLabel[JmpLabel.Count - 1])
                                    {
                                            //Only one {}
                                        case "ROS":
                                            string TmpRosenames = tmp.Substring(JmpptrIndx[0] + 1, i - JmpptrIndx[0] - 1).Trim();
                                            int TmpRosenamesC = TmpRosenames.IndexOf(',');
                                            if (TmpRosenamesC <= 0)
                                            {
                                                rbase.oname = TmpRosenames.Trim();
                                            }
                                            else
                                            {
                                                rbase.oname = TmpRosenames.Substring(0, TmpRosenamesC);
                                                string[] TmpIndxnames = TmpRosenames.Substring(TmpRosenamesC + 1).Split(new char[] { ',' });
                                                foreach (var v in TmpIndxnames)
                                                {
                                                    rbase.iname.Add(v.Trim());
                                                }
                                            }
                                            JmpLabel.RemoveAt(JmpLabel.Count - 1);
                                            JmpoutSym.RemoveAt(JmpoutSym.Count - 1);
                                            JmpptrIndx.RemoveAt(JmpptrIndx.Count - 1);
                                            break;
                                        case "TXT":
                                            //COMPLEX
                                            JmpLabel.RemoveAt(JmpLabel.Count - 1);
                                            JmpoutSym.RemoveAt(JmpoutSym.Count - 1);
                                            JmpptrIndx.RemoveAt(JmpptrIndx.Count - 1);
                                            break;
                                        case "NOD":
                                            string TmpNode = tmp.Substring(JmpptrIndx[0] + 1, i - JmpptrIndx[0] - 1).Trim();
                                            int TmpNodeC = TmpNode.IndexOf(',');
                                            if (TmpNodeC <= 0)
                                            {
                                                rbase.parent = TmpNode.Trim();//Trim for =0
                                            }
                                            else
                                            {
                                                rbase.parent = TmpNode.Substring(0, TmpNodeC);
                                                string[] TmpChildren = TmpNode.Substring(TmpNodeC + 1).Split(new char[] { ',' });
                                                foreach (var v in TmpChildren)
                                                {
                                                    rbase.children.Add(v.Trim());
                                                }
                                            }
                                            JmpLabel.RemoveAt(JmpLabel.Count - 1);
                                            JmpoutSym.RemoveAt(JmpoutSym.Count - 1);
                                            JmpptrIndx.RemoveAt(JmpptrIndx.Count - 1);
                                            break;
                                        case "AUT":
                                            rbase.author = tmp.Substring(JmpptrIndx[0] + 1, i - JmpptrIndx[0] - 1).Trim();
                                            JmpLabel.RemoveAt(JmpLabel.Count - 1);
                                            JmpoutSym.RemoveAt(JmpoutSym.Count - 1);
                                            JmpptrIndx.RemoveAt(JmpptrIndx.Count - 1);
                                            break;
                                        case "EVL":
                                            rbase.history = tmp.Substring(JmpptrIndx[0] + 1, i - JmpptrIndx[0] - 1).Trim();
                                            JmpLabel.RemoveAt(JmpLabel.Count - 1);
                                            JmpoutSym.RemoveAt(JmpoutSym.Count - 1);
                                            JmpptrIndx.RemoveAt(JmpptrIndx.Count - 1);
                                            break;
                                        case "LBL":
                                            string[] TmpLabels = tmp.Substring(JmpptrIndx[0] + 1, i - JmpptrIndx[0] - 1).Trim().Split(new char[] { ',' });
                                            JmpLabel.RemoveAt(JmpLabel.Count - 1);
                                            JmpoutSym.RemoveAt(JmpoutSym.Count - 1);
                                            JmpptrIndx.RemoveAt(JmpptrIndx.Count - 1);
                                            break;
                                        case "REF":
                                            rbase.source = tmp.Substring(JmpptrIndx[0] + 1, i - JmpptrIndx[0] - 1).Trim();
                                            JmpLabel.RemoveAt(JmpLabel.Count - 1);
                                            JmpoutSym.RemoveAt(JmpoutSym.Count - 1);
                                            JmpptrIndx.RemoveAt(JmpptrIndx.Count - 1);
                                            break;
                                        case "PRO":
                                            int TmpPtrComma = tmp.Substring(JmpptrIndx[0] + 1, i - JmpptrIndx[0] - 1).Trim().IndexOf(',');
                                            if (TmpPtrComma > 0)
                                            {
                                                PropName.Add(tmp.Substring(JmpptrIndx[0] + 1, i - JmpptrIndx[0] - 1).Trim().Substring(0, TmpPtrComma));
                                                PropVal.Add(tmp.Substring(JmpptrIndx[0] + 1, i - JmpptrIndx[0] - 1).Trim().Substring(TmpPtrComma + 1));
                                            }
                                            JmpLabel.RemoveAt(JmpLabel.Count - 1);
                                            JmpoutSym.RemoveAt(JmpoutSym.Count - 1);
                                            JmpptrIndx.RemoveAt(JmpptrIndx.Count - 1);
                                            break;
                                        case "HTM"://HTML
                                            JmpLabel.RemoveAt(JmpLabel.Count - 1);
                                            JmpoutSym.RemoveAt(JmpoutSym.Count - 1);
                                            JmpptrIndx.RemoveAt(JmpptrIndx.Count - 1);
                                            break;
                                        default:
                                            JmpLabel.RemoveAt(JmpLabel.Count - 1);
                                            JmpoutSym.RemoveAt(JmpoutSym.Count - 1);
                                            JmpptrIndx.RemoveAt(JmpptrIndx.Count - 1);
                                            break;
                                    }
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case '<':
                            //just function before string
                            if (JmpLabel.Count > 0 && JmpLabel[JmpLabel.Count - 1] == "TXT" && JmpoutSym[JmpoutSym.Count - 1] == ')') 
                            {
                                JmpLabel.Add("(TXTP)");
                                JmpoutSym.Add('>');
                                JmpptrIndx.Add(i);
                            }
                            break;
                        case '>':
                            if (JmpoutSym.Count > 0 && JmpoutSym[JmpoutSym.Count - 1] == '>')
                            {
                                TmpStringProp = tmp.Substring(JmpptrIndx[JmpptrIndx.Count - 1] + 1, i - JmpptrIndx[JmpptrIndx.Count - 1] - 1);
                                JmpLabel.RemoveAt(JmpLabel.Count - 1);
                                JmpoutSym.RemoveAt(JmpoutSym.Count - 1);
                                JmpptrIndx.RemoveAt(JmpptrIndx.Count - 1);
                            }
                            break;
                        //case ',':
                        //    if (JmpoutSym.Count > 0 && JmpoutSym[JmpoutSym.Count - 1] == '\"') ;
                        //    break;
                        case '\"':
                            if (JmpoutSym.Count == 0) break;
                            if (JmpLabel[JmpLabel.Count - 1] == "HTM"||JmpLabel[JmpLabel.Count-1]=="(HTML)")
                            {
                                if (JmpoutSym[JmpoutSym.Count - 1] == '\"')
                                {
                                    string TmpPreText = tmp.Substring(JmpptrIndx[JmpptrIndx.Count - 1] + 1, i - JmpptrIndx[JmpptrIndx.Count - 1] - 1);
                                    string TmpTmpText = "";
                                    TmpPreText = TmpPreText.Replace("\\n", "\n").Replace("\\t", "\t");
                                    for (int ii = 0; ii < TmpPreText.Length; ii++)
                                    {
                                        if (TmpPreText[ii] == '\\')
                                            ii++;
                                        TmpTmpText += TmpPreText[ii];
                                    }
                                    subitems.Add(TmpTmpText);
                                    JmpLabel.RemoveAt(JmpLabel.Count - 1);
                                    JmpoutSym.RemoveAt(JmpoutSym.Count - 1);
                                    JmpptrIndx.RemoveAt(JmpptrIndx.Count - 1);
                                }
                                else
                                {
                                    JmpLabel.Add("(HTML)");
                                    JmpoutSym.Add('\"');
                                    JmpptrIndx.Add(i);
                                }
                            }
                            
                            if (JmpLabel[JmpLabel.Count - 1] != "TXT" && JmpLabel[JmpLabel.Count-1]!="(TEXT)") break;
                            if (tmp[i - 1] == '\\') break;
                            if (JmpoutSym[JmpoutSym.Count - 1] == '\"')
                                // yo str if : jump out
                            {
                                //process text
                                string TmpPreText = tmp.Substring(JmpptrIndx[JmpptrIndx.Count - 1] + 1, i - JmpptrIndx[JmpptrIndx.Count - 1] - 1);
                                string TmpFFamily = "", TmpFSize = "";
                                string TmpTmpText = "";
                                for (int ii = 0; ii < TmpPreText.Length; ii++)
                                {
                                    if (TmpPreText[ii] == '\\')
                                        ii++;
                                    TmpTmpText += TmpPreText[ii];
                                }
                                
                                    //subitems.Add(TmpPreText);
                                    if (TmpStringProp != null)
                                    {
                                        //find FontFamily
                                        int TmpPtrFFamily, TmpPtrFFamilyEnd;
                                        TmpPtrFFamily = TmpStringProp.IndexOf("ffamily(");
                                        if (TmpPtrFFamily >= 0)
                                        {
                                            TmpPtrFFamilyEnd = TmpStringProp.IndexOf(")", TmpPtrFFamily + 8);
                                            if (TmpPtrFFamilyEnd > TmpPtrFFamily)
                                            {
                                                TmpFFamily = TmpStringProp.Substring(TmpPtrFFamily + 8, TmpPtrFFamilyEnd - TmpPtrFFamily - 8);
                                            }
                                        }
                                        //find FontSize
                                        int TmpPtrFSize, TmpPtrFSizeEnd;
                                        TmpPtrFSize = TmpStringProp.IndexOf("fsize(");
                                        if (TmpPtrFSize >= 0)
                                        {
                                            TmpPtrFSizeEnd = TmpStringProp.IndexOf(")", TmpPtrFSize + 6);
                                            if (TmpPtrFSizeEnd > TmpPtrFSize)
                                            {
                                                TmpFSize = TmpStringProp.Substring(TmpPtrFSize + 6, TmpPtrFSizeEnd - TmpPtrFSize - 6);
                                            }
                                        }
                                        //虽然应当写在外一个{}中
                                        subitems.Add("<a style=\"font-family:\'" + TmpFFamily + "\',\'Consolas\';font-size:" + TmpFSize + "px\">" + TmpTmpText + "</a>");
                                    }
                                JmpLabel.RemoveAt(JmpLabel.Count - 1);
                                JmpoutSym.RemoveAt(JmpoutSym.Count - 1);
                                JmpptrIndx.RemoveAt(JmpptrIndx.Count - 1);
                            }
                            else
                                //outof str if : enter str
                            {
                                JmpLabel.Add("(TEXT)");
                                JmpoutSym.Add('\"');
                                JmpptrIndx.Add(i);
                            }
                            break;
                        case '\\':
                            if (JmpoutSym.Count > 0 && JmpoutSym[JmpoutSym.Count - 1] == '\"')
                            {
                                //now don't consider but process '\"'
                                i++;
                            }
                            break;
                        default:
                            break;
                    }
                }
                #endregion

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
            else RPARTITIONS.Clear();
            string[] logicdrives = System.IO.Directory.GetLogicalDrives();
            // e.g. C:\
            for (int i = 0; i < logicdrives.Length; i++)
            {
                if(!File.Exists(logicdrives[i]+"R")) continue;
                string RPID = File.ReadAllText(logicdrives[i] + "R");
                if (RPID.Trim().Length < 7) continue;

                RPARTITIONS.Add(logicdrives[i][0].ToString() + RPID.Substring(0,7).ToUpper());
            }
            IsInit = true;
        }

    }
}
