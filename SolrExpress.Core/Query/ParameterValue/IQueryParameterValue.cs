namespace SolrExpress.Core.Query.ParameterValue
{
    /// <summary>
    /// Signature to use in solr query parameter value
    /// </summary>
    public interface IQueryParameterValue
    {
        /// <summary>
        /// Execute parameter value generator
        /// </summary>
        /// <returns>Result generated value</returns>
        string Execute();
    }
}
