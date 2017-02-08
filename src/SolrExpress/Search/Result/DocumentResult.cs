using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
        where TDocument : IDocument
    {
        private bool executed = false;

        IEnumerable<TDocument> IDocumentResult<TDocument>.Data { get; set; }

        void ISearchResult.Execute(List<ISearchParameter> searchParameters, JsonToken currentToken, string currentPath, JsonReader jsonReader)
        {
            if (!this.executed && currentToken == JsonToken.StartArray && currentPath == "response.docs")
            {
                var jsonSerializer = new JsonSerializer();
                jsonSerializer.Converters.Add(new GeoCoordinateConverter());
                jsonSerializer.Converters.Add(new DateTimeConverter());
                jsonSerializer.ContractResolver = new CustomContractResolver();

                ((IDocumentResult<TDocument>)this).Data = JArray.Load(jsonReader).ToObject<List<TDocument>>(jsonSerializer);

                this.executed = true;
            }
        }
    }
}
