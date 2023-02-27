using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace NumericalMethodLib
{
    public class NumericalMethodObject
    {
        public decimal OriginalValue { get; set; }
        public decimal RoundedValue { get; set; }
        public decimal AbsoluteError { get; set; }
        public decimal RelativeError { get; set; }

        public static (NumericalMethodObject, string) Init(decimal value, int roundDigitCount)
        {
            var obj = new NumericalMethodObject();
            obj.OriginalValue = value;
            obj.RoundedValue = Math.Round(value, roundDigitCount);
            obj.AbsoluteError = Math.Abs(value - obj.RoundedValue);
            obj.RelativeError = Math.Round(obj.AbsoluteError / Math.Abs(obj.RoundedValue), GetDecimalDigitsCount(value));

            string output = $"a ≈ {obj.RoundedValue}\n" + 

            $"Абсолютная погрешность:\n" +
                $"Δx=|x-a|\n" +
                $"Δx=|{obj.OriginalValue} - {obj.RoundedValue}|\n" +
                $"Δx=|{obj.OriginalValue - obj.RoundedValue}|\n" +
                $"Δx={Math.Abs(obj.AbsoluteError)}\n" +

            $"Относительная погрешность:\n" +
                $"δx=Δx/a\n" +
                $"δx={obj.AbsoluteError} / {obj.RoundedValue}\n" +
                $"δx={RoundFirstSignificantDigit(obj.AbsoluteError / obj.RoundedValue)}\n";

            return (obj, output);
        }

        public (NumericalMethodObject, string) Add(NumericalMethodObject x)
        {
            var addObject = new NumericalMethodObject();
            addObject.OriginalValue = x.OriginalValue + OriginalValue;
            addObject.RoundedValue = x.RoundedValue + RoundedValue;
            addObject.AbsoluteError = x.AbsoluteError + AbsoluteError;
            addObject.RelativeError = RoundFirstSignificantDigit((Math.Abs(x.RoundedValue) / Math.Abs(x.RoundedValue + RoundedValue) * x.RelativeError +
                Math.Abs(RoundedValue) / Math.Abs(x.RoundedValue + RoundedValue) * RelativeError)
                );

            string output = $"a1 + a2 = {x.RoundedValue} + {RoundedValue}\n" +
                $"a1 + a2 = {addObject.RoundedValue}\n\n" +

                $"Δ(x1 + x2) = Δx1 + Δx2\n" +
                $"Δ(x1 + x2) = {x.AbsoluteError} + {AbsoluteError}\n" +
                $"Δ(x1 + x2) = {addObject.AbsoluteError}\n\n" +

                $"δ(x1 + x2) = |a1|/|a1 + a2| * δx1 + |a2|/|a1 + a2| * δx2\n" +
                $"δ(x1 + x2) = {Math.Abs(x.RoundedValue)}/{Math.Abs(x.RoundedValue + RoundedValue)} * {x.RelativeError} + {Math.Abs(RoundedValue)}/{Math.Abs(x.RoundedValue + RoundedValue)} * {RelativeError}\n" +
                $"δ(x1 + x2) = {addObject.RelativeError}";

            return (addObject, output);
        }

        public (NumericalMethodObject, string) Sub(NumericalMethodObject x)
        {
            var addObject = new NumericalMethodObject();
            addObject.OriginalValue = x.OriginalValue - OriginalValue;
            addObject.RoundedValue = x.RoundedValue - RoundedValue;
            addObject.AbsoluteError = x.AbsoluteError + AbsoluteError;
            addObject.RelativeError = RoundFirstSignificantDigit(Math.Abs(x.RoundedValue) / Math.Abs(x.RoundedValue - RoundedValue) * x.RelativeError +
                Math.Abs(RoundedValue) / Math.Abs(x.RoundedValue - RoundedValue) * RelativeError
                );

            string output = $"a1 - a2 = {x.RoundedValue} - {RoundedValue}\n" +
                $"a1 - a2 = {addObject.RoundedValue}\n\n" +

                $"Δ(x1 - x2) = Δx1 - Δx2\n" +
                $"Δ(x1 - x2) = {x.AbsoluteError} + {AbsoluteError}\n" +
                $"Δ(x1 - x2) = {addObject.AbsoluteError}\n\n" +

                $"δ(x1 - x2) = |a1|/|a1 - a2| * δx1 + |a2|/|a1 - a2| * δx2\n" +
                $"δ(x1 - x2) = {Math.Abs(x.RoundedValue)}/{Math.Abs(x.RoundedValue - RoundedValue)} * {x.RelativeError} + {Math.Abs(RoundedValue)}/{Math.Abs(x.RoundedValue - RoundedValue)} * {RelativeError}\n" +
                $"δ(x1 + x2) = {addObject.RelativeError}";

            return (addObject, output);
        }
        public (NumericalMethodObject, string) Mul(NumericalMethodObject x)
        {
            var addObject = new NumericalMethodObject();
            addObject.OriginalValue = x.OriginalValue * OriginalValue;
            addObject.RoundedValue = x.RoundedValue * RoundedValue;
            addObject.AbsoluteError = (Math.Abs(x.RoundedValue) * x.AbsoluteError) + (Math.Abs(RoundedValue) * AbsoluteError);
            addObject.RelativeError = RoundFirstSignificantDigit(x.RelativeError + RelativeError);

            string output = $"a1 * a2 = {x.RoundedValue} * {RoundedValue}\n" +
                $"a1 * a2 = {addObject.RoundedValue}\n\n" +

                $"Δ(x1 * x2) = |a1|Δx1 + |a2|Δx2\n" +
                $"Δ(x1 * x2) = {Math.Abs(x.RoundedValue)}*{x.AbsoluteError} + {Math.Abs(RoundedValue)}*{AbsoluteError}\n" +
                $"Δ(x1 * x2) = {addObject.AbsoluteError}\n\n" +

                $"δ(x1 * x2) = δx1 + δx2\n" +
                $"δ(x1 * x2) = {x.RelativeError} + {RelativeError}\n" +
                $"δ(x1 * x2) = {addObject.RelativeError}";

            return (addObject, output);
        }
        public (NumericalMethodObject, string) Div(NumericalMethodObject x)
        {
            var addObject = new NumericalMethodObject();
            addObject.OriginalValue = x.OriginalValue / OriginalValue;
            addObject.RoundedValue = x.RoundedValue / RoundedValue;
            addObject.AbsoluteError = ((Math.Abs(x.RoundedValue) * x.AbsoluteError) + (Math.Abs(RoundedValue) * AbsoluteError)) / (RoundedValue * RoundedValue);
            addObject.RelativeError = RoundFirstSignificantDigit(x.RelativeError + RelativeError);

            string output = $"a1 / a2 = {x.RoundedValue} / {RoundedValue}\n" +
                $"a1 / a2 = {addObject.RoundedValue}\n\n" +

                $"Δ(x1 / x2) = |a1|Δx1 + |a2|Δx2 / a2^2\n" +
                $"Δ(x1 * x2) = {Math.Abs(x.RoundedValue)}*{x.AbsoluteError} + {Math.Abs(RoundedValue)}*{AbsoluteError} / {RoundedValue}^2\n" +
                $"Δ(x1 * x2) = {addObject.AbsoluteError}\n\n" +

                $"δ(x1 * x2) = δx1 + δx2\n" +
                $"δ(x1 * x2) = {x.RelativeError} + {RelativeError}\n" +
                $"δ(x1 * x2) = {addObject.RelativeError}";

            return (addObject, output);
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
            return (str.Length == 2 ? str[1].Length : 0) + 1;
        }
    }
}
