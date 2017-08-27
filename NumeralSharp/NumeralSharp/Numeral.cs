using System;
using System.Text.RegularExpressions;

namespace NumeralSharp
{
    public class Numeral
    {
        public double? Value { get; set; }
        public NumeralOptions Options { get; set; }

        public string Format(string format) {
            var value = Value;
            //format = inputString || options.defaultFormat,
            string output;
            
            // format based on value
            if (value == 0 && Options.ZeroFormat != null) {
                output = Options.ZeroFormat;
            } else if (value == null && Options.NullFormat != null) {
                output = Options.NullFormat;
            } else {
                //for (kind in formats) {
                //    if (format.match(formats[kind].regexps.format)) {
                //        formatFunction = formats[kind].format;

                //        break;
                //    }
                //}
                //formatFunction = formatFunction || numeral._.numberToFormat;
                output = NumberToFormat(value, format);
            }
            return output;
        }

        public string NumberToFormat(double? value, string format)
        {
            var locale = new Locale();
            bool negP = false;
            bool optDec = false;
            int leadingCount = 0;
            string abbr = "";
            long trillion = 1000000000000;
            int billion = 1000000000;
            int million = 1000000;
            int thousand = 1000;
            string decmal = "";
            bool neg = false;
            MatchCollection abbrForce = null; // force abbreviation
            double? abs;
            string intPart;
            string precisionPart = null;
            int signed = -1;
            int thousands;
            string output = null;

            // make sure we never format a null value
            double val = value ?? 0;
            abs = Math.Abs(val);

            // see if we should use parentheses for negative number or if we should prefix with a sign
            // if both are present we default to parentheses
            if (format.Contains("("))
            {
                negP = true;
                Regex rgx = new Regex(@"/[\(|\)]/");
                format = rgx.Replace(format, "");
            }
            else if (format.Contains("+") || format.Contains("-"))
            {
                signed = format.Contains("+") ? format.IndexOf('+') : val < 0 ? format.IndexOf('-') : -1;
                Regex rgx = new Regex(@"/[\+|\-] /");
                format = rgx.Replace(format, "");
            }

            // see if abbreviation is wanted
            if (format.Contains("a"))
            {
                Regex rgx = new Regex(@"/a(k|m|b|t)?/");
                abbrForce = rgx.Matches(format);

                // check for space before abbreviation
                if (format.Contains(" a"))
                {
                    abbr = " ";
                }

                rgx = new Regex(abbr + @"a[kmbt]?");
                format = rgx.Replace(format, "");

                if (abs >= trillion && abbrForce.Count == 0 || (abbrForce.Count > 0 && abbrForce[0].Value == "t"))
                {
                    // trillion
                    abbr += locale.Abbreviations.Trillion;
                    val = val / trillion;
                }
                else if (abs < trillion && abs >= billion && abbrForce.Count == 0 || (abbrForce.Count > 0 && abbrForce[0].Value == "b"))
                {
                    // billion
                    abbr += locale.Abbreviations.Billion;
                    val = val / billion;
                }
                else if (abs < billion && abs >= million && abbrForce.Count == 0 || (abbrForce.Count > 0 && abbrForce[0].Value == "m"))
                {
                    // million
                    abbr += locale.Abbreviations.Million;
                    val = val / million;
                }
                else if (abs < million && abs >= thousand && abbrForce.Count == 0 || (abbrForce.Count > 0 && abbrForce[0].Value == "k"))
                {
                    // thousand
                    abbr += locale.Abbreviations.Thousand;
                    val = val / thousand;
                }
            }

            // check for optional decmals
            if (format.Contains("[.]"))
            {
                optDec = true;
                Regex rgx = new Regex(@"[.]");
                format = rgx.Replace(format, ".");
            }

            // break number and format
            intPart = val.ToString().Split('.')[0];
            var splitFormat = format.Split('.');
            if (splitFormat.Length > 1) {
                precisionPart = splitFormat[1];
            }
            thousands = format.IndexOf(',');
            if(splitFormat.Length > 0)
            {
                var splitThous = splitFormat[0].Split(',');
                if(splitThous.Length > 0)
                {
                    Regex rgx = new Regex(@"/0/");
                    leadingCount = rgx.Matches(splitThous[0]).Count;
                }
            }
            

            if (precisionPart != null)
            {
                //if (precisionPart.Contains("["))
                //{
                //    Regex rgx = new Regex(@"/]/");
                //    precisionPart = rgx.Replace(precisionPart, "");
                //    precisionPart = precisionPart.Split('[');
                //    decmal = ToFixed(value, (precisionPart[0].Length + precisionPart[1].Length), roundingFunction, precisionPart[1].Length);
                //}
                //else
                //{
                    decmal = ToFixed(val, precisionPart.Length);
                //}

                intPart = decmal.Split('.')[0];

                if (decmal.Contains("."))
                {
                    decmal = locale.Delimiters.Decimalk + decmal.Split('.')[1];
                }
                else
                {
                    decmal = "";
                }

                if (optDec && Convert.ToInt64(decmal.Substring(1)) == 0) //TODO possible bug
                {
                    decmal = "";
                }
            }
            else
            {
                intPart = ToFixed(val, 0);
            }

            // check abbreviation again after rounding
            if (!string.IsNullOrEmpty(abbr) 
                && abbrForce != null 
                && abbrForce.Count == 0 
                && Convert.ToInt64(intPart) >= 1000 
                && abbr != locale.Abbreviations.Trillion)
            {
                intPart = (Convert.ToInt64(intPart) / 1000).ToString();

                if(abbr == locale.Abbreviations.Thousand)
                {
                    abbr = locale.Abbreviations.Million;
                }
                else if (abbr == locale.Abbreviations.Million)
                {
                    abbr = locale.Abbreviations.Billion;
                }
                else if (abbr == locale.Abbreviations.Billion)
                {
                    abbr = locale.Abbreviations.Trillion;
                }
            }


            // format number
            if (intPart.Contains("-"))
            {
                intPart = intPart.Substring(1);
                neg = true;
            }

            if (intPart.Length < leadingCount)
            {
                for (var i = leadingCount - intPart.Length; i > 0; i--)
                {
                    intPart = '0' + intPart;
                }
            }

            if (thousands > -1)
            {
                Regex rgx = new Regex(@"/(\d)(?=(\d{3})+(?!\d))/");
                intPart = rgx.Replace(intPart, "$1" + locale.Delimiters.Thousands);
            }

            if (format.IndexOf('.') == 0)
            {
                intPart = "";
            }

            output = intPart + decmal + (abbr != null ? abbr : "");

            if (negP)
            {
                output = (negP && neg ? "(" : "") + output + (negP && neg ? ")" : "");
            }
            else
            {
                if (signed >= 0)
                {
                    output = signed == 0 ? (neg ? '-' : '+') + output : output + (neg ? '-' : '+');
                }
                else if (neg)
                {
                    output = '-' + output;
                }
            }

            return output;
        }

