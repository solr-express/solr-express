using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Moq;
using SolrExpress.Benchmarks.Helper;
using SolrExpress.Core;
using SolrExpress.Core.Query;
using SolrExpress.Core.Query.Parameter;
using System.Collections.Generic;
using System.Linq;

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
            var mockProvider = new Mock<IProvider>();
            var mockResolver = new Mock<IResolver>();
            var configuration = new Configuration();
            var parameters = new List<IParameter<object>>();

            mockProvider.Setup(q => q.Get(It.IsAny<string>(), It.IsAny<string>())).Returns("json");

            mockResolver.Setup(q => q.GetInstance<IOffsetParameter>()).Returns(new Mock<IOffsetParameter>().Object);
            mockResolver.Setup(q => q.GetInstance<ILimitParameter>()).Returns(new Mock<ILimitParameter>().Object);
            mockResolver.Setup(q => q.GetInstance<ISystemParameter>()).Returns(new Mock<ISystemParameter>().Object);
            mockResolver.Setup(q => q.GetInstance<IParameterContainer>()).Returns(new Mock<IParameterContainer>().Object);

            for (int i = 0; i < ElementsCount; i++)
            {
                var parameterMock = new Mock<IParameter<object>>();
                parameterMock.Setup(q => q.AllowMultipleInstances).Returns(true);
                parameterMock.Setup(q => q.Execute(It.IsAny<object>()));

                parameters.Add(parameterMock.Object);
            }
            
            _solrQueryable = new SolrQueryable<TestDocument>(mockProvider.Object, mockResolver.Object, configuration);

            _solrQueryable.Parameter(parameters.Take(ElementsCount).ToArray());
        }

        /// <summary>
        /// Where   Using a SolrQueryable instance
        /// When    Invoking the method "Execute"
        /// With    Using N parameters
        /// </summary>
        [Benchmark]
        public void Execute()
        {
            return this._solrQueryable.Execute();
        }
    }
}