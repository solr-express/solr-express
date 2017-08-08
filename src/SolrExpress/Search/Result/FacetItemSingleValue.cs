namespace SolrExpress.Search.Result
{
    /// <summary>
    /// Represents a facet item with multiple values
    /// </summary>
    public sealed class FacetItemSingleValue: FacetItem
    {
        /// <summary>
        /// Quantity of value
        /// </summary>
        public long Quantity { get; set; }
    }
}
