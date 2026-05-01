using System.Text.Json.Serialization;

namespace SubtitleQc.Core.Models;

public sealed class ExternalQcData
{
    public static ExternalQcData Empty { get; } = new(Array.Empty<TimeSpan>(), Array.Empty<long>(), 25.0);

    [JsonConstructor]
    public ExternalQcData(
        IReadOnlyList<TimeSpan> shotChangeTimestamps,
        IReadOnlyList<long> shotChangeFrames,
        double framesPerSecond)
    {
        ShotChangeTimestamps = shotChangeTimestamps;
        ShotChangeFrames = shotChangeFrames;
        FramesPerSecond = framesPerSecond;
    }

    public IReadOnlyList<TimeSpan> ShotChangeTimestamps { get; }

    public IReadOnlyList<long> ShotChangeFrames { get; }

    public double FramesPerSecond { get; }
}
