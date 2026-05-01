using System.Text.Json.Serialization;

namespace SubtitleQc.Core.Qc;

public sealed class QcIssue
{
    [JsonConstructor]
    public QcIssue(string cueId, string ruleName, string message)
    {
        CueId = cueId;
        RuleName = ruleName;
        Message = message;
    }

    public string CueId { get; }

    public string RuleName { get; }

    public string Message { get; }
}
