namespace SolrExpress.Search.Parameter
{
    /// <summary>
    /// Signatures to use in filter parameter
    /// </summary>
    public interface IFilterParameter<TDocument> : ISearchParameter
        where TDocument : IDocument
    {
        /// <summary>
        /// Configure value of filter
        /// </summary>
        /// <param name="query">Value of filter</param>
        IFilterParameter<TDocument> Query(ISearchQuery<TDocument> query);

        /// <summary>
        /// Configure tag name to use in facet excluding list
        /// </summary>
        /// <param name="tagName">Tag name to use in facet excluding list</param>
        IFilterParameter<TDocument> TagName(string tagName);
    }
}
