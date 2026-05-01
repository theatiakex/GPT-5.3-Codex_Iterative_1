using SubtitleQc.Core.Models;
using SubtitleQc.Core.Parsers.Abstractions;

namespace SubtitleQc.Core.Parsers;

public sealed class WebVttParser : ISubtitleParser
{
    public SubtitleDocument Parse(string rawSubtitle)
    {
        var blocks = SplitBlocks(rawSubtitle).SkipWhile(IsHeaderBlock);
        var cues = blocks.Select(ParseBlock).ToList();
        return new SubtitleDocument(cues);
    }

    private static IEnumerable<string[]> SplitBlocks(string rawSubtitle)
    {
        return rawSubtitle.Replace("\r\n", "\n")
            .Split("\n\n", StringSplitOptions.RemoveEmptyEntries)
            .Select(b => b.Split('\n', StringSplitOptions.RemoveEmptyEntries));
    }

    private static bool IsHeaderBlock(string[] lines) =>
        lines.Length > 0 && lines[0].StartsWith("WEBVTT", StringComparison.OrdinalIgnoreCase);

    private static Cue ParseBlock(string[] lines)
    {
        var hasIdentifier = !lines[0].Contains("-->", StringComparison.Ordinal);
        var timingIndex = hasIdentifier ? 1 : 0;
        var id = hasIdentifier ? lines[0].Trim() : Guid.NewGuid().ToString("N");
        var ranges = lines[timingIndex].Split(" --> ", StringSplitOptions.TrimEntries);
        var cueLines = lines.Skip(timingIndex + 1).ToList();
        return new Cue(id, ParserTimeSpan.ParseVtt(ranges[0]), ParserTimeSpan.ParseVtt(ranges[1]), cueLines);
    }
}
