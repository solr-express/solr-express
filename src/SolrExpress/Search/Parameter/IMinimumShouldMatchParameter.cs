namespace SolrExpress.Search.Parameter
{
    /// <summary>
    /// Minimum should match parameter
    /// </summary>
    public interface IMinimumShouldMatchParameter<TDocument> : ISearchParameter
        where TDocument : Document
    {
        /// <summary>
        /// Value used to make mm parameter
        /// </summary>
        string Value { get; set; }
    }
}
