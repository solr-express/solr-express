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
        private JObject _jsonObject10;
        private JObject _jsonObject100;
        private JObject _jsonObject500;
        private JObject _jsonObject1000;
        private FacetRangeResult<TestDocument> _facetRangeResult10;
        private FacetRangeResult<TestDocument> _facetRangeResult100;
        private FacetRangeResult<TestDocument> _facetRangeResult500;
        private FacetRangeResult<TestDocument> _facetRangeResult1000;

        [Setup]
        public void Setup()
        {
            this._parameters = new List<IParameter> {
                new FacetRangeParameter<TestDocument>().Configure("facetRange", q=> q.Age, "10", "10", "100")
            };

            this._facetRangeResult10 = new FacetRangeResult<TestDocument>();
            this._facetRangeResult100 = new FacetRangeResult<TestDocument>();
            this._facetRangeResult500 = new FacetRangeResult<TestDocument>();
            this._facetRangeResult1000 = new FacetRangeResult<TestDocument>();

            // Data using http://www.json-generator.com/
            var assembly = typeof(FacetRangeResultBenchmarks).GetTypeInfo().Assembly;
            var str10 = EmbeddedResourceHelper.GetByName(assembly, "Solr4.Query.Result.FacetRangeResultBenchmarks10");
            var str100 = EmbeddedResourceHelper.GetByName(assembly, "Solr4.Query.Result.FacetRangeResultBenchmarks100");
            var str500 = EmbeddedResourceHelper.GetByName(assembly, "Solr4.Query.Result.FacetRangeResultBenchmarks500");
            var str1000 = EmbeddedResourceHelper.GetByName(assembly, "Solr4.Query.Result.FacetRangeResultBenchmarks1000");

            this._jsonObject10 = JObject.Parse(str10);
            this._jsonObject100 = JObject.Parse(str100);
            this._jsonObject500 = JObject.Parse(str500);
            this._jsonObject1000 = JObject.Parse(str1000);
        }

        /// <summary>
        /// Where   Using a FacetRangeResult instance
        /// When    Invoking the method "Execute"
        /// With    Using 10 itens in raw text
        /// </summary>
        [Benchmark(Baseline = true)]
        public void With10Parameters()
        {
            this._facetRangeResult10.Execute(this._parameters, this._jsonObject10);
        }

        /// <summary>
        /// Where   Using a FacetRangeResult instance
        /// When    Invoking the method "Execute"
        /// With    Using 100 itens in raw text
        /// </summary>
        [Benchmark]
        public void With100Parameters()
        {
            this._facetRangeResult10.Execute(this._parameters, this._jsonObject100);
        }

        /// <summary>
        /// Where   Using a FacetRangeResult instance
        /// When    Invoking the method "Execute"
        /// With    Using 500 itens in raw text
        /// </summary>
        [Benchmark]
        public void With500Parameters()
        {
            this._facetRangeResult10.Execute(this._parameters, this._jsonObject500);
        }

        /// <summary>
        /// Where   Using a FacetRangeResult instance
        /// When    Invoking the method "Execute"
        /// With    Using 1000 itens in raw text
        /// </summary>
        [Benchmark]
        public void With1000Parameters()
        {
            this._facetRangeResult10.Execute(this._parameters, this._jsonObject1000);
        }
    }
}
