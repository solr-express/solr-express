using SolrExpress.Core.Query;

namespace SolrExpress.Core.ParameterValue
{
    //TODO: Create unit tests

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
            this._value = value;
        }

        /// <summary>
        /// Execute the parameter value generator
        /// </summary>
        /// <returns>Result of the value generator</returns>
        public string Execute()
        {
            return _value;
        }
    }
}
