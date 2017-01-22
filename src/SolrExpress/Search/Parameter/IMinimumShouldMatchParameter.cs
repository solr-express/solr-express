namespace SolrExpress.Search.Parameter
{
    /// <summary>
    /// Signatures to use in minimum should match parameter
    /// </summary>
    public interface IMinimumShouldMatchParameter<TDocument> : ISearchParameter
        where TDocument : IDocument
    {
        /// <summary>
        /// Expression used to make mm parameter
        /// </summary>
        string Expression { get; set;}
    }
}
