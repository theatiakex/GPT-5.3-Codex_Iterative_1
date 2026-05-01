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
        return Evaluate(new SubtitleDocument(cues.OrderBy(c => c.Start).ToList()));
    }

    public QcReport Evaluate(SubtitleDocument document)
    {
        var orderedCues = document.Cues.OrderBy(c => c.Start).ToList();
        var issues = EvaluateRules(orderedCues, document.ExternalData);
        var issueMap = GroupByCueId(issues);
        var results = orderedCues.Select(c => BuildResult(c.Id, issueMap)).ToList();
        return new QcReport(results);
    }

    private IReadOnlyList<QcIssue> EvaluateRules(IReadOnlyList<Cue> cues, ExternalQcData externalData)
    {
        return _rules.SelectMany(rule => EvaluateRule(rule, cues, externalData)).ToList();
    }

    private static IReadOnlyList<QcIssue> EvaluateRule(
        IQcRule rule,
        IReadOnlyList<Cue> cues,
        ExternalQcData externalData)
    {
        if (rule is IExternalDataQcRule externalRule)
        {
            return externalRule.Evaluate(cues, externalData);
        }

        return rule.Evaluate(cues);
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
