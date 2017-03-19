using Flurl;
using Flurl.Http;
using SolrExpress.Utility;
using System.Net.Http;

namespace SolrExpress
{
    /// <summary>
    /// Signatures to SOLR connection
    /// </summary>
    public class SolrConnection
    {
        private readonly SolrExpressOptions _options;

        public SolrConnection(SolrExpressOptions options)
        {
            Checker.IsNull(options);

            this._options = options;
        }

        /// <summary>
        /// Set authentication configurations
        /// </summary>
        /// <param name="url">Uri to configure</param>
        private void SetAuthentication(Url url)
        {
            switch (this._options.Security.AuthenticationType)
            {
                case AuthenticationType.Basic:
                    url.WithBasicAuth(this._options.Security.UserName, this._options.Security.Password);
                    break;
            }
        }

        /// <summary>
        /// Execute a request to informed handler
        /// </summary>
        /// <param name="handler">Handler name used in solr request</param>
        /// <param name="data">Data to execute</param>
        /// <returns>Result of request</returns>
        public string Get(string handler, string data)
        {
            var url = this._options.HostAddress
                .SetQueryParam(handler, data);

            this.SetAuthentication(url);

            return url
                .GetStringAsync()
                .Result;
        }

        /// <summary>
        /// Execute a request to informed handler
        /// </summary>
        /// <param name="handler">Handler name used in solr request</param>
        /// <param name="data">Data to execute</param>
        /// <returns>Result of request</returns>
        public string GetX(string handler, string data)
        {
            var url = this._options.HostAddress
                .AppendPathSegment(handler);

            this.SetAuthentication(url);

#if NETCORE
            return url
                .SendJsonAsync(HttpMethod.Get, JsonConvert.DeserializeObject(data))
                .Result
                .Content
                .ReadAsStringAsync()
                .Result;
#else
            return url
                .SetQueryParam("json", data)
                .GetStringAsync()
                .Result;
#endif
        }

        /// <summary>
        /// Execute a request to informed handler
        /// </summary>
        /// <param name="handler">Handler name used in solr request</param>
        /// <param name="data">Data to execute</param>
        /// <returns>Result of request</returns>
        public string Post(string handler, string data)
        {
            var url = this._options.HostAddress
                .AppendPathSegment(handler);

            this.SetAuthentication(url);

            return url
                .PostJsonAsync(data)
                .ReceiveString()
                .Result;
        }
    }
}
