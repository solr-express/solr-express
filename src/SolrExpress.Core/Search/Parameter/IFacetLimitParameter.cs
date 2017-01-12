namespace SolrExpress.Core.Search.Parameter
{
    /// <summary>
    /// Signatures to use in facet limit parameter
    /// </summary>
    public interface IFacetLimitParameter<TDocument> : ISearchParameter
        where TDocument : IDocument
    {
        /// <summary>
        /// Configure current instance
        /// </summary>
        /// <param name="value">Value of limit</param>
        IFacetLimitParameter<TDocument> Configure(int value);

        /// <summary>
        /// Value of limit
        /// </summary>
        int Value { get; }
    }
}
