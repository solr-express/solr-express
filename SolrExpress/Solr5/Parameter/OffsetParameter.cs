using Newtonsoft.Json.Linq;
using SolrExpress.Query;

namespace SolrExpress.Solr5.Parameter
{
    public class OffsetParameter : IQueryParameter
    {
        private int _value;

        /// <summary>
        /// Create a offset parameter
        /// </summary>
        /// <param name="value">Value of the parameter limit</param>
        public OffsetParameter(int value)
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
        public string ParameterName { get { return "offset"; } }

        /// <summary>
        /// Execute the creation of the parameter "offset"
        /// </summary>
        /// <param name="jObject">JSON object with parameters to request to SOLR</param>
        public void Execute(JObject jObject)
        {
            jObject[this.ParameterName] = new JValue(_value);
        }
    }
}
