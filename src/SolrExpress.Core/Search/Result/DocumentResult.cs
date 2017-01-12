using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SolrExpress.Core.Serialization;
using SolrExpress.Core.Utility;
using System.Collections.Generic;

namespace SolrExpress.Core.Search.Result
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
        /// <param name="parameters">List of the parameters arranged in the queryable class</param>
        /// <param name="jsonObject">JSON object used in the parse</param>
        void IConvertJsonObject.Execute(IEnumerable<ISearchParameter> parameters, JObject jsonObject)
        {
            Checker.IsNull(parameters);
            Checker.IsTrue<UnexpectedJsonFormatException>(jsonObject["response"]?["docs"] == null, jsonObject.ToString());

            var jsonSerializer = JsonSerializer.Create();
            jsonSerializer.Converters.Add(new GeoCoordinateConverter());
            jsonSerializer.Converters.Add(new DateTimeConverter());
            jsonSerializer.ContractResolver = new CustomContractResolver();

            this.Data = jsonObject["response"]["docs"].ToObject<List<TDocument>>(jsonSerializer);
        }

        /// <summary>
        /// Documents of the search
        /// </summary>
        public IEnumerable<TDocument> Data { get; private set; }
    }
}
