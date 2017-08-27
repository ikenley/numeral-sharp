using System;
using NumeralSharp;

namespace TestNumeral
{
    class Program
    {
        static void Main(string[] args)
        {
            PrintLine(0, "0.0");
            PrintLine(1, "0.0");
            PrintLine(null, "0.0");
            PrintLine(1, "0.00%");
            PrintLine(0.47351, "0.0%");
            PrintLine(0.47351, "0.00%");
            PrintLine(10059.30, "$0,0.0");
            PrintLine(1000045023, "$0,0.00a");
        }

        static void PrintLine(double? value, string format)
        {
            var numeral = new Numeral(value);
            var output = numeral.Format(format);
            Console.WriteLine(
                string.Format("({0}, {1}) => {2}", value, format, output)
            );
        }
    }
}
