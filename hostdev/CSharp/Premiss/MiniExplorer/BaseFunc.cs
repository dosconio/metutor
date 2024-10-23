using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Collections;
using System.IO.Compression;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Runtime.Serialization;
using Microsoft.Win32;

namespace MiniExplorer
{
    class BaseFunc
    {
        public static string AllPath = "";// 缓存全路径
        public class Win32
        {
            public const uint SHGFI_ICON = 0x100;
            public const uint SHGFI_LARGEICON = 0x0; // Large icon
            public const uint SHGFI_SMALLICON = 0x1; // Small icon
            [DllImport("shell32.dll", EntryPoint = "ExtractIcon")]
            public static extern int ExtractIcon(IntPtr hInst, string lpFileName, int nIndex);
            [DllImport("shell32.dll", EntryPoint = "SHGetFileInfo")]
            public static extern IntPtr SHGetFileInfo(string pszPath, uint dwFileAttribute, ref SHFILEINFO psfi, uint cbSizeFileInfo, uint Flags);
            [DllImport("User32.dll", EntryPoint = "DestroyIcon")]
            public static extern int DestroyIcon(IntPtr hIcon);
            [DllImport("shell32.dll")]
            public static extern uint ExtractIconEx(string lpszFile, int nIconIndex, int[] phiconLarge, int[] phiconSmall, uint nIcons);
            [StructLayout(LayoutKind.Sequential)]
            public struct SHFILEINFO
            {
                public IntPtr hIcon;
                public IntPtr iIcon;
                public uint dwAttributes;
                [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
                public string szDisplayName;
                [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
                public string szTypeName;
            }
        }

        #region  文件夹的复制
        /// <summary>
        /// 文件夹的复制,注意参数
        /// </summary>
        /// <param Ddir="string">要复制的目的路径</param>
        /// <param Sdir="string">要复制的原路径(整个都复制)</param>
        public bool FilesCopy(string Ddir, string Sdir)
        {
            DirectoryInfo dir = new DirectoryInfo(Sdir);
            string SubDir = Ddir;
            try
            {
                if (!dir.Exists) return false;
                if (Ddir == Sdir) return false;
                DirectoryInfo dirD = dir as DirectoryInfo;
                //如果给定参数不是文件夹则退出?
                string UpDir = BackDir(Ddir); 
                // 上一级的文件夹
                Directory.CreateDirectory(UpDir + "\\" + dirD.Name);
                if (dirD == null) return false;
                SubDir = UpDir + "\\" + dirD.Name + "\\";
                FileSystemInfo[] files = dirD.GetFileSystemInfos();
                //获取文件夹中所有文件和文件夹
                //对单个FileSystemInfo进行判断,如果是文件夹则进行[递归]操作
                foreach (FileSystemInfo FSys in files)
                {
                    FileInfo file = FSys as FileInfo;
                    if (file != null)
                        //如果是文件的话，进行文件的复制操作
                    {
                        FileInfo SFInfo = new FileInfo(file.DirectoryName + "\\" + file.Name);//获取文件所在的原始路径
                        SFInfo.CopyTo(SubDir + "\\" + file.Name, true);//将文件复制到指定的路径中
                    }
                    else
                        //文件夹なよ
                    {
                        string pp = FSys.Name;//获取当前搜索到的文件夹名称
                        FilesCopy(SubDir + FSys.ToString(), Sdir + "\\" + FSys.ToString());//如果是文件，则进行递归调用
                    }
                }
                return true;
            }
            catch
            {
                MessageBox.Show("文件复制失败。");
                return false;
            }
        }
        #endregion
        #region  返回上一级目录
        /// <summary>
        /// 返回上一级目录
        /// </summary>
        /// <param dir="string">目录</param>
        /// <returns>返回String对象</returns>
        public string BackDir(string dir)
        {
            string Change_dir = "";
            Change_dir = Directory.GetParent(dir).FullName;
            return Change_dir;
        }
        #endregion


        public void ListFilesItems(ToolStripComboBox tscb)
        {
            tscb.Items.Clear();
            string[] logicdrives = System.IO.Directory.GetLogicalDrives();
            for (int i = 0; i < logicdrives.Length; i++)
            {
                tscb.Items.Add(logicdrives[i]);
                tscb.SelectedIndex = 0;
            }
        }
        
