namespace SolrExpress.Search.Query
{
    /// <summary>
    /// Signature to use in solr queries
    /// </summary>
    public interface ISearchQuery<TDocument>
        where TDocument : IDocument
    {
        /// <summary>
        /// Execute parameter value generator
        /// </summary>
        /// <returns>Result generated value</returns>
        string Execute();
    }
}
