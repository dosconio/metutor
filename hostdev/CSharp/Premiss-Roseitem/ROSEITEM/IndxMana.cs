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
    public partial class IndxMana : Form
    {
        public IndxMana()
        {
            InitializeComponent();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            string ThisPath = Application.StartupPath;//only add \ in root path
            if (ThisPath[ThisPath.Length - 1] != '\\') ThisPath += "\\";
            string IdxPath = rose.CnrvLocation("IDX\\" + textBox1.Text);
            if (e.KeyChar == 0x0D && textBox1.Text != "" && IdxPath != "")  
            {
                textBox2.Text = File.ReadAllText(IdxPath);
                //...
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (textBox3.Text != "")
            {
                string RosePath = rose.CnrvLocation("ROS\\" + textBox3.Text);
                if (RosePath == "")
                {
                    label2.Text = "NOT FOUND";
                }
                else
                {
                    label2.Text = "";
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (label2.Text != "" || textBox4.Text == "") return;
            //string IdxPath = rose.CnrvLocation("IDX\\" + textBox4.Text);
            //if (IdxPath == "") return;
            string IdxPath = textBox5.Text + ":\\INDX\\" + textBox4.Text;
            string Prev = (File.Exists(IdxPath)?File.ReadAllText(IdxPath):"");
            Prev += "\n" + textBox6.Text + ":" + textBox3.Text;
            File.WriteAllText(IdxPath, Prev);
            MessageBox.Show("Successful!");
        }
    }
}
