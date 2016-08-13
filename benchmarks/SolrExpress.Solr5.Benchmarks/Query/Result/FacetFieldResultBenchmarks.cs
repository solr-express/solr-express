using BenchmarkDotNet.Attributes;
using Newtonsoft.Json.Linq;
using SolrExpress.Benchmarks.Helper;
using SolrExpress.Core.Query.Parameter;
using SolrExpress.Solr5.Query.Result;
using System.Collections.Generic;
using System.Reflection;

namespace SolrExpress.Benchmarks.Solr5.Query.Result
{
    public class FacetFieldResultBenchmarks
    {
        private List<IParameter> _emptyParameters;
        private JObject _jsonObject;
        private FacetFieldResult<TestDocument> _facetFieldResult;

        [Params(10, 100, 500, 1000)]
        public int ElementsCount { get; set; }

        [Setup]
        public void Setup()
        {
            this._emptyParameters = new List<IParameter>();

            this._facetFieldResult = new FacetFieldResult<TestDocument>();

            // Data using http://www.json-generator.com/
            var assembly = typeof(FacetFieldResultBenchmarks).GetTypeInfo().Assembly;
            var str = EmbeddedResourceHelper.GetByName(assembly, $"SolrExpress.Solr5.Benchmarks.Query.Result.FacetFieldResultBenchmarks{this.ElementsCount}.json");

            this._jsonObject = JObject.Parse(str);
        }

        [Benchmark]
        public void Execute()
        {
            this._facetFieldResult.Execute(this._emptyParameters, this._jsonObject);
        }
    }
}
