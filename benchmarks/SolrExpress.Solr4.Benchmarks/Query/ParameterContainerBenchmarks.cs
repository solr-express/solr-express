using BenchmarkDotNet.Attributes;
using Moq;
using SolrExpress.Core.Query.Parameter;
using SolrExpress.Solr4.Query;
using System.Collections.Generic;

namespace SolrExpress.Solr4.Benchmarks.Query
{
    public class ParameterContainerBenchmarks
    {
        private ParameterContainer _parameterContainerLarge;
        private ParameterContainer _parameterContainerSmall;

        [Setup]
        public void Setup()
        {
            var parametersLarge = new List<IParameter>();
            var parametersSmall = new List<IParameter>();

            for (int i = 0; i < 1000; i++)
            {
                var parameterMock = new Mock<IParameter<List<string>>>();
                parameterMock.Setup(q => q.AllowMultipleInstances).Returns(true);
                parameterMock.Setup(q => q.Execute(It.IsAny<List<string>>())).Callback((List<string> list) => list.Add("X"));

                parametersLarge.Add(parameterMock.Object);
            }

            for (int i = 0; i < 10; i++)
            {
                var parameterMock = new Mock<IParameter<List<string>>>();
                parameterMock.Setup(q => q.AllowMultipleInstances).Returns(true);
                parameterMock.Setup(q => q.Execute(It.IsAny<List<string>>())).Callback((List<string> list) => list.Add("X"));

                parametersSmall.Add(parameterMock.Object);
            }

            this._parameterContainerLarge = new ParameterContainer();
            this._parameterContainerLarge.AddParameters(parametersLarge);

            this._parameterContainerSmall = new ParameterContainer();
            this._parameterContainerSmall.AddParameters(parametersSmall);
        }

        /// <summary>
        /// Where   Using a SolrQueryable instance
        /// When    Invoking the method "Execute"
        /// With    Using 1000 parameters
        /// </summary>
        [Benchmark]
        public void SolrQueryableWith1000Parameters()
        {
            var result = this._parameterContainerLarge.Execute();
        }

        /// <summary>
        /// Where   Using a SolrQueryable instance
        /// When    Invoking the method "Execute"
        /// With    Using 10 parameters
        /// </summary>
        [Benchmark]
        public void SolrQueryableWith10Parameters()
        {
            var result = this._parameterContainerSmall.Execute();
        }
    }
}
