namespace SolrExpress.Core.Enumerator
{
    /// <summary>
    /// Sort's type of the facet result
    /// </summary>
    public enum SolrFacetSortType
    {
        /// <summary>
        /// Sort by name, ascending
        /// </summary>
        IndexAsc,

        /// <summary>
        /// Sort by name, descending
        /// </summary>
        IndexDesc,

        /// <summary>
        /// Sort by quantity, ascending
        /// </summary>
        QuantityAsc,

        /// <summary>
        /// Sort by quantity, descending
        /// </summary>
        QuantityDesc
    }
}
