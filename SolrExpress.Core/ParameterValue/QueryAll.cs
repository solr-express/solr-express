namespace SolrExpress.Core.ParameterValue
{
    /// <summary>
    /// Query all value parameter
    /// </summary>
    public sealed class QueryAll : IQueryParameterValue
    {
        /// <summary>
        /// Execute the parameter value generator
        /// </summary>
        /// <returns>Result generated value</returns>
        public string Execute() => "*:*";
    }
}
