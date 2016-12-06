using SolrExpress.Core.Search.Parameter;
using SolrExpress.Core.Search.ParameterValue;
using System;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace SolrExpress.Core.Utility
{
    /// <summary>
    /// Utility class used in expression calculations
    /// </summary>
    public class ExpressionUtility
    {
        /// <summary>
        /// Returns the property name of the indicated expression
        /// </summary>
        /// <typeparam name="T">Type of the document used in the query</typeparam>
        /// <param name="expression">Expression used to find the property name</param>
        /// <returns>Property name indicated in the expression</returns>
        private static PropertyInfo GetPropertyInfoFromExpression<TDocument>(Expression<Func<TDocument, object>> expression)
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
        /// Get the sort type and direction
        /// </summary>
        /// <param name="solrFacetSortType">Type used in match</param>
        /// <param name="typeName">Type name</param>
        /// <param name="sortName">Sort direction</param>
        internal static void GetSolrFacetSort(FacetSortType solrFacetSortType, out string typeName, out string sortName)
        {
            switch (solrFacetSortType)
            {
                case FacetSortType.IndexAsc:
                    typeName = "index";
                    sortName = "asc";
                    break;
                case FacetSortType.IndexDesc:
                    typeName = "index";
                    sortName = "desc";
                    break;
                case FacetSortType.CountAsc:
                    typeName = "count";
                    sortName = "asc";
                    break;
                case FacetSortType.CountDesc:
                    typeName = "count";
                    sortName = "desc";
                    break;
                default:
                    throw new ArgumentException(nameof(solrFacetSortType));
            }
        }

        /// <summary>
        /// Get spatial formule
        /// </summary>
        /// <param name="functionType">Spatial function to use</param>
        /// <param name="fieldName">Field name</param>
        /// <param name="centerPoint">Center point information</param>
        /// <param name="distance">Distance</param>
        /// <returns></returns>
        internal static string GetSolrSpatialFormule(SolrSpatialFunctionType functionType, string fieldName, GeoCoordinate centerPoint, decimal distance)
        {
            return string.Format(
                "{{!{0} sfield={1} pt={2} d={3}}}",
                functionType.ToString().ToLower(),
                fieldName,
                $"{centerPoint.Latitude.ToString("G", CultureInfo.InvariantCulture)},{centerPoint.Longitude.ToString("G", CultureInfo.InvariantCulture)}",
                distance.ToString("G", CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// Get the filter with tag
        /// </summary>
        /// <param name="query">Query value</param>
        /// <param name="tagName">Tag name</param>
        internal static string GetSolrFilterWithTag(string query, string aliasName)
        {
            return !string.IsNullOrWhiteSpace(aliasName) ? $"{{!tag={aliasName}}}{query}" : query;
        }

        /// <summary>
        /// Returns the property name of the indicated expression
        /// </summary>
        /// <typeparam name="TDocument">Type of the document used in the query</typeparam>
        /// <param name="expression">Expression used to find the property name</param>
        /// <returns>Property name indicated in the expression</returns>
        internal static string GetFieldNameFromExpression<TDocument>(Expression<Func<TDocument, object>> expression)
            where TDocument : IDocument
        {
            var propertyInfo = GetPropertyInfoFromExpression(expression);
            var solrFieldAttribute = GetSolrFieldAttributeFromPropertyInfo(expression);

            return solrFieldAttribute == null ? propertyInfo.Name : solrFieldAttribute.Name;
        }

        /// <summary>
        /// Returns the SolrFieldAttribute associated with the informed property
        /// </summary>
        /// <typeparam name="T">Type of the document used in the query</typeparam>
        /// <param name="expression">Expression used to find the property name</param>
        /// <returns>SolrFieldAttribute associated4 with the informed property, otherwise null</returns>
        internal static SolrFieldAttribute GetSolrFieldAttributeFromPropertyInfo<TDocument>(Expression<Func<TDocument, object>> expression)
            where TDocument : IDocument
        {
            var propertyInfo = GetPropertyInfoFromExpression(expression);
            var attrs = propertyInfo.GetCustomAttributes(true);
            return (SolrFieldAttribute)attrs.FirstOrDefault(q => q is SolrFieldAttribute);
        }

        /// <summary>
        /// Returns the property name of the indicated expression
        /// </summary>
        /// <typeparam name="TDocument">Type of the document used in the query</typeparam>
        /// <param name="expression">Expression used to find the property name</param>
        /// <returns>Property name indicated in the expression</returns>
        internal static string GetPropertyNameFromExpression<TDocument>(Expression<Func<TDocument, object>> expression)
            where TDocument : IDocument
        {
            return GetPropertyInfoFromExpression(expression).Name;
        }

        /// <summary>
        /// Returns the property name of the indicated expression
        /// </summary>
        /// <typeparam name="TDocument">Type of the document used in the query</typeparam>
        /// <param name="expression">Expression used to find the property name</param>
        /// <returns>Property name indicated in the expression</returns>
        internal static Type GetPropertyTypeFromExpression<TDocument>(Expression<Func<TDocument, object>> expression)
            where TDocument : IDocument
        {
            return GetPropertyInfoFromExpression(expression).PropertyType;
        }
    }
}
