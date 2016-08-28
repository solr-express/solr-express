using BenchmarkDotNet.Attributes;
using Newtonsoft.Json.Linq;
using SolrExpress.Benchmarks.Helper;
using SolrExpress.Core.Search;
using SolrExpress.Core.Search.Result;
using SolrExpress.Solr5.Search.Result;
using System.Collections.Generic;
using System.Reflection;

namespace SolrExpress.Benchmarks.Solr5.Search.Result
{
    public class DocumentResultBenchmarks
    {
        private List<ISearchParameter> _emptyParameters;
        private JObject _jsonObject;
        private IConvertJsonObject _documentResult;

        [Params(10, 100, 500, 1000)]
        public int ElementsCount { get; set; }

        [Setup]
        public void Setup()
        {
            this._emptyParameters = new List<ISearchParameter>();

            this._documentResult = new DocumentResult<TestDocument>();

            // Data using http://www.json-generator.com/
            var assembly = typeof(DocumentResultBenchmarks).GetTypeInfo().Assembly;
            var str = EmbeddedResourceHelper.GetByName(assembly, $"SolrExpress.Solr5.Benchmarks.Search.Result.DocumentResultBenchmarks{this.ElementsCount}.json");

            this._jsonObject = JObject.Parse(str);
        }
        
        [Benchmark]
        public void Execute()
        {
            this._documentResult.Execute(this._emptyParameters, this._jsonObject);
        }
    }
}
