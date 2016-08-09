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
        private JObject _jsonObject10;
        private JObject _jsonObject100;
        private JObject _jsonObject500;
        private JObject _jsonObject1000;
        private DocumentResult<TestDocument> _documentResult10;
        private DocumentResult<TestDocument> _documentResult100;
        private DocumentResult<TestDocument> _documentResult500;
        private DocumentResult<TestDocument> _documentResult1000;

        [Setup]
        public void Setup()
        {
            this._emptyParameters = new List<IParameter>();

            this._documentResult10 = new DocumentResult<TestDocument>();
            this._documentResult100 = new DocumentResult<TestDocument>();
            this._documentResult500 = new DocumentResult<TestDocument>();
            this._documentResult1000 = new DocumentResult<TestDocument>();

            // Data using http://www.json-generator.com/
            var assembly = typeof(DocumentResultBenchmarks).GetTypeInfo().Assembly;
            var str10 = EmbeddedResourceHelper.GetByName(assembly, "DocumentResultBenchmarks10");
            var str100 = EmbeddedResourceHelper.GetByName(assembly, "DocumentResultBenchmarks100");
            var str500 = EmbeddedResourceHelper.GetByName(assembly, "DocumentResultBenchmarks500");
            var str1000 = EmbeddedResourceHelper.GetByName(assembly, "DocumentResultBenchmarks1000");

            this._jsonObject10 = JObject.Parse(str10);
            this._jsonObject100 = JObject.Parse(str100);
            this._jsonObject500 = JObject.Parse(str500);
            this._jsonObject1000 = JObject.Parse(str1000);
        }

        /// <summary>
        /// Where   Using a DocumentResult instance
        /// When    Invoking the method "Execute"
        /// With    Using 10 itens in raw text
        /// </summary>
        [Benchmark(Baseline = true)]
        public void DocumentResultWith10Parameters()
        {
            this._documentResult10.Execute(this._emptyParameters, this._jsonObject10);
        }

        /// <summary>
        /// Where   Using a DocumentResult instance
        /// When    Invoking the method "Execute"
        /// With    Using 100 itens in raw text
        /// </summary>
        [Benchmark]
        public void DocumentResultWith100Parameters()
        {
            this._documentResult10.Execute(this._emptyParameters, this._jsonObject100);
        }

        /// <summary>
        /// Where   Using a DocumentResult instance
        /// When    Invoking the method "Execute"
        /// With    Using 500 itens in raw text
        /// </summary>
        [Benchmark]
        public void DocumentResultWith500Parameters()
        {
            this._documentResult10.Execute(this._emptyParameters, this._jsonObject500);
        }

        /// <summary>
        /// Where   Using a DocumentResult instance
        /// When    Invoking the method "Execute"
        /// With    Using 1000 itens in raw text
        /// </summary>
        [Benchmark]
        public void DocumentResultWith1000Parameters()
        {
            this._documentResult10.Execute(this._emptyParameters, this._jsonObject1000);
        }
    }
}
