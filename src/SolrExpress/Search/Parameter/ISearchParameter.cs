namespace SolrExpress.Search.Parameter
{
    /// <summary>
    /// Signatures to use in search parameter
    /// </summary>
    public interface ISearchParameter : ISearchItem
    {
        /// <summary>
        /// True to indicate multiple instance of parameter, otherwise false
        /// </summary>
        bool AllowMultipleInstances { get; set; }
    }
}
