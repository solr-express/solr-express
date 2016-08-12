using BenchmarkDotNet.Attributes;
using Newtonsoft.Json.Linq;
using SolrExpress.Benchmarks.Helper;
using SolrExpress.Core.Query.Parameter;
using SolrExpress.Solr4.Query.Parameter;
using SolrExpress.Solr4.Query.Result;
using System.Collections.Generic;
using System.Reflection;

namespace SolrExpress.Benchmarks.Solr4.Query.Result
{
    public class FacetRangeResultBenchmarks
    {
        private List<IParameter> _parameters;
        private JObject _jsonObject;
        private FacetRangeResult<TestDocument> _facetRangeResult;

        [Params(10, 100, 500, 1000)]
        public int ElementsCount { get; set; }

        [Setup]
        public void Setup()
        {
            this._parameters = new List<IParameter> {
                new FacetRangeParameter<TestDocument>().Configure("facetRange", q=> q.Age, "10", "10", "100")
            };

            this._facetRangeResult = new FacetRangeResult<TestDocument>();
            
            // Data using http://www.json-generator.com/
            var assembly = typeof(FacetRangeResultBenchmarks).GetTypeInfo().Assembly;
            var str = EmbeddedResourceHelper.GetByName(assembly, $"SolrExpress.Benchmarks.Solr4.Query.Result.FacetRangeResultBenchmarks{this.ElementsCount}");
            
            this._jsonObject = JObject.Parse(str);
        }

        [Benchmark]
        public void Execute()
        {
            this._facetRangeResult.Execute(this._parameters, this._jsonObject);
        }
    }
}
