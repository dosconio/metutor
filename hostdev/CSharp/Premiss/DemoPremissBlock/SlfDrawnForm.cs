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
    //定义一个枚举，表示拖动方向
    public enum MouseDirection
    {
        Herizontal,//水平方向拖动，只改变窗体的宽度
        Vertical,//垂直方向拖动，只改变窗体的高度
        Declining,//倾斜方向，同时改变窗体的宽度和高度
        None//不做标志，即不拖动窗体改变大小
    }
    public partial class SlfDrawnForm : Form // REW 11 , 自绘部分耗时2小时
    {
        // 随意拖动的实现
        private bool click = false;//鼠标是否点击
        private int xposition = 0;//面板相对屏幕的X轴坐标
        private int yposition = 0;//面板相对屏幕的Y轴坐标

        bool isMouseDown = false; //表示鼠标当前是否处于按下状态，初始值为否 
        MouseDirection direction = MouseDirection.None;//表示拖动的方向，起始为None，表示不拖动

        public SlfDrawnForm()
        {
            InitializeComponent();
            // 设置无边框模式
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;


        }

        private void SlfDrawnForm_Load(object sender, EventArgs e)
        {
            TabPage tabPage = new TabPage("AutoPage");
            tabControl1.TabPages.Add(tabPage);
        }

        private void SlfDrawnForm_MouseDown(object sender, MouseEventArgs e)
        {
            // 随意拖动的实现
            click = true;
            xposition = e.X;
            yposition = e.Y;

            isMouseDown = true;
        }

        private void SlfDrawnForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (!isMouseDown) { 
                //鼠标移动过程中，坐标时刻在改变
                //当鼠标移动时横坐标距离窗体右边缘5像素以内且纵坐标距离下边缘也在5像素以内时，要将光标变为倾斜的箭头形状，同时拖拽方向direction置为MouseDirection.Declining
                if (e.Location.X >= this.Width - 5 && e.Location.Y > this.Height - 5)
                {
                    this.Cursor = Cursors.SizeNWSE;
                    direction = MouseDirection.Declining;
                }
                //当鼠标移动时横坐标距离窗体右边缘5像素以内时，要将光标变为倾斜的箭头形状，同时拖拽方向direction置为MouseDirection.Herizontal
                else if (e.Location.X >= this.Width - 5)
                {
                    this.Cursor = Cursors.SizeWE;
                    direction = MouseDirection.Herizontal;
                }
                //同理当鼠标移动时纵坐标距离窗体下边缘5像素以内时，要将光标变为倾斜的箭头形状，同时拖拽方向direction置为MouseDirection.Vertical
                else if (e.Location.Y >= this.Height - 5)
                {
                    this.Cursor = Cursors.SizeNS;
                    direction = MouseDirection.Vertical;
                }
                else 
                {
                    this.Cursor = Cursors.Arrow;
                    direction = MouseDirection.None;
                }
                //否则，以外的窗体区域，鼠标星座均为单向箭头（默认）
            }
            else
                     if (direction != MouseDirection.None) ResizeWindow();
                     else
                     {
                         // 随意拖动的实现
                         if (e.Button == MouseButtons.Left)
                         {
                             if (click)
                             {
                                 label1.Text = "MousePosition" + MousePosition.X.ToString() + ":" + MousePosition.Y.ToString() + "\n"
                                     + "Position" + xposition.ToString() + ":" + yposition.ToString() + "\n"
                                 + Width.ToString() + "*" + Height.ToString();
                                 this.SetBounds((MousePosition.X - xposition), (MousePosition.Y - yposition), this.Size.Width, this.Size.Height);
                             }
                         }
                         ///end
                     }
                

        }

        private void SlfDrawnForm_MouseUp(object sender, MouseEventArgs e)
        {
            // 随意拖动的实现
            click = true;
            xposition = e.X;
            yposition = e.Y;

            isMouseDown = false;
            //既然鼠标弹起了，那么就不能再改变窗体尺寸，拖拽方向置 none
            direction = MouseDirection.None;
        }

        private void ResizeWindow()
 {
    //这个判断很重要，只有在鼠标按下时才能拖拽改变窗体大小，如果不作判断，那么鼠标弹起和按下时，窗体都可以改变
   if (!isMouseDown)
     return;
   //MousePosition的参考点是屏幕的左上角，表示鼠标当前相对于屏幕左上角的坐标this.left和this.top的参考点也是屏幕，属性MousePosition是该程序的重点
   if (direction == MouseDirection.Declining)
   {
          //此行代码在mousemove事件中已经写过，在此再写一遍，并不多余，一定要写
      this.Cursor = Cursors.SizeNWSE;
      //下面是改变窗体宽和高的代码，不明白的可以仔细思考一下
     this.Width = MousePosition.X - this.Left;
     this.Height = MousePosition.Y - this.Top;
    }
     //以下同理
     if (direction == MouseDirection.Herizontal)
    {
         this.Cursor = Cursors.SizeWE;
         this.Width = MousePosition.X - this.Left;
    }
   else if (direction == MouseDirection.Vertical)
   {
          this.Cursor = Cursors.SizeNS;
          this.Height = MousePosition.Y - this.Top;
   }
  //即使鼠标按下，但是不在窗口右和下边缘，那么也不能改变窗口大小
   else
     this.Cursor = Cursors.Arrow;

  }

        private void button1_Click(object sender, EventArgs e)
        {
            colorDialog1.AllowFullOpen = true; // false 调色板
            colorDialog1.AnyColor = true;
            colorDialog1.Color = Color.AliceBlue;
            // colorDialog1.Reset();
            colorDialog1.FullOpen = true; // 渐变条带形式
            // colorDialog1.SolidColorOnly = false;
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                label1.Text = colorDialog1.Color.ToString();
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (fontDialog1.ShowDialog() == DialogResult.OK)
            {
                label1.Text = fontDialog1.Font.ToString();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Title = "This is title";
            
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                label1. Text = saveFileDialog1.FileName.ToString();
            }
        }
    
    }
}
