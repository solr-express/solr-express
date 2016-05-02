using SolrExpress.Core.Query.Parameter;
using SolrExpress.Core.Update;
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
        string GetQueryInstruction(List<IParameter> parameters);

        /// <summary>
        /// Execute the atomic update commands and return the formed solr query
        /// </summary>
        /// <param name="atomicUpdate">Atomic update to be executed</param>
        /// <param name="atomicDelete">Atomic delete to be executed</param>
        /// <returns>Solr query</returns>
        string GetAtomicUpdateInstruction(IAtomicUpdate atomicUpdate = null, IAtomicDelete atomicDelete = null);

        /// <summary>
        /// Execute the informated uri and return the result of the request
        /// </summary>
        /// <param name="handler">Handler name used in solr request</param>
        /// <param name="instruction">Solr query</param>
        /// <returns>Result of the request</returns>
        string Execute(string handler, string instruction);
    }
}
