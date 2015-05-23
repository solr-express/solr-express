using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace SolrExpress.QueryBuilder
{
    /// <summary>
    /// Signatures of the result data builder
    /// </summary>
    /// <typeparam name="TDocument">Type of the document returned in the search</typeparam>
    public interface IResultDataBuilder<TDocument>
        where TDocument : IDocument
    {
        /// <summary>
        /// Execute the parse of the JSON object in the list of informed document
        /// </summary>
        /// <param name="jsonObject">JSON object used in the parse</param>
        /// <returns>List of informed document</returns>
        List<TDocument> Execute(JObject jsonObject);
    }
}
