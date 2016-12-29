namespace SolrExpress.Core.Search.Parameter
{
    /// <summary>
    /// Signatures to use in minimum should match parameter
    /// </summary>
    public interface IMinimumShouldMatchParameter<TDocument> : ISearchParameter<TDocument>
        where TDocument : IDocument
    {
        /// <summary>
        /// Configure current instance
        /// </summary>
        /// <param name="expression">Expression used to make the mm parameter</param>
        IMinimumShouldMatchParameter<TDocument> Configure(string expression);

        /// <summary>
        /// Expression used to make the mm parameter
        /// </summary>
        string Expression { get; }
    }
}
