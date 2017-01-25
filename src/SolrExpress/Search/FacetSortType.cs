namespace SolrExpress.Search
{
    /// <summary>
    /// Sort's type of facet result
    /// </summary>
    public enum FacetSortType
    {
        /// <summary>
        /// Sort by index, ascending
        /// </summary>
        IndexAsc,

        /// <summary>
        /// Sort by index, descending
        /// </summary>
        IndexDesc,

        /// <summary>
        /// Sort by count, ascending
        /// </summary>
        CountAsc,

        /// <summary>
        /// Sort by count, descending
        /// </summary>
        CountDesc
    }
}
