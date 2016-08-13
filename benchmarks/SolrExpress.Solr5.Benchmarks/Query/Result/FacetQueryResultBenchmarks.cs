using BenchmarkDotNet.Attributes;
using Newtonsoft.Json.Linq;
using SolrExpress.Benchmarks.Helper;
using SolrExpress.Core.Query.Parameter;
using SolrExpress.Solr5.Query.Result;
using System.Collections.Generic;
using System.Reflection;

namespace SolrExpress.Benchmarks.Solr5.Query.Result
{
    public class FacetQueryResultBenchmarks
    {
        private List<IParameter> _emptyParameters;
        private JObject _jsonObject;
        private FacetQueryResult<TestDocument> _facetQueryResult;

        [Params(10, 100, 500, 1000)]
        public int ElementsCount { get; set; }

        [Setup]
        public void Setup()
        {
            this._emptyParameters = new List<IParameter>();

            this._facetQueryResult = new FacetQueryResult<TestDocument>();

            // Data using http://www.json-generator.com/
            var assembly = typeof(FacetQueryResultBenchmarks).GetTypeInfo().Assembly;
            var str = EmbeddedResourceHelper.GetByName(assembly, $"SolrExpress.Solr5.Benchmarks.Query.Result.FacetQueryResultBenchmarks{this.ElementsCount}.json");
            
            this._jsonObject = JObject.Parse(str);
        }
        
        [Benchmark]
        public void Execute()
        {
            this._facetQueryResult.Execute(this._emptyParameters, this._jsonObject);
        }
    }
}
