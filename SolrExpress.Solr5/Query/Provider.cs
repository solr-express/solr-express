using Newtonsoft.Json.Linq;
using SolrExpress.Core.Exception;
using SolrExpress.Core.Helper;
using SolrExpress.Core.Query;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace SolrExpress.Solr5.Query
{
    /// <summary>
    /// SOLR 5.x access provider
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
        /// Execute the parameters and return the formed solr query
        /// </summary>
        /// <param name="parameters">List of the parameters arranged in the queryable class</param>
        /// <returns>Solr query</returns>
        public string GetQuery(List<IParameter> parameters)
        {
            if (parameters.Any())
            {
                var jsonObj = new JObject();

                foreach (var item in parameters.OrderBy(q => q.GetType().ToString()))
                {
                    ((IParameter<JObject>)item).Execute(jsonObj);
                }

                return jsonObj.ToString();
            }

            return string.Empty;
        }

        /// <summary>
        /// Execute the informated uri and return the result of the request
        /// </summary>
        /// <param name="handler">Handler name used in solr request</param>
        /// <param name="query">Solr query</param>
        /// <returns>Result of the request</returns>
        public string Execute(string handler, string query)
        {
            var baseUrl = $"{this._solrHost}/{handler}?echoParams=none&wt=json&indent=off";

            var encoding = new UTF8Encoding();
            var bytes = encoding.GetBytes(query);

            var request = WebRequest.Create(baseUrl);
            request.Method = "GET-X";
            request.ContentType = "application/json";
            request.ContentLength = bytes.Length;

            var stream = request.GetRequestStream();
            stream.Write(bytes, 0, bytes.Length);
            stream.Close();

            try
            {
                var response = (HttpWebResponse)request.GetResponse();

                string content;

                using (var dataStream = response.GetResponseStream())
                {
                    using (var reader = new StreamReader(dataStream))
                    {
                        content = reader.ReadToEnd();
                    }
                }

                ThrowHelper<UnexpectedJsonQueryException>.If(response.StatusCode != HttpStatusCode.OK, content);

                return content;
            }
            catch (System.Exception e)
            {
                throw new UnexpectedJsonQueryException(e.Message);
            }
        }

        /// <summary>
        /// Handler name used in solr request
        /// </summary>
        public string Handler { get; set; }
    }
}
