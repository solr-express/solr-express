using SolrExpress.Builder;
using System;
using System.Linq.Expressions;

namespace SolrExpress.Search
{
    /// <summary>
    /// Signatures to indicate use of property FieldExpression
    /// </summary>
    public interface ISearchItemFieldExpression<TDocument>
        where TDocument : Document
    {
        /// <summary>
        /// Build expressions engine
        /// </summary>
        ExpressionBuilder<TDocument> ExpressionBuilder { get; set; }

        /// <summary>
        /// Expression used to find field name
        /// </summary>
        Expression<Func<TDocument, object>> FieldExpression { get; set; }
    }
}
