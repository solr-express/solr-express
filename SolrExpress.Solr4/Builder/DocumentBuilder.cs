using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SolrExpress.Core.Exception;
using SolrExpress.Core.Json;
using SolrExpress.Core.Query;

namespace SolrExpress.Solr4.Builder
{
    /// <summary>
    /// Document data builder
    /// </summary>
    /// <typeparam name="TDocument">Type of the document returned in the search</typeparam>
    public sealed class DocumentBuilder<TDocument> : IResultBuilder, IConvertJsonObject
        where TDocument : IDocument
    {
        /// <summary>
        /// Execute the JSON object parse in the list of informed document
        /// </summary>
        /// <param name="jsonObject">JSON object used in the parse</param>
        public void Execute(JObject jsonObject)
        {
            //Sugestão do resharper: if (jsonObject["response"]?["docs"] == null)  bacana, não?
            if ((jsonObject["response"] == null) || (jsonObject["response"]["docs"] == null))
            {
                throw new UnexpectedJsonFormatException(jsonObject.ToString());
            }

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
