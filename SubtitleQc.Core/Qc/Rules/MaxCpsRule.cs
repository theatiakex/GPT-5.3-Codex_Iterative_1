using SubtitleQc.Core.Models;
using SubtitleQc.Core.Qc.Abstractions;

namespace SubtitleQc.Core.Qc.Rules;

public sealed class MaxCpsRule : IQcRule
{
    private readonly double _threshold;

    public MaxCpsRule(double threshold)
    {
        _threshold = threshold;
    }

    public string RuleName => "MaxCps";

    public IReadOnlyList<QcIssue> Evaluate(IReadOnlyList<Cue> cues)
    {
        return cues.Where(IsAboveThreshold)
            .Select(c => new QcIssue(c.Id, RuleName, $"CPS exceeds {_threshold:F2}."))
            .ToList();
    }

    private bool IsAboveThreshold(Cue cue)
    {
        var seconds = cue.Duration.TotalSeconds;
        if (seconds <= 0) return true;
        var charCount = cue.Lines.Sum(l => l.Length);
        return charCount / seconds > _threshold;
    }
}
