using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
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
            Console.WriteLine(x1 + x2);
            Console.ReadLine();

        }
    }
    public class NumericalMethodObject
    {
        public static bool WriteOperations { get; set; } = true;
        public decimal OriginalValue { get; }
        public decimal RoundedValue { get; }
        public decimal AbsoluteError { get; }
        public decimal RelativeError { get; }

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
                $"δx=Δx/|a|\n" +
                $"δx=|{AbsoluteError} / {Math.Abs(RoundedValue)}|\n" +
                $"δx=|{AbsoluteError / Math.Abs(RoundedValue)}|\n" +
                $"δx={RelativeError}");



        }

        public static NumericalMethodObject operator +(NumericalMethodObject x, NumericalMethodObject y)
        {
            return new NumericalMethodObject(
                x.OriginalValue + y.OriginalValue,
                x.RoundedValue + y.RoundedValue,
                x.AbsoluteError + y.AbsoluteError,
               (Math.Abs(x.RoundedValue) / Math.Abs(x.RoundedValue + y.RoundedValue) * x.AbsoluteError +
                Math.Abs(y.RoundedValue) / Math.Abs(x.RoundedValue + y.RoundedValue) * y.AbsoluteError
                ));
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

        public override string ToString()
        {
            return $"{OriginalValue}, {RoundedValue}, Δ{AbsoluteError}, δ{RelativeError}";
        }

        private void WriteOperation(string text)
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
            return str.Length == 2 ? str[1].Length : 0;
        }
    }
}
