using SolrExpress.Core.Query;
using System;
using System.Diagnostics.Contracts;

namespace SolrExpress.Core.ParameterValue
{
    /// <summary>
    /// Result negative form (NOT) value parameter
    /// </summary>
    public sealed class NegativeValue : IQueryParameterValue
    {
        private readonly IQueryParameterValue _value;

        /// <summary>
        /// Create a negative form (NOT) from informed parameter value
        /// </summary>
        /// <param name="parameterValue">Paramater value used to created negative form</param>
        public NegativeValue(IQueryParameterValue parameterValue)
        {
            Contract.Requires<ArgumentNullException>(parameterValue != null);

            this._value = parameterValue;
        }

        /// <summary>
        /// Execute parameter value generator
        /// </summary>
        /// <returns>Result generated value</returns>
        public string Execute()
        {
            return string.Concat("-", _value.Execute());
        }
    }
}