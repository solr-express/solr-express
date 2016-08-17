using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Moq;
using SolrExpress.Benchmarks.Helper;
using SolrExpress.Core.DependencyInjection;
using SolrExpress.Core.Query;
using SolrExpress.Core.Query.Parameter;
using System.Collections.Generic;

namespace SolrExpress.Core.Benchmarks.Query
{
    public class SolrQueryableBenchmarks
    {
        private SolrQueryable<TestDocument> _solrQueryable;

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
            mockEngine.Setup(q => q.GetService<IParameterContainer>()).Returns(new Mock<IParameterContainer>().Object);
            ApplicationServices.Current = mockEngine;

            var options = new DocumentCollectionOptions<TestDocument>();
            var parameters = new List<IParameter<object>>();

            for (int i = 0; i < this.ElementsCount; i++)
            {
                var parameterMock = new Mock<IParameter<object>>();
                parameterMock.Setup(q => q.AllowMultipleInstances).Returns(true);
                parameterMock.Setup(q => q.Execute(It.IsAny<object>()));

                parameters.Add(parameterMock.Object);
            }

            _solrQueryable = new SolrQueryable<TestDocument>(options);

            _solrQueryable.Parameter(parameters.ToArray());
        }

        [Benchmark]
        public void Execute()
        {
            this._solrQueryable.Execute();
        }
    }
}