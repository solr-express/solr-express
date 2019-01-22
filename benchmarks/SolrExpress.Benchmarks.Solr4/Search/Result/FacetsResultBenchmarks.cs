using BenchmarkDotNet.Attributes;
using Newtonsoft.Json;
using SolrExpress.Benchmarks.Helper;
using SolrExpress.Builder;
using SolrExpress.Options;
using SolrExpress.Search.Parameter;
using SolrExpress.Search.Result;
using SolrExpress.Solr4.Search.Parameter;
using SolrExpress.Solr4.Search.Result;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace SolrExpress.Benchmarks.Solr4.Search.Result
{
    public class FacetsResultBenchmarks : IDisposable
    {
        private List<ISearchParameter> _searchParameters;
        private Stream _jsonStream;
        private IFacetsResult<TestDocument> _result;

        [Params("Field", "Range", "Query")]
        public string FacetTypes { get; set; }

        [Params(10, 100, 500, 1000)]
        public int ElementsCount { get; set; }

        [GlobalSetup]
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
            var jsonPlainText = EmbeddedResourceHelper.GetByName(assembly, $"SolrExpress.Benchmarks.Solr4.Search.Result.FacetsResultBenchmarks_{this.FacetTypes}{this.ElementsCount}.json");
            this._jsonStream = new MemoryStream(Encoding.GetEncoding(0).GetBytes(jsonPlainText));
        }

        /// <summary>
        /// Where   Using a FacetFieldResult instance
        /// When    Invoking the method "Execute"
        /// With    Using 1000 itens in raw text
        /// </summary>
        [Benchmark(Description = "Solr4.Search.Result.FacetsResult")]
        public void Execute()
        {
            var jsonReader = new JsonTextReader(new StreamReader(this._jsonStream));

            while (jsonReader.Read())
            {
                this._result.Execute(
                    this._searchParameters,
                    jsonReader.TokenType,
                    jsonReader.Path,
                    jsonReader);
            }
        }

        public void Dispose()
        {
            this._jsonStream.Dispose();
        }
    }
}
