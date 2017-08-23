namespace SolrExpress.Search.Result
{
    /// <summary>
    /// Signature to information about search result
    /// </summary>
    public interface IInformationResult<TDocument> : ISearchResult<TDocument>
        where TDocument : Document
    {
        /// <summary>
        /// Search result data
        /// </summary>
        Information Data { get; set; }
    }
}
