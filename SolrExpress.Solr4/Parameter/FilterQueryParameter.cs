using SolrExpress.Core.Helper;
using SolrExpress.Core.Query;
using System;
using System.Collections.Generic;

namespace SolrExpress.Solr4.Parameter
{
    public sealed class FilterQueryParameter : IParameter<List<string>>
    {
        private readonly IQueryParameterValue _value;

        /// <summary>
        /// Create a filter parameter
        /// </summary>
        /// <param name="value">Parameter value used to create the query</param>
        public FilterQueryParameter(IQueryParameterValue value)
        {
            ThrowHelper<ArgumentNullException>.If(value == null);

            this._value = value;
        }

        /// <summary>
        /// True to indicate multiple instances of the parameter, otherwise false
        /// </summary>
        public bool AllowMultipleInstances { get { return true; } }

        /// <summary>
        /// Execute the creation of the parameter "fq"
        /// </summary>
        /// <param name="container">Container to parameters to request to SOLR</param>
        public void Execute(List<string> container)
        {
            container.Add(string.Concat("fq=", this._value.Execute()));
        }
    }
}
