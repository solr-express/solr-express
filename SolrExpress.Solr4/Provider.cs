using SolrExpress.Core;
using System;
using System.IO;
using System.Net;
using System.Text;

namespace SolrExpress.Solr4
{
    /// <summary>
    /// SOLR 4.9x access provider
    /// </summary>
    public class Provider : IProvider
    {
        private readonly string _solrHost;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="solrHost">Solr host address</param>
        public Provider(string solrHost)
        {
            this._solrHost = solrHost;
        }

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
        /// Execute the informated uri and return the result of the request
        /// </summary>
        /// <param name="handler">Handler name used in solr request</param>
        /// <param name="data">Data to execute</param>
        /// <returns>Result of the request</returns>
        public string Get(string handler, string data)
        {
            var baseUrl = $"{this._solrHost}/{handler}?{data}";

            var encoding = new UTF8Encoding();

            var request = WebRequest.Create(baseUrl);
            request.Method = "GET";

            return this.Execute(request, data);
        }

        /// <summary>
        /// Execute the informated uri and return the result of the request
        /// </summary>
        /// <param name="handler">Handler name used in solr request</param>
        /// <param name="data">Data to execute</param>
        /// <returns>Result of the request</returns>
        public string Post(string handler, string data)
        {
            var baseUrl = $"{this._solrHost}/{handler}";

            var encoding = new UTF8Encoding();
            var bytes = encoding.GetBytes(data);

            var request = WebRequest.Create(baseUrl);
            request.Method = "POST";
            request.ContentType = "application/json";
            request.ContentLength = bytes.Length;

            var stream = request.GetRequestStream();
            stream.Write(bytes, 0, bytes.Length);
            stream.Close();

            return this.Execute(request, data);
        }
    }
}
