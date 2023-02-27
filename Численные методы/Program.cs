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
            var x1 = NumericalMethodObject.Init(5.032M, 2);
            var x2 = NumericalMethodObject.Init(2.159M, 2);
            //Console.WriteLine(x1);
            var x3 = x1.Item1.Div(x2.Item1);
            //Console.WriteLine(x1 +Z)
            Console.ReadLine();

        }
    }
    class BisectionMethod
    {

    }
}
