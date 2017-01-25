namespace SolrExpress.Search.Parameter
{
    /// <summary>
    /// Signatures to use in query parameter
    /// </summary>
    public interface IQueryParameter<TDocument> : ISearchParameter
        where TDocument : IDocument
    {
        /// <summary>
        /// Configure parameter to include in query
        /// </summary>
        /// <param name="value">Parameter to include in query</param>
        IQueryParameter<TDocument> Value(ISearchQuery<TDocument> value);
    }
}
