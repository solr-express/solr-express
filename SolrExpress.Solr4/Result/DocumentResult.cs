using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SolrExpress.Core;
using SolrExpress.Core.Result;
using System.Collections.Generic;

namespace SolrExpress.Solr4.Result
{
    /// <summary>
    /// Document data builder
    /// </summary>
    /// <typeparam name="TDocument">Type of the document returned in the search</typeparam>
    public sealed class DocumentResult<TDocument> : IDocumentResult<TDocument>, IConvertJsonObject
        where TDocument : IDocument
    {
        /// <summary>
        /// Execute the JSON object parse in the list of informed document
        /// </summary>
        /// <param name="jsonObject">JSON object used in the parse</param>
        public void Execute(JObject jsonObject)
        {
            Checker.IsTrue<UnexpectedJsonFormatException>(jsonObject["response"]?["docs"] == null, jsonObject.ToString());

            var jsonSerializer = JsonSerializer.Create();
            jsonSerializer.Converters.Add(new GeoCoordinateConverter());
            jsonSerializer.ContractResolver = new CustomContractResolver();

            this.Data = jsonObject["response"]["docs"].ToObject<List<TDocument>>(jsonSerializer);
        }

        /// <summary>
        /// Documents of the search
        /// </summary>
        public List<TDocument> Data { get; private set; }
    }
}
