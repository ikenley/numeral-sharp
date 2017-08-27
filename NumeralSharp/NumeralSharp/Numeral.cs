using System;

namespace NumeralSharp
{
    public class Numeral
    {
        public double? Value { get; set; }

        public string Format(string format)
        {
            return Value.ToString();
        }

        public Numeral(double? value)
        {
            Value = value;
        }
    }
}
