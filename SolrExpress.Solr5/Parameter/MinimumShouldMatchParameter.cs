using Newtonsoft.Json.Linq;
using SolrExpress.Core.Helper;
using SolrExpress.Core.Query;
using System;

namespace SolrExpress.Solr5.Parameter
{
    public sealed class MinimumShouldMatchParameter : IParameter<JObject>
    {
        private readonly string _expression;

        /// <summary>
        /// Create a minimun should parameter parameter
        /// </summary>
        /// <param name="expression">Expression used to make the mm parameter</param>
        public MinimumShouldMatchParameter(string expression)
        {
            ThrowHelper<ArgumentNullException>.If(string.IsNullOrWhiteSpace(expression));

            this._expression = expression;
        }

        /// <summary>
        /// True to indicate multiple instances of the parameter, otherwise false
        /// </summary>
        public bool AllowMultipleInstances { get { return false; } }

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
    }
}
