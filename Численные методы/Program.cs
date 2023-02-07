using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            Console.WriteLine(x1 * x2);
            Console.ReadLine();

        }
    }
    public class NumericalMethodObject
    {
        public static bool WriteOperations { get; set; } = true;
        public decimal OriginalValue { get; set; }
        public decimal RoundedValue { get; set; }
        public decimal AbsoluteError { get; set; }
        public decimal RelativeError { get; set; }

        public NumericalMethodObject(decimal value, int roundDigitCount)
        {
            OriginalValue = value;
            RoundedValue = Math.Round(value, roundDigitCount);
            AbsoluteError = Math.Abs(value - RoundedValue);
            RelativeError = Math.Round(AbsoluteError / Math.Abs(RoundedValue), GetDecimalDigitsCount(value));

            WriteOperation($"a ≈ {RoundedValue}");

            WriteOperation($"Абсолютная погрешность:\n" +
                $"Δx=|x-a|\n" +
                $"Δx=|{OriginalValue} - {RoundedValue}|\n" +
                $"Δx=|{OriginalValue - RoundedValue}|\n" +
                $"Δx={Math.Abs(AbsoluteError)}\n");

            WriteOperation($"Относительная погрешность:\n" +
                $"δx=Δx/a\n" +
                $"δx={AbsoluteError} / {RoundedValue}\n" +
                $"δx={RoundFirstSignificantDigit(AbsoluteError / RoundedValue)}\n");



        }

        public static NumericalMethodObject operator +(NumericalMethodObject x, NumericalMethodObject y)
        {
            var addObject = new NumericalMethodObject();
            addObject.OriginalValue = x.OriginalValue + y.OriginalValue;
            addObject.RoundedValue = x.RoundedValue + y.RoundedValue;
            addObject.AbsoluteError =  x.AbsoluteError + y.AbsoluteError;
            addObject.RelativeError = RoundFirstSignificantDigit((Math.Abs(x.RoundedValue) / Math.Abs(x.RoundedValue + y.RoundedValue) * x.RelativeError +
                Math.Abs(y.RoundedValue) / Math.Abs(x.RoundedValue + y.RoundedValue) * y.RelativeError)
                );

            addObject.WriteOperation($"a1 + a2 = {x.RoundedValue} + {y.RoundedValue}\n" +
                $"a1 + a2 = {addObject.RoundedValue}\n\n" +

                $"Δ(x1 + x2) = Δx1 + Δx2\n" +
                $"Δ(x1 + x2) = {x.AbsoluteError} + {y.AbsoluteError}\n" +
                $"Δ(x1 + x2) = {addObject.AbsoluteError}\n\n" +

                $"δ(x1 + x2) = |a1|/|a1 + a2| * δx1 + |a2|/|a1 + a2| * δx2\n" +
                $"δ(x1 + x2) = {Math.Abs(x.RoundedValue)}/{Math.Abs(x.RoundedValue + y.RoundedValue)} * {x.RelativeError} + {Math.Abs(y.RoundedValue)}/{Math.Abs(x.RoundedValue + y.RoundedValue)} * {y.RelativeError}\n" +
                $"δ(x1 + x2) = {addObject.RelativeError}");

            return addObject;
        }
        public static NumericalMethodObject operator -(NumericalMethodObject x, NumericalMethodObject y)
        {
            var addObject = new NumericalMethodObject();
            addObject.OriginalValue = x.OriginalValue - y.OriginalValue;
            addObject.RoundedValue = x.RoundedValue - y.RoundedValue;
            addObject.AbsoluteError = x.AbsoluteError + y.AbsoluteError;
            addObject.RelativeError = RoundFirstSignificantDigit(Math.Abs(x.RoundedValue) / Math.Abs(x.RoundedValue - y.RoundedValue) * x.RelativeError +
                Math.Abs(y.RoundedValue) / Math.Abs(x.RoundedValue - y.RoundedValue) * y.RelativeError
                );

            addObject.WriteOperation($"a1 - a2 = {x.RoundedValue} - {y.RoundedValue}\n" +
                $"a1 - a2 = {addObject.RoundedValue}\n\n" +

                $"Δ(x1 - x2) = Δx1 - Δx2\n" +
                $"Δ(x1 - x2) = {x.AbsoluteError} + {y.AbsoluteError}\n" +
                $"Δ(x1 - x2) = {addObject.AbsoluteError}\n\n" +

                $"δ(x1 - x2) = |a1|/|a1 - a2| * δx1 + |a2|/|a1 - a2| * δx2\n" +
                $"δ(x1 - x2) = {Math.Abs(x.RoundedValue)}/{Math.Abs(x.RoundedValue - y.RoundedValue)} * {x.RelativeError} + {Math.Abs(y.RoundedValue)}/{Math.Abs(x.RoundedValue - y.RoundedValue)} * {y.RelativeError}\n" +
                $"δ(x1 + x2) = {addObject.RelativeError}");

            return addObject;
        }
        public static NumericalMethodObject operator *(NumericalMethodObject x, NumericalMethodObject y)
        {
            var addObject = new NumericalMethodObject();
            addObject.OriginalValue = x.OriginalValue * y.OriginalValue;
            addObject.RoundedValue = x.RoundedValue * y.RoundedValue;
            addObject.AbsoluteError = (Math.Abs(x.RoundedValue)*x.AbsoluteError) + (Math.Abs(y.RoundedValue)*y.AbsoluteError);
            addObject.RelativeError = RoundFirstSignificantDigit(x.RelativeError + y.RelativeError);

            addObject.WriteOperation($"a1 * a2 = {x.RoundedValue} * {y.RoundedValue}\n" +
                $"a1 * a2 = {addObject.RoundedValue}\n\n" +

                $"Δ(x1 * x2) = |a1|Δx1 + |a2|Δx2\n" +
                $"Δ(x1 * x2) = {Math.Abs(x.RoundedValue)}*{x.AbsoluteError} + {Math.Abs(y.RoundedValue)}*{y.AbsoluteError}\n" +
                $"Δ(x1 * x2) = {addObject.AbsoluteError}\n\n" +

                $"δ(x1 * x2) = δx1 + δx2\n" +
                $"δ(x1 * x2) = {x.RelativeError} + {y.RelativeError}\n" +
                $"δ(x1 * x2) = {addObject.RelativeError}");

            return addObject;
        }
        public NumericalMethodObject(float value, int roundDigitCount) : this((decimal)value, roundDigitCount)
        {
        }
        public NumericalMethodObject(double value, int roundDigitCount) : this((decimal)value, roundDigitCount)
        {
        }

        public NumericalMethodObject(decimal originalValue, decimal roundedValue, decimal absoluteError, decimal relativeError)
        {
            OriginalValue = originalValue;
            RoundedValue = roundedValue;
            AbsoluteError = absoluteError;
            RelativeError = relativeError;
        }

        public NumericalMethodObject()
        {
        }

        public override string ToString()
        {
            return $"{OriginalValue}, {RoundedValue}, Δ{AbsoluteError}, δ{RelativeError}";
        }

        public void WriteOperation(string text)
        {
            if (WriteOperations)
                Console.WriteLine(text);
        }

        static decimal RoundFirstSignificantDigit(decimal input)
        {

            if (input == 0)
                return (decimal)input;

            int precision = 0;
            var val = input - Math.Round(input, 0);
            while (Math.Abs(val) < 1)
            {
                val *= 10;
                precision++;
            }
            return (decimal)Math.Round(input, precision);
        }


        static int GetDecimalDigitsCount(decimal number)
        {
            string[] str = number.ToString(new System.Globalization.NumberFormatInfo() { NumberDecimalSeparator = "." }).Split('.');
            return (str.Length == 2 ? str[1].Length : 0)+1;
        }
    }
}
