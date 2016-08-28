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
    public class FacetFieldResultBenchmarks
    {
        private List<ISearchParameter> _emptyParameters;
        private JObject _jsonObject;
        private IConvertJsonObject _facetFieldResult;

        [Params(10, 100, 500, 1000)]
        public int ElementsCount { get; set; }

        [Setup]
        public void Setup()
        {
            this._emptyParameters = new List<ISearchParameter>();

            this._facetFieldResult = new FacetFieldResult<TestDocument>();

            // Data using http://www.json-generator.com/
            var assembly = typeof(FacetFieldResultBenchmarks).GetTypeInfo().Assembly;
            var str = EmbeddedResourceHelper.GetByName(assembly, $"SolrExpress.Benchmarks.Solr4.Search.Result.FacetFieldResultBenchmarks{this.ElementsCount}.json");

            this._jsonObject = JObject.Parse(str);
        }
        
        /// <summary>
        /// Where   Using a FacetFieldResult instance
        /// When    Invoking the method "Execute"
        /// With    Using 1000 itens in raw text
        /// </summary>
        [Benchmark]
        public void Execute()
        {
            this._facetFieldResult.Execute(this._emptyParameters, this._jsonObject);
        }
    }
}
