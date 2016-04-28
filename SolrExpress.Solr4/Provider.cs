using SolrExpress.Core;
using SolrExpress.Core.Query.Parameter;
using SolrExpress.Core.Update;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;

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
        /// Execute the parameters and return the formed solr query
        /// </summary>
        /// <param name="parameters">List of the parameters arranged in the queryable class</param>
        /// <returns>Solr query</returns>
        public string GetQueryInstruction(List<IParameter> parameters)
        {
            var list = new List<string>();

            foreach (var item in parameters.OrderBy(q => q.GetType().ToString()))
            {
                ((IParameter<List<string>>)item).Execute(list);
            }

            return string.Join("&", list);
        }

        /// <summary>
        /// Execute the atomic update commands and return the formed solr query
        /// </summary>
        /// <param name="atomicUpdate">Atomic update to be executed</param>
        /// <param name="atomicDelete">Atomic delete to be executed</param>
        /// <returns>Solr query</returns>
        public string GetAtomicUpdateInstruction(IAtomicUpdate atomicUpdate = null, IAtomicDelete atomicDelete = null)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Execute the informated uri and return the result of the request
        /// </summary>
        /// <param name="handler">Handler name used in solr request</param>
        /// <param name="instruction">Solr query</param>
        /// <returns>Result of the request</returns>
        public string Execute(string handler, string instruction)
        {
            var baseUrl = $"{this._solrHost}/{handler}?{instruction}&echoParams=none&wt=json&indent=off";

            var request = WebRequest.Create(baseUrl);
            request.Method = "GET";

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

                Checker.IsTrue<UnexpectedSolrRequestException>(response.StatusCode != HttpStatusCode.OK, content);

                return content;
            }
            catch (System.Exception e)
            {
                throw new UnexpectedSolrRequestException(e.Message);
            }
        }
    }
}
