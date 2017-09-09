using System;
using NumeralSharp;

namespace TestNumeral
{
    class Program
    {
        static void Main(string[] args)
        {
            /* NumeralJS style */
            PrintLineJS(0, "0.0");
            PrintLineJS(1, "0.0");
            PrintLineJS(null, "0.0");
            PrintLineJS(1, "0.00%");
            PrintLineJS(0.47351, "0.0%");
            PrintLineJS(0.47351, "0.00%");
            PrintLineJS(10059.30, "$0,0.0");
            PrintLineJS(1000045023, "$0,0.00a");

            Console.WriteLine("***********");

            /* C# style */
            var numeral = new Numeral("Less than 11", "Zero", null);
            PrintLineC(0, "0.0", numeral);
            PrintLineC(1, "0.0", numeral);
            PrintLineC(null, "0.0", numeral);
            PrintLineC(1, "0.00%", numeral);
            PrintLineC(0.47351, "0.0%", numeral);
            PrintLineC(0.47351, "0.00%", numeral);
            PrintLineC(10059.30, "$0,0.0", numeral);
            PrintLineC(1000045023, "$0,0.00a", numeral);
        }

        static void PrintLineJS(double? value, string format)
        {
            var numeral = new Numeral(value);
            var output = numeral.Format(format);
            Console.WriteLine(
                string.Format("({0}, {1}) => {2}", value, format, output)
            );
        }

        static void PrintLineC(double? value, string format, Numeral numeral)
        {
            var output = numeral.Format(value, format);
            Console.WriteLine(
                string.Format("({0}, {1}) => {2}", value, format, output)
            );
        }
    }
}
