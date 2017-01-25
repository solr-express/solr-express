using System;
using System.Linq.Expressions;

namespace SolrExpress.Search.Parameter
{
    /// <summary>
    /// Signatures to use in sort parameter
    /// </summary>
    public interface ISortParameter<TDocument> : ISearchParameter
        where TDocument : IDocument
    {
        /// <summary>
        /// Configure expression used to find field name
        /// </summary>
        /// <param name="fieldExpression">Expression used to find field name</param>
        ISortParameter<TDocument> FieldExpression(Expression<Func<TDocument, object>> fieldExpression);

        /// <summary>
        /// Configure true to ascendent order, otherwise false
        /// </summary>
        /// <param name="ascendent">True to ascendent order, otherwise false</param>
        ISortParameter<TDocument> Ascendent(bool ascendent);
    }
}
