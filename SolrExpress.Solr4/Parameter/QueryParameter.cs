using SolrExpress.Core;
using SolrExpress.Core.Parameter;
using SolrExpress.Core.ParameterValue;
using System.Collections.Generic;

namespace SolrExpress.Solr4.Parameter
{
    public sealed class QueryParameter<TDocument> : IQueryParameter<TDocument>, IParameter<List<string>>
        where TDocument : IDocument
    {
        private IQueryParameterValue _value;

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

        /// <summary>
        /// Configure current instance
        /// </summary>
        /// <param name="value">Parameter to include in the query</param>
        public IQueryParameter<TDocument> Configure(IQueryParameterValue value)
        {
            Checker.IsNull(value);

            this._value = value;

            return this;
        }
    }
}
