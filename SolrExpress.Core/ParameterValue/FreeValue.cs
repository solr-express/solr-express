using SolrExpress.Core.Query;
using System;
using System.Diagnostics.Contracts;

namespace SolrExpress.Core.ParameterValue
{
    /// <summary>
    /// Free value parameter
    /// </summary>
    public sealed class FreeValue : IQueryParameterValue
    {
        private readonly string _value;

        /// <summary>
        /// Create a free solr parameter value
        /// </summary>
        /// <param name="value">Value of the filter</param>
        public FreeValue(string value)
        {
            Contract.Requires<ArgumentNullException>(!string.IsNullOrWhiteSpace(value));

            this._value = value;
        }

        /// <summary>
        /// Execute the parameter value generator
        /// </summary>
        /// <returns>Result generated value</returns>
        public string Execute()
        {
            return _value;
        }
    }
}
