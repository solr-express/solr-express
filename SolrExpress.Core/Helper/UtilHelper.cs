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


    }
}
