using SolrExpress.Builder;
using System;
using System.Linq.Expressions;

namespace SolrExpress.Search
{
    public interface ISearchItemFieldExpressions<TDocument>
        where TDocument : IDocument
    {
        /// <summary>
        /// Build expressions engine
        /// </summary>
        ExpressionBuilder<TDocument> ExpressionBuilder { get; set; }

        /// <summary>
        /// Expressions used to find fields name
        /// </summary>
        Expression<Func<TDocument, object>>[] FieldExpressions { get; set; }
    }
}
