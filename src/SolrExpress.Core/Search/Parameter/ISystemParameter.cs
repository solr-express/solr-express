namespace SolrExpress.Core.Search.Parameter
{
    /// <summary>
    /// Signatures to use in internal use
    /// </summary>
    public interface ISystemParameter : ISearchParameter
    {
        /// <summary>
        /// Configure current instance
        /// </summary>
        ISystemParameter Configure();
    }
}
