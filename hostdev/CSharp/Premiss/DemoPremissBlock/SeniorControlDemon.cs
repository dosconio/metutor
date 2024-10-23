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
    public partial class SeniorControlDemon : Form
    {
        public SeniorControlDemon()
        {
            InitializeComponent();
        }

        private void SeniorControlDemon_Load(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(button1, "Button!");
        }

        private void btnAddRootNode_Click(object sender, EventArgs e)
        {
            //要添加的节点名称为空，即文本框是否为空
           if(string.IsNullOrEmpty(txtNodeName.Text.Trim()))
               //删除前后空白
            {
                MessageBox.Show("要添加的节点名称不能为空！");
                return;
            }
               //添加根节点
                treeView1.Nodes.Add(txtNodeName.Text.Trim());
                txtNodeName.Text = "";
        }

        private void btnAddSonNode_Click(object sender, EventArgs e)
        {
          //要添加的节点名称为空，即文本框是否为空
            if (string.IsNullOrEmpty(txtNodeName.Text.Trim()))
            {
                 MessageBox.Show("要添加的节点名称不能为空！");
                 return;
            }
            if(treeView1.SelectedNode==null)
             {
                MessageBox.Show("请选择要添加子节点的节点！");
                 return;
             }               
            treeView1.SelectedNode.Nodes.Add(txtNodeName.Text.Trim());
            txtNodeName.Text = "";
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
         if (treeView1.SelectedNode == null)
            {
                 MessageBox.Show("请选择要删除的节点！");
                 return;
            }
               treeView1.SelectedNode.Remove();
        }
    }
}
