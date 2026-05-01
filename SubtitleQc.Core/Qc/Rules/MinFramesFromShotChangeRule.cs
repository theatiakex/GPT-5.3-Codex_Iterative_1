using SubtitleQc.Core.Models;
using SubtitleQc.Core.Qc.Abstractions;

namespace SubtitleQc.Core.Qc.Rules;

public sealed class MinFramesFromShotChangeRule : IQcRule
{
    private readonly IShotChangeProvider _shotChangeProvider;
    private readonly int _thresholdFrames;

    public MinFramesFromShotChangeRule(IShotChangeProvider shotChangeProvider, int thresholdFrames)
    {
        _shotChangeProvider = shotChangeProvider;
        _thresholdFrames = thresholdFrames;
    }

    public string RuleName => "MinFramesFromShotChange";

    public IReadOnlyList<QcIssue> Evaluate(IReadOnlyList<Cue> cues)
    {
        var shotChangeFrames = _shotChangeProvider.GetShotChangeFrames();
        return cues.Where(cue => IsTooCloseToShotChange(cue, shotChangeFrames))
            .Select(cue => new QcIssue(cue.Id, RuleName, $"Cue starts within {_thresholdFrames} frame(s) from a shot change."))
            .ToList();
    }

    private bool IsTooCloseToShotChange(Cue cue, IReadOnlyList<int> shotFrames)
    {
        if (cue.StartFrame is null || shotFrames.Count == 0) return false;
        var cueStartFrame = cue.StartFrame.Value;
        var minDistance = shotFrames.Min(cutFrame => Math.Abs(cutFrame - cueStartFrame));
        return minDistance < _thresholdFrames;
    }
}
