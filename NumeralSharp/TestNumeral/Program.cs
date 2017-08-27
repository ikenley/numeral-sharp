using System;
using NumeralSharp;

namespace TestNumeral
{
    class Program
    {
        static void Main(string[] args)
        {
            var numeral = new Numeral(0.47351);
            var formattedNum = numeral.Format("0.00%");
            Console.WriteLine(formattedNum); //"47.35%"
        }
    }
}
