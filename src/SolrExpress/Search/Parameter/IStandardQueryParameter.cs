using SolrExpress.Search.Query;

namespace SolrExpress.Search.Parameter
{
    /// <summary>
    /// Standard query parameter
    /// </summary>
    public interface IStandardQueryParameter<TDocument> : ISearchParameter
        where TDocument : Document
    {
        /// <summary>
        /// Parameter to include in standard query
        /// </summary>
        SearchQuery<TDocument> Value { get; set; }
    }
}
