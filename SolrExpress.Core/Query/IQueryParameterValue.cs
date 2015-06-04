namespace SolrExpress.Core.Query
{
    /// <summary>
    /// Signatures to use in solr query parameter value
    /// </summary>
    public interface IQueryParameterValue
    {
        /// <summary>
        /// Execute parameter value generator
        /// </summary>
        /// <returns>Result of the value generator</returns>
        string Execute();
    }
}
