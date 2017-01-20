using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Moq;
using SolrExpress.Benchmarks.Helper;
using SolrExpress.Core.Search;
using SolrExpress.Core.Search.Parameter;
using System.Collections.Generic;

namespace SolrExpress.Core.Benchmarks.Search
{
    public class SolrSearchBenchmarks
    {
        private ISolrSearch<TestDocument> _solrSearch;

        [Params(10, 100, 500, 1000)]
        public int ElementsCount { get; set; }

        [Setup]
        public void Setup()
        {
            var solrConnection = new Mock<ISolrConnection>();
            solrConnection.Setup(q => q.Get(new SecurityOptions(), It.IsAny<string>(), It.IsAny<string>())).Returns("json");

            var engine = new MockEngine();
            engine.Setup(q => q.GetService<ISolrConnection>()).Returns(solrConnection.Object);
            engine.Setup(q => q.GetService<IOffsetParameter<TestDocument>>()).Returns(new Mock<IOffsetParameter<TestDocument>>().Object);
            engine.Setup(q => q.GetService<ILimitParameter<TestDocument>>()).Returns(new Mock<ILimitParameter<TestDocument>>().Object);
            engine.Setup(q => q.GetService<ISystemParameter<TestDocument>>()).Returns(new Mock<ISystemParameter<TestDocument>>().Object);
            engine.Setup(q => q.GetService<ISearchParameterCollection<TestDocument>>()).Returns(new Mock<ISearchParameterCollection<TestDocument>>().Object);

            var options = new DocumentCollectionOptions<TestDocument>();
            var parameters = new List<ISearchItem>();

            for (int i = 0; i < this.ElementsCount; i++)
            {
                var parameterMock = new Mock<ISearchParameter>();
                parameterMock.Setup(q => q.AllowMultipleInstances).Returns(true);

                parameters.Add(parameterMock.Object);
            }

            _solrSearch = new SolrSearch<TestDocument>(options, engine.Object);

            parameters.ForEach(item => _solrSearch.Add(item));
        }

        [Benchmark]
        public void Execute()
        {
            this._solrSearch.Execute();
        }
    }
}