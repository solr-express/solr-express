using Newtonsoft.Json.Linq;
using SolrExpress.Core;
using SolrExpress.Core.Query.Parameter;

namespace SolrExpress.Solr5.Query.Parameter
{
    public sealed class MinimumShouldMatchParameter : IMinimumShouldMatchParameter, IParameter<JObject>
    {
        private string _expression;

        /// <summary>
        /// True to indicate multiple instances of the parameter, otherwise false
        /// </summary>
        public bool AllowMultipleInstances { get; } = false;

        /// <summary>
        /// Execute the creation of the parameter "query field"
        /// </summary>
        /// <param name="jObject">JSON object with parameters to request to SOLR</param>
        public void Execute(JObject jObject)
        {
            var jObj = (JObject)jObject["params"] ?? new JObject();
            var jProperty = new JProperty("mm", this._expression);

            jObj.Add(jProperty);

            jObject["params"] = jObj;
        }

        /// <summary>
        /// Configure current instance
        /// </summary>
        /// <param name="expression">Expression used to make the mm parameter</param>
        public IMinimumShouldMatchParameter Configure(string expression)
        {
            Checker.IsNullOrWhiteSpace(expression);

            this._expression = expression;

            return this;
        }
    }
}
