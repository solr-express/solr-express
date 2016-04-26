using Newtonsoft.Json.Linq;
using SolrExpress.Core.Query.Parameter;

namespace SolrExpress.Solr5.Parameter
{
    public sealed class LimitParameter : ILimitParameter, IParameter<JObject>
    {
        private int _value;

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

        /// <summary>
        /// Configure current instance
        /// </summary>
        /// <param name="value">Value of limit</param>
        public ILimitParameter Configure(int value)
        {
            this._value = value;

            return this;
        }
    }
}
