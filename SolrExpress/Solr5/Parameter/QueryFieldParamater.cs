using Newtonsoft.Json.Linq;
using SolrExpress.Query;

namespace SolrExpress.Solr5.Parameter
{
    public sealed class QueryFieldParamater : IQueryParameter
    {
        private readonly JProperty _value;

        /// <summary>
        /// Create a query field parameter
        /// </summary>
        /// <param name="query">Query used to make the query field</param>
        public QueryFieldParamater(string query)
        {
            this._value = new JProperty("qf", query);
        }

        /// <summary>
        /// True to indicate multiple instance of the parameter, otherwise false
        /// </summary>
        public bool AllowMultipleInstances { get { return true; } }

        /// <summary>
        /// Parameter name
        /// </summary>
        public string ParameterName { get { return "params"; } }

        /// <summary>
        /// Execute the creation of the parameter "query field"
        /// </summary>
        /// <param name="jObject">JSON object with parameters to request to SOLR</param>
        public void Execute(JObject jObject)
        {
            var facetObject = (JObject)jObject[this.ParameterName] ?? new JObject();

            facetObject.Add(this._value);

            jObject[this.ParameterName] = facetObject;
        }
    }
}
