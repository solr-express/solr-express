namespace SolrExpress.Search.Parameter
{
    /// <summary>
    /// Facet spatial parameter
    /// </summary>
    public interface IFacetSpatialParameter<TDocument> : IFacetParameter<TDocument>, ISearchItemFieldExpression<TDocument>
        where TDocument : Document
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
    }
}
