namespace SolrExpress.Search.Interceptor
{
    /// <summary>
    /// Signature to use in solr result interceptor
    /// </summary>
    public interface IResultInterceptor : ISearchItem
    {
        /// <summary>
        /// Execute the interception
        /// </summary>
        /// <param name="requestHandler">Handler to use in SOLR request</param>
        /// <param name="json">Json to intercept</param>
        void Execute(string requestHandler, ref string json);
    }
}
