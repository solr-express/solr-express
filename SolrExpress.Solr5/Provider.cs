using Newtonsoft.Json.Linq;
using RestSharp;
using SolrExpress.Core.Exception;
using SolrExpress.Core.Query;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace SolrExpress.Solr5
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
        /// Process the queryable class
        /// </summary>
        /// <param name="parameters">List of the parameters arranged in the queryable class</param>
        /// <returns>JSON string</returns>
        private string ProcessParameters(List<IParameter> parameters)
        {
            var jsonObj = new JObject();

            foreach (var item in parameters.OrderBy(q => q.GetType().ToString()))
            {
                ((IParameter<JObject>)item).Execute(jsonObj);
            }

            return jsonObj.ToString();
        }

        /// <summary>
        /// Process the json
        /// </summary>
        /// <param name="json">Json string used by SOLR JSON Api</param>
        /// <returns>Response from SOLR</returns>
        private string ProcessExpression(string json)
        {
            var client = new RestClient(this._solrHost);

            var request = new RestRequest("query", Method.GET);
            request.AddParameter("echoParams", "none");
            request.AddParameter("json", json);

            var response = client.Execute(request);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new UnexpectedJsonQueryException(response.Content);
            }

            return response.Content;
        }

        /// <summary>
        /// Execute the informated uri and return the result of the request
        /// </summary>
        /// <param name="parameters">List of the parameters arranged in the queryable class</param>
        /// <returns>Result of the request</returns>
        public string Execute(List<IParameter> parameters)
        {
            var expressionToRequest = this.ProcessParameters(parameters);

            return this.ProcessExpression(expressionToRequest);
        }
    }
}
