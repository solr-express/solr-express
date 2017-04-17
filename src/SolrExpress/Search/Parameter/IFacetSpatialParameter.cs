using SolrExpress.Core.Search.Parameter;

namespace SolrExpress.Search.Parameter
{
    /// <summary>
    /// Signatures to use in facet spatial parameter
    /// </summary>
    public interface IFacetSpatialParameter<TDocument> : ISearchParameter, ISearchItemFieldExpression<TDocument>
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
        /// Center point to spatial filter
        /// </summary>
        GeoCoordinate CenterPoint { get; set; }

        /// <summary>
        /// Distance from center point
        /// </summary>
        decimal Distance { get; set; }

        /// <summary>
        /// Minimum count of itens in facet's result
        /// </summary>
        int? Minimum { get; set; }

        /// <summary>
        /// Limit of itens in facet's result
        /// </summary>
        int? Limit { get; set; }

        /// <summary>
        /// Sort type of result of facet
        /// </summary>
        FacetSortType? SortType { get; set; }

        /// <summary>
        /// List of tags to exclude in facet calculation
        /// </summary>
        string[] Excludes { get; set; }
    }
}
