using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Moq;
using SolrExpress.Core.Query;
using SolrExpress.Core.Query.Parameter;
using System.Collections.Generic;

namespace SolrExpress.Core.Benchmarks.Query
{
    public class SolrQueryableBenchmarks
    {
        private SolrQueryable<TestDocument> _queryableLarge;
        private SolrQueryable<TestDocument> _queryableSmall;

        [Setup]
        public void Setup()
        {
            var mockProvider = new Mock<IProvider>();
            var mockResolver = new Mock<IDependencyResolver>();
            var configuration = new Configuration();
            var parametersLarge = new List<IParameter<object>>();
            var parametersSmall = new List<IParameter<object>>();

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

                parametersLarge.Add(parameterMock.Object);
            }

            for (int i = 0; i < 10; i++)
            {
                var parameterMock = new Mock<IParameter<object>>();
                parameterMock.Setup(q => q.AllowMultipleInstances).Returns(true);
                parameterMock.Setup(q => q.Execute(It.IsAny<object>()));

                parametersSmall.Add(parameterMock.Object);
            }

            this._queryableLarge = new SolrQueryable<TestDocument>(mockProvider.Object, mockResolver.Object, configuration);
            this._queryableLarge.Parameter(parametersLarge.ToArray());

            this._queryableSmall = new SolrQueryable<TestDocument>(mockProvider.Object, mockResolver.Object, configuration);
            this._queryableSmall.Parameter(parametersSmall.ToArray());
        }

        /// <summary>
        /// Where   Using a SolrQueryable instance
        /// When    Invoking the method "Execute"
        /// With    Using 1000 parameters
        /// </summary>
        [Benchmark]
        public void SolrQueryableWith1000Parameters()
        {
            var result = this._queryableLarge.Execute();
        }

        /// <summary>
        /// Where   Using a SolrQueryable instance
        /// When    Invoking the method "Execute"
        /// With    Using 10 parameters
        /// </summary>
        [Benchmark]
        public void SolrQueryableWith10Parameters()
        {
            var result = this._queryableLarge.Execute();
        }
    }
}