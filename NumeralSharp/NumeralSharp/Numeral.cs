using NumeralSharp.Formats;
using System;

namespace NumeralSharp
{
    public class Numeral
    {
        public double? Value { get; set; }
        public NumeralOptions Options { get; set; }

        /// <summary>Idiomatic C# version.  Assumes Options set in constructor</summary>
        public string Format(double? value, string format = null)
        {
            return NumberToFormat(value, format);
        }

        /// <summary>NumeralJS-style call.  Assumes value was passed in constructor</summary>
        public string Format(string format) {
            return NumberToFormat(Value, format);
        }

        private string NumberToFormat(double? value, string format)
        {
            format = format ?? Options.DefaultFormat;
            string output;
            // format based on value
            if (value == 0 && Options.ZeroFormat != null)
            {
                output = Options.ZeroFormat;
            }
            else if (value == null && Options.NullFormat != null)
            {
                output = Options.NullFormat;
            }
            else
            {
                var factory = new FormatFactory();
                var formatter = factory.GetNumberFormatter(format);
                output = formatter.NumberToFormat(value, format, Options);
            }
            return output;
        }

        public Numeral(double? value, NumeralOptions opts = null)
        {
            Value = value;
            Options = opts ?? new NumeralOptions();
        }

        public Numeral(string nullFormat, string zeroFormat, string defaultFormat)
        {
            Options = new NumeralOptions(nullFormat, zeroFormat, defaultFormat);
        }
    }
}
