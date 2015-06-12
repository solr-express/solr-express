using System.Collections.Generic;
using SolrExpress.Core.Query;

namespace SolrExpress.Solr4.Parameter
{
    public sealed class QueryParameter : IParameter<List<string>>
    {
        private readonly IQueryParameterValue _value;

        /// <summary>
        /// Create a query parameter
        /// </summary>
        /// <param name="value">Parameter value used to create the query</param>
        public QueryParameter(IQueryParameterValue value)
        {
            this._value = value;
        }

        /// <summary>
        /// True to indicate multiple instances of the parameter, otherwise false
        /// </summary>
        public bool AllowMultipleInstances { get { return false; } }

        /// <summary>
        /// Execute the creation of the parameter "limit"
        /// </summary>
        /// <param name="container">Container to parameters to request to SOLR</param>
        public void Execute(List<string> container)
        {
            container.Add(string.Concat("q=", this._value.Execute()));
        }
    }
}
