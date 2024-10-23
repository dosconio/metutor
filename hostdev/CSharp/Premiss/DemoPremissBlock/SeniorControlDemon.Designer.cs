namespace DemoPremissBlock
{
    partial class SeniorControlDemon
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.txtNodeName = new System.Windows.Forms.MaskedTextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.toolTip2 = new System.Windows.Forms.ToolTip(this.components);
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.button3 = new System.Windows.Forms.Button();
            this.splitter2 = new System.Windows.Forms.Splitter();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.button4 = new System.Windows.Forms.Button();
            this.btnAddRootNode = new System.Windows.Forms.Button();
            this.btnAddSonNode = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtNodeName
            // 
            this.txtNodeName.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtNodeName.Location = new System.Drawing.Point(222, 61);
            this.txtNodeName.Name = "txtNodeName";
            this.txtNodeName.Size = new System.Drawing.Size(300, 30);
            this.txtNodeName.TabIndex = 1;
            this.toolTip2.SetToolTip(this.txtNodeName, "CC");
            this.toolTip1.SetToolTip(this.txtNodeName, "HH");
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(222, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 39);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.toolTip2.SetToolTip(this.button1, "\r\n\r\n12542543453l");
            this.button1.UseVisualStyleBackColor = true;
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.Location = new System.Drawing.Point(221, 97);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.Size = new System.Drawing.Size(209, 398);
            this.propertyGrid1.TabIndex = 2;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(441, 95);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(473, 118);
            this.richTextBox1.TabIndex = 3;
            this.richTextBox1.Text = "23<a>12</a>";
            // 
            // button2
            // 
            this.button2.Dock = System.Windows.Forms.DockStyle.Left;
            this.button2.Location = new System.Drawing.Point(0, 0);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 596);
            this.button2.TabIndex = 4;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(75, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 596);
            this.splitter1.TabIndex = 5;
            this.splitter1.TabStop = false;
            // 
            // button3
            // 
            this.button3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.button3.Location = new System.Drawing.Point(78, 520);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(948, 76);
            this.button3.TabIndex = 6;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // splitter2
            // 
            this.splitter2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitter2.Location = new System.Drawing.Point(78, 517);
            this.splitter2.Name = "splitter2";
            this.splitter2.Size = new System.Drawing.Size(948, 3);
            this.splitter2.TabIndex = 7;
            this.splitter2.TabStop = false;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Location = new System.Drawing.Point(441, 219);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.treeView1);
            this.splitContainer1.Size = new System.Drawing.Size(473, 276);
            this.splitContainer1.SplitterDistance = 60;
            this.splitContainer1.TabIndex = 8;
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.LabelEdit = true;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(409, 276);
            this.treeView1.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.button4, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(94, 320);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(121, 175);
            this.tableLayoutPanel1.TabIndex = 9;
            // 
            // button4
            // 
            this.button4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button4.Location = new System.Drawing.Point(3, 3);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(54, 81);
            this.button4.TabIndex = 0;
            this.button4.Text = "button4";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // btnAddRootNode
            // 
            this.btnAddRootNode.Location = new System.Drawing.Point(529, 61);
            this.btnAddRootNode.Name = "btnAddRootNode";
            this.btnAddRootNode.Size = new System.Drawing.Size(104, 30);
            this.btnAddRootNode.TabIndex = 10;
            this.btnAddRootNode.Text = "添加根节点";
            this.btnAddRootNode.UseVisualStyleBackColor = true;
            this.btnAddRootNode.Click += new System.EventHandler(this.btnAddRootNode_Click);
            // 
            // btnAddSonNode
            // 
            this.btnAddSonNode.Location = new System.Drawing.Point(639, 61);
            this.btnAddSonNode.Name = "btnAddSonNode";
            this.btnAddSonNode.Size = new System.Drawing.Size(104, 30);
            this.btnAddSonNode.TabIndex = 10;
            this.btnAddSonNode.Text = "添加子节点";
            this.btnAddSonNode.UseVisualStyleBackColor = true;
            this.btnAddSonNode.Click += new System.EventHandler(this.btnAddSonNode_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(749, 61);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(113, 30);
            this.btnDelete.TabIndex = 10;
            this.btnDelete.Text = "删除选中节点";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // SeniorControlDemon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1026, 596);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnAddSonNode);
            this.Controls.Add(this.btnAddRootNode);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.splitter2);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.propertyGrid1);
            this.Controls.Add(this.txtNodeName);
            this.Controls.Add(this.button1);
            this.Name = "SeniorControlDemon";
            this.Text = "SeniorControlDemon";
            this.Load += new System.EventHandler(this.SeniorControlDemon_Load);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.MaskedTextBox txtNodeName;
        private System.Windows.Forms.ToolTip toolTip2;
        private System.Windows.Forms.PropertyGrid propertyGrid1;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Splitter splitter2;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button btnAddRootNode;
        private System.Windows.Forms.Button btnAddSonNode;
        private System.Windows.Forms.Button btnDelete;


    }
}