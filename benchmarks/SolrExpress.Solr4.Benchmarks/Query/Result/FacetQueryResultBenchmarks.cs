using BenchmarkDotNet.Attributes;
using Newtonsoft.Json.Linq;
using SolrExpress.Benchmarks.Helper;
using SolrExpress.Core.Query.Parameter;
using SolrExpress.Solr4.Query.Result;
using System.Collections.Generic;
using System.Reflection;

namespace SolrExpress.Benchmarks.Solr4.Query.Result
{
    public class FacetQueryResultBenchmarks
    {
        private List<IParameter> _emptyParameters;
        private JObject _jsonObject10;
        private JObject _jsonObject100;
        private JObject _jsonObject500;
        private JObject _jsonObject1000;
        private FacetQueryResult<TestDocument> _facetQueryResult10;
        private FacetQueryResult<TestDocument> _facetQueryResult100;
        private FacetQueryResult<TestDocument> _facetQueryResult500;
        private FacetQueryResult<TestDocument> _facetQueryResult1000;

        [Setup]
        public void Setup()
        {
            this._emptyParameters = new List<IParameter>();

            this._facetQueryResult10 = new FacetQueryResult<TestDocument>();
            this._facetQueryResult100 = new FacetQueryResult<TestDocument>();
            this._facetQueryResult500 = new FacetQueryResult<TestDocument>();
            this._facetQueryResult1000 = new FacetQueryResult<TestDocument>();

            // Data using http://www.json-generator.com/
            var assembly = typeof(FacetQueryResultBenchmarks).GetTypeInfo().Assembly;
            var str10 = EmbeddedResourceHelper.GetByName(assembly, "SolrExpress.Benchmarks.Solr4.Query.Result.FacetQueryResultBenchmarks10");
            var str100 = EmbeddedResourceHelper.GetByName(assembly, "SolrExpress.Benchmarks.Solr4.Query.Result.FacetQueryResultBenchmarks100");
            var str500 = EmbeddedResourceHelper.GetByName(assembly, "SolrExpress.Benchmarks.Solr4.Query.Result.FacetQueryResultBenchmarks500");
            var str1000 = EmbeddedResourceHelper.GetByName(assembly, "SolrExpress.Benchmarks.Solr4.Query.Result.FacetQueryResultBenchmarks1000");

            this._jsonObject10 = JObject.Parse(str10);
            this._jsonObject100 = JObject.Parse(str100);
            this._jsonObject500 = JObject.Parse(str500);
            this._jsonObject1000 = JObject.Parse(str1000);
        }

        /// <summary>
        /// Where   Using a FacetQueryResult instance
        /// When    Invoking the method "Execute"
        /// With    Using 10 itens in raw text
        /// </summary>
        [Benchmark(Baseline = true)]
        public void With10Parameters()
        {
            this._facetQueryResult10.Execute(this._emptyParameters, this._jsonObject10);
        }

        /// <summary>
        /// Where   Using a FacetQueryResult instance
        /// When    Invoking the method "Execute"
        /// With    Using 100 itens in raw text
        /// </summary>
        [Benchmark]
        public void With100Parameters()
        {
            this._facetQueryResult10.Execute(this._emptyParameters, this._jsonObject100);
        }

        /// <summary>
        /// Where   Using a FacetQueryResult instance
        /// When    Invoking the method "Execute"
        /// With    Using 500 itens in raw text
        /// </summary>
        [Benchmark]
        public void With500Parameters()
        {
            this._facetQueryResult10.Execute(this._emptyParameters, this._jsonObject500);
        }

        /// <summary>
        /// Where   Using a FacetQueryResult instance
        /// When    Invoking the method "Execute"
        /// With    Using 1000 itens in raw text
        /// </summary>
        [Benchmark]
        public void With1000Parameters()
        {
            this._facetQueryResult10.Execute(this._emptyParameters, this._jsonObject1000);
        }
    }
}
