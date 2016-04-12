using SolrExpress.Core.Parameter;
using System.Collections.Generic;

namespace SolrExpress.Core
{
    /// <summary>
    /// Signatures of SOLR access provider
    /// </summary>
    public interface IProvider
    {
        /// <summary>
        /// Execute the parameters and return the formed solr query
        /// </summary>
        /// <param name="parameters">List of the parameters arranged in the queryable class</param>
        /// <returns>Solr query</returns>
        string GetQuery(List<IParameter> parameters);

        /// <summary>
        /// Execute the informated uri and return the result of the request
        /// </summary>
        /// <summary>
        /// Execute the informated uri and return the result of the request
        /// </summary>
        /// <param name="handler">Handler name used in solr request</param>
        /// <param name="query">Solr query</param>
        /// <returns>Result of the request</returns>
        string Execute(string handler, string query);
    }
}
