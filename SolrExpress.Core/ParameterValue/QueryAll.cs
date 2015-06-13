using SolrExpress.Core.Query;

namespace SolrExpress.Core.ParameterValue
{
    //TODO: Create unit tests

    /// <summary>
    /// Query all value parameter
    /// </summary>
    public sealed class QueryAll : IQueryParameterValue
    {
        /// <summary>
        /// Execute the parameter value generator
        /// </summary>
        /// <returns>Result of the value generator</returns>
        public string Execute()
        {
            return "*:*";
        }
    }
}
