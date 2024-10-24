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
    public partial class MediaFilesBlk : UserControl
    {
        #region Property
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
        //public event ClickHandle _OnLblClick;
        public event TableText.ClickHandle _OnLblClick;

        public List<string> files;
        public List<string> filesnoimg;
        public List<string> eventid;
        public int fileid = -1;
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
        //public string _EVENTID
        //{
        //    get;
        //    set;
        //}
        #endregion
        public void Refr(int _fileid)
        {
            if (files != null && files.Count > _fileid) 
            {
                fileid = _fileid;
                //_EVENTID = label1.Text = files[_fileid];
                label1.Text = files[_fileid];
                if (filesnoimg != null && filesnoimg.Count > fileid) 
                {
                    this.BackgroundImage = Image.FromFile(filesnoimg[fileid]);
                }
            }
        }
        public MediaFilesBlk()
        {
            files = new List<string>();
            filesnoimg = new List<string>();
            eventid = new List<string>();
            InitializeComponent();
            labelLeft.BackColor = Color.FromArgb(63, Color.White);
            labelRight.BackColor = Color.FromArgb(63, Color.White);
        }

        private void label1_Click(object sender, EventArgs e)
        {
            if (_OnLblClick != null) 
            _OnLblClick(sender, e, (eventid!=null&&eventid.Count>fileid)?eventid
                [fileid]:"");
        }

        private void labelLeft_Click(object sender, EventArgs e)
        {
            if (files != null && files.Count > 1) 
            {
                if (fileid == 0)
                {
                    Refr(files.Count - 1);
                    return;
                }
                fileid--;
                //_EVENTID = label1.Text = files[fileid];
                label1.Text = files[fileid];
                if (filesnoimg != null && filesnoimg.Count > fileid)
                {
                    this.BackgroundImage = Image.FromFile(filesnoimg[fileid]);
                }
                
            }

        }

        private void labelRight_Click(object sender, EventArgs e)
        {
            if (files != null && files.Count > 1) 
            {
                if (files.Count == fileid + 1) 
                {
                    Refr(0);
                    return;
                }
                fileid++;
                //_EVENTID = label1.Text = files[fileid];
                label1.Text = files[fileid];
                if (filesnoimg != null && filesnoimg.Count > fileid)
                {
                    this.BackgroundImage = Image.FromFile(filesnoimg[fileid]);
                }
                
            }
        }
    }
}
