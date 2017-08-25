using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Environments;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Validators;
using SolrExpress.Benchmarks.Exporter;
using System;

namespace SolrExpress.Benchmarks
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var config = ManualConfig
                .Create(DefaultConfig.Instance)
                .With(new CustomMarkdownExporter())
                .With(StatisticColumn.Max)
                .With(StatisticColumn.Min)
                .With(StatisticColumn.Mean)
                .With(StatisticColumn.Median)
                .With(StatisticColumn.OperationsPerSecond)
                .With(MemoryDiagnoser.Default)
                .With(ExecutionValidator.FailOnError)
                .With(Job.Default.With(Runtime.Core))
                .With(Job.Default.With(Runtime.Clr));

            BenchmarkRunner.Run<Core.DocumentSearchBenchmarks>(config);

            BenchmarkRunner.Run<Solr4.Search.Result.DocumentResultBenchmarks>(config);
            BenchmarkRunner.Run<Solr4.Search.Result.FacetsResultBenchmarks>(config);

            BenchmarkRunner.Run<Solr5.Search.Result.DocumentResultBenchmarks>(config);
            BenchmarkRunner.Run<Solr5.Search.Result.FacetsResultBenchmarks>(config);

            Console.Read();
        }
    }
}
