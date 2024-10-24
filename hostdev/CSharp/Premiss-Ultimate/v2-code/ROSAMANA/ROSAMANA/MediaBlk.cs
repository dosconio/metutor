using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ROSAMANA
{
    public partial class MediaBlk : UserControl
    {

        [Description("Text"), Category("Appearance")]
        public string Text
        {
            get
            {
                return label1.Text;
            }
            set
            {
                label1.Text = value;
            }
        }
        [Description("ForeColor"), Category("Appearance")]
        public Color _LblForeColor
        {
            get
            {
                return label1.ForeColor;
            }
            set
            {
                label1.ForeColor = value;
            }
        }
        [Description("Transparency"), Category("Appearance")]
        public int _Transparency
        {
            get
            {
                return BackColor.A;
            }
            set
            {
                BackColor = Color.FromArgb(value,Color.White);
            }
        }
        public delegate void ClickHandle(object sender, EventArgs e, string msg);
        public event ClickHandle _OnLblClick;
        public event ClickHandle _OnLeftClick;
        public event ClickHandle _OnRightClick;
        public string _EVENTID
        {
            get;
            set;
        }
        public MediaBlk()
        {
            InitializeComponent();
            labelLeft.BackColor = Color.FromArgb(63, Color.White);
            labelRight.BackColor = Color.FromArgb(63, Color.White);
        }
        public Font _FONT
        {
            get
            {
                return label1.Font;
            }
            set
            {
                label1.Font = value;
            }
        }
        private void label1_Click(object sender, EventArgs e)
        {
            if (_OnLblClick != null) 
            _OnLblClick(sender, e, _EVENTID);
        }

        private void labelLeft_Click(object sender, EventArgs e)
        {
            if (_OnLeftClick != null)
                _OnLeftClick(sender, e, _EVENTID);
        }

        private void labelRight_Click(object sender, EventArgs e)
        {
            if (_OnRightClick != null)
                _OnRightClick(sender, e, _EVENTID);
        }
    }
}
