using SubtitleQc.Core.Models;
using SubtitleQc.Core.Qc.Abstractions;

namespace SubtitleQc.Core.Qc;

public sealed class RuleEngine
{
    private readonly IReadOnlyList<IQcRule> _rules;

    public RuleEngine(IEnumerable<IQcRule> rules)
    {
        _rules = rules.ToList();
    }

    public QcReport Evaluate(IEnumerable<Cue> cues)
    {
        var orderedCues = cues.OrderBy(c => c.Start).ToList();
        var issues = _rules.SelectMany(r => r.Evaluate(orderedCues)).ToList();
        var issueMap = GroupByCueId(issues);
        var results = orderedCues.Select(c => BuildResult(c.Id, issueMap)).ToList();
        return new QcReport(results);
    }

    private static IReadOnlyDictionary<string, IReadOnlyList<QcIssue>> GroupByCueId(IReadOnlyList<QcIssue> issues)
    {
        return issues.GroupBy(i => i.CueId).ToDictionary(g => g.Key, g => (IReadOnlyList<QcIssue>)g.ToList());
    }

    private static QcResult BuildResult(string cueId, IReadOnlyDictionary<string, IReadOnlyList<QcIssue>> issueMap)
    {
        if (!issueMap.TryGetValue(cueId, out var cueIssues))
        {
            return new QcResult(cueId, QcStatus.Passed, Array.Empty<QcIssue>());
        }

        return new QcResult(cueId, QcStatus.Failed, cueIssues);
    }
}
