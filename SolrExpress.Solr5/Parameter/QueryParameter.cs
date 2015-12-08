using Newtonsoft.Json.Linq;
using SolrExpress.Core.Helper;
using SolrExpress.Core.Parameter;
using SolrExpress.Core.Query;
using System;

namespace SolrExpress.Solr5.Parameter
{
    public sealed class QueryParameter : IQueryParameter, IParameter<JObject>
    {
        private readonly IQueryParameterValue _value;

        /// <summary>
        /// Create a query parameter
        /// </summary>
        /// <param name="value">Parameter value used to create the query</param>
        public QueryParameter(IQueryParameterValue value)
        {
            ThrowHelper<ArgumentNullException>.If(value == null);

            this._value = value;
        }

        /// <summary>
        /// True to indicate multiple instances of the parameter, otherwise false
        /// </summary>
        public bool AllowMultipleInstances { get; } = false;

        /// <summary>
        /// Execute the creation of the parameter "limit"
        /// </summary>
        /// <param name="jObject">JSON object with parameters to request to SOLR</param>
        public void Execute(JObject jObject)
        {
            jObject["query"] = new JValue(this._value.Execute());
        }
    }
}
