using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace NumeralSharp.Formats
{
    public class PercentageFormatter : NumberFormatter
    {
        /// <summary>Whether a given format is applicable</summary>
        /// <param name="format"></param>
        /// <returns></returns>
        public new static string RegexPattern = @"(%)";

        /// <summary>Convert a given number to a format string</summary>
        public override string NumberToFormat(double? value, string format, NumeralOptions options)
        {
            string space = format.Contains(" %") ? " " : "";
            string output;

            if (options.ScalePercentBy100)
            {
                value = value * 100;
            }

            // check for space before %
            format = new Regex(@"\s?\%").Replace(format, "");

            output = base.NumberToFormat(value, format, options);

            if (output.Contains(")"))
            {
                output = string.Format("{0}{1})", output.Substring(0, output.Length - 1), space + "%");
            }
            else
            {
                output = output + space + '%';
            }
            return output;
        }
    }
}
