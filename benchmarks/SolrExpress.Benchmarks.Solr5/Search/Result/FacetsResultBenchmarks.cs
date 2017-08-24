using BenchmarkDotNet.Attributes;
using Newtonsoft.Json;
using SolrExpress.Benchmarks.Helper;
using SolrExpress.Builder;
using SolrExpress.Search.Parameter;
using SolrExpress.Search.Result;
using SolrExpress.Solr5.Search.Parameter;
using SolrExpress.Solr5.Search.Result;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace SolrExpress.Benchmarks.Solr5.Search.Result
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

            var facetField1 = (IFacetFieldParameter<TestDocument>)new FacetFieldParameter<TestDocument>(expressionBuilder, null);
            facetField1.FieldExpression = (field) => field.About;
            var facetField2 = (IFacetFieldParameter<TestDocument>)new FacetFieldParameter<TestDocument>(expressionBuilder, null);
            facetField2.FieldExpression = (field) => field.Address;
            var facetField3 = (IFacetFieldParameter<TestDocument>)new FacetFieldParameter<TestDocument>(expressionBuilder, null);
            facetField3.FieldExpression = (field) => field.Age;
            var facetField4 = (IFacetFieldParameter<TestDocument>)new FacetFieldParameter<TestDocument>(expressionBuilder, null);
            facetField4.FieldExpression = (field) => field.Balance;
            var facetField5 = (IFacetFieldParameter<TestDocument>)new FacetFieldParameter<TestDocument>(expressionBuilder, null);
            facetField5.FieldExpression = (field) => field.Company;
            var facetField6 = (IFacetFieldParameter<TestDocument>)new FacetFieldParameter<TestDocument>(expressionBuilder, null);
            facetField6.FieldExpression = (field) => field.Email;
            var facetField7 = (IFacetFieldParameter<TestDocument>)new FacetFieldParameter<TestDocument>(expressionBuilder, null);
            facetField7.FieldExpression = (field) => field.EyeColor;
            var facetField8 = (IFacetFieldParameter<TestDocument>)new FacetFieldParameter<TestDocument>(expressionBuilder, null);
            facetField8.FieldExpression = (field) => field.FavoriteFruit;
            var facetField9 = (IFacetFieldParameter<TestDocument>)new FacetFieldParameter<TestDocument>(expressionBuilder, null);
            facetField9.FieldExpression = (field) => field.Gender;
            var facetField10 = (IFacetFieldParameter<TestDocument>)new FacetFieldParameter<TestDocument>(expressionBuilder, null);
            facetField10.FieldExpression = (field) => field.Greeting;
            var facetField11 = (IFacetFieldParameter<TestDocument>)new FacetFieldParameter<TestDocument>(expressionBuilder, null);
            facetField11.FieldExpression = (field) => field.IsActive;
            var facetField12 = (IFacetFieldParameter<TestDocument>)new FacetFieldParameter<TestDocument>(expressionBuilder, null);
            facetField12.FieldExpression = (field) => field.Latitude;
            var facetField13 = (IFacetFieldParameter<TestDocument>)new FacetFieldParameter<TestDocument>(expressionBuilder, null);
            facetField13.FieldExpression = (field) => field.Longitude;
            var facetField14 = (IFacetFieldParameter<TestDocument>)new FacetFieldParameter<TestDocument>(expressionBuilder, null);
            facetField14.FieldExpression = (field) => field.Name;
            var facetField15 = (IFacetFieldParameter<TestDocument>)new FacetFieldParameter<TestDocument>(expressionBuilder, null);
            facetField15.FieldExpression = (field) => field.Phone;
            var facetField16 = (IFacetFieldParameter<TestDocument>)new FacetFieldParameter<TestDocument>(expressionBuilder, null);
            facetField16.FieldExpression = (field) => field.Registered;
            var facetField17 = (IFacetFieldParameter<TestDocument>)new FacetFieldParameter<TestDocument>(expressionBuilder, null);
            facetField17.FieldExpression = (field) => field.Score;

            var facetRange1 = (IFacetRangeParameter<TestDocument>)new FacetRangeParameter<TestDocument>(expressionBuilder, null);
            facetRange1.FieldExpression = (field) => field.Age;
            facetRange1.AliasName = "facetRange";

            this._searchParameters = new List<ISearchParameter>
            {
                facetField1,
                facetField2,
                facetField3,
                facetField4,
                facetField5,
                facetField6,
                facetField7,
                facetField8,
                facetField9,
                facetField10,
                facetField11,
                facetField12,
                facetField13,
                facetField14,
                facetField15,
                facetField16,
                facetField17,
                facetRange1
            };

            for (int i = 0; i < this.ElementsCount; i++)
            {
                var facetQuery = (IFacetQueryParameter<TestDocument>)new FacetQueryParameter<TestDocument>(null);
                facetQuery.AliasName = $"VaLUE{i}";

                _searchParameters.Add(facetQuery);
            }
            
            this._result = new FacetsResult<TestDocument>();

            // Data using http://www.json-generator.com/
            var assembly = typeof(FacetsResultBenchmarks).GetTypeInfo().Assembly;
            this._jsonPlainText = EmbeddedResourceHelper.GetByName(assembly, $"SolrExpress.Benchmarks.Solr5.Search.Result.FacetsResultBenchmarks_{this.FacetTypes}{this.ElementsCount}.json");
        }

        /// <summary>
        /// Where   Using a FacetFieldResult instance
        /// When    Invoking the method "Execute"
        /// With    Using 1000 itens in raw text
        /// </summary>
        [Benchmark(Description = "Solr5.Search.Result.FacetsResult")]
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
