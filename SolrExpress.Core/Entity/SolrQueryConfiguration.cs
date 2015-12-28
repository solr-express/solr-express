namespace SolrExpress.Core.Entity
{
    /// <summary>
    /// Configurations to control SOLR Query behavior
    /// </summary>
    public sealed class SolrQueryConfiguration
    {
        /// <summary>
        /// If true, check for possibles fails in the use of the Solr Queriable (using SolrFieldAttribute), otherwise false
        /// </summary>
        public bool FailFast { get; set; }
        
        /// <summary>
        /// Handler name used in solr request, default is "query"
        /// </summary>
        public string Handler { get; set; }
    }
}
