using BenchmarkDotNet.Attributes;
using Newtonsoft.Json.Linq;
using SolrExpress.Benchmarks.Helper;
using SolrExpress.Core.Search;
using SolrExpress.Core.Search.Result;
using SolrExpress.Solr4.Search.Result;
using System.Collections.Generic;
using System.Reflection;

namespace SolrExpress.Benchmarks.Solr4.Search.Result
{
    public class FacetQueryResultBenchmarks
    {
        private List<ISearchParameter> _emptyParameters;
        private JObject _jsonObject;
        private IConvertJsonObject _facetQueryResult;

        [Params(10, 100, 500, 1000)]
        public int ElementsCount { get; set; }

        [Setup]
        public void Setup()
        {
            this._emptyParameters = new List<ISearchParameter>();

            this._facetQueryResult = new FacetQueryResult<TestDocument>();

            // Data using http://www.json-generator.com/
            var assembly = typeof(FacetQueryResultBenchmarks).GetTypeInfo().Assembly;
            var str = EmbeddedResourceHelper.GetByName(assembly, $"SolrExpress.Benchmarks.Solr4.Search.Result.FacetQueryResultBenchmarks{this.ElementsCount}.json");

            this._jsonObject = JObject.Parse(str);
        }
        
        [Benchmark]
        public void Execute()
        {
            this._facetQueryResult.Execute(this._emptyParameters, this._jsonObject);
        }
    }
}
