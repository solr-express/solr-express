using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Loggers;
using BenchmarkDotNet.Reports;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SolrExpress.Benchmarks.Exporter
{
    public class CustomMarkdownExporter : IExporter
    {
        private readonly IExporter _exporter;

        public string Name { get; set; }

        public CustomMarkdownExporter()
        {
            this._exporter = MarkdownExporter.GitHub;
            this.Name = this._exporter.Name;
        }

        private Summary GetSummary(Summary summary)
        {
            var assemblyFullName = summary
                .Benchmarks
                .Select(b => b.Target.Type.Namespace)
                .Distinct()
                .First();

            var resultsDirectoryPath = Path.Combine(
                summary.ResultsDirectoryPath,
                assemblyFullName);

            return new Summary(
                summary.Title,
                summary.Reports,
                summary.HostEnvironmentInfo,
                summary.Config,
                resultsDirectoryPath,
                summary.TotalTime,
                summary.ValidationErrors);
        }

        public void ExportToLog(Summary summary, ILogger logger)
        {
            MarkdownExporter.GitHub.ExportToLog(this.GetSummary(summary), logger);
        }

        public IEnumerable<string> ExportToFiles(Summary summary, ILogger consoleLogger)
        {
            var customSummary = this.GetSummary(summary);

            Directory.CreateDirectory(customSummary.ResultsDirectoryPath);

            return MarkdownExporter.GitHub.ExportToFiles(customSummary, consoleLogger);
        }
    }
}
