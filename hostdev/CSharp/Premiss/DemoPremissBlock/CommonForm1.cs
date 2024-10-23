using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DemoPremissBlock
{
    public partial class CommonForm1 : Form
    {
        public CommonForm1()
        {
            InitializeComponent();
            // 试图实现半透明效果 失败
            label3.BackColor=Color.FromArgb(100,Color.Blue);
            // SetStyle(ControlStyles.Opaque, false);
            TransparencyKey = Color.Red;
            
        }

        private void CommonForm1_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label2.BackColor = Color.Red;
        }

        private void label1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("A");
        }
    }
}
