using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace SolrExpress.Core.Extension.Internal
{
    /// <summary>
    /// Extension class used in Expression<Func<TDocument>> to manipulate IDocument
    /// </summary>
    internal static class ExpressionIDocumentExtension
    {
        /// <summary>
        /// Returns the property name of the indicated expression
        /// </summary>
        /// <typeparam name="T">Type of the document used in the query</typeparam>
        /// <param name="expression">Expression used to find the property name</param>
        /// <returns>Property name indicated in the expression</returns>
        private static PropertyInfo GetPropertyInfoFromExpression<TDocument>(this Expression<Func<TDocument, object>> expression)
            where TDocument : IDocument
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

                    Checker.IsNull(propertyInfo, Resource.ExpressionMustBePropertyException);

                    return propertyInfo;
                case ExpressionType.MemberAccess:
                    memberExpression = (MemberExpression)lambda.Body;

                    propertyInfo = memberExpression.Member as PropertyInfo;

                    Checker.IsNull(propertyInfo, Resource.ExpressionMustBePropertyException);

                    return propertyInfo;
            }

            throw new InvalidOperationException(Resource.UnknownToResolveExpressionException);
        }

        /// <summary>
        /// Returns the property name of the indicated expression
        /// </summary>
        /// <typeparam name="TDocument">Type of the document used in the query</typeparam>
        /// <param name="expression">Expression used to find the property name</param>
        /// <returns>Property name indicated in the expression</returns>
        internal static string GetFieldNameFromExpression<TDocument>(this Expression<Func<TDocument, object>> expression)
            where TDocument : IDocument
        {
            var propertyInfo = expression.GetPropertyInfoFromExpression();
            var solrFieldAttribute = expression.GetSolrFieldAttributeFromPropertyInfo();

            return solrFieldAttribute == null ? propertyInfo.Name : solrFieldAttribute.Name;
        }

        /// <summary>
        /// Returns the SolrFieldAttribute associated with the informed property
        /// </summary>
        /// <typeparam name="T">Type of the document used in the query</typeparam>
        /// <param name="expression">Expression used to find the property name</param>
        /// <returns>SolrFieldAttribute associated4 with the informed property, otherwise null</returns>
        internal static SolrFieldAttribute GetSolrFieldAttributeFromPropertyInfo<TDocument>(this Expression<Func<TDocument, object>> expression)
            where TDocument : IDocument
        {
            var propertyInfo = expression.GetPropertyInfoFromExpression();
            var attrs = propertyInfo.GetCustomAttributes(true);
            return (SolrFieldAttribute)attrs.FirstOrDefault(q => q is SolrFieldAttribute);
        }
    }
}
