using System.Text.Json.Serialization;

namespace SubtitleQc.Core.Qc;

public sealed class QcReport
{
    [JsonConstructor]
    public QcReport(IReadOnlyList<QcResult> results)
    {
        Results = results;
    }

    public IReadOnlyList<QcResult> Results { get; }
}
