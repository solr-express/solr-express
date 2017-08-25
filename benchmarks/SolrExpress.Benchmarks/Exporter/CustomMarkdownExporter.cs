using System.Linq;
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Loggers;
using BenchmarkDotNet.Reports;

namespace SolrExpress.Benchmarks.Exporter
{
    public class CustomMarkdownExporter : ExporterBase
    {
        public override void ExportToLog(Summary summary, ILogger logger)
        {
            var summaryFullName = summary
                .Benchmarks
                .Select(b => b.Target.Type.FullName)
                .Distinct()
                .First();

            var customSummary = new Summary(
                summaryFullName,
                summary.Reports,
                summary.HostEnvironmentInfo,
                summary.Config,
                summary.ResultsDirectoryPath,
                summary.TotalTime,
                summary.ValidationErrors);

            MarkdownExporter.GitHub.ExportToLog(customSummary, logger);
        }
    }
}
