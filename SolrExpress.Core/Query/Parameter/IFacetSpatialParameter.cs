using SolrExpress.Core.Query.ParameterValue;
using System;
using System.Linq.Expressions;

namespace SolrExpress.Core.Query.Parameter
{
    /// <summary>
    /// Signatures to use in facet spatial parameter
    /// </summary>
    public interface IFacetSpatialParameter<TDocument> : IParameter
        where TDocument : IDocument
    {
        /// <summary>
        /// Configure current instance
        /// </summary>
        /// <param name="aliasName">Name of the alias added in the query</param>
        /// <param name="functionType">Function used in the spatial filter</param>
        /// <param name="expression">Expression used to find the property name</param>
        /// <param name="centerPoint">Center point to spatial filter</param>
        /// <param name="distance">Distance from the center point</param>
        /// <param name="sortType">Sort type of the result of the facet</param>
        /// <param name="excludes">List of tags to exclude in facet calculation</param>
        IFacetSpatialParameter<TDocument> Configure(string aliasName, SolrSpatialFunctionType functionType, Expression<Func<TDocument, object>> expression, GeoCoordinate centerPoint, decimal distance, SolrFacetSortType? sortType = null, params string[] excludes);
    }
}
