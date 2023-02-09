using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NumericalMethodLib;


namespace Численные_методы
{
    class Program
    {
        // Δ δ
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            var x1 = new NumericalMethodObject(5.032, 2);
            var x2 = new NumericalMethodObject(2.159, 2);
            //Console.WriteLine(x1);
            Console.WriteLine(x1 / x2);
            Console.ReadLine();

        }
    }
}
