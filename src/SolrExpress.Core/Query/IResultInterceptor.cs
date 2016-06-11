namespace SolrExpress.Core.Query
{
    /// <summary>
    /// Signature to use in solr result interceptor
    /// </summary>
    public interface IResultInterceptor
    {
        /// <summary>
        /// Execute the interception
        /// </summary>
        /// <param name="json">Json to intercept</param>
        void Execute(ref string json);
    }
}
