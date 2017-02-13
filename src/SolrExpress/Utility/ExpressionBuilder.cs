using System;
using System.Linq.Expressions;

namespace SolrExpress.Utility
{
    /// <summary>
    /// Build expressions helper class
    /// </summary>
    /// <typeparam name="TDocument"></typeparam>
    public sealed class ExpressionBuilder<TDocument>
        where TDocument : IDocument
    {
        /// <summary>
        /// Returns property name of indicated expression
        /// </summary>
        /// <typeparam name="TDocument">Type of document used in query</typeparam>
        /// <param name="expression">Expression used to find property name</param>
        /// <returns>Property name indicated in expression</returns>
        public string GetFieldNameFromExpression(Expression<Func<TDocument, object>> expression)
        {
            //TODO: To implement
            return string.Empty;
        }

        public string GetPropertyNameFromExpression(Expression<Func<TDocument, object>> fieldExpression)
        {
            //TODO: To implement
            return string.Empty;
        }
    }
}
