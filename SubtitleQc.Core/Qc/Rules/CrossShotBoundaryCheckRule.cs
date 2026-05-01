using SubtitleQc.Core.Models;
using SubtitleQc.Core.Qc.Abstractions;

namespace SubtitleQc.Core.Qc.Rules;

public sealed class CrossShotBoundaryCheckRule : IQcRule
{
    private readonly IShotChangeProvider _shotChangeProvider;

    public CrossShotBoundaryCheckRule(IShotChangeProvider shotChangeProvider)
    {
        _shotChangeProvider = shotChangeProvider;
    }

    public string RuleName => "CrossShotBoundaryCheck";

    public IReadOnlyList<QcIssue> Evaluate(IReadOnlyList<Cue> cues)
    {
        var shotChanges = _shotChangeProvider.GetShotChangeTimestamps();
        return cues.Where(cue => CrossesAnyShotBoundary(cue, shotChanges))
            .Select(cue => new QcIssue(cue.Id, RuleName, "Cue spans across a shot boundary."))
            .ToList();
    }

    private static bool CrossesAnyShotBoundary(Cue cue, IReadOnlyList<TimeSpan> shotChanges)
    {
        return shotChanges.Any(cut => cut > cue.Start && cut < cue.End);
    }
}
