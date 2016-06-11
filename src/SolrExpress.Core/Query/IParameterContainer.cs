using SolrExpress.Core.Query.Parameter;
using System.Collections.Generic;

namespace SolrExpress.Core.Query
{
    /// <summary>
    /// Signature used to contain parameters
    /// </summary>
    public interface IParameterContainer
    {
        /// <summary>
        /// Add a parameter to the query
        /// </summary>
        /// <param name="parameters">The parameter to add in the query</param>
        /// <returns>Itself</returns>
        void AddParameters(List<IParameter> parameters);

        /// <summary>
        /// Execute parameters and get query instructions
        /// </summary>
        /// <returns>Query instructions</returns>
        string Execute();
    }
}
