using SolrExpress.Search.Query;

namespace SolrExpress.Search.Parameter
{
    /// <summary>
    /// Query parameter
    /// </summary>
    public interface IQueryParameter<TDocument> : ISearchParameter
        where TDocument : Document
    {
        /// <summary>
        /// Parameter to include in query
        /// </summary>
        SearchQuery<TDocument> Value { get; set; }
    }
}
