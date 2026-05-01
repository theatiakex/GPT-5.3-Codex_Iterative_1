using SubtitleQc.Core.Models;
using SubtitleQc.Core.Qc.Abstractions;

namespace SubtitleQc.Core.Qc.Rules;

public sealed class OverlapCheckRule : IQcRule
{
    public string RuleName => "OverlapCheck";

    public IReadOnlyList<QcIssue> Evaluate(IReadOnlyList<Cue> cues)
    {
        var issues = new List<QcIssue>();
        for (var i = 1; i < cues.Count; i++)
        {
            var previous = cues[i - 1];
            var current = cues[i];
            if (previous.End > current.Start)
            {
                issues.Add(new QcIssue(current.Id, RuleName, "Cue overlaps with a previous cue."));
            }
        }

        return issues;
    }
}
