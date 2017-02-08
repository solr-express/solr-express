using Newtonsoft.Json;
using SolrExpress.Search.Parameter;
using System.Collections.Generic;

namespace SolrExpress.Search.Result
{
    /// <summary>
    /// Base interface used to parse the SOLR results
    /// </summary>
    public interface ISearchResult
    {
        /// <summary>
        /// Execute parse of the JSON string
        /// </summary>
        /// <param name="searchParameters">List of parameters arranged in queryable class</param>
        /// <param name="currentPath">Current object path used in parse</param>
        /// <param name="currentToken">Current JSON token used in parse</param>
        /// <param name="jsonReader">JSON reader used in parse</param>
        void Execute(List<ISearchParameter> searchParameters, JsonToken currentToken, string currentPath, JsonReader jsonReader);
    }
}
