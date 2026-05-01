using SubtitleQc.Core.Models;
using SubtitleQc.Core.Qc.Abstractions;

namespace SubtitleQc.Core.Qc.Rules;

public sealed class MaxLinesRule : IQcRule
{
    private readonly int _threshold;

    public MaxLinesRule(int threshold)
    {
        _threshold = threshold;
    }

    public string RuleName => "MaxLines";

    public IReadOnlyList<QcIssue> Evaluate(IReadOnlyList<Cue> cues)
    {
        return cues.Where(c => c.Lines.Count > _threshold)
            .Select(c => new QcIssue(c.Id, RuleName, $"Line count {c.Lines.Count} exceeds {_threshold}."))
            .ToList();
    }
}
