using SolrExpress.Search.Query;

namespace SolrExpress.Search.Parameter
{
    /// <summary>
    /// Local parameter
    /// </summary>
    public interface ILocalParameter<TDocument> : ISearchParameter
        where TDocument : Document
    {
        /// <summary>
        /// Name of parameter added in query
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Value to include in query
        /// </summary>
        SearchQuery<TDocument> Query { get; set; }

        /// <summary>
        /// Plain value to include in query
        /// </summary>
        string Value { get; set; }
    }
}
