using SolrExpress.Core;
using SolrExpress.Core.Utility;
using System;
using System.IO;
using System.Net;
using System.Text;
#if NETCORE || NET45
using Flurl;
using Flurl.Http;
using System.Net.Http;
using Newtonsoft.Json;
#endif

namespace SolrExpress.Solr5
{
    /// <summary>
    /// SOLR 5.5x access provider
    /// </summary>
    public class SolrConnection : ISolrConnection
    {
#if NETCORE || NET45
        /// <summary>
        /// Set authentication configurations
        /// </summary>
        /// <param name="options">Options to security connection</param>
        /// <param name="url">Uri to configure</param>
        private void SetAuthentication(SecurityOptions options, Url url)
        {
            switch (options.AuthenticationType)
            {
                case AuthenticationType.Basic:
                    url.WithBasicAuth(options.UserName, options.Password);
                    break;
            }
        }

        /// <summary>
        /// Execute the informated uri and return the result of the request
        /// </summary>
        /// <param name="options">Options to security connection</param>
        /// <param name="handler">Handler name used in solr request</param>
        /// <param name="data">Data to execute</param>
        /// <returns>Result of the request</returns>
        public string Get(SecurityOptions options, string handler, string data)
        {
            var url = this.HostAddress
              .AppendPathSegment(handler);

            this.SetAuthentication(options, url);

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
        /// Execute the informated uri and return the result of the request
        /// </summary>
        /// <param name="options">Options to security connection</param>
        /// <param name="handler">Handler name used in solr request</param>
        /// <param name="data">Data to execute</param>
        /// <returns>Result of the request</returns>
        public string Post(SecurityOptions options, string handler, string data)
        {
            var url = this.HostAddress
                .AppendPathSegment(handler);

            this.SetAuthentication(options, url);

            return url
                .PostJsonAsync(JsonConvert.DeserializeObject(data))
                .ReceiveString()
                .Result;
        }
#else
        /// <summary>
        /// Execute the informated WebRequest and return the result of the request
        /// </summary>
        /// <param name="request">Configured request used in comunication with SOLR</param>
        /// <param name="rawData">Raw data send in request (used in log)</param>
        /// <returns>Result of the request</returns>
        private string Execute(WebRequest request, string rawData)
        {
            try
            {
                var response = (HttpWebResponse)request.GetResponse();

                using (var dataStream = response.GetResponseStream())
                {
                    using (var reader = new StreamReader(dataStream))
                    {
                        var content = reader.ReadToEnd();

                        Checker.IsTrue<UnexpectedSolrRequestException>(response.StatusCode != HttpStatusCode.OK, $"{request.RequestUri}\r\n{rawData}", content);

                        return content;
                    }
                }
            }
            catch (Exception e)
            {
                var response = ((WebException)e).Response;

                using (var dataStream = response.GetResponseStream())
                {
                    using (var reader = new StreamReader(dataStream))
                    {
                        var content = $"{e.Message}\r\n{reader.ReadToEnd()}";

                        throw new UnexpectedSolrRequestException($"{request.RequestUri}\r\n{rawData}", content);
                    }
                }
            }
        }

        /// <summary>
        /// Prepare request
        /// </summary>
        /// <param name="options">Options to security connection</param>
        /// <param name="requestMethod">Request method to execute</param>
        /// <param name="handler">Handler name used in solr request</param>
        /// <param name="data">Data to execute</param>
        /// <returns>WebRequest read to execute</returns>
        private WebRequest Prepare(SecurityOptions options, string requestMethod, string handler, string data)
        {
            var baseUrl = $"{this.HostAddress}/{handler}";

            var encoding = new UTF8Encoding();
            var bytes = encoding.GetBytes(data);

            var request = WebRequest.Create(baseUrl);

            if (options.AuthenticationType == AuthenticationType.Basic)
            {
                var encoded = Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1").GetBytes(options.UserName + ":" + options.Password));
                request.Headers[HttpRequestHeader.Authorization] = "Basic " + encoded;
            }

            request.Method = requestMethod;
            request.ContentType = "application/json";
            request.ContentLength = bytes.Length;
            var stream = request.GetRequestStream();
            stream.Write(bytes, 0, bytes.Length);
            stream.Close();

            return request;
        }

        /// <summary>
        /// Execute the informated uri and return the result of the request
        /// </summary>
        /// <param name="options">Options to security connection</param>
        /// <param name="handler">Handler name used in solr request</param>
        /// <param name="data">Data to execute</param>
        /// <returns>Result of the request</returns>
        public string Get(SecurityOptions options, string handler, string data)
        {
            var request = this.Prepare(options, "GET-X", handler, data);

            return this.Execute(request, data);
        }

        /// <summary>
        /// Execute the informated uri and return the result of the request
        /// </summary>
        /// <param name="options">Options to security connection</param>
        /// <param name="handler">Handler name used in solr request</param>
        /// <param name="data">Data to execute</param>
        /// <returns>Result of the request</returns>
        public string Post(SecurityOptions options, string handler, string data)
        {
            var request = this.Prepare(options, "POST", handler, data);

            return this.Execute(request, data);
        }
#endif

            /// <summary>
            /// Solr host address
            /// </summary>
        public string HostAddress { get; set; }
    }
}
