namespace SolrExpress.Core.Search.Parameter
{
    /// <summary>
    /// Signatures to use in query parameter
    /// </summary>
    public interface IQueryParameter<TDocument> : ISearchParameter
        where TDocument : IDocument
    {
        /// <summary>
        /// Configure current instance
        /// </summary>
        /// <param name="value">Parameter to include in the query</param>
        IQueryParameter<TDocument> Configure(ISearchParameterValue value);

        /// <summary>
        /// Parameter to include in the query
        /// </summary>
        ISearchParameterValue Value { get; }
    }
}
