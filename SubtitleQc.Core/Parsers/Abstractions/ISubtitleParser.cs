using SubtitleQc.Core.Models;

namespace SubtitleQc.Core.Parsers.Abstractions;

public interface ISubtitleParser
{
    SubtitleDocument Parse(string rawSubtitle);
}
