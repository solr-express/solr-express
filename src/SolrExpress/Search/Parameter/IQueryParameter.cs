namespace SolrExpress.Search.Parameter
{
    /// <summary>
    /// Signatures to use in query parameter
    /// </summary>
    public interface IQueryParameter<TDocument> : ISearchParameter
        where TDocument : IDocument
    {
        /// <summary>
        /// Parameter to include in query
        /// </summary>
        ISearchQuery<TDocument> Value { get; set; }
    }
}
