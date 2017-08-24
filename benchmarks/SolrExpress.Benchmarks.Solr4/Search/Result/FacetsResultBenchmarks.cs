using BenchmarkDotNet.Attributes;
using Newtonsoft.Json;
using SolrExpress.Benchmarks.Helper;
using SolrExpress.Builder;
using SolrExpress.Search.Parameter;
using SolrExpress.Search.Result;
using SolrExpress.Solr4.Search.Parameter;
using SolrExpress.Solr4.Search.Result;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using SolrExpress.Options;

namespace SolrExpress.Benchmarks.Solr4.Search.Result
{
    public class FacetsResultBenchmarks
    {
        private List<ISearchParameter> _searchParameters;
        private string _jsonPlainText;
        private IFacetsResult<TestDocument> _result;

        [Params("Field", "Range", "Query")]
        public string FacetTypes { get; set; }

        [Params(10, 100, 500, 1000)]
        public int ElementsCount { get; set; }

#if CORE
        [GlobalSetup]
#else
        [Setup]
#endif
        public void Setup()
        {
            var solrExpressOptions = new SolrExpressOptions();
            var solrConnection = new FakeSolrConnection<TestDocument>();
            var expressionBuilder = new ExpressionBuilder<TestDocument>(solrExpressOptions, solrConnection);
            expressionBuilder.LoadDocument();

            var facetRange1 = (IFacetRangeParameter<TestDocument>)new FacetRangeParameter<TestDocument>(expressionBuilder, null);
            facetRange1.FieldExpression = field => field.Age;
            facetRange1.AliasName = "facetRange";

            this._searchParameters = new List<ISearchParameter>
            {
                facetRange1
            };

            this._result = new FacetsResult<TestDocument>();

            // Data using http://www.json-generator.com/
            var assembly = typeof(FacetsResultBenchmarks).GetTypeInfo().Assembly;
            this._jsonPlainText = EmbeddedResourceHelper.GetByName(assembly, $"SolrExpress.Benchmarks.Solr4.Search.Result.FacetsResultBenchmarks_{this.FacetTypes}{this.ElementsCount}.json");
        }

        /// <summary>
        /// Where   Using a FacetFieldResult instance
        /// When    Invoking the method "Execute"
        /// With    Using 1000 itens in raw text
        /// </summary>
        [Benchmark(Description = "Solr4.Search.Result.FacetsResult")]
        public void Execute()
        {
            var jsonReader = new JsonTextReader(new StringReader(this._jsonPlainText));

            while (jsonReader.Read())
            {
                this._result.Execute(
                    this._searchParameters,
                    jsonReader.TokenType,
                    jsonReader.Path,
                    jsonReader);
            }
        }
    }
}
