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
        /// Name of alias added in query
        /// </summary>
        string AliasName { get; set; }

        /// <summary>
        /// Function used in spatial filter
        /// </summary>
        SpatialFunctionType FunctionType { get; set; }

        /// <summary>
        /// Expression used to find field name
        /// </summary>
        Expression<Func<TDocument, object>> FieldExpression { get; set; }

        /// <summary>
        /// Center point to spatial filter
        /// </summary>
        GeoCoordinate CenterPoint { get; set; }

        /// <summary>
        /// Distance from center point
        /// </summary>
        decimal Distance { get; set; }

        /// <summary>
        /// Sort type of result of facet
        /// </summary>
        FacetSortType SortType { get; set; }

        /// <summary>
        /// List of tags to exclude in facet calculation
        /// </summary>
        string[] Excludes { get; set; }
    }
}
