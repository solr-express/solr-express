namespace SolrExpress.Search.Result
{
    /// <summary>
    /// Represents a facet item value
    /// </summary>
    /// <typeparam name="TKey">Value of the facet</typeparam>
    public sealed class FacetValue<TKey> : FacetValue
    {
        /// <summary>
        /// Key of item
        /// </summary>
        public TKey Key { get; set; }
    }
}
