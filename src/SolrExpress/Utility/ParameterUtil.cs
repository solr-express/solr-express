using SolrExpress.Core.Search.Parameter;
using SolrExpress.Search.Parameter;
using System;
using System.Globalization;

namespace SolrExpress.Utility
{
    /// <summary>
    /// Helper class used to extract information inside parameters
    /// </summary>
    internal static class ParameterUtil
    {
        /// <summary>
        /// Get the sort type and direction
        /// </summary>
        /// <param name="solrFacetSortType">Type used in match</param>
        /// <param name="typeName">Type name</param>
        /// <param name="sortName">Sort direction</param>
        public static void GetFacetSort(FacetSortType solrFacetSortType, out string typeName, out string sortName)
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
        /// Calculate and returns spatial formule
        /// </summary>
        /// <param name="fieldName">Field name</param>
        /// <param name="functionType">Function used in spatial filter</param>
        /// <param name="centerPoint">Center point to spatial filter</param>
        /// <param name="distance">Distance from center point</param>
        /// <returns>Spatial formule</returns>
        internal static string GetSpatialFormule(string fieldName, SpatialFunctionType functionType, GeoCoordinate centerPoint, decimal distance)
        {
            return string.Format(
                "{{!{0} sfield={1} pt={2} d={3}}}",
                functionType.ToString().ToLower(),
                fieldName,
                $"{centerPoint.Latitude.ToString("G", CultureInfo.InvariantCulture)},{centerPoint.Longitude.ToString("G", CultureInfo.InvariantCulture)}",
                distance.ToString("G", CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// Get the field with excludes
        /// </summary>
        /// <param name="excludes">Excludes tags</param>
        /// <param name="aliasName">Alias name</param>
        /// <param name="fieldName">Field name</param>
        internal static string GetFacetName( string[] excludes, string aliasName, string fieldName)
        {
            if (excludes != null && excludes.Length > 0)
            {
                return $"{{!ex={string.Join(",", excludes)} key={aliasName}}}{fieldName}";
            }

            return $"{{!key={aliasName}}}{fieldName}";
        }

        /// <summary>
        /// Get the filter with tag
        /// </summary>
        /// <param name="query">Query value</param>
        /// <param name="aliasName">Alias name</param>
        public static string GetFilterWithTag(string query, string aliasName)
        {
            return !string.IsNullOrWhiteSpace(aliasName) ? $"{{!tag={aliasName}}}{query}" : query;
        }
    }
}
