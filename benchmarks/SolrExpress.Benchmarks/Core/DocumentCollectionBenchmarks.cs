using BenchmarkDotNet.Attributes;
using SimpleInjector;
using SolrExpress.Search;

namespace SolrExpress.Benchmarks.Core
{
    public class DocumentCollectionBenchmarks
    {
        private DocumentSearch<TestDocument> _documentSearch;

        //[Params(10, 100, 500, 1000)]
        [Params(10)]
        public int ElementsCount { get; set; }

#if CORE
        [GlobalSetup]
#else
        [Setup]
#endif
        public void Setup()
        {
            var container = new Container();

            container.AddSolrExpressFake<TestDocument>(q => q.UseSolrFake());

            this._documentSearch = container
                .GetInstance<DocumentCollection<TestDocument>>()
                .Select();

            for (int i = 0; i < this.ElementsCount; i++)
            {
                var anyParameter = new FakeAnyParameter
                {
                    Name = $"Name{i}",
                    Value = $"Value{i}",
                };

                this._documentSearch.Add(anyParameter);
            }
        }

        [Benchmark]
        public void Execute()
        {
            this._documentSearch.Execute();
        }
    }
}
