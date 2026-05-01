using System.Xml.Linq;
using SubtitleQc.Core.Models;
using SubtitleQc.Core.Parsers.Abstractions;

namespace SubtitleQc.Core.Parsers;

public sealed class TtmlParser : ISubtitleParser
{
    public SubtitleDocument Parse(string rawSubtitle)
    {
        var document = XDocument.Parse(rawSubtitle);
        var cues = ExtractCueElements(document)
            .Select(CreateCue)
            .ToList();
        return new SubtitleDocument(cues);
    }

    private static IEnumerable<XElement> ExtractCueElements(XDocument document)
    {
        return document.Descendants().Where(e => e.Name.LocalName == "p");
    }

    private static Cue CreateCue(XElement cueElement)
    {
        var start = ParseTime(GetRequiredAttribute(cueElement, "begin"));
        var end = ParseTime(GetRequiredAttribute(cueElement, "end"));
        var lines = ExtractLines(cueElement);
        return new Cue(Guid.NewGuid().ToString("N"), start, end, lines);
    }

    private static string GetRequiredAttribute(XElement cueElement, string attributeName)
    {
        var value = cueElement.Attributes().FirstOrDefault(a => a.Name.LocalName == attributeName)?.Value;
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new FormatException($"TTML cue is missing required '{attributeName}' attribute.");
        }

        return value;
    }

    private static TimeSpan ParseTime(string value)
    {
        var normalized = value.Replace(',', '.');
        return TimeSpan.Parse(normalized, System.Globalization.CultureInfo.InvariantCulture);
    }

    private static IReadOnlyList<string> ExtractLines(XElement cueElement)
    {
        var raw = string.Concat(cueElement.Nodes().Select(GetNodeText));
        var lines = raw.Split('\n').Select(l => l.TrimEnd('\r')).ToList();
        return lines;
    }

    private static string GetNodeText(XNode node)
    {
        if (node is XText textNode) return textNode.Value;
        if (node is not XElement element) return string.Empty;
        if (element.Name.LocalName == "br") return "\n";
        return string.Concat(element.Nodes().Select(GetNodeText));
    }
}
