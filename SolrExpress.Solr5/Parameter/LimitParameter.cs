using Newtonsoft.Json.Linq;
using SolrExpress.Core.Parameter;
using SolrExpress.Core.Query;

namespace SolrExpress.Solr5.Parameter
{
    public sealed class LimitParameter : ILimitParameter, IParameter<JObject>
    {
        private readonly int _value;

        /// <summary>
        /// Create a limit parameter
        /// </summary>
        /// <param name="value">Parameter to include in the query</param>
        public LimitParameter(int value)
        {
            this._value = value;
        }

        /// <summary>
        /// True to indicate multiples instance of the parameter, otherwise false
        /// </summary>
        public bool AllowMultipleInstances { get; } = false;

        /// <summary>
        /// Execute the creation of the parameter "limit"
        /// </summary>
        /// <param name="jObject">JSON object with parameters to request to SOLR</param>
        public void Execute(JObject jObject)
        {
            jObject["limit"] = new JValue(_value);
        }
    }
}
