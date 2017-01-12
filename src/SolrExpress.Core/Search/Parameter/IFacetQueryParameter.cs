namespace SolrExpress.Core.Search.Parameter
{
    /// <summary>
    /// Signatures to use in facet query parameter
    /// </summary>
    public interface IFacetQueryParameter<TDocument> : ISearchParameter
        where TDocument : IDocument
    {
        /// <summary>
        /// Configure current instance
        /// </summary>
        /// <param name="aliasName">Name of the alias added in the query</param>
        /// <param name="query">Query used to make the facet</param>
        /// <param name="sortType">Sort type of the result of the facet</param>
        /// <param name="excludes">List of tags to exclude in facet calculation</param>
        IFacetQueryParameter<TDocument> Configure(string aliasName, ISearchParameterValue<TDocument> query, FacetSortType? sortType = null, params string[] excludes);

        /// <summary>
        /// Name of the alias added in the query
        /// </summary>
        string AliasName { get; }

        /// <summary>
        /// Query used to make the facet
        /// </summary>
        ISearchParameterValue<TDocument> Query { get; }

        /// <summary>
        /// Sort type of the result of the facet
        /// </summary>
        FacetSortType? SortType { get; }

        /// <summary>
        /// List of tags to exclude in facet calculation
        /// </summary>
        string[] Excludes { get; }
    }
}
