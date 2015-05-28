using RestSharp;
using SolrExpress.Exception;
using SolrExpress.Query;
using System.Net;

namespace SolrExpress.Solr4
{
    /// <summary>
    /// SOLR access provider
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
        /// Execute the informated uri and return the result of the request
        /// </summary>
        /// <param name="expressionToRequest">Expression created basead in the commands triggereds</param>
        /// <returns>Result of the request</returns>
        public string Execute(string expressionToRequest)
        {
            var client = new RestClient(this._solrHost);

            var request = new RestRequest(string.Concat("query?", expressionToRequest), Method.GET);
            request.AddParameter("omitHeader", "true");
            request.AddParameter("echoParams", "none");

            var response = client.Execute(request);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new UnexpectedJsonQueryException(response.Content);
            }

            return response.Content;
        }
    }
}
