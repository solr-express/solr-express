namespace SolrExpress.Core.Search.ParameterValue
{
    /// <summary>
    /// Query all value parameter
    /// </summary>
    public sealed class QueryAll : ISearchParameterValue
    {
        /// <summary>
        /// Execute the parameter value generator
        /// </summary>
        /// <returns>Result generated value</returns>
        public string Execute() => "*:*";
    }
}
