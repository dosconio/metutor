using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace ROSAMANA
{
    partial class MainForm
    {
        /// <summary>
        /// 将颜色转换为整形值
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static Int32 ParseRGB(Color color)
        {
            return (Int32)(((uint)color.B << 16) | (ushort)(((ushort)color.G << 8) | color.R));
        }

        /// <summary>
        /// 将整形值还原为颜色。
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static Color RGB(Int32 color)
        {
            Int32 r = 0xFF & color;
            Int32 g = 0xFF00 & color;
            g >>= 8;
            Int32 b = 0xFF0000 & color;
            b >>= 16;
            return Color.FromArgb(r, g, b);
        }
    }
}
