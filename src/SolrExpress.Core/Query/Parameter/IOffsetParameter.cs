namespace SolrExpress.Core.Query.Parameter
{
    /// <summary>
    /// Signatures to use in offset parameter
    /// </summary>
    public interface IOffsetParameter : IParameter
    {
        /// <summary>
        /// Configure current instance
        /// </summary>
        /// <param name="value">Value of limit</param>
        /// <returns></returns>
        IOffsetParameter Configure(long value);

        /// <summary>
        /// Value of limit
        /// </summary>
        long Value { get; }
    }
}
