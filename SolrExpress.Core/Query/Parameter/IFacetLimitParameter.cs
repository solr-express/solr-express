namespace SolrExpress.Core.Query.Parameter
{
    /// <summary>
    /// Signatures to use in facet limit parameter
    /// </summary>
    public interface IFacetLimitParameter : IParameter
    {
        /// <summary>
        /// Configure current instance
        /// </summary>
        /// <param name="value">Value of limit</param>
        IFacetLimitParameter Configure(int value);
    }
}
