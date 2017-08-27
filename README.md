# numeral-sharp
C# port of Adam W Draper's [NumeralJS library](https://github.com/adamwdraper/Numeral-js)

## Install
NuGet package coming soon

## Usage
```C#
var numeral = new Numeral(0.47351);
var formattedNum = numeral.Format("0.00%");
Console.WriteLine(formattedNum); //"47.35%"
```

For a full explanation of the formatting, see Adam W Draper's [NumeralJS library](https://github.com/adamwdraper/Numeral-js).
Note: This library currently supports only the format() functionality for American locales.  
