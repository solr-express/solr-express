using BenchmarkDotNet.Attributes;
using Moq;
using Newtonsoft.Json.Linq;
using SolrExpress.Benchmarks.Helper;
using SolrExpress.Core.Search;
using SolrExpress.Solr5.Search;
using System.Collections.Generic;
using System.Linq;

namespace SolrExpress.Benchmarks.Solr5.Search
{
    public class ParameterContainerBenchmarks
    {
        private ISearchParameterCollection<TestDocument> _parameterContainer;

        [Params(10, 100, 500, 1000)]
        public int ElementsCount { get; set; }

        [Setup]
        public void Setup()
        {
            var parameters = new List<ISearchParameter>();

            for (int i = 0; i < this.ElementsCount; i++)
            {
                var parameterMock = new Mock<ISearchParameter<JObject>>();
                parameterMock.Setup(q => q.AllowMultipleInstances).Returns(true);
                parameterMock.Setup(q => q.Execute(It.IsAny<JObject>())).Callback((JObject jObject) => { });

                parameters.Add(parameterMock.Object);
            }

            this._parameterContainer = new SearchParameterCollection<TestDocument>();

            this._parameterContainer.Add(parameters.ToList());
        }

        [Benchmark]
        public void Execute()
        {
            this._parameterContainer.Execute();
        }
    }
}
