using BenchmarkDotNet.Attributes;
using Moq;
using Newtonsoft.Json.Linq;
using SolrExpress.Core.Query.Parameter;
using SolrExpress.Solr5.Query;
using System.Collections.Generic;
using System.Linq;

namespace SolrExpress.Benchmarks.Solr5.Query
{
    public class ParameterContainerBenchmarks
    {
        private ParameterContainer _parameterContainer10;
        private ParameterContainer _parameterContainer100;
        private ParameterContainer _parameterContainer500;
        private ParameterContainer _parameterContainer1000;

        [Setup]
        public void Setup()
        {
            var parameters = new List<IParameter>();

            for (int i = 0; i < 1000; i++)
            {
                var parameterMock = new Mock<IParameter<JObject>>();
                parameterMock.Setup(q => q.AllowMultipleInstances).Returns(true);
                parameterMock.Setup(q => q.Execute(It.IsAny<JObject>())).Callback((JObject jObject) => { });

                parameters.Add(parameterMock.Object);
            }

            this._parameterContainer10 = new ParameterContainer();
            this._parameterContainer100 = new ParameterContainer();
            this._parameterContainer500 = new ParameterContainer();
            this._parameterContainer1000 = new ParameterContainer();

            this._parameterContainer10.AddParameters(parameters.Take(10).ToList());
            this._parameterContainer100.AddParameters(parameters.Take(100).ToList());
            this._parameterContainer500.AddParameters(parameters.Take(500).ToList());
            this._parameterContainer1000.AddParameters(parameters);
        }

        /// <summary>
        /// Where   Using a ParameterContainer instance
        /// When    Invoking the method "Execute"
        /// With    Using 10 parameters
        /// </summary>
        [Benchmark(Baseline = true)]
        public void ParameterContainerWith10Parameters()
        {
            var result = this._parameterContainer10.Execute();
        }

        /// <summary>
        /// Where   Using a ParameterContainer instance
        /// When    Invoking the method "Execute"
        /// With    Using 100 parameters
        /// </summary>
        [Benchmark]
        public void ParameterContainerWith100Parameters()
        {
            var result = this._parameterContainer100.Execute();
        }

        /// <summary>
        /// Where   Using a ParameterContainer instance
        /// When    Invoking the method "Execute"
        /// With    Using 500 parameters
        /// </summary>
        [Benchmark]
        public void ParameterContainerWith500Parameters()
        {
            var result = this._parameterContainer500.Execute();
        }

        /// <summary>
        /// Where   Using a ParameterContainer instance
        /// When    Invoking the method "Execute"
        /// With    Using 1000 parameters
        /// </summary>
        [Benchmark]
        public void ParameterContainerWith1000Parameters()
        {
            var result = this._parameterContainer1000.Execute();
        }
    }
}
