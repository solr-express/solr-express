using Newtonsoft.Json.Linq;
using SolrExpress.Core.Query;
using System;
using System.Diagnostics.Contracts;

namespace SolrExpress.Solr5.Parameter
{
    public sealed class QueryFieldParameter : IParameter<JObject>
    {
        private readonly string _query;

        /// <summary>
        /// Create a query field parameter
        /// </summary>
        /// <param name="query">Query used to make the query field</param>
        public QueryFieldParameter(string query)
        {
            Contract.Requires<ArgumentNullException>(!string.IsNullOrWhiteSpace(query));

            this._query = query;
        }

        /// <summary>
        /// True to indicate multiple instances of the parameter, otherwise false
        /// </summary>
        public bool AllowMultipleInstances { get { return false; } }

        /// <summary>
        /// Execute the creation of the parameter "query field"
        /// </summary>
        /// <param name="jObject">JSON object with parameters to request to SOLR</param>
        public void Execute(JObject jObject)
        {
            var jObj = (JObject)jObject["params"] ?? new JObject();
            var jProperty = new JProperty("qf", this._query);

            jObj.Add(jProperty);

            jObject["params"] = jObj;
        }
    }
}
