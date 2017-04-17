namespace SolrExpress.Search.Query.Extension
{
    /// <summary>
    /// Extensions to configure search queries
    /// </summary>
    public static class ISearchQueryExtension
    {
        /// <summary>
        /// Create a search query to find all informed values (conditional AND)
        /// </summary>
        /// <param name="searchQuery">Search query instance used to make query</param>
        /// <param name="values">Values to find</param>
        /// <returns>Search query configured</returns>
        public static ISearchQuery All<TValue>(this ISearchQuery searchQuery, params TValue[] values)
        {
            return searchQuery;
        }

        /// <summary>
        /// Create a search query to find all informed values (conditional AND)
        /// </summary>
        /// <param name="searchQuery">Search query instance used to make query</param>
        /// <param name="values">Values to find</param>
        /// <returns>Search query configured</returns>
        public static ISearchQuery Any<TValue>(this ISearchQuery searchQuery, params TValue[] values)
        {
            return searchQuery;
        }
    }
}
