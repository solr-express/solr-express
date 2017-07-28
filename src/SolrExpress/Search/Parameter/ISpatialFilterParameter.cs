using SolrExpress.Core.Search.Parameter;

namespace SolrExpress.Search.Parameter
{
    /// <summary>
    /// Signatures to use in spatial filter parameter
    /// </summary>
    public interface ISpatialFilterParameter<TDocument> : ISearchParameter, ISearchItemFieldExpression<TDocument>
        where TDocument : Document
    {
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