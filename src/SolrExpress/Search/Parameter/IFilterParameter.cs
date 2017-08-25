using SolrExpress.Search.Query;

namespace SolrExpress.Search.Parameter
{
    /// <summary>
    /// Filter parameter
    /// </summary>
    public interface IFilterParameter<TDocument> : ISearchParameter
        where TDocument : Document
    {
        /// <summary>
        /// Value of filter
        /// </summary>
        SearchQuery<TDocument> Query { get; set; }

        /// <summary>
        /// Tag name to use in facet excluding list
        /// </summary>
        string TagName { get; set; }
    }
}
