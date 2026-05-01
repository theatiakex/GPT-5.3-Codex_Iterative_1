using System.Globalization;

namespace SubtitleQc.Core.Parsers;

internal static class ParserTimeSpan
{
    public static TimeSpan ParseSrt(string value) => Parse(value, "hh\\:mm\\:ss\\,fff");

    public static TimeSpan ParseVtt(string value) => Parse(value, "hh\\:mm\\:ss\\.fff");

    private static TimeSpan Parse(string value, string format)
    {
        return TimeSpan.ParseExact(value, format, CultureInfo.InvariantCulture);
    }
}
