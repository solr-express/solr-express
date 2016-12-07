using System;
using System.Linq.Expressions;
using System.Reflection;

namespace SolrExpress.Core.Utility
{
    public interface IExpressionBuilder<TDocument>
        where TDocument : IDocument
    {
        /// <summary>
        /// Returns the property name of the indicated expression
        /// </summary>
        /// <typeparam name="T">Type of the document used in the query</typeparam>
        /// <param name="expression">Expression used to find the property name</param>
        /// <returns>Property name indicated in the expression</returns>
        void GetInfosFromExpression(Expression<Func<TDocument, object>> expression, out PropertyInfo propertyInfo, out SolrFieldAttribute solrFieldAttribute);

        /// <summary>
        /// Returns the property name of the indicated expression
        /// </summary>
        /// <typeparam name="TDocument">Type of the document used in the query</typeparam>
        /// <param name="expression">Expression used to find the property name</param>
        /// <returns>Property name indicated in the expression</returns>
        string GetFieldNameFromExpression(Expression<Func<TDocument, object>> expression);

        /// <summary>
        /// Returns the SolrFieldAttribute associated with the informed property
        /// </summary>
        /// <typeparam name="T">Type of the document used in the query</typeparam>
        /// <param name="expression">Expression used to find the property name</param>
        /// <returns>SolrFieldAttribute associated4 with the informed property, otherwise null</returns>
        SolrFieldAttribute GetSolrFieldAttributeFromPropertyInfo(Expression<Func<TDocument, object>> expression);

        /// <summary>
        /// Returns the property name of the indicated expression
        /// </summary>
        /// <typeparam name="TDocument">Type of the document used in the query</typeparam>
        /// <param name="expression">Expression used to find the property name</param>
        /// <returns>Property name indicated in the expression</returns>
        string GetPropertyNameFromExpression(Expression<Func<TDocument, object>> expression);

        /// <summary>
        /// Returns the property name of the indicated expression
        /// </summary>
        /// <typeparam name="TDocument">Type of the document used in the query</typeparam>
        /// <param name="expression">Expression used to find the property name</param>
        /// <returns>Property name indicated in the expression</returns>
        Type GetPropertyTypeFromExpression(Expression<Func<TDocument, object>> expression);
    }
}
