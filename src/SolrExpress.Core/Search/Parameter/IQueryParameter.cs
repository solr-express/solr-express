namespace SolrExpress.Core.Search.Parameter
{
    /// <summary>
    /// Signatures to use in query parameter
    /// </summary>
    public interface IQueryParameter<TDocument> : ISearchParameter<TDocument>
        where TDocument : IDocument
    {
        /// <summary>
        /// Configure current instance
        /// </summary>
        /// <param name="value">Parameter to include in the query</param>
        IQueryParameter<TDocument> Configure(ISearchParameterValue<TDocument> value);

        /// <summary>
        /// Parameter to include in the query
        /// </summary>
        ISearchParameterValue<TDocument> Value { get; }
    }
}
