using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SolrExpress.Configuration;
using SolrExpress.Search.Parameter;
using SolrExpress.Serialization;
using System.Collections.Generic;

namespace SolrExpress.Search.Result
{
    /// <summary>
    /// Document data builder
    /// </summary>
    /// <typeparam name="TDocument">Type of the document returned in the search</typeparam>
    public sealed class DocumentResult<TDocument> : IDocumentResult<TDocument>
        where TDocument : Document
    {
        private bool _executed;
        private readonly SolrDocumentConfiguration<TDocument> _configuration;

        public DocumentResult(SolrDocumentConfiguration<TDocument> configuration)
        {
            this._configuration = configuration;
        }

        public IEnumerable<TDocument> Data { get; private set; }

        public void Execute(IList<ISearchParameter> searchParameters, JsonToken currentToken, string currentPath, JsonReader jsonReader)
        {
            if (this._executed || currentToken != JsonToken.StartArray || currentPath != "response.docs")
            {
                return;
            }

            var jsonSerializer = new JsonSerializer();
            jsonSerializer.Converters.Add(new GeoCoordinateConverter());
            jsonSerializer.Converters.Add(new DateTimeConverter());
            jsonSerializer.ContractResolver = new CustomContractResolver<TDocument>(this._configuration);

            this.Data = JArray.Load(jsonReader).ToObject<List<TDocument>>(jsonSerializer);

            this._executed = true;
        }
    }
}