        public Numeral(double? value, NumeralOptions opts = null)
        {
            Value = value;
            Options = opts ?? new NumeralOptions();
        }

        private static string ToFixed(double value, int maxDecimals, string roundingFunction = null, int optionals = 0)
        {
            //Gordian Knot
            return value.ToString("N" + maxDecimals);
            //var splitValue = value.ToString().Split('.');
            //var minDecimals = maxDecimals - optionals;
            //int boundedPrecision;
            //string output;

            //// Use the smallest precision value possible to avoid errors from floating point representation
            //if (splitValue.Length == 2)
            //{
            //    boundedPrecision = Math.Min(Math.Max(splitValue[1].Length, minDecimals), maxDecimals);
            //}
            //else
            //{
            //    boundedPrecision = minDecimals;
            //}

            //var power = Math.Pow(10, boundedPrecision);

            //// Multiply up by precision, round accurately, then divide and use native toFixed():
            //output = (roundingFunction(value + 'e+' + boundedPrecision) / power).toFixed(boundedPrecision);

            //if (optionals > maxDecimals - boundedPrecision)
            //{
            //    Regex optionalsRegExp = new RegExp(@"\\.?0{1,' + (optionals - (maxDecimals - boundedPrecision)) + '}$");
            //    output = output.replace(optionalsRegExp, "");
            //}

            //return output;
        }
    }
}
