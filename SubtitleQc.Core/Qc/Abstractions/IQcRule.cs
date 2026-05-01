using SubtitleQc.Core.Models;

namespace SubtitleQc.Core.Qc.Abstractions;

public interface IQcRule
{
    string RuleName { get; }

    IReadOnlyList<QcIssue> Evaluate(IReadOnlyList<Cue> cues);
}
