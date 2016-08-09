using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Helpers;
using BenchmarkDotNet.Horology;
using BenchmarkDotNet.Loggers;
using BenchmarkDotNet.Reports;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SolrExpress.Benchmarks.Exporter
{
    public class CustomMarkdownExporter : IExporter
    {
        private string prefix = string.Empty;
        private bool useCodeBlocks = true;
        private string codeBlocksSyntax = "ini";
        private bool startOfGroupInBold = true;

        private ILogger GetRightLogger(ILogger logger)
        {
            if (string.IsNullOrEmpty(prefix)) // most common scenario!! we don't need expensive LoggerWithPrefix
            {
                return logger;
            }

            return new LoggerWithPrefix(logger, prefix);
        }

        private void PrintTable(SummaryTable table, ILogger logger)
        {
            if (table.FullContent.Length == 0)
            {
                logger.WriteLineError("There are no benchmarks found ");
                logger.WriteLine();
                return;
            }

            table.PrintCommonColumns(logger);
            logger.WriteLine();

            logger.WriteLine("Time unit definitions");

            foreach (var item in TimeUnit.All)
            {
                logger.WriteLine($"{item.Name}={item.Description}");
            }

            logger.WriteLine();

            if (useCodeBlocks)
            {
                logger.Write("```");
                logger.WriteLine();
            }

            table.PrintLine(table.FullHeader, logger, string.Empty, " |");
            logger.WriteLineStatistic(string.Join("", table.Columns.Where(c => c.NeedToShow).Select(c => new string('-', c.Width) + " |")));
            var rowCounter = 0;
            var highlightRow = false;
            foreach (var line in table.FullContent)
            {
                // Each time we hit the start of a new group, alternative the colour (in the console) or display bold in Markdown
                if (table.FullContentStartOfGroup[rowCounter])
                {
                    highlightRow = !highlightRow;
                }

                table.PrintLine(line, logger, string.Empty, " |", highlightRow, table.FullContentStartOfGroup[rowCounter], startOfGroupInBold);
                rowCounter++;
            }

            logger.WriteLine();
        }

        public IEnumerable<string> ExportToFiles(Summary summary)
        {
            var summaryFullName = summary.Benchmarks.Select(b => b.Target.Type.Name).Distinct().First();
            var fileName = $"{summaryFullName}.md";

            var filePath = Path.Combine(summary.ResultsDirectoryPath, fileName);

            using (var stream = Export.StreamWriter.FromPath(filePath))
            {
                ExportToLog(summary, new StreamLogger(stream));
            }

            return new[] { filePath };
        }

        public void ExportToLog(Summary summary, ILogger logger)
        {
            if (useCodeBlocks)
                logger.WriteLine($"```{codeBlocksSyntax}");
            logger = GetRightLogger(logger);
            logger.WriteLine();
            foreach (var infoLine in HostEnvironmentInfo.GetCurrent().ToFormattedString())
            {
                logger.WriteLineInfo(infoLine);
            }
            logger.WriteLine();

            PrintTable(summary.Table, logger);

            // TODO: move this logic to an analyser
            var benchmarksWithTroubles = summary.Reports.Where(r => !r.GetResultRuns().Any()).Select(r => r.Benchmark).ToList();
            if (benchmarksWithTroubles.Count > 0)
            {
                logger.WriteLine();
                logger.WriteLineError("Benchmarks with issues:");
                foreach (var benchmarkWithTroubles in benchmarksWithTroubles)
                    logger.WriteLineError("  " + benchmarkWithTroubles.ShortInfo);
            }
        }
    }
}
