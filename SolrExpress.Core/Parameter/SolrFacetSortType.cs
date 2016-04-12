namespace SolrExpress.Core.Parameter
{
    /// <summary>
    /// Sort's type of the facet result
    /// </summary>
    public enum SolrFacetSortType
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
