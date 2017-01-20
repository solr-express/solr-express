namespace SolrExpress.Core
{
    /// <summary>
    /// Signatures of SOLR connection
    /// </summary>
    public interface ISolrConnection
    {
        /// <summary>
        /// Execute the informated uri and return the result of the request
        /// </summary>
        /// <param name="options">Options to security connection</param>
        /// <param name="handler">Handler name used in solr request</param>
        /// <param name="data">Data to execute</param>
        /// <returns>Result of the request</returns>
        string Get(SecurityOptions options, string handler, string data);

        /// <summary>
        /// Execute the informated uri and return the result of the request
        /// </summary>
        /// <param name="options">Options to security connection</param>
        /// <param name="handler">Handler name used in solr request</param>
        /// <param name="data">Data to execute</param>
        /// <returns>Result of the request</returns>
        string Post(SecurityOptions options, string handler, string data);

        /// <summary>
        /// Solr host address
        /// </summary>
        string HostAddress { get; set; }
    }
}
