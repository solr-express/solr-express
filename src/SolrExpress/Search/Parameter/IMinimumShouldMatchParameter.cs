namespace SolrExpress.Search.Parameter
{
    /// <summary>
    /// Signatures to use in minimum should match parameter
    /// </summary>
    public interface IMinimumShouldMatchParameter<TDocument> : ISearchParameter
        where TDocument : IDocument
    {
        /// <summary>
        /// Configure value used to make mm parameter
        /// </summary>
        /// <param name="value">Value used to make mm parameter</param>
        IMinimumShouldMatchParameter<TDocument> Value(string value);
    }
}
