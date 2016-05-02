using Newtonsoft.Json.Linq;
using SolrExpress.Core.Query.Parameter;

namespace SolrExpress.Solr5.Query.Parameter
{
    public sealed class OffsetParameter : IOffsetParameter, IParameter<JObject>
    {
        private int _value;

        /// <summary>
        /// True to indicate multiple instances of the parameter, otherwise false
        /// </summary>
        public bool AllowMultipleInstances { get; } = false;

        /// <summary>
        /// Execute the creation of the parameter "offset"
        /// </summary>
        /// <param name="jObject">JSON object with parameters to request to SOLR</param>
        public void Execute(JObject jObject)
        {
            jObject["offset"] = new JValue(_value);
        }

        /// <summary>
        /// Configure current instance
        /// </summary>
        /// <param name="value">Value of limit</param>
        /// <returns></returns>
        public IOffsetParameter Configure(int value)
        {
            this._value = value;

            return this;
        }
    }
}
