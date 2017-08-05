using System.Collections.Generic;

namespace SolrExpress
{
    /// <summary>
    /// Signauters to SOLR connection
    /// </summary>
    public interface ISolrConnection
    {
        /// <summary>
        /// Execute a request to informed handler
        /// </summary>
        /// <param name="handler">Handler name used in solr request</param>
        /// <param name="data">Data to execute</param>
        /// <returns>Result of request</returns>
        string Get(string handler, List<string> data);

        /// <summary>
        /// Execute a request to informed handler
        /// </summary>
        /// <param name="handler">Handler name used in solr request</param>
        /// <param name="data">Data to execute</param>
        /// <returns>Result of request</returns>
        string GetX(string handler, object data);

        /// <summary>
        /// Execute a request to informed handler
        /// </summary>
        /// <param name="handler">Handler name used in solr request</param>
        /// <param name="data">Data to execute</param>
        /// <returns>Result of request</returns>
        string PostJson(string handler, string data);
    }
}
