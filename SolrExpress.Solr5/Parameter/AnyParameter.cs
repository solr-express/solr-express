using Newtonsoft.Json.Linq;
using SolrExpress.Core.Parameter;
using SolrExpress.Core.Query;

namespace SolrExpress.Solr5.Parameter
{
    public sealed class AnyParameter : IAnyParameter, IParameter<JObject>
    {
        private readonly string _name;
        private readonly string _value;

        /// <summary>
        /// Create any parameter
        /// </summary>
        /// <param name="name">Name of the parameter</param>
        /// <param name="value">Value of the parameter</param>
        public AnyParameter(string name, string value)
        {
            this._name = name;
            this._value = value;
        }

        /// <summary>
        /// True to indicate multiple instances of the parameter, otherwise false
        /// </summary>
        public bool AllowMultipleInstances { get; } = true;

        /// <summary>
        /// Execute the creation of the parameter
        /// </summary>
        /// <param name="jObject">JSON object with parameters to request to SOLR</param>
        public void Execute(JObject jObject)
        {
            var jObj = (JObject)jObject["params"] ?? new JObject();
           
            jObj.Add(new JProperty(this._name, this._value));

            jObject["params"] = jObj;
        }
    }
}
