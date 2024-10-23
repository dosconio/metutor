using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DemoPremissBlock
{
    class Program
    {
        // 字体 星夜奶酪体
        [STAThread]
        static void Main(string[] args)
        {
            /// モシ版本阶乘冒泡排序示例
            //int[] a = { 5, 4, 3, 2, 1, 7, 9 };
            //BubbleSorter.BubbleSortIntREW(ref a);
            //foreach (int ai in a)
            //{
            //    Console.Write(ai.ToString() + " ");
            //}

            /// Label控件
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //LabelDemons ld = new LabelDemons();
            ////ld.CreateMain(); 现换为构造函数
            //Application.Run(ld);

            /// Panel Button Textbook CheckBox ChecklistBox ComboBox
            /// 
            /// ColorDialog LinkLabel PictureBox WebBrowser
            /// FontDialog RatioButton SaveFileDialog RichTextBox
            /// ProgressBar MenuStrip ToolStrip ListView
            /// MonthCalendar~DateTimePicker StatusStrip
            /// ContextMenuStrip GroupBox=Label+Panel
            /// HScrollBar(横向滚动条)  VScrollBar(纵向滚动条)
            /// ToolTip 绑定提示框
            /// ImageList 由其他控件使用的图片组
            /// MaskedTextBox 掩码
            /// TrackBar 滑动条
            /// PropertyGrid 属性框
            /// TabControl 有 Panel 的原理，可以绑定
            
            ///
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new TakeInExtWin());


            //Console.Read();
        }
    }
}
