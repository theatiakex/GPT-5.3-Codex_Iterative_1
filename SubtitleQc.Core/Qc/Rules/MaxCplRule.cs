using SubtitleQc.Core.Models;
using SubtitleQc.Core.Qc.Abstractions;

namespace SubtitleQc.Core.Qc.Rules;

public sealed class MaxCplRule : IQcRule
{
    private readonly int _threshold;

    public MaxCplRule(int threshold)
    {
        _threshold = threshold;
    }

    public string RuleName => "MaxCpl";

    public IReadOnlyList<QcIssue> Evaluate(IReadOnlyList<Cue> cues)
    {
        return cues.Where(HasOverlongLine)
            .Select(c => new QcIssue(c.Id, RuleName, $"A line exceeds {_threshold} characters."))
            .ToList();
    }

    private bool HasOverlongLine(Cue cue) => cue.Lines.Any(l => l.Length > _threshold);
}
