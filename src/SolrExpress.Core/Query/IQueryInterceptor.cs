namespace SolrExpress.Core.Query
{
    /// <summary>
    /// Signature to use in solr query interceptor
    /// </summary>
    public interface IQueryInterceptor
    {
        /// <summary>
        /// Execute the interception
        /// </summary>
        /// <param name="query">Query to intercept</param>
        void Execute(ref string query);
    }
}
