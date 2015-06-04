using Newtonsoft.Json.Linq;
using SolrExpress.Core.Query;

namespace SolrExpress.Solr5.Parameter
{
    public sealed class SpatialFilterParameter : IParameter
    {
        private readonly IQueryParameterValue _value;

        /// <summary>
        /// Create a spatial filter parameter
        /// </summary>
        /// <param name="value">Parameter value used to create the query</param>
        public SpatialFilterParameter(IQueryParameterValue value)
        {
            this._value = value;
        }

        /// <summary>
        /// True to indicate multiple instance of the parameter, otherwise false
        /// </summary>
        public bool AllowMultipleInstances { get { return false; } }

        /// <summary>
        /// Execute the creation of the parameter "sort"
        /// </summary>
        /// <param name="jObject">JSON object with parameters to request to SOLR</param>
        public void Execute(JObject jObject)
        {
            var jObj = (JObject)jObject["params"] ?? new JObject();

            var jProperty = new JProperty("fq", this._value.Execute());

            jObj.Add(jProperty);

            jObject["params"] = jObj;
        }
    }
}
