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
        private SolrSearch<TestDocument> _solrSearch;

        [Params(10, 100, 500, 1000)]
        public int ElementsCount { get; set; }

        [Setup]
        public void Setup()
        {
            var mockSolrConnection = new Mock<ISolrConnection>();
            mockSolrConnection.Setup(q => q.Get(It.IsAny<string>(), It.IsAny<string>())).Returns("json");

            var mockEngine = new MockEngine();
            mockEngine.Setup(q => q.GetService<ISolrConnection>()).Returns(mockSolrConnection.Object);
            mockEngine.Setup(q => q.GetService<IOffsetParameter>()).Returns(new Mock<IOffsetParameter>().Object);
            mockEngine.Setup(q => q.GetService<ILimitParameter>()).Returns(new Mock<ILimitParameter>().Object);
            mockEngine.Setup(q => q.GetService<ISystemParameter>()).Returns(new Mock<ISystemParameter>().Object);
            mockEngine.Setup(q => q.GetService<ISearchParameterCollection>()).Returns(new Mock<ISearchParameterCollection>().Object);
            ApplicationServices.Current = mockEngine;

            var options = new DocumentCollectionOptions<TestDocument>();
            var parameters = new List<ISearchParameter<object>>();

            for (int i = 0; i < this.ElementsCount; i++)
            {
                var parameterMock = new Mock<ISearchParameter<object>>();
                parameterMock.Setup(q => q.AllowMultipleInstances).Returns(true);
                parameterMock.Setup(q => q.Execute(It.IsAny<object>()));

                parameters.Add(parameterMock.Object);
            }

            _solrSearch = new SolrSearch<TestDocument>(options);

            parameters.ForEach(_solrSearch.Add);
        }

        [Benchmark]
        public void Execute()
        {
            this._solrSearch.Execute();
        }
    }
}