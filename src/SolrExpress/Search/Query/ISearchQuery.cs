namespace SolrExpress.Search.Query
{
    /// <summary>
    /// Signature to use in solr queries
    /// </summary>
    public interface ISearchQuery
    {
        /// <summary>
        /// Execute parameter value generator
        /// </summary>
        /// <returns>Result generated value</returns>
        string Execute();
    }
}
