using SolrExpress.QueryBuilder;
using System;
using System.Linq.Expressions;
using System.Reflection;

namespace SolrExpress.Helper
{
    /// <summary>
    /// Helper class use in the SOLR Query core
    /// </summary>
    internal static class UtilHelper
    {
        #region Private methods

        /// <summary>
        /// Returns the property name of the indicated expression
        /// </summary>
        /// <typeparam name="T">Type of the document used in the query</typeparam>
        /// <param name="expression">Expression used to find the property name</param>
        /// <returns>Property name indicated in the expression</returns>
        internal static string GetPropertyNameFromExpression<T>(Expression<Func<T, object>> expression)
            where T : IDocument
        {
            var lambda = (LambdaExpression)expression;

            if (lambda.Body.NodeType != ExpressionType.MemberAccess)
            {
                throw new InvalidOperationException("Expression must be a MemberExpression");
            }

            var memberExpression = (MemberExpression)lambda.Body;

            var propertyInfo = memberExpression.Member as PropertyInfo;
            if (propertyInfo == null)
            {
                throw new InvalidOperationException("Expression must be a property reference.");
            }

            return propertyInfo.Name;
        }

        #endregion Private methods
    }
}