        //Turnto BackPath
        public void TurnBackPath(ImageList il,ListView lv,string path)
        {
            if (AllPath.Length <= 3) goto fin;
            path = AllPath.Remove(AllPath.LastIndexOf("\\")).Remove(AllPath.Remove(AllPath.LastIndexOf("\\")).LastIndexOf("\\")) + "\\";
        fin:
            AllPath = path;
            RefreshListViewItem(AllPath, il, lv);
        }

        //获取指定路径下所有文件及其图标
        public void RefreshListViewItem(string path, ImageList imglist, ListView lv)
        {

            lv.Items.Clear();
            Win32.SHFILEINFO shfi = new Win32.SHFILEINFO();
            try
            {
                string[] dirs = Directory.GetDirectories(path);
                string[] files = Directory.GetFiles(path);
                for (int i = 0; i < dirs.Length; i++)
                {
                    string[] info = new string[4];
                    DirectoryInfo dir = new DirectoryInfo(dirs[i]);
                    if (dir.Name.ToUpper() != "RECYCLER" && dir.Name != "System Volume Information")
                    {
                        //获得图标
                        Win32.SHGetFileInfo(dirs[i],(uint)0x80,ref shfi,(uint)System.Runtime.InteropServices.Marshal.SizeOf(shfi),(uint)(0x100 | 0x400)); //取得Icon和TypeName
                        //添加图标
                        imglist.Images.Add(dir.Name, (Icon)Icon.FromHandle(shfi.hIcon).Clone());
                        info[0] = dir.Name;
                        info[1] = "";
                        info[2] = "文件夹";
                        info[3] = dir.LastWriteTime.ToString();
                        ListViewItem item = new ListViewItem(info, dir.Name);
                        lv.Items.Add(item);
                        //销毁图标
                        Win32.DestroyIcon(shfi.hIcon);
                    }
                }
                for (int i = 0; i < files.Length; i++)
                {
                    string[] info = new string[4];
                    FileInfo fi = new FileInfo(files[i]);
                    string Filetype = fi.Name.Substring(fi.Name.LastIndexOf(".") + 1, fi.Name.Length - fi.Name.LastIndexOf(".") - 1);
                    string newtype = Filetype.ToLower();
                    if (newtype == "sys" || newtype == "ini" || newtype == "bin" || newtype == "log" || newtype == "com" || newtype == "bat" || newtype == "db") return;
                    {
                        //获得图标
                        Win32.SHGetFileInfo(files[i],(uint)0x80,ref shfi,(uint)System.Runtime.InteropServices.Marshal.SizeOf(shfi),(uint)(0x100 | 0x400)); //取得Icon和TypeName
                        //添加图标
                        imglist.Images.Add(fi.Name, (Icon)Icon.FromHandle(shfi.hIcon).Clone());
                        info[0] = fi.Name;
                        info[1] = fi.Length.ToString();
                        info[2] = fi.Extension.ToString();
                        info[3] = fi.LastWriteTime.ToString();
                        ListViewItem item = new ListViewItem(info, fi.Name);
                        lv.Items.Add(item);
                        //销毁图标
                        Win32.DestroyIcon(shfi.hIcon);
                    }
                }
            }
            catch
            {
            }
        }

        public void OpenItem(string path, ImageList imglist, ListView lv, bool IsRootDrvPrt)
        {
            string pp = "";//
            string uu = "";//要打开的文件=路径+文件名
            if (IsRootDrvPrt)
            {
                    AllPath = path;
                    RefreshListViewItem(AllPath, imglist, lv);
            }
            else
            {
                uu = AllPath + path;
                if (Directory.Exists(uu))
                //是文件夹
                {
                    AllPath = AllPath + path + "\\";
                    pp = AllPath.Substring(0, AllPath.Length - 1);
                    RefreshListViewItem(pp, imglist, lv);
                }
                else
                //打开
                {
                    uu = AllPath + path;// 为什么写了两遍？
                    System.Diagnostics.Process.Start(uu);
                }
            }
        }

        public string Mpath()
        {
            string path = AllPath;
            return path;
        }
    }
}
