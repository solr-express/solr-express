using System.Collections.Generic;

namespace SolrExpress.Core.Query
{
    /// <summary>
    /// Signatures of the SOLR access provider
    /// </summary>
    public interface IProvider
    {
        /// <summary>
        /// Execute the informated uri and return the result of the request
        /// </summary>
        /// <param name="parameters">List of the parameters arranged in the queryable class</param>
        /// <returns>Result of the request</returns>
        string Execute(List<IParameter> parameters);
    }
}
