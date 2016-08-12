using BenchmarkDotNet.Attributes;
using Newtonsoft.Json.Linq;
using SolrExpress.Benchmarks.Helper;
using SolrExpress.Core.Query.Parameter;
using SolrExpress.Solr4.Query.Result;
using System.Collections.Generic;
using System.Reflection;

namespace SolrExpress.Benchmarks.Solr4.Query.Result
{
    public class DocumentResultBenchmarks
    {
        private List<IParameter> _emptyParameters;
        private JObject _jsonObject;
        private DocumentResult<TestDocument> _documentResult;

        [Params(10, 100, 500, 1000)]
        public int ElementsCount { get; set; }

        [Setup]
        public void Setup()
        {
            this._emptyParameters = new List<IParameter>();

            this._documentResult = new DocumentResult<TestDocument>();

            // Data using http://www.json-generator.com/
            var assembly = typeof(DocumentResultBenchmarks).GetTypeInfo().Assembly;
            var str = EmbeddedResourceHelper.GetByName(assembly, $"SolrExpress.Benchmarks.Solr4.Query.Result.DocumentResultBenchmarks{this.ElementsCount}");

            this._jsonObject = JObject.Parse(str);
        }
        
        [Benchmark]
        public void Execute()
        {
            this._documentResult.Execute(this._emptyParameters, this._jsonObject);
        }
    }
}
