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

            PropertyInfo propertyInfo;
            MemberExpression memberExpression;

            switch (lambda.Body.NodeType)
            {
                case ExpressionType.Convert:
                    var unaryExpression = (UnaryExpression)lambda.Body;

                    memberExpression = (MemberExpression)unaryExpression.Operand;

                    propertyInfo = memberExpression.Member as PropertyInfo;
                    if (propertyInfo == null)
                    {
                        throw new InvalidOperationException("Expression must be a property reference.");
                    }

                    return propertyInfo.Name;
                case ExpressionType.MemberAccess:
                    memberExpression = (MemberExpression)lambda.Body;

                    propertyInfo = memberExpression.Member as PropertyInfo;
                    if (propertyInfo == null)
                    {
                        throw new InvalidOperationException("Expression must be a property reference.");
                    }

                    return propertyInfo.Name;
            }

            throw new InvalidOperationException("Unknown to resolve the expression");
        }

        #endregion Private methods
    }
}
