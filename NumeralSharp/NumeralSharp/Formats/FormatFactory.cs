using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace NumeralSharp.Formats
{
    /// <summary>Gets relevant NumberFormatter for given format string</summary>
    public class FormatFactory
    {
        /// <summary>Gets relevant NumberFormatter for given format string</summary>
        public NumberFormatter GetNumberFormatter(string format)
        {
            if(new Regex(CurrencyFormatter.RegexPattern).IsMatch(format))
            {
                return new CurrencyFormatter();
            }
            else if(new Regex(PercentageFormatter.RegexPattern).IsMatch(format))
            {
                return new PercentageFormatter();
            }
            return new NumberFormatter();
        }
    }
}
