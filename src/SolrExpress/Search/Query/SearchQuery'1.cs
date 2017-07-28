using SolrExpress.Builder;
using System;
using System.Linq.Expressions;

namespace SolrExpress.Search.Query
{
    /// <summary>
    /// Solr queries container with TDocument link
    /// </summary>
    public class SearchQuery<TDocument> : SearchQuery
        where TDocument : Document
    {
        private readonly ExpressionBuilder<TDocument> _expressionBuilder;

        public SearchQuery(ExpressionBuilder<TDocument> expressionBuilder)
        {
            this._expressionBuilder = expressionBuilder;
        }

        /// <summary>
        /// Add field name into expression stack
        /// </summary>
        /// <param name="fieldExpression">Expressions used to find field name</param>
        /// <returns>It self</returns>
        internal SearchQuery AddField(Expression<Func<TDocument, object>> fieldExpression)
        {
            this._InternalQuery.Append(this._expressionBuilder.GetFieldName(fieldExpression));

            this._InternalLinkValue = ":";

            return this;
        }
    }
}
