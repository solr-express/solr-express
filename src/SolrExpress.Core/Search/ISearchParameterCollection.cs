using System.Collections.Generic;

namespace SolrExpress.Core.Search
{
    /// <summary>
    /// Signature used to parameter collection
    /// </summary>
    public interface ISearchParameterCollection
    {
        /// <summary>
        /// Add a parameter to the query
        /// </summary>
        /// <param name="parameters">The parameter to add in the query</param>
        /// <returns>Itself</returns>
        void Add(IEnumerable<ISearchParameter> parameters);

        /// <summary>
        /// Execute parameters and get query instructions
        /// </summary>
        /// <returns>Query instructions</returns>
        string Execute();
    }
}
