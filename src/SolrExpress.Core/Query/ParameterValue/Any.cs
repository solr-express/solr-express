namespace SolrExpress.Core.Query.ParameterValue
{
    /// <summary>
    /// Free value parameter
    /// </summary>
    public sealed class Any : IQueryParameterValue
    {
        /// <summary>
        /// Create a free solr parameter value
        /// </summary>
        /// <param name="value">Value of filter</param>
        public Any(string value)
        {
            Checker.IsNullOrWhiteSpace(value);

            this.Value = value;
        }

        /// <summary>
        /// Execute the parameter value generator
        /// </summary>
        /// <returns>Result generated value</returns>
        public string Execute()
        {
            return Value;
        }

        /// <summary>
        /// Value of filter
        /// </summary>
        public string Value { get; private set; }
    }
}
