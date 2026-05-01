using SubtitleQc.Core.Models;
using SubtitleQc.Core.Qc.Abstractions;

namespace SubtitleQc.Core.Qc.Rules;

public sealed class MinDurationRule : IQcRule
{
    private readonly TimeSpan _threshold;

    public MinDurationRule(TimeSpan threshold)
    {
        _threshold = threshold;
    }

    public string RuleName => "MinDuration";

    public IReadOnlyList<QcIssue> Evaluate(IReadOnlyList<Cue> cues)
    {
        return cues.Where(c => c.Duration < _threshold)
            .Select(c => new QcIssue(c.Id, RuleName, $"Duration is below {_threshold.TotalSeconds:F3}s."))
            .ToList();
    }
}
