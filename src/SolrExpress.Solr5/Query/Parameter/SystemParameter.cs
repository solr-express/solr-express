using Newtonsoft.Json.Linq;
using SolrExpress.Core.Query.Parameter;

namespace SolrExpress.Solr5.Query.Parameter
{
    /// <summary>
    /// Internal use
    /// </summary>
    internal class SystemParameter : ISystemParameter, IParameter<JObject>
    {
        /// <summary>
        /// True to indicate multiple instances of the parameter, otherwise false
        /// </summary>
        public bool AllowMultipleInstances { get; } = false;

        /// <summary>
        /// Configure current instance
        /// </summary>
        public ISystemParameter Configure()
        {
            return this;
        }

        /// <summary>
        /// Execute the creation of the parameter
        /// </summary>
        /// <param name="jObject">JSON object with parameters to request to SOLR</param>
        public void Execute(JObject jObject)
        {
            var jObj = (JObject)jObject["params"] ?? new JObject();

            jObj.Add(new JProperty("echoParams", "none"));
            jObj.Add(new JProperty("wt", "json"));
            jObj.Add(new JProperty("indent", "off"));

            jObject["params"] = jObj;
        }
    }
}
