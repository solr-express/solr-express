using Flurl;
using Flurl.Http;
using Newtonsoft.Json.Linq;
using SolrExpress.Options;
using SolrExpress.Utility;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SolrExpress.Connection
{
    /// <summary>
    /// SOLR connection
    /// </summary>
    public class SolrConnection<TDocument> : ISolrConnection<TDocument>
        where TDocument : Document
    {
        private readonly SolrExpressOptions _options;
        private readonly ISolrExpressServiceProvider<TDocument> _serviceProvider;

        public SolrConnection(SolrExpressOptions options, ISolrExpressServiceProvider<TDocument> serviceProvider)
        {
            Checker.IsNull(options);
            Checker.IsNull(serviceProvider);

            this._options = options;
            this._serviceProvider = serviceProvider;
        }

        /// <summary>
        /// Set authentication configurations
        /// </summary>
        /// <param name="url">Uri to configure</param>
#if NETCOREAPP2_1
        private IFlurlRequest SetAuthentication(Url url)
#else
        private IFlurlClient SetAuthentication(Url url)
#endif
        {
            switch (this._options.Security.AuthenticationType)
            {
                case AuthenticationType.None:
#if NETCOREAPP2_1
                    return new FlurlRequest(url);
#else
                    return new FlurlClient(url, true);
#endif
                case AuthenticationType.Basic:
                    return url.WithBasicAuth(this._options.Security.BasicAuthentication.UserName, this._options.Security.BasicAuthentication.Password);
                case AuthenticationType.BearerToken:
                    return url.WithOAuthBearerToken(this._options.Security.BearerToken.Token);
                case AuthenticationType.Custom:
#if NETCOREAPP2_1
                    var flurlClient = new FlurlRequest(url);
#else
                    var flurlClient = new FlurlClient(url, true);
#endif
                    var service = this._serviceProvider.GetService<ICustomSolrConnectionAuthenticationSettings>();
                    Checker.IsNull<CustomSolrConnectionAuthenticationNotFoundException>(service);

                    service.Configure(flurlClient);

                    return flurlClient;
                default:
                    throw new System.ArgumentOutOfRangeException(nameof(this._options.Security.AuthenticationType));
            }
        }

        public string Get(string handler, List<string> data)
        {
            var url = this._options.HostAddress
                .AppendPathSegment(handler);

            if (data?.Any() ?? false)
            {
                url = url.SetQueryParams(data);
            }

            return this
                .SetAuthentication(url)
                .GetStringAsync()
                .Result;
        }

        public Stream GetStream(string handler, List<string> data)
        {
            var url = this._options.HostAddress
                .AppendPathSegment(handler);

            if (data?.Any() ?? false)
            {
                url = url.SetQueryParams(data);
            }

            return this
                .SetAuthentication(url)
                .GetStreamAsync()
                .Result;
        }

        public string Post(string handler, JObject data)
        {
            var url = this._options.HostAddress
                .AppendPathSegment(handler);

            return this
                .SetAuthentication(url)
                .PostJsonAsync(data)
                .ReceiveString()
                .Result;
        }

        public Stream PostStream(string handler, JObject data)
        {
            var url = this._options.HostAddress
                .AppendPathSegment(handler);

            return this
                .SetAuthentication(url)
                .PostJsonAsync(data)
                .ReceiveStream()
                .Result;
        }
    }
}
