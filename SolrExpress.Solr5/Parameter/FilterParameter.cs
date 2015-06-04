using Newtonsoft.Json.Linq;
using SolrExpress.Core.Query;

namespace SolrExpress.Solr5.Parameter
{
    public sealed class FilterParameter<T> : IQueryParameter
        where T : IDocument
    {
        private readonly SolrExpression _expression;

        /// <summary>
        /// Create a filter parameter
        /// </summary>
        /// <param name="expression">Expression used to create the SOLR query</param>
        /// <param name="value">Value of the filter</param>
        public FilterParameter(SolrExpression<T> expression)
        {
            this._expression = expression;
        }

        /// <summary>
        /// True to indicate multiple instance of the parameter, otherwise false
        /// </summary>
        public bool AllowMultipleInstances { get { return true; } }

        /// <summary>
        /// Execute the creation of the parameter "sort"
        /// </summary>
        /// <param name="jObject">JSON object with parameters to request to SOLR</param>
        public void Execute(JObject jObject)
        {
            var jArray = (JArray)jObject["filter"] ?? new JArray();

            jArray.Add(this._expression.Resolve());

            jObject["filter"] = jArray;
        }
    }
}
