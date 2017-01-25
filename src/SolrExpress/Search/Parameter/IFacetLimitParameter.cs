namespace SolrExpress.Search.Parameter
{
    /// <summary>
    /// Signatures to use in facet limit parameter
    /// </summary>
    public interface IFacetLimitParameter<TDocument> : ISearchParameter
        where TDocument : IDocument
    {
        /// <summary>
        /// Configure value of limit
        /// </summary>
        /// <param name="value">Value of limit</param>
        IFacetLimitParameter<TDocument> Value(long value);
    }
}