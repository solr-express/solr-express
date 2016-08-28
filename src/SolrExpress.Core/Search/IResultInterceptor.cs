namespace SolrExpress.Core.Search
{
    /// <summary>
    /// Signature to use in solr result interceptor
    /// </summary>
    public interface IResultInterceptor : ISearchItem
    {
        /// <summary>
        /// Execute the interception
        /// </summary>
        /// <param name="json">Json to intercept</param>
        void Execute(ref string json);
    }
}
