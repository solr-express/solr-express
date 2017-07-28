namespace SolrExpress.Search.Parameter
{
    /// <summary>
    /// Signatures to use in facet limit parameter
    /// </summary>
    public interface IFacetLimitParameter<TDocument> : ISearchParameter
        where TDocument : Document
    {
        /// <summary>
        /// Value of limit
        /// </summary>
        long Value { get; set; }
    }
}