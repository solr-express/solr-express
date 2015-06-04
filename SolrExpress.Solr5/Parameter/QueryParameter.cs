using Newtonsoft.Json.Linq;
using SolrExpress.Core.Query;

namespace SolrExpress.Solr5.Parameter
{
    public sealed class QueryParameter<T> : IParameter
        where T : IDocument
    {
        private readonly SolrExpression _expression;

        /// <summary>
        /// Create a query parameter
        /// </summary>
        /// <param name="expression">Expression used to create the SOLR query</param>
        public QueryParameter(SolrExpression expression)
        {
            this._expression = expression;
        }

        /// <summary>
        /// Create a query parameter
        /// </summary>
        /// <param name="expression">Expression used to create the SOLR query</param>
        public QueryParameter(SolrExpression<T> expression)
        {
            this._expression = expression;
        }

        /// <summary>
        /// True to indicate multiple instance of the parameter, otherwise false
        /// </summary>
        public bool AllowMultipleInstances { get { return false; } }

        /// <summary>
        /// Execute the creation of the parameter "limit"
        /// </summary>
        /// <param name="jObject">JSON object with parameters to request to SOLR</param>
        public void Execute(JObject jObject)
        {
            jObject["query"] = new JValue(this._expression.Resolve());
        }
    }
}
