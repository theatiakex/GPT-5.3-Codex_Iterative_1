using System.Text.Json.Serialization;

namespace SubtitleQc.Core.Models;

public sealed class SubtitleDocument
{
    [JsonConstructor]
    public SubtitleDocument(IReadOnlyList<Cue> cues)
    {
        Cues = cues;
    }

    public IReadOnlyList<Cue> Cues { get; }
}
