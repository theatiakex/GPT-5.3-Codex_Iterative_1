using SubtitleQc.Core.Models;

namespace SubtitleQc.Core.Qc.Abstractions;

public interface IExternalDataQcRule
{
    IReadOnlyList<QcIssue> Evaluate(IReadOnlyList<Cue> cues, ExternalQcData externalData);
}
