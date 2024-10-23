using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Drawing;

namespace DemoPremissBlock
{
    class TabLabelX:System.Windows.Forms.Label
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label label;
        public System.Windows.Forms.Label btn;
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        public TabLabelX()
        {
            InitializeComponent();
            this.SuspendLayout();
            Width = 150;
            Height = 32;
            btn.Location = new Point(118, 0);
            btn.Size = new System.Drawing.Size(32, 32);
            label.Top = label.Left = 0;
            label.Width = btn.Left;
            label.Height = this.Height;
            this.ResumeLayout(false);
            Console.WriteLine(this.Size.ToString());

        }

        private void InitializeComponent()
        {
            this.label = new System.Windows.Forms.Label();
            this.btn = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label
            // 
            this.label.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label.Location = new System.Drawing.Point(0, 0);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(112, 32);
            this.label.TabIndex = 0;
            this.label.Text = "TabLabel";
            this.label.AutoSize = false;
            this.label.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.label.BackColorChanged += new System.EventHandler(this.label_BackColorChanged);
            this.label.Click += new System.EventHandler(this.label_Click);
            this.label.MouseEnter += new System.EventHandler(this.label_MouseEnter);
            this.label.MouseLeave += new System.EventHandler(this.label_MouseLeave);
            // 
            // btn
            // 
            this.btn.AutoSize = false;
            this.btn.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btn.Font = new System.Drawing.Font("微软雅黑", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn.Location = new System.Drawing.Point(118, 0);
            this.btn.Name = "btn";
            this.btn.Size = new System.Drawing.Size(32, 32);
            this.btn.TabIndex = 1;
            this.btn.Text = "×";
            this.btn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btn.Click += new System.EventHandler(this.btn_Click);
            this.btn.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btn_MouseDown);
            this.btn.MouseEnter += new System.EventHandler(this.btn_MouseEnter);
            this.btn.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btn.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btn_MouseUp);
            // 
            // TabLabel
            // 
            
            this.Controls.Add(this.label);
            this.Controls.Add(this.btn);
            this.MaximumSize = new System.Drawing.Size(150, 32);
            this.MinimumSize = new System.Drawing.Size(150, 32);
            this.Name = "TabLabel";
            this.AutoSize = false;
            this.ResumeLayout(false);

        }
        public delegate void ClickHandle(object sender, EventArgs e);
        public event ClickHandle LabelClick;
        public event ClickHandle CloseBtnClick;
        private bool _checked = false;
        //标签
        private Color NormalBackColor = SystemPens.ControlDarkDark.Color
                       , FocusBackColor = SystemPens.ControlDark.Color
                       , DownBackColor = Color.Pink;
        //关闭按钮
        private Color NormalBtnColor = SystemPens.ControlDarkDark.Color
               , FocusBtnColor = SystemPens.ControlDark.Color
               , DownBtnColor = Color.Pink;
        [Description("是否被按下"), Category("Appearance")]
        public bool Checked
        {
            get { return _checked; }
            set { _checked = value; }
        }
        [Description("获取或设置标题"), Category("Appearance")]
        public string Title
        {
            get
            {
                return label.Text;
            }
            set
            {
                label.Text = value;
            }
        }

        [Description("普通状态标签背景颜色"), Category("Appearance")]
        public Color _LabelNormalBackColor
        {
            get
            {
                return NormalBackColor;
            }
            set
            {
                NormalBackColor = value;
                if (!_checked)
                    this.label.BackColor = NormalBackColor; // 否则刷新不及时
            }
        }

        [Description("点燃状态标签背景颜色"), Category("Appearance")]
        public Color _FocusBackColor
        {
            get
            {
                return FocusBackColor;
            }
            set
            {
                FocusBackColor = value;
            }
        }

        [Description("按下状态标签背景颜色"), Category("Appearance")]
        public Color _DownBackColor
        {
            get { return DownBackColor; }
            set
            {
                DownBackColor = value;
                if (!_checked) this.label.BackColor = DownBackColor;
            }
        }
        [Description("字符前景颜色"), Category("Appearance")]
        public Color _FontColor
        {
            get
            {
                return label.ForeColor;
            }
            set
            {
                label.ForeColor = value;
            }
        }
        [Description("字符字体"), Category("Appearance")]
        public Font _FontSet
        {
            get
            {
                return label.Font;
            }
            set
            {
                label.Font = value;
            }
        }
        [Description("普通状态按钮颜色"), Category("Appearance")]
        public Color _BtnNormalBackColor
        {
            get
            {
                return NormalBtnColor;
            }
            set
            {
                NormalBtnColor = value;

                this.btn.ForeColor = NormalBtnColor; // 否则刷新不及时
            }
        }

        [Description("点燃状态按钮颜色"), Category("Appearance")]
        public Color _BtnFocusBackColor
        {
            get
            {
                return FocusBtnColor;
            }
            set
            {
                FocusBtnColor = value;
            }
        }

        [Description("按下状态按钮颜色"), Category("Appearance")]
        public Color _BtnDownBackColor
        {
            get { return DownBtnColor; }
            set
            {
                DownBtnColor = value;
            }
        }

        private void label_Click(object sender, EventArgs e)
        {
            if (LabelClick != null)
            {
                LabelClick(sender, e);
                if (_checked)
                    label.BackColor = DownBackColor;
            }

        }

        private void btn_Click(object sender, EventArgs e)
        {
            if (CloseBtnClick != null)
                CloseBtnClick(sender, e);
        }
        private void label_MouseEnter(object sender, EventArgs e)
        {
            if (!_checked)
                label.BackColor = FocusBackColor;
        }

        private void label_MouseLeave(object sender, EventArgs e)
        {
            if (!_checked)
                label.BackColor = NormalBackColor;
        }

        private void btn_MouseEnter(object sender, EventArgs e)
        {
            btn.ForeColor = FocusBtnColor;
        }

        private void btn_MouseLeave(object sender, EventArgs e)
        {
            btn.ForeColor = NormalBtnColor;
        }

        private void btn_MouseDown(object sender, MouseEventArgs e)
        {
            btn.ForeColor = DownBtnColor;
        }

        private void label_BackColorChanged(object sender, EventArgs e)
        {
            btn.BackColor = label.BackColor;
        }

        private void btn_MouseUp(object sender, MouseEventArgs e)
        {
            btn.ForeColor = FocusBtnColor;
        }


    }
}
