using SolrExpress.Core.Attribute;
using SolrExpress.Core.Entity;
using SolrExpress.Core.Enumerator;
using System;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace SolrExpress.Core.Helper
{
    /// <summary>
    /// Helper class use in the SOLR Query core
    /// </summary>
    internal static class UtilHelper
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

                    ThrowHelper<InvalidOperationException>.If(propertyInfo == null, Resource.ExpressionMustBePropertyException);
                    
                    return propertyInfo;
                case ExpressionType.MemberAccess:
                    memberExpression = (MemberExpression)lambda.Body;

                    propertyInfo = memberExpression.Member as PropertyInfo;

                    ThrowHelper<InvalidOperationException>.If(propertyInfo == null, Resource.ExpressionMustBePropertyException);

                    return propertyInfo;
            }
            
            throw new InvalidOperationException(Resource.UnknownToResolveExpressionException);
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
            var propertyInfo = UtilHelper.GetPropertyInfoFromExpression(expression);
            var attrs = propertyInfo.GetCustomAttributes(true);
            return (SolrFieldAttribute)attrs.FirstOrDefault(q => q is SolrFieldAttribute);
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
            var propertyInfo = UtilHelper.GetPropertyInfoFromExpression(expression);
            var solrFieldAttribute = UtilHelper.GetSolrFieldAttributeFromPropertyInfo(expression);

            return solrFieldAttribute == null ? propertyInfo.Name : solrFieldAttribute.Name;
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
            return UtilHelper.GetPropertyInfoFromExpression(expression).Name;
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
            return UtilHelper.GetPropertyInfoFromExpression(expression).PropertyType;
        }

        /// <summary>
        /// Get the sort type and direction
        /// </summary>
        /// <param name="solrFacetSortType">Type used in match</param>
        /// <param name="typeName">Type name</param>
        /// <param name="sortName">Sort direction</param>
        internal static void GetSolrFacetSort(SolrFacetSortType solrFacetSortType, out string typeName, out string sortName)
        {
            switch (solrFacetSortType)
            {
                case SolrFacetSortType.IndexAsc:
                    typeName = "index";
                    sortName = "asc";
                    break;
                case SolrFacetSortType.IndexDesc:
                    typeName = "index";
                    sortName = "desc";
                    break;
                case SolrFacetSortType.CountAsc:
                    typeName = "count";
                    sortName = "asc";
                    break;
                case SolrFacetSortType.CountDesc:
                    typeName = "count";
                    sortName = "desc";
                    break;
                default:
                    throw new ArgumentException(nameof(solrFacetSortType));
            }
        }

        /// <summary>
        /// Get the field with excludes
        /// </summary>
        /// <param name="aliasName">Alias name</param>
        /// <param name="fieldName">Field name</param>
        /// <param name="sortName">List of excludes</param>
        internal static string GetSolrFacetWithExcludesSolr4(string aliasName, string fieldName, string[] excludes)
        {
            if (excludes != null && excludes.Length > 0)
            {
                return string.Format(
                    "{{!ex={0} key={1}}}{2}",
                    string.Join(",", excludes),
                    aliasName,
                    fieldName);
            }

            return string.Format("{{!key={0}}}{1}", aliasName, fieldName);
        }

        /// <summary>
        /// Get the field with excludes
        /// </summary>
        /// <param name="fieldName">Field name</param>
        /// <param name="sortName">List of excludes</param>
        internal static string GetSolrFacetWithExcludesSolr5(string fieldName, string[] excludes)
        {
            if (excludes != null && excludes.Length > 0)
            {
                return string.Format(
                    "{{!ex={0}}}{1}",
                    string.Join(",", excludes),
                    fieldName);
            }

            return fieldName;
        }

        /// <summary>
        /// Get the filter with tag
        /// </summary>
        /// <param name="tagName">Tag name</param>
        /// <param name="fieldName">Field name</param>
        internal static string GetSolrFilterWithTag(string aliasName, string query)
        {
            if (!string.IsNullOrWhiteSpace(aliasName))
            {
                return string.Format(
                    "{{!tag={0}}}{1}",
                    aliasName,
                    query);
            }

            return query;
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
                centerPoint.ToString(),
                distance.ToString("0.#", CultureInfo.InvariantCulture));
        }
    }
}
