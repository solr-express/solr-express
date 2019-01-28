using BenchmarkDotNet.Attributes;
using Newtonsoft.Json;
using SolrExpress.Benchmarks.Helper;
using SolrExpress.Search.Parameter;
using SolrExpress.Search.Result;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace SolrExpress.Benchmarks.Solr4.Search.Result
{
    public class DocumentResultBenchmarks : IDisposable
    {
        private List<ISearchParameter> _searchParameters;
        private Stream _jsonStream;
        private IDocumentResult<TestDocument> _result;

        [Params(10, 100, 500, 1000)]
        public int ElementsCount { get; set; }

        [GlobalSetup]
        public void Setup()
        {
            this._searchParameters = new List<ISearchParameter>();

            this._result = new DocumentResult<TestDocument>();

            // Data using http://www.json-generator.com/
            var assembly = typeof(DocumentResultBenchmarks).GetTypeInfo().Assembly;
            var jsonPlainText = EmbeddedResourceHelper.GetByName(assembly, $"SolrExpress.Benchmarks.Solr4.Search.Result.DocumentResultBenchmarks{this.ElementsCount}.json");
            this._jsonStream = new MemoryStream(Encoding.GetEncoding(0).GetBytes(jsonPlainText));
        }

        [Benchmark(Description = "Solr4.Search.Result.DocumentResult")]
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
