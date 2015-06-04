using Newtonsoft.Json.Linq;
using SolrExpress.Core.Query;

namespace SolrExpress.Solr5.Parameter
{
    public sealed class SpatialFilterParameter<T> : IParameter
            where T : IDocument
    {
        private readonly SolrExpression _expression;

        /// <summary>
        /// Create a spatial filter parameter
        /// </summary>
        /// <param name="expression">Expression used to create the SOLR query</param>
        /// <param name="value">Value of the filter</param>
        public SpatialFilterParameter(SolrExpression<T> expression)
        {
            this._expression = expression;
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

            var jProperty = new JProperty("fq", this._expression.Resolve());

            jObj.Add(jProperty);

            jObject["params"] = jObj;
        }
    }
}
