using System.Text.Json.Serialization;

namespace SubtitleQc.Core.Qc;

public sealed class QcResult
{
    [JsonConstructor]
    public QcResult(string cueId, QcStatus status, IReadOnlyList<QcIssue> issues)
    {
        CueId = cueId;
        Status = status;
        Issues = issues;
    }

    public string CueId { get; }

    public QcStatus Status { get; }

    public IReadOnlyList<QcIssue> Issues { get; }
}
