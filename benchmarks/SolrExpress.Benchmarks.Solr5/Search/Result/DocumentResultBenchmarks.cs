using BenchmarkDotNet.Attributes;
using Newtonsoft.Json;
using SolrExpress.Benchmarks.Helper;
using SolrExpress.Search.Parameter;
using SolrExpress.Search.Result;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace SolrExpress.Benchmarks.Solr5.Search.Result
{
    public class DocumentResultBenchmarks
    {
        private List<ISearchParameter> _searchParameters;
        private string _jsonPlainText;
        private IDocumentResult<TestDocument> _result;

        [Params(10, 100, 500, 1000)]
        public int ElementsCount { get; set; }

#if CORE
        [GlobalSetup]
#else
        [Setup]
#endif
        public void Setup()
        {
            this._searchParameters = new List<ISearchParameter>();

            this._result = new DocumentResult<TestDocument>();

            // Data using http://www.json-generator.com/
            var assembly = typeof(DocumentResultBenchmarks).GetTypeInfo().Assembly;
            this._jsonPlainText = EmbeddedResourceHelper.GetByName(assembly, $"SolrExpress.Benchmarks.Solr5.Search.Result.DocumentResultBenchmarks{this.ElementsCount}.json");
        }

        [Benchmark(Description = "Solr5.Search.Result.DocumentResult")]
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
