using SolrExpress.Core.Search;
using System;
using System.Linq.Expressions;

namespace SolrExpress.Search.Parameter
{
    /// <summary>
    /// Signatures to use in facet spatial parameter
    /// </summary>
    public interface IFacetSpatialParameter<TDocument> : ISearchParameter
        where TDocument : IDocument
    {
        /// <summary>
        /// Configure name of alias added in query
        /// </summary>
        /// <param name="aliasName">Name of alias added in query</param>
        IFacetSpatialParameter<TDocument> AliasName(string aliasName);

        /// <summary>
        /// Configure function used in spatial filter
        /// </summary>
        /// <param name="functionType">Function used in spatial filter</param>
        IFacetSpatialParameter<TDocument> FunctionType(SpatialFunctionType functionType);

        /// <summary>
        /// Configure expression used to find field name
        /// </summary>
        /// <param name="fieldExpression">Expression used to find field name</param>
        IFacetSpatialParameter<TDocument> FieldExpression(Expression<Func<TDocument, object>> fieldExpression);

        /// <summary>
        /// Configure center point to spatial filter
        /// </summary>
        /// <param name="centerPoint">Center point to spatial filter</param>
        IFacetSpatialParameter<TDocument> CenterPoint(GeoCoordinate centerPoint);

        /// <summary>
        /// Configure distance from center point
        /// </summary>
        /// <param name="distance">Distance from center point</param>
        IFacetSpatialParameter<TDocument> Distance(decimal distance);

        /// <summary>
        /// Configure sort type of result of facet
        /// </summary>
        /// <param name="sortType">Sort type of result of facet</param>
        IFacetSpatialParameter<TDocument> SortType(FacetSortType sortType);

        /// <summary>
        /// Configure list of tags to exclude in facet calculation
        /// </summary>
        /// <param name="excludes">List of tags to exclude in facet calculation</param>
        IFacetSpatialParameter<TDocument> Excludes(params string[] excludes);
    }
}
