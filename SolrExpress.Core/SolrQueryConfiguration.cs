namespace SolrExpress.Core
{
    /// <summary>
    /// Configurations to control SOLR Query behavior
    /// </summary>
    public sealed class SolrQueryConfiguration
    {
        public SolrQueryConfiguration()
        {
            this.FailFast = true;
            this.Handler = RequestHandler.QUERY;
        }

        /// <summary>
        /// If true, check for possibles fails in the use of the Solr Queriable (using SolrFieldAttribute), otherwise false. Default is true
        /// </summary>
        public bool FailFast { get; set; }

        /// <summary>
        /// Handler name used in solr request. Default is "query"
        /// </summary>
        public string Handler { get; set; }
    }
}
