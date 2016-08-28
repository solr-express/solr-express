using System.Collections.Generic;

namespace SolrExpress.Core.Search.Result
{
    /// <summary>
    /// Base interface used to parse the SOLR results when a json string is necessary
    /// </summary>
    public interface IConvertJsonPlainText
    {
        /// <summary>
        /// Execute the parse of the JSON string
        /// </summary>
        /// <param name="parameters">List of the parameters arranged in the queryable class</param>
        /// <param name="jsonPlainText">JSON string used in the parse</param>
        void Execute(List<ISearchParameter> parameters, string jsonPlainText);
    }
}
