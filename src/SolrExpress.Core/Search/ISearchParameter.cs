namespace SolrExpress.Core.Search
{
    /// <summary>
    /// Signatures to use in solr parameter
    /// </summary>
    public interface ISearchParameter : ISearchItem
    {
        /// <summary>
        /// True to indicate multiple instance of the parameter, otherwise false
        /// </summary>
        bool AllowMultipleInstances { get; }
    }
}
