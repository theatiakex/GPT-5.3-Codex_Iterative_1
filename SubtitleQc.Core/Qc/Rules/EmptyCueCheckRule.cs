using SubtitleQc.Core.Models;
using SubtitleQc.Core.Qc.Abstractions;

namespace SubtitleQc.Core.Qc.Rules;

public sealed class EmptyCueCheckRule : IQcRule
{
    public string RuleName => "EmptyCueCheck";

    public IReadOnlyList<QcIssue> Evaluate(IReadOnlyList<Cue> cues)
    {
        return cues.Where(IsEmpty)
            .Select(c => new QcIssue(c.Id, RuleName, "Cue has no visible text content."))
            .ToList();
    }

    private static bool IsEmpty(Cue cue) => cue.Lines.All(string.IsNullOrWhiteSpace);
}
