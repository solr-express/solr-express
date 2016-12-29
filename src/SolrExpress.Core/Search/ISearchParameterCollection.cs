using SolrExpress.Core.Utility;
using System.Collections.Generic;

namespace SolrExpress.Core.Search
{
    /// <summary>
    /// Signature used to parameter collection
    /// </summary>
    public interface ISearchParameterCollection<TDocument>
        where TDocument : IDocument
    {
        /// <summary>
        /// Add a parameter to the query
        /// </summary>
        /// <param name="parameters">The parameter to add in the query</param>
        /// <returns>Itself</returns>
        void Add(IEnumerable<ISearchParameter<TDocument>> parameters);

        /// <summary>
        /// Execute parameters and get query instructions
        /// </summary>
        /// <returns>Query instructions</returns>
        string Execute();

        /// <summary>
        /// Expressions builder
        /// </summary>
        IExpressionBuilder<TDocument> ExpressionBuilder { get; set; }
    }
}
