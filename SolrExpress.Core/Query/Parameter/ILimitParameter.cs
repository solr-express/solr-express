namespace SolrExpress.Core.Query.Parameter
{
    /// <summary>
    /// Signatures to use in limit parameter
    /// </summary>
    public interface ILimitParameter : IParameter
    {
        /// <summary>
        /// Configure current instance
        /// </summary>
        /// <param name="value">Value of limit</param>
        ILimitParameter Configure(int value);
    }
}
