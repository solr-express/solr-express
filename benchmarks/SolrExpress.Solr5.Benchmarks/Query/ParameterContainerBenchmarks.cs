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
        private ParameterContainer _parameterContainer;

        [Params(10, 100, 500, 1000)]
        public int ElementsCount { get; set; }

        [Setup]
        public void Setup()
        {
            var parameters = new List<IParameter>();

            for (int i = 0; i < this.ElementsCount; i++)
            {
                var parameterMock = new Mock<IParameter<JObject>>();
                parameterMock.Setup(q => q.AllowMultipleInstances).Returns(true);
                parameterMock.Setup(q => q.Execute(It.IsAny<JObject>())).Callback((JObject jObject) => { });

                parameters.Add(parameterMock.Object);
            }

            this._parameterContainer = new ParameterContainer();

            this._parameterContainer.AddParameters(parameters.ToList());
        }

        [Benchmark]
        public void Execute()
        {
            this._parameterContainer.Execute();
        }
    }
}
