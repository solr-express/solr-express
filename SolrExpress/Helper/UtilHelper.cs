using SolrExpress.Query;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using SolrExpress.Attribute;

namespace SolrExpress.Helper
{
    /// <summary>
    /// Helper class use in the SOLR Query core
    /// </summary>
    internal static class UtilHelper
    {
        /// <summary>
        /// Returns the SolrFieldAtribute associete with the informed property
        /// </summary>
        /// <param name="propertyInfo">Property information to find the attribute</param>
        /// <returns>SolrFieldAtribute associete with the informed property, otherwise null</returns>
        private static SolrFieldAtribute GetSolrFieldAtributeFrompropertyInfo(PropertyInfo propertyInfo)
        {
            var attrs = propertyInfo.GetCustomAttributes(true);
            return (SolrFieldAtribute)attrs.FirstOrDefault(q => q is SolrFieldAtribute);
        }

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
            SolrFieldAtribute solrFieldAtribute;

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

                    solrFieldAtribute = UtilHelper.GetSolrFieldAtributeFrompropertyInfo(propertyInfo);

                    return solrFieldAtribute == null ? propertyInfo.Name : solrFieldAtribute.Label;
                case ExpressionType.MemberAccess:
                    memberExpression = (MemberExpression)lambda.Body;

                    propertyInfo = memberExpression.Member as PropertyInfo;
                    if (propertyInfo == null)
                    {
                        throw new InvalidOperationException("Expression must be a property reference.");
                    }

                    solrFieldAtribute = UtilHelper.GetSolrFieldAtributeFrompropertyInfo(propertyInfo);

                    return solrFieldAtribute == null ? propertyInfo.Name : solrFieldAtribute.Label;
            }

            throw new InvalidOperationException("Unknown to resolve the expression");
        }
    }
}
