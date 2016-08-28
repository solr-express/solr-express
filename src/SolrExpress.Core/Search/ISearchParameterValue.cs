namespace SolrExpress.Core.Search
{
    /// <summary>
    /// Signature to use in solr query parameter value
    /// </summary>
    public interface ISearchParameterValue
    {
        /// <summary>
        /// Execute parameter value generator
        /// </summary>
        /// <returns>Result generated value</returns>
        string Execute();
    }
}
