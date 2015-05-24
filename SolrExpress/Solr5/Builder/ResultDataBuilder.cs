using Newtonsoft.Json.Linq;
using SolrExpress.QueryBuilder;
using System.Collections.Generic;

namespace SolrExpress.Solr5.Builder
{
    /// <summary>
    /// Result data builder
    /// </summary>
    /// <typeparam name="TDocument">Type of the document returned in the search</typeparam>
    public class ResultDataBuilder<TDocument> : IResultDataBuilder<TDocument>
        where TDocument : IDocument
    {
        /// <summary>
        /// Execute the parse of the JSON object in the list of informed document
        /// </summary>
        /// <param name="jsonObject">JSON object used in the parse</param>
        /// <returns>List of informed document</returns>
        public List<TDocument> Execute(JObject jsonObject)
        {
            if ((jsonObject["response"] != null) && (jsonObject["response"]["docs"] != null))
            {
                return jsonObject["response"]["docs"].ToObject<List<TDocument>>();
            }

            throw new Exception.UnexpectedJsonFormatException(jsonObject.ToString());
        }
    }
}
