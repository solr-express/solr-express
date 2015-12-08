using Newtonsoft.Json.Linq;
using SolrExpress.Core.Parameter;
using SolrExpress.Core.Query;

namespace SolrExpress.Solr5.Parameter
{
    public sealed class FacetLimitParameter : IFacetLimitParameter, IParameter<JObject>
    {
        private readonly int _value;

        /// <summary>
        /// Create a facet limit parameter
        /// </summary>
        /// <param name="value">Value of the parameter limit</param>
        public FacetLimitParameter(int value)
        {
            this._value = value;
        }

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
    }
}
