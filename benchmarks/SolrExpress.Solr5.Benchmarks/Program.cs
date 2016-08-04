using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Validators;

namespace SolrExpress.Solr5.Benchmarks
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var config = ManualConfig
                .Create(DefaultConfig.Instance)
                .With(MarkdownExporter.GitHub)
                .With(StatisticColumn.AllStatistics)
                .With(ExecutionValidator.FailOnError)
                .With(Job.LongRun);

            //BenchmarkRunner.Run<SolrQueryableBenchmarks>(config);
        }
    }
}
