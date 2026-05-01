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


### Iteration 2

Extended the prototype for Iteration 2 by adding TTML support while leaving the QC engine and existing rules unchanged.

Added TtmlParser in SubtitleQc.Core/Parsers/TtmlParser.cs
Parser behavior:
Parses TTML XML using XDocument
Reads cue nodes from <p> elements (namespace-agnostic via LocalName)
Requires begin and end attributes per cue (throws FormatException if missing)
Converts TTML text (including <br/>) into multiline Cue.Lines
Produces the same internal Cue/SubtitleDocument model used by SRT/WebVTT
Verification
Ran dotnet test (SubtitleQc.Tests/SubtitleQc.Tests.csproj)
Result: Passed 13 / Failed 0
Appended iteration telemetry entry to test_metrics.jsonl:
project_iteration: 2
attempt: 1
pass_rate_percentage: 100
If you want, I can continue with Iteration 3 immediately in the same incremental style.


### Iteration 3

Extended the prototype for Iteration 3 on top of your existing implementation, using agents_iterative.md + iteration_3.md and aligning to the current test contract.

Added shot-change provider abstraction: SubtitleQc.Core/Qc/Abstractions/IShotChangeProvider.cs
Upgraded internal cue model for external frame-linked data:
SubtitleQc.Core/Models/Cue.cs now supports optional startFrame
Added shot-change rules:
SubtitleQc.Core/Qc/Rules/CrossShotBoundaryCheckRule.cs
SubtitleQc.Core/Qc/Rules/MinFramesFromShotChangeRule.cs
Kept existing QC engine behavior intact for existing rules and compatibility:
Existing RuleEngine.Evaluate(IEnumerable<Cue>) still works
Added model extension groundwork in SubtitleQc.Core/Models/ExternalQcData.cs and SubtitleQc.Core/Models/SubtitleDocument.cs for external attributes
Validation
Ran dotnet test after changes.
Final result: 19 passed, 0 failed.
Logged all Iteration 3 test attempts in test_metrics.jsonl:
attempt 1: build failed (missing interface before fix)
attempt 2: build successful, 100% pass rate