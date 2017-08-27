using System;
using System.Collections.Generic;
using System.Text;

namespace NumeralSharp
{
    class Locale
    {
        public Delimiters Delimiters { get; set; }
        public Abbreviations Abbreviations { get; set; }
        public Currency Currency { get; set; }

        public Locale()
        {
            Delimiters = new Delimiters();
            Abbreviations = new Abbreviations();
            Currency = new Currency();
        }
        //        ordinal: function(number)
        //{
        //    var b = number % 10;
        //    return (~~(number % 100 / 10) === 1) ? 'th' :
        //        (b === 1) ? 'st' :
        //        (b === 2) ? 'nd' :
        //        (b === 3) ? 'rd' : 'th';
        //},
    }

    public class Delimiters
    {
        public string Thousands { get; set; }
        public string Decimalk { get; set; }

        public Delimiters() : this(",", ".")
        { }

        public Delimiters(string thousands, string decimalk)
        {
            Thousands = thousands;
            Decimalk = decimalk;
        }
    }

    public class Abbreviations
    {
        public string Thousand { get; set; }
        public string Million { get; set; }
        public string Billion { get; set; }
        public string Trillion { get; set; }

        public Abbreviations (): this("k", "m", "b", "t")
        { }

        public Abbreviations(string thousand, string million, string billion, string trillion)
        {
            Thousand = thousand;
            Million = million;
            Billion = billion;
            Trillion = trillion;
        }
    }

    public class Currency
    {
        public string Symbol { get; set; }

        public Currency() : this("$")
        { }

        public Currency(string symbol)
        {
            Symbol = symbol;
        }
    }
}
