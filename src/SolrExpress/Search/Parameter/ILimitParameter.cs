namespace SolrExpress.Search.Parameter
{
    /// <summary>
    /// Limit parameter
    /// </summary>
    public interface ILimitParameter<TDocument> : ISearchParameter
        where TDocument : Document
    {
        /// <summary>
        /// Value of limit
        /// </summary>
        long Value { get; set; }
    }
}
