namespace SolrExpress.Search.Parameter
{
    /// <summary>
    /// Signatures to use in offset parameter
    /// </summary>
    public interface IOffsetParameter<TDocument> : ISearchParameter
        where TDocument : IDocument
    {
        /// <summary>
        /// Value of offset
        /// </summary>
        long Value { get; set; }
    }
}
