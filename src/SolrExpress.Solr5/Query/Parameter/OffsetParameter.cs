using Newtonsoft.Json.Linq;
using SolrExpress.Core.Query.Parameter;

namespace SolrExpress.Solr5.Query.Parameter
{
    public sealed class OffsetParameter : IOffsetParameter, IParameter<JObject>
    {
        /// <summary>
        /// True to indicate multiple instances of the parameter, otherwise false
        /// </summary>
        public bool AllowMultipleInstances { get; } = false;

        /// <summary>
        /// Value of limit
        /// </summary>
        public int Value { get; private set; }

        /// <summary>
        /// Execute the creation of the parameter "offset"
        /// </summary>
        /// <param name="jObject">JSON object with parameters to request to SOLR</param>
        public void Execute(JObject jObject)
        {
            jObject["offset"] = new JValue(this.Value);
        }

        /// <summary>
        /// Configure current instance
        /// </summary>
        /// <param name="value">Value of limit</param>
        /// <returns></returns>
        public IOffsetParameter Configure(int value)
        {
            this.Value = value;

            return this;
        }
    }
}
