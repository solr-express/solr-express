using Flurl;
using Flurl.Http;
using Newtonsoft.Json.Linq;
using SolrExpress.Utility;
using System.Collections.Generic;
using System.Linq;
using SolrExpress.Options;

namespace SolrExpress
{
    /// <summary>
    /// SOLR connection
    /// </summary>
    public class SolrConnection : ISolrConnection
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
            // ReSharper disable once SwitchStatementMissingSomeCases
            switch (this._options.Security.AuthenticationType)
            {
                case AuthenticationType.Basic:
                    url.WithBasicAuth(this._options.Security.UserName, this._options.Security.Password);
                    break;
            }
        }

        string ISolrConnection.Get(string handler, List<string> data)
        {
            var url = this._options.HostAddress
                .AppendPathSegment(handler);

            if (data?.Any() ?? false)
            {
                url.SetQueryParams(data);
            }

            this.SetAuthentication(url);

            return url
                .GetStringAsync()
                .Result;
        }

        string ISolrConnection.Post(string handler, JObject data)
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
