namespace SolrExpress.Search.Parameter
{
    /// <summary>
    /// Offset parameter
    /// </summary>
    public interface IOffsetParameter<TDocument> : ISearchParameter
        where TDocument : Document
    {
        /// <summary>
        /// Value of offset
        /// </summary>
        long Value { get; set; }
    }
}
