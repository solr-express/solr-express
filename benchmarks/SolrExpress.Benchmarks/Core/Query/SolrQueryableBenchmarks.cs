using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Moq;
using SolrExpress.Core;
using SolrExpress.Core.Query;
using SolrExpress.Core.Query.Parameter;
using System.Collections.Generic;
using System.Linq;

namespace SolrExpress.Benchmarks.Core.Query
{
    public class SolrQueryableBenchmarks
    {
        private SolrQueryable<TestDocument> _solrQueryable10;
        private SolrQueryable<TestDocument> _solrQueryable100;
        private SolrQueryable<TestDocument> _solrQueryable500;
        private SolrQueryable<TestDocument> _solrQueryable1000;

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

            for (int i = 0; i < 1000; i++)
            {
                var parameterMock = new Mock<IParameter<object>>();
                parameterMock.Setup(q => q.AllowMultipleInstances).Returns(true);
                parameterMock.Setup(q => q.Execute(It.IsAny<object>()));

                parameters.Add(parameterMock.Object);
            }

            this._solrQueryable10 = new SolrQueryable<TestDocument>(mockProvider.Object, mockResolver.Object, configuration);
            this._solrQueryable100 = new SolrQueryable<TestDocument>(mockProvider.Object, mockResolver.Object, configuration);
            this._solrQueryable500 = new SolrQueryable<TestDocument>(mockProvider.Object, mockResolver.Object, configuration);
            this._solrQueryable1000 = new SolrQueryable<TestDocument>(mockProvider.Object, mockResolver.Object, configuration);

            this._solrQueryable10.Parameter(parameters.Take(10).ToArray());
            this._solrQueryable100.Parameter(parameters.Take(100).ToArray());
            this._solrQueryable500.Parameter(parameters.Take(500).ToArray());
            this._solrQueryable1000.Parameter(parameters.ToArray());
        }

        /// <summary>
        /// Where   Using a SolrQueryable instance
        /// When    Invoking the method "Execute"
        /// With    Using 10 parameters
        /// </summary>
        [Benchmark(Baseline = true)]
        public void With10Parameters()
        {
            var result = this._solrQueryable10.Execute();
        }

        /// <summary>
        /// Where   Using a SolrQueryable instance
        /// When    Invoking the method "Execute"
        /// With    Using 100 parameters
        /// </summary>
        [Benchmark]
        public void With100Parameters()
        {
            var result = this._solrQueryable100.Execute();
        }

        /// <summary>
        /// Where   Using a SolrQueryable instance
        /// When    Invoking the method "Execute"
        /// With    Using 500 parameters
        /// </summary>
        [Benchmark]
        public void With500Parameters()
        {
            var result = this._solrQueryable500.Execute();
        }

        /// <summary>
        /// Where   Using a SolrQueryable instance
        /// When    Invoking the method "Execute"
        /// With    Using 1000 parameters
        /// </summary>
        [Benchmark]
        public void With1000Parameters()
        {
            var result = this._solrQueryable1000.Execute();
        }
    }
}