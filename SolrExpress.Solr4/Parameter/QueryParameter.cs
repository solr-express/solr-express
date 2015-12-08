using SolrExpress.Core.Helper;
using SolrExpress.Core.Parameter;
using SolrExpress.Core.Query;
using System;
using System.Collections.Generic;

namespace SolrExpress.Solr4.Parameter
{
    public sealed class QueryParameter : IQueryParameter, IParameter<List<string>>
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
        /// <param name="container">Container to parameters to request to SOLR</param>
        public void Execute(List<string> container)
        {
            container.Add($"q={this._value.Execute()}");
        }
    }
}
