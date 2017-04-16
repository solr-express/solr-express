using System;
using System.Linq.Expressions;

namespace SolrExpress.Utility
{
    /// <summary>
    /// Build expressions class
    /// </summary>
    /// <typeparam name="TDocument"></typeparam>
    public sealed class ExpressionBuilder<TDocument>
        where TDocument : IDocument
    {
        /// <summary>
        /// Returns if extracted property from indicated expression is indexed (in SOLR) or not
        /// </summary>
        /// <param name="expression">Expression used to find property features</param>
        /// <returns>True to indexed (in SOLR), otherwise false</returns>
        public bool IsIndexed(Expression<Func<TDocument, object>> expression)
        {
            //TODO: To implement
            return false;
        }

        /// <summary>
        /// Returns if extracted property from indicated expression is stored (in SOLR) or not
        /// </summary>
        /// <param name="expression">Expression used to find property features</param>
        /// <returns>True to stored (in SOLR), otherwise false</returns>
        public bool IsStored(Expression<Func<TDocument, object>> expression)
        {
            //TODO: To implement
            return false;
        }

        /// <summary>
        /// Returns property name of indicated expression
        /// </summary>
        /// <param name="expression">Expression used to find property name</param>
        /// <returns>Property name indicated in expression</returns>
        public string GetFieldName(Expression<Func<TDocument, object>> expression)
        {
            //TODO: To implement
            return string.Empty;
        }

        /// <summary>
        /// Returns the property name of the indicated expression
        /// </summary>
        /// <param name="expression">Expression used to find the property name</param>
        /// <returns>Property name indicated in the expression</returns>
        public string GetPropertyName(Expression<Func<TDocument, object>> fieldExpression)
        {
            //TODO: To implement
            return string.Empty;
        }

        /// <summary>
        /// Returns the property name of the indicated expression
        /// </summary>
        /// <param name="expression">Expression used to find the property name</param>
        /// <returns>Property name indicated in the expression</returns>
        public Type GetPropertyType(Expression<Func<TDocument, object>> expression)
        {
            //TODO: To implement
            return null;
        }
    }
}
