using Newtonsoft.Json.Linq;
using SolrExpress.Core.Query;

namespace SolrExpress.Solr5.Parameter
{
    public sealed class QueryParameter : IParameter<JObject>
    {
        private readonly IQueryParameterValue _value;

        /// <summary>
        /// Create a query parameter
        /// </summary>
        /// <param name="value">Parameter value used to create the query</param>
        public QueryParameter(IQueryParameterValue value)
        {
            this._value = value;
        }
        
        /// <summary>
        /// True to indicate multiple instances of the parameter, otherwise false
        /// </summary>
        public bool AllowMultipleInstances { get { return false; } }

        /// <summary>
        /// Execute the creation of the parameter "limit"
        /// </summary>
        /// <param name="jObject">JSON object with parameters to request to SOLR</param>
        public void Execute(JObject jObject)
        {
            jObject["query"] = new JValue(this._value.Execute());
        }
    }
}
