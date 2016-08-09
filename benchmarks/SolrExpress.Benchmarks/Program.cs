using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Validators;
using SolrExpress.Benchmarks.Exporter;

namespace SolrExpress.Benchmarks
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var config = ManualConfig
                .Create(DefaultConfig.Instance.KeepBenchmarkFiles())
                .With(new CustomMarkdownExporter())
                .With(StatisticColumn.AllStatistics)
                .With(ExecutionValidator.FailOnError)
                .With(Job.Dry.With(Runtime.Core))
                .With(Job.Dry.With(Runtime.Clr));

            BenchmarkRunner.Run<Core.Query.SolrQueryableBenchmarks>(config);

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
        }
    }
}
