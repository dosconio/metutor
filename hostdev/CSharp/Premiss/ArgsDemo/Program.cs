using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArgsDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Arguments demonstration application.\nCiallo～切喽(∠·ω< )⌒<★");
            foreach (var v in args)
            {
                Console.WriteLine(v);
            }
            Console.ReadLine();
        }
    }
}
