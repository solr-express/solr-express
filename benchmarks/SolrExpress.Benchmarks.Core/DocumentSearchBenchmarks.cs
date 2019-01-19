using BenchmarkDotNet.Attributes;
using SimpleInjector;
using SolrExpress.Benchmarks.Helper;
using SolrExpress.Search;

namespace SolrExpress.Benchmarks.Core
{
    public class DocumentSearchBenchmarks
    {
        private DocumentSearch<TestDocument> _documentSearch;

        [Params(10, 100, 500, 1000)]
        public int ElementsCount { get; set; }

        [GlobalSetup]
        public void Setup()
        {
            var container = new Container();

            container.AddSolrExpressFake<TestDocument>(q => q.UseSolrFake());

            this._documentSearch = container
                .GetInstance<DocumentCollection<TestDocument>>()
                .Select();

            for (var i = 0; i < this.ElementsCount; i++)
            {
                var anyParameter = new FakeAnyParameter
                {
                    Name = $"Name{i}",
                    Value = $"Value{i}"
                };

                this._documentSearch.Add(anyParameter);
            }
        }

        [Benchmark(Description = "DocumentSearch")]
        public void Execute()
        {
            this._documentSearch.Execute();
        }
    }
}
