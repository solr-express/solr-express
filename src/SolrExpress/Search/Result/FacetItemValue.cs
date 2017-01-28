namespace SolrExpress.Search.Result
{
    /// <summary>
    /// Represents a facet item value
    /// </summary>
    /// <typeparam name="TKey">Value of the facet</typeparam>
    public class FacetItemValue<TKey>
    {
        /// <summary>
        /// Key of item
        /// </summary>
        public TKey Key { get; set; }

        /// <summary>
        /// Quantity of item
        /// </summary>
        public long Quantity { get; set; }

        /// <summary>
        /// Tag
        /// </summary>
        public object Tag { get; set; }
    }
}
