using System.Globalization;

namespace NmeaParser;

public static class NmeaExtensions
{
    public static TimeOnly ToTimeOnly(string nmeaUtcString)
    {
        return TimeOnly.ParseExact(nmeaUtcString, "hhmmss.ff", CultureInfo.InvariantCulture);
    }

    public static DateOnly ToDateOnly(string nmeaDateDay, string nmeaDateMonth, string nmeaDateYear)
    {
        return DateOnly.ParseExact($"{nmeaDateDay}{nmeaDateMonth}{nmeaDateYear}", "ddMMyyyy", CultureInfo.InvariantCulture);
    }

    public static double ToCoordinates(
        this string inputString,
        string cardinalDirection,
        NmeaCoordinate coordinate
    )
    {
        if (string.IsNullOrEmpty(inputString))
            return 0.0;
        var num = coordinate == NmeaCoordinate.Latitude ? 2 : 3;
        var d = inputString[..num].ToDouble() + inputString[num..].ToDouble() / 60.0;
        if (!double.IsNaN(d) && cardinalDirection is "S" or "W")
            d *= -1.0;
        return d;
    }

    public static int ToInteger(this string inputString)
    {
        return string.IsNullOrEmpty(inputString) ? 0 : int.Parse(inputString, CultureInfo.InvariantCulture);
    }

    public static double ToDouble(this string inputString)
    {
        return string.IsNullOrEmpty(inputString) ? double.NaN : double.Parse(inputString, CultureInfo.InvariantCulture);
    }

    public static float ToFloat(this string inputString)
    {
        return string.IsNullOrEmpty(inputString) ? float.NaN : float.Parse(inputString, CultureInfo.InvariantCulture);
    }

    public static bool ToBoolean(this string inputString, string validValue)
    {
        return inputString == validValue;
    }

    public static string RemoveAfter(this string inputString, string charValue)
    {
        var length = inputString.IndexOf(charValue, StringComparison.Ordinal);
        return length <= 0 ? inputString : inputString[..length];
    }
}