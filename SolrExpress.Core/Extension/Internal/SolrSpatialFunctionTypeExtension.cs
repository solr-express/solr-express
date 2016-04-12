using SolrExpress.Core.ParameterValue;
using System.Globalization;

namespace SolrExpress.Core.Extension.Internal
{
    /// <summary>
    /// Extension class used in SolrSpatialFunctionTypeExtension
    /// </summary>
    internal static class SolrSpatialFunctionTypeExtension
    {
        /// <summary>
        /// Get spatial formule
        /// </summary>
        /// <param name="functionType">Spatial function to use</param>
        /// <param name="fieldName">Field name</param>
        /// <param name="centerPoint">Center point information</param>
        /// <param name="distance">Distance</param>
        /// <returns></returns>
        internal static string GetSolrSpatialFormule(this SolrSpatialFunctionType functionType, string fieldName, GeoCoordinate centerPoint, decimal distance)
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
