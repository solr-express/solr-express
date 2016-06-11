namespace SolrExpress.Core.Query.ParameterValue
{
    /// <summary>
    /// Free value parameter
    /// </summary>
    public sealed class Any : IQueryParameterValue
    {
        private readonly string _value;

        /// <summary>
        /// Create a free solr parameter value
        /// </summary>
        /// <param name="value">Value of the filter</param>
        public Any(string value)
        {
            Checker.IsNullOrWhiteSpace(value);

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
