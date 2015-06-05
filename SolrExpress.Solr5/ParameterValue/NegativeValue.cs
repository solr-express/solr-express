using SolrExpress.Core.Query;

namespace SolrExpress.Solr5.ParameterValue
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
            this._value = parameterValue;
        }

        /// <summary>
        /// Execute parameter value generator
        /// </summary>
        /// <returns>Result of the value generator</returns>
        public string Execute()
        {
            return string.Concat("-", _value.Execute());
        }
    }
}