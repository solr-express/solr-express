namespace SolrExpress.Core.Search.Parameter
{
    /// <summary>
    /// Signatures to use in filter parameter
    /// </summary>
    public interface IFilterParameter<TDocument> : ISearchParameter
        where TDocument : IDocument
    {
        /// <summary>
        /// Configure current instance
        /// </summary>
        /// <param name="value">Value of the filter</param>
        /// <param name="tagName">Tag name to use in facet excluding list</param>
        IFilterParameter<TDocument> Configure(ISearchParameterValue<TDocument> value, string tagName = null);

        /// <summary>
        /// Value of the filter
        /// </summary>
        ISearchParameterValue<TDocument> Value { get; }

        /// <summary>
        /// Tag name to use in facet excluding list
        /// </summary>
        string TagName { get; }
    }
}
