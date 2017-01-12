namespace SolrExpress.Core.Search.Parameter
{
    /// <summary>
    /// Signatures to use in offset parameter
    /// </summary>
    public interface IOffsetParameter<TDocument> : ISearchParameter
        where TDocument : IDocument
    {
        /// <summary>
        /// Configure current instance
        /// </summary>
        /// <param name="value">Value of limit</param>
        /// <returns></returns>
        IOffsetParameter<TDocument> Configure(long value);

        /// <summary>
        /// Value of limit
        /// </summary>
        long Value { get; }
    }
}
