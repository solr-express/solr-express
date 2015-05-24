using Newtonsoft.Json.Linq;
using SolrExpress.QueryBuilder;

namespace SolrExpress.Solr5.Parameter
{
    public class LimitParameter : IQueryParameter
    {
        private int _value;

        /// <summary>
        /// Create a limit parameter
        /// </summary>
        /// <param name="value">Parameter to include in the query</param>
        public LimitParameter(int value)
        {
            this._value = value;
        }

        /// <summary>
        /// True to indicate multiple instance of the parameter, otherwise false
        /// </summary>
        public bool AllowMultipleInstance { get { return false; } }

        /// <summary>
        /// Parameter name
        /// </summary>
        public string ParameterName { get { return "limit"; } }

        /// <summary>
        /// Execute the creation of the parameter "limit"
        /// </summary>
        /// <param name="jObject">JSON object with parameters to request to SOLR</param>
        public void Execute(JObject jObject)
        {
            jObject[this.ParameterName] = new JValue(_value);
        }
    }
}
