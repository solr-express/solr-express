using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace SolrExpress.Core.Search.Result
{
    /// <summary>
    /// Base interface used to parse the SOLR results when a json object is necessary
    /// </summary>
    public interface IConvertJsonObject<TDocument>
        where TDocument : IDocument
    {
        /// <summary>
        /// Execute the parse of the JSON object
        /// </summary>
        /// <param name="parameters">List of the parameters arranged in the queryable class</param>
        /// <param name="jsonObject">JSON object used in the parse</param>
        void Execute(IEnumerable<ISearchParameter<TDocument>> parameters, JObject jsonObject);
    }
}
