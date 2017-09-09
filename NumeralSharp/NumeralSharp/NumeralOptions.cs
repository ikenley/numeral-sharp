using System;
using System.Collections.Generic;
using System.Text;

namespace NumeralSharp
{
    /// <summary>Global config options</summary>
    public class NumeralOptions
    {
        public string ZeroFormat { get; set; }

        public string NullFormat { get; set; }

        public string DefaultFormat { get; set; }

        public bool ScalePercentBy100 { get; set; }

        public NumeralOptions()
        {
            ZeroFormat = null;
            NullFormat = null;
            DefaultFormat = "0.0";
            ScalePercentBy100 = true;
        }

        public NumeralOptions(string nullFormat, string zeroFormat, string defaultFormat)
        {
            NullFormat = nullFormat;
            ZeroFormat = zeroFormat;
            DefaultFormat = defaultFormat;
            ScalePercentBy100 = true;
        }
    }
}
