using BenchmarkDotNet.Attributes;
using Moq;
using SolrExpress.Core.Query.Parameter;
using SolrExpress.Solr4.Query;
using System.Collections.Generic;

namespace SolrExpress.Benchmarks.Solr4.Query
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
                var parameterMock = new Mock<IParameter<List<string>>>();
                parameterMock.Setup(q => q.AllowMultipleInstances).Returns(true);
                parameterMock.Setup(q => q.Execute(It.IsAny<List<string>>())).Callback((List<string> list) => list.Add("X"));

                parameters.Add(parameterMock.Object);
            }

            this._parameterContainer = new ParameterContainer();
            this._parameterContainer.AddParameters(parameters);
        }

        [Benchmark]
        public void Execute()
        {
            this._parameterContainer.Execute();
        }
    }
}
