using SolrExpress.Search.Query;

namespace SolrExpress.Search.Parameter
{
    /// <summary>
    /// Signatures to use in facet query parameter
    /// </summary>
    public interface IFacetQueryParameter<TDocument> : ISearchParameter
        where TDocument : IDocument
    {
        /// <summary>
        /// Name of alias added in query
        /// </summary>
        string AliasName { get; set; }

        /// <summary>
        /// Query used to make facet
        /// </summary>
        ISearchQuery Query { get; set; }

        /// <summary>
        /// Minimum count of itens in facet's result
        /// </summary>
        int? Minimum { get; set; }

        /// <summary>
        /// Limit of itens in facet's result
        /// </summary>
        int? Limit { get; set; }

        /// <summary>
        /// Sort type of result of facet
        /// </summary>
        FacetSortType? SortType { get; set; }

        /// <summary>
        /// List of tags to exclude in facet calculation
        /// </summary>
        string[] Excludes { get; set; }
    }
}
