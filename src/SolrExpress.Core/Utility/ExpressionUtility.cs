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
    public static class ExpressionUtility
    {
        /// <summary>
        /// Get the sort type and direction
        /// </summary>
        /// <param name="solrFacetSortType">Type used in match</param>
        /// <param name="typeName">Type name</param>
        /// <param name="sortName">Sort direction</param>
        public static void GetSolrFacetSort(FacetSortType solrFacetSortType, out string typeName, out string sortName)
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
        public static string GetSolrSpatialFormule(SolrSpatialFunctionType functionType, string fieldName, GeoCoordinate centerPoint, decimal distance)
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
        public static string GetSolrFilterWithTag(string query, string aliasName)
        {
            return !string.IsNullOrWhiteSpace(aliasName) ? $"{{!tag={aliasName}}}{query}" : query;
        }
    }
}
