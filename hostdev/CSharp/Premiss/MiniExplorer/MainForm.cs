using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MiniExplorer
{
    public partial class MainForm : Form
    {
        BaseFunc baseFunc = new BaseFunc();
        private bool IsCopy;// remain source
        private string path;// na copy to link pasteboard
        private static ArrayList al = new ArrayList();//存储文件全名以备复制等操作

        public MainForm()
        {
            InitializeComponent();
            
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            //MessageBox.Show(baseFunc.BackDir(@"C:\a\s\s\s.txt"));
            //baseFunc.FilesCopy("E:\\tmp\\2\\", "E:\\tmp\\1\\");
            //TRY:FilesCopy ("**\**","**\**")  失败
            //              ("**\**","**\**\") 失败              
            //              ("**\**\","**\**") 成功
            //              ("**\**\","**\**\")成功
            baseFunc.ListFilesItems(DriverPrtCozBox);
        }

        private void DriverPrtCozBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            baseFunc.OpenItem(DriverPrtCozBox.Text, imageList, listView1, true);
            Text = baseFunc.Mpath();
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                string pp = listView1.SelectedItems[0].Text;
                baseFunc.OpenItem(pp.Trim(), imageList, listView1, false);
                Text = baseFunc.Mpath();
            }
            catch
            {
                MessageBox.Show("无法打开此文件(夹)", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            baseFunc.TurnBackPath(imageList, listView1, DriverPrtCozBox.Text);
            Text = baseFunc.Mpath();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            baseFunc.RefreshListViewItem(baseFunc.Mpath(), imageList, listView1);
            Text = baseFunc.Mpath();
        }

        private void 剪切ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count != 0)
            {
                al.Clear();
                for (int i = 0; i < listView1.SelectedItems.Count; i++)
                {
                    al.Add(baseFunc.Mpath() + "\\" + listView1.SelectedItems[i].Text);
                }
                IsCopy = false;
            }
        }

        private void 复制ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                al.Clear();
                path = baseFunc.Mpath() + "\\" + listView1.SelectedItems[0].Text;
                for (int i = 0; i < listView1.SelectedItems.Count; i++)
                {
                    al.Add(baseFunc.Mpath() + "\\" + listView1.SelectedItems[i].Text);
                }
                System.Collections.Specialized.StringCollection files = new System.Collections.Specialized.StringCollection();
                files.Add(path);
                Clipboard.SetFileDropList(files);
                //MyPath = path;
                IsCopy = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void 粘贴ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!IsCopy)
            {
                try
                {
                    for (int i = 0; i < al.Count; i++)
                    {
                        // name1 zon 不含路径信息的文件名
                        string name1 = al[i].ToString().Substring(al[i].ToString().LastIndexOf("\\") + 1, al[i].ToString().Length - al[i].ToString().LastIndexOf("\\") - 1);
                        string paths = baseFunc.Mpath() + "\\" + name1;
                        if (File.Exists(al[i].ToString()))
                        {
                            if (al[i].ToString() != paths)
                            {
                                File.Move(al[i].ToString(), paths);
                            }
                        }
                        if (Directory.Exists(al[i].ToString()))
                        {
                            baseFunc.FilesCopy(paths, al[i].ToString());
                            DirectoryInfo di = new DirectoryInfo(al[i].ToString());
                            di.Delete(true);
                        }
                    }
                    
                    baseFunc.RefreshListViewItem(baseFunc.Mpath(), imageList, listView1);
                    Text = baseFunc.Mpath();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                try
                {
                    for (int i = 0; i < al.Count; i++)
                    {
                        string name1 = al[i].ToString().Substring(al[i].ToString().LastIndexOf("\\") + 1, al[i].ToString().Length - al[i].ToString().LastIndexOf("\\") - 1);
                        string paths = baseFunc.Mpath() + "\\" + name1;
                        if (File.Exists(al[i].ToString()))
                        {
                            if (al[i].ToString() != paths)
                            {
                                File.Copy(al[i].ToString(), paths, false);
                            }
                        }
                        if (Directory.Exists(al[i].ToString()))
                        {
                            baseFunc.FilesCopy(paths, al[i].ToString());
                        }
                    }
                    
                    baseFunc.RefreshListViewItem(baseFunc.Mpath(), imageList, listView1);
                    Text = baseFunc.Mpath();
                }
                catch { }
            }
        }

        private void 全选ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //listView1.SelectedItems = listView1.Items;
            for (int i = 0; i < listView1.Items.Count; i++)
            {
                listView1.Items[i].Selected = true;
            }
        }

        private void 打开ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string tmp = listView1.SelectedItems[0].Text;
                baseFunc.OpenItem(tmp.Trim(), imageList, listView1, false);
                Text = baseFunc.Mpath();
            }
            catch
            {
                MessageBox.Show("无法打开此文件", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void 重命名ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count != 0)
            {
                listView1.SelectedItems[0].BeginEdit();
            }
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (listView1.SelectedItems.Count == 0)
                {
                    MessageBox.Show("请选择文件", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    for (int i = 0; i < listView1.SelectedItems.Count; i++)
                    {
                        string path = baseFunc.Mpath() + "\\" + listView1.SelectedItems[i].Text;
                        if (File.GetAttributes(path).CompareTo(FileAttributes.Directory) == 0)
                        {
                            DirectoryInfo dinfo = new DirectoryInfo(path);
                            dinfo.Delete(true);
                        }
                        else
                        {
                            string path1 = baseFunc.Mpath() + "\\" + listView1.SelectedItems[i].Text;
                            FileInfo finfo = new FileInfo(path1);
                            finfo.Delete();
                        }
                    }
                    
                    baseFunc.RefreshListViewItem(baseFunc.Mpath(), imageList, listView1);
                    Text = baseFunc.Mpath();
                }
            }
            catch
            { }
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void listView1_AfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            try
            {
                string MyPath = baseFunc.Mpath() + "\\" + listView1.SelectedItems[0].Text;
                string newFilename = e.Label;
                string path2 = baseFunc.Mpath() + "\\" + newFilename;
                if (File.Exists(MyPath))
                {
                    if (MyPath != path2)
                    {
                        File.Move(MyPath, path2);
                    }
                }
                if (Directory.Exists(MyPath))
                {
                    DirectoryInfo di = new DirectoryInfo(MyPath);
                    di.MoveTo(path2);
                }
                listView1.Items.Clear();
                baseFunc.RefreshListViewItem(baseFunc.Mpath(), imageList, listView1);
                Text = baseFunc.Mpath();
            }
            catch { }
        }




    }
}
