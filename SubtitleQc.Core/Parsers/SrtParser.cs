using SubtitleQc.Core.Models;
using SubtitleQc.Core.Parsers.Abstractions;

namespace SubtitleQc.Core.Parsers;

public sealed class SrtParser : ISubtitleParser
{
    public SubtitleDocument Parse(string rawSubtitle)
    {
        var blocks = SplitBlocks(rawSubtitle);
        var cues = blocks.Select(ParseBlock).ToList();
        return new SubtitleDocument(cues);
    }

    private static IEnumerable<string[]> SplitBlocks(string rawSubtitle)
    {
        return rawSubtitle.Replace("\r\n", "\n")
            .Split("\n\n", StringSplitOptions.RemoveEmptyEntries)
            .Select(b => b.Split('\n', StringSplitOptions.RemoveEmptyEntries));
    }

    private static Cue ParseBlock(string[] lines)
    {
        var timingLine = lines[1];
        var ranges = timingLine.Split(" --> ", StringSplitOptions.TrimEntries);
        var cueLines = lines.Skip(2).ToList();
        return new Cue(
            id: lines[0].Trim(),
            start: ParserTimeSpan.ParseSrt(ranges[0]),
            end: ParserTimeSpan.ParseSrt(ranges[1]),
            lines: cueLines);
    }
}
