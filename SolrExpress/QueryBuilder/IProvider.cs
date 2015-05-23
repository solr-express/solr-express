namespace SolrExpress.QueryBuilder
{
    /// <summary>
    /// Signatures of the SOLR access provider
    /// </summary>
    public interface IProvider
    {
        /// <summary>
        /// Execute the informated uri and return the result of the request
        /// </summary>
        /// <param name="expressionToRequest">Expression created basead in the commands triggereds</param>
        /// <returns>Result of the request</returns>
        string Execute(string expressionToRequest);
    }
}
