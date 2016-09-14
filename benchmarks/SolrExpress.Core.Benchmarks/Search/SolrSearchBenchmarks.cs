using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Moq;
using SolrExpress.Benchmarks.Helper;
using SolrExpress.Core.DependencyInjection;
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
            solrConnection.Setup(q => q.Get(It.IsAny<string>(), It.IsAny<string>())).Returns("json");

            var engine = new MockEngine();
            engine.Setup(q => q.GetService<ISolrConnection>()).Returns(solrConnection.Object);
            engine.Setup(q => q.GetService<IOffsetParameter>()).Returns(new Mock<IOffsetParameter>().Object);
            engine.Setup(q => q.GetService<ILimitParameter>()).Returns(new Mock<ILimitParameter>().Object);
            engine.Setup(q => q.GetService<ISystemParameter>()).Returns(new Mock<ISystemParameter>().Object);
            engine.Setup(q => q.GetService<ISearchParameterCollection>()).Returns(new Mock<ISearchParameterCollection>().Object);

            var options = new DocumentCollectionOptions<TestDocument>();
            var parameters = new List<ISearchItem>();

            for (int i = 0; i < this.ElementsCount; i++)
            {
                var parameterMock = new Mock<ISearchParameter<object>>();
                parameterMock.Setup(q => q.AllowMultipleInstances).Returns(true);
                parameterMock.Setup(q => q.Execute(It.IsAny<object>()));

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