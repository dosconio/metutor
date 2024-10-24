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
    public partial class AlbumMana : Form
    {
        string albpath;
        public AlbumMana()
        {
            InitializeComponent();
        }

        private void AlbumMana_Load(object sender, EventArgs e)
        {
            Refr();
        }
        private void Refr()
        {
            SuspendLayout();
            listAlb.Items.Clear();
            string ThisPath = Application.StartupPath;//only add \ in root path
            if (ThisPath[ThisPath.Length - 1] != '\\') ThisPath += "\\";
            if (File.Exists(ThisPath + "ALBUM"))
            {
                string[] TmpAlbums = File.ReadAllText(ThisPath + "ALBUM").Split(new char[] { '\n' });
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
        private void button1_Click(object sender, EventArgs e)
        {
            string ThisPath = Application.StartupPath;//only add \ in root path
            if (ThisPath[ThisPath.Length - 1] != '\\') ThisPath += "\\";
            if (!File.Exists(ThisPath + "ALBUM")) return;
            if (textBox2.Text == "") return;
            {
                //Append Album
                string PreviousAlb = File.ReadAllText(ThisPath + "ALBUM");
                PreviousAlb += "\n" + textBox2.Text;
                File.WriteAllText(ThisPath + "ALBUM", PreviousAlb);
                //Create Empty file
                File.WriteAllText(ThisPath + textBox2.Text, "");
            }
            Refr();
        }

        private void listAlb_DoubleClick(object sender, EventArgs e)
        {
            if (listAlb.SelectedIndex >= 0)
            {
                string ThisPath = Application.StartupPath;//only add \ in root path
                if (ThisPath[ThisPath.Length - 1] != '\\') ThisPath += "\\";
                albpath = ThisPath + listAlb.Items[listAlb.SelectedIndex];
                textBox1.Text = File.ReadAllText(albpath);
                Text = albpath;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string ThisPath = Application.StartupPath;//only add \ in root path
            if (ThisPath[ThisPath.Length - 1] != '\\') ThisPath += "\\";
            if (albpath != null)
            {
                File.WriteAllText(albpath, textBox1.Text);
            }
        }
    }
}
