### Iteration 1

Implemented a working Iteration 1 prototype from agents_iterative.md and iteration_1.md with a clean split between model, parsing, and QC logic.

What I built
Added new core library project: SubtitleQc.Core/SubtitleQc.Core.csproj
Implemented unified internal subtitle model:
SubtitleQc.Core/Models/Cue.cs
SubtitleQc.Core/Models/SubtitleDocument.cs
Implemented isolated format parsers (decoupled from QC engine):
SubtitleQc.Core/Parsers/Abstractions/ISubtitleParser.cs
SubtitleQc.Core/Parsers/SrtParser.cs
SubtitleQc.Core/Parsers/WebVttParser.cs
SubtitleQc.Core/Parsers/ParserTimeSpan.cs
Implemented QC engine + serializable report/results:
SubtitleQc.Core/Qc/Abstractions/IQcRule.cs
SubtitleQc.Core/Qc/RuleEngine.cs
SubtitleQc.Core/Qc/QcStatus.cs
SubtitleQc.Core/Qc/QcIssue.cs
SubtitleQc.Core/Qc/QcResult.cs
SubtitleQc.Core/Qc/QcReport.cs
Implemented all Iteration 1 rules:
MaxLinesRule
MaxCplRule
MaxCpsRule
MinDurationRule
OverlapCheckRule
EmptyCueCheckRule in SubtitleQc.Core/Qc/Rules/
Project wiring
Updated SubtitleQc.Tests/SubtitleQc.Tests.csproj to:
reference SubtitleQc.Core
include explicit package versions for test dependencies
Validation
Ran dotnet test on SubtitleQc.Tests/SubtitleQc.Tests.csproj
Result: 13 passed, 0 failed
Added test metrics log entry to test_metrics.jsonl per iteration protocol.