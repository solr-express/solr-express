using Newtonsoft.Json.Linq;
using SolrExpress.Core.Query;

namespace SolrExpress.Solr5.Parameter
{
    public sealed class MinimumShouldMatchParameter: IParameter
    {
        private readonly JProperty _value;

        /// <summary>
        /// Create a minimun should parameter parameter
        /// </summary>
        /// <param name="expression">Expression used to make the mm parameter</param>
        public MinimumShouldMatchParameter(string expression)
        {
            this._value = new JProperty("mm", expression);
        }

        /// <summary>
        /// True to indicate multiple instance of the parameter, otherwise false
        /// </summary>
        public bool AllowMultipleInstances { get { return true; } }

        /// <summary>
        /// Execute the creation of the parameter "query field"
        /// </summary>
        /// <param name="jObject">JSON object with parameters to request to SOLR</param>
        public void Execute(JObject jObject)
        {
            var jObj = (JObject)jObject["params"] ?? new JObject();

            jObj.Add(this._value);

            jObject["params"] = jObj;
        }
    }
}
