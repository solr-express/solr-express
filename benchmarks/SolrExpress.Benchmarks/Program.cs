using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Environments;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Validators;
using System;

namespace SolrExpress.Benchmarks
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var config = ManualConfig
                .Create(DefaultConfig.Instance)
                //.With(new CustomMarkdownExporter())
                .With(StatisticColumn.AllStatistics)
                .With(ExecutionValidator.FailOnError)
                .With(Job.Default.With(Runtime.Core))
                .With(Job.Default.With(Runtime.Clr));

            BenchmarkRunner.Run<Core.DocumentCollectionBenchmarks>(config);

            //BenchmarkRunner.Run<Solr4.Search.ParameterContainerBenchmarks>(config);
            //BenchmarkRunner.Run<Solr4.Search.Result.DocumentResultBenchmarks>(config);
            //BenchmarkRunner.Run<Solr4.Search.Result.FacetFieldResultBenchmarks>(config);
            //BenchmarkRunner.Run<Solr4.Search.Result.FacetQueryResultBenchmarks>(config);
            //BenchmarkRunner.Run<Solr4.Search.Result.FacetRangeResultBenchmarks>(config);

            //BenchmarkRunner.Run<Solr5.Search.ParameterContainerBenchmarks>(config);
            //BenchmarkRunner.Run<Solr5.Search.Result.DocumentResultBenchmarks>(config);
            //BenchmarkRunner.Run<Solr5.Search.Result.FacetFieldResultBenchmarks>(config);
            //BenchmarkRunner.Run<Solr5.Search.Result.FacetQueryResultBenchmarks>(config);
            //BenchmarkRunner.Run<Solr5.Search.Result.FacetRangeResultBenchmarks>(config);

            Console.Read();
        }
    }
}
