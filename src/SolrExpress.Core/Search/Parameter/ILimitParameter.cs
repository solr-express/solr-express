namespace SolrExpress.Core.Search.Parameter
{
    /// <summary>
    /// Signatures to use in limit parameter
    /// </summary>
    public interface ILimitParameter<TDocument> : ISearchParameter
        where TDocument : IDocument
    {
        /// <summary>
        /// Configure current instance
        /// </summary>
        /// <param name="value">Value of limit</param>
        ILimitParameter<TDocument> Configure(long value);

        /// <summary>
        /// Value of limit
        /// </summary>
        long Value { get; }
    }
}
