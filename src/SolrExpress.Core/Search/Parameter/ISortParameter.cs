using System;
using System.Linq.Expressions;

namespace SolrExpress.Core.Search.Parameter
{
    /// <summary>
    /// Signatures to use in sort parameter
    /// </summary>
    public interface ISortParameter<TDocument> : ISearchParameter
        where TDocument : IDocument
    {
        /// <summary>
        /// Configure current instance
        /// </summary>
        /// <param name="expression">Expression used to find the property name</param>
        /// <param name="ascendent">True to ascendent order, otherwise false</param>
        ISortParameter<TDocument> Configure(Expression<Func<TDocument, object>> expression, bool ascendent);

        /// <summary>
        /// Expression used to find the property name
        /// </summary>
        Expression<Func<TDocument, object>> Expression { get; }

        /// <summary>
        /// True to ascendent order, otherwise false
        /// </summary>
        bool Ascendent { get; }
    }
}
