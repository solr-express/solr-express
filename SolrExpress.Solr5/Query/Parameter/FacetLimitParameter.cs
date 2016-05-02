using Newtonsoft.Json.Linq;
using SolrExpress.Core.Query.Parameter;

namespace SolrExpress.Solr5.Query.Parameter
{
    public sealed class FacetLimitParameter : IFacetLimitParameter, IParameter<JObject>
    {
        private int _value;

        /// <summary>
        /// True to indicate multiple instances of the parameter, otherwise false
        /// </summary>
        public bool AllowMultipleInstances { get; } = false;

        /// <summary>
        /// Execute the creation of the parameter "rows"
        /// </summary>
        /// <param name="jObject">JSON object with parameters to request to SOLR</param>
        public void Execute(JObject jObject)
        {
            var jObj = (JObject)jObject["params"] ?? new JObject();
            var jProperty = new JProperty("facet.limit", this._value);

            jObj.Add(jProperty);

            jObject["params"] = jObj;
        }

        /// <summary>
        /// Configure current instance
        /// </summary>
        /// <param name="value">Value of limit</param>
        public IFacetLimitParameter Configure(int value)
        {
            this._value = value;

            return this;
        }
    }
}
