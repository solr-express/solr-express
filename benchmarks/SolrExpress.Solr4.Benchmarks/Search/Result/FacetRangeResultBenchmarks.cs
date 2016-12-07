using BenchmarkDotNet.Attributes;
using Newtonsoft.Json.Linq;
using SolrExpress.Benchmarks.Helper;
using SolrExpress.Core.Search;
using SolrExpress.Core.Search.Result;
using SolrExpress.Core.Utility;
using SolrExpress.Solr4.Search.Parameter;
using SolrExpress.Solr4.Search.Result;
using System.Collections.Generic;
using System.Reflection;

namespace SolrExpress.Benchmarks.Solr4.Search.Result
{
    public class FacetRangeResultBenchmarks
    {
        private List<ISearchParameter> _parameters;
        private JObject _jsonObject;
        private IConvertJsonObject _facetRangeResult;

        [Params(10, 100, 500, 1000)]
        public int ElementsCount { get; set; }

        [Setup]
        public void Setup()
        {
            var expressionCache = new ExpressionCache<TestDocument>();
            var expressionBuilder = (IExpressionBuilder<TestDocument>)new ExpressionBuilder<TestDocument>(expressionCache);

            this._parameters = new List<ISearchParameter> {
                new FacetRangeParameter<TestDocument>(expressionBuilder).Configure("facetRange", q=> q.Age, "10", "10", "100")
            };

            this._facetRangeResult = new FacetRangeResult<TestDocument>(expressionBuilder);
            
            // Data using http://www.json-generator.com/
            var assembly = typeof(FacetRangeResultBenchmarks).GetTypeInfo().Assembly;
            var str = EmbeddedResourceHelper.GetByName(assembly, $"SolrExpress.Benchmarks.Solr4.Search.Result.FacetRangeResultBenchmarks{this.ElementsCount}.json");
            
            this._jsonObject = JObject.Parse(str);
        }

        [Benchmark]
        public void Execute()
        {
            this._facetRangeResult.Execute(this._parameters, this._jsonObject);
        }
    }
}
