using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;

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
        Stream GetStream(string handler, List<string> data);
        
        /// <summary>
        /// Execute a request to informed handler
        /// </summary>
        /// <param name="handler">Handler name used in solr request</param>
        /// <param name="data">Data to execute</param>
        /// <returns>Result of request</returns>
        string Post(string handler, JObject data);

        /// <summary>
        /// Execute a request to informed handler
        /// </summary>
        /// <param name="handler">Handler name used in solr request</param>
        /// <param name="data">Data to execute</param>
        /// <returns>Result of request</returns>
        Stream PostStream(string handler, JObject data);
    }
}
