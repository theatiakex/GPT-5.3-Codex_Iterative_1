using System.Text.Json.Serialization;

namespace SubtitleQc.Core.Models;

public sealed class SubtitleDocument
{
    public SubtitleDocument(IReadOnlyList<Cue> cues)
        : this(cues, ExternalQcData.Empty)
    {
    }

    [JsonConstructor]
    public SubtitleDocument(IReadOnlyList<Cue> cues, ExternalQcData externalData)
    {
        Cues = cues;
        ExternalData = externalData;
    }

    public IReadOnlyList<Cue> Cues { get; }

    public ExternalQcData ExternalData { get; }
}
