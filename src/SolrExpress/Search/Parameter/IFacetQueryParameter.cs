namespace SolrExpress.Search.Parameter
{
    /// <summary>
    /// Signatures to use in facet query parameter
    /// </summary>
    public interface IFacetQueryParameter<TDocument> : ISearchParameter
        where TDocument : IDocument
    {
        /// <summary>
        /// Configure name of alias added in query
        /// </summary>
        /// <param name="aliasName">Name of alias added in query</param>
        IFacetQueryParameter<TDocument> AliasName(string aliasName);

        /// <summary>
        /// Configure query used to make facet
        /// </summary>
        /// <param name="query">Query used to make facet</param>
        IFacetQueryParameter<TDocument> Query(ISearchQuery<TDocument> query);

        /// <summary>
        /// Configure sort type of result of facet
        /// </summary>
        /// <param name="sortType">Sort type of result of facet</param>
        IFacetQueryParameter<TDocument> SortType(FacetSortType sortType);

        /// <summary>
        /// Configure list of tags to exclude in facet calculation
        /// </summary>
        /// <param name="excludes">List of tags to exclude in facet calculation</param>
        IFacetQueryParameter<TDocument> Excludes(params string[] excludes);
    }
}
