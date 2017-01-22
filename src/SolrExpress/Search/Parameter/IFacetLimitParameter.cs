namespace SolrExpress.Search.Parameter
{
    /// <summary>
    /// Signatures to use in facet limit parameter
    /// </summary>
    public interface IFacetLimitParameter<TDocument> : ISearchParameter
        where TDocument : IDocument
    {
        /// <summary>
        /// Value of limit
        /// </summary>
        int Value { get; set;}
    }
}