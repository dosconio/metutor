using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoPremissBlock
{
    /// <summary>
    /// 冒泡排序
    /// 适合少量的数据排序
    /// </summary>
    class BubbleSorter
    {
        public static void BubbleSortIntREW(ref int[] oArr)
        {
            int tmp = 0;
            for (int i = 0; i < oArr.Length - 1; i++)
            {
                for (int j = i + 1; j < oArr.Length; j++)
                {
                    if (oArr[i] > oArr[j])
                    {
                        tmp = oArr[i];
                        oArr[i] = oArr[j];
                        oArr[j] = tmp;
                    }
                }
            }
        }
    }

    // pg239 yoin c#高级编程第七版例程-予[2]
}
