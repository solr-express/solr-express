using Newtonsoft.Json.Linq;
using SolrExpress.Core;
using SolrExpress.Core.Query.Parameter;
using SolrExpress.Core.Query.ParameterValue;

namespace SolrExpress.Solr5.Query.Parameter
{
    public sealed class QueryParameter<TDocument> : IQueryParameter<TDocument>, IParameter<JObject>
        where TDocument : IDocument
    {
        /// <summary>
        /// True to indicate multiple instances of the parameter, otherwise false
        /// </summary>
        public bool AllowMultipleInstances { get; } = false;

        /// <summary>
        /// Parameter to include in the query
        /// </summary>
        public IQueryParameterValue Value { get; private set; }

        /// <summary>
        /// Execute the creation of the parameter "limit"
        /// </summary>
        /// <param name="jObject">JSON object with parameters to request to SOLR</param>
        public void Execute(JObject jObject)
        {
            jObject["query"] = new JValue(this.Value.Execute());
        }

        /// <summary>
        /// Configure current instance
        /// </summary>
        /// <param name="value">Parameter to include in the query</param>
        public IQueryParameter<TDocument> Configure(IQueryParameterValue value)
        {
            Checker.IsNull(value);

            this.Value = value;

            return this;
        }
    }
}
