using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace NumeralSharp.Formats
{
    public class CurrencyFormatter: NumberFormatter
    {
        /// <summary>Whether a given format is applicable</summary>
        /// <param name="format"></param>
        /// <returns></returns>
        public new static string RegexPattern = @"(\$)";

        /// <summary>Convert a given number to a format string</summary>
        public override string NumberToFormat(double? value, string format, NumeralOptions options)
        {
            var locale = new Locale();
            string before = new Regex(@"^([\+|\-|\(|\s|\$]*)").Match(format).Value;
            string after = new Regex(@"([\+|\-|\)|\s|\$]*)$").Match(format).Value;
            string output = null;
            string symbol = null;
            int i;

            // strip format of spaces and $
            Regex rgx = new Regex(@"\s?\$\s?");
            format = rgx.Replace(format, "");

            // format the number
            output = base.NumberToFormat(value, format, options);

            // update the before and after based on value
            if (value >= 0) {
                before = new Regex(@"[\-\(]").Replace(before, "");
                after = new Regex(@"[\-\)]").Replace(after, "");
            } else if (value< 0 && (!before.Contains("-") && !before.Contains("("))) {
                before = '-' + before;
            }

            // loop through each before symbol
            for (i = 0; i<before.Length; i++) {
                symbol = before.Substring(i, 1);

                switch (symbol) {
                    case "$":
                        output = output.Insert(i, locale.Currency.Symbol);
                        break;
                    case " ":
                        output = output.Insert(i + locale.Currency.Symbol.Length - 1, " ");
                        break;
                }
            }

            // loop through each after symbol
            for (i = after.Length - 1; i >= 0; i--) {
                symbol = after.Substring(i, 1);

                switch (symbol) {
                    case "$":
                        output = i == after.Length - 1 ? output + locale.Currency.Symbol : output.Insert(-(after.Length - (1 + i)), locale.Currency.Symbol);
                        break;
                    case " ":
                        output = i == after.Length - 1 ? output + " " : output.Insert(-(after.Length - (1 + i) + locale.Currency.Symbol.Length - 1), " ");
                        break;
                }
            }


            return output;
        }
    }
}
