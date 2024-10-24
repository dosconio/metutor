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
    public partial class TableText : UserControl
    {
        public string LockedContext=null;
        bool Changed = false;

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

        public delegate void ClickHandle(object sender, EventArgs e, string msg);
        public event ClickHandle _OnLblClick;
        public string _EVENTID
        {
            get;
            set;
        }
        public TableText()
        {
            InitializeComponent();
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

        private void label1_DoubleClick(object sender, EventArgs e)
        {
            if (_OnLblClick != null)
                _OnLblClick(sender, e, _EVENTID);
        }

        private void label1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = false;
        }

        private void label1_TextChanged(object sender, EventArgs e)
        {
            if (Changed)
            {
                label1.Text = LockedContext;
                //MessageBox.Show(LockedContext);
            }
            else
            {
                LockedContext = label1.Text;
                Changed = true;
                //MessageBox.Show(LockedContext);
            }
        }

        private void label1_MouseEnter(object sender, EventArgs e)
        {
            label1.ReadOnly = false;
        }

        private void label1_MouseLeave(object sender, EventArgs e)
        {
            label1.ReadOnly = true;
        }
    }
}
