using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Validators;
using SolrExpress.Benchmarks.Exporter;
using SolrExpress.Core.Benchmarks.Query;
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
                .With(StatisticColumn.AllStatistics)
                .With(ExecutionValidator.FailOnError)
                .With(Job.Default.With(Runtime.Core))
                .With(Job.Default.With(Runtime.Clr));

            BenchmarkRunner.Run<SolrQueryableBenchmarks>(config);

            BenchmarkRunner.Run<Solr4.Query.ParameterContainerBenchmarks>(config);
            BenchmarkRunner.Run<Solr4.Query.Result.DocumentResultBenchmarks>(config);
            BenchmarkRunner.Run<Solr4.Query.Result.FacetFieldResultBenchmarks>(config);
            BenchmarkRunner.Run<Solr4.Query.Result.FacetQueryResultBenchmarks>(config);
            BenchmarkRunner.Run<Solr4.Query.Result.FacetRangeResultBenchmarks>(config);

            BenchmarkRunner.Run<Solr5.Query.ParameterContainerBenchmarks>(config);
            BenchmarkRunner.Run<Solr5.Query.Result.DocumentResultBenchmarks>(config);
            BenchmarkRunner.Run<Solr5.Query.Result.FacetFieldResultBenchmarks>(config);
            BenchmarkRunner.Run<Solr5.Query.Result.FacetQueryResultBenchmarks>(config);
            BenchmarkRunner.Run<Solr5.Query.Result.FacetRangeResultBenchmarks>(config);

            Console.Read();
        }
    }
}
