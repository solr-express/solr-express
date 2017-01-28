using SolrExpress.Search.Parameter;
using SolrExpress.Search.Query;

namespace SolrExpress.Extension
{
    /// <summary>
    /// Extensions to configure in filter parameter
    /// </summary>
    public static class IFilterParameterExtension
    {
        /// <summary>
        /// Configure value of filter
        /// </summary>
		/// <param name="parameter">Parameter to congigure</param>
        /// <param name="query">Value of filter</param>
        public static IFilterParameter<TDocument> Query<TDocument>(this IFilterParameter<TDocument> parameter, ISearchQuery<TDocument> query)
            where TDocument : IDocument
        {
            parameter.Query = query;

            return parameter;
        }

        /// <summary>
        /// Configure tag name to use in facet excluding list
        /// </summary>
		/// <param name="parameter">Parameter to congigure</param>
        /// <param name="tagName">Tag name to use in facet excluding list</param>
        public static IFilterParameter<TDocument> TagName<TDocument>(this IFilterParameter<TDocument> parameter, string tagName)
            where TDocument : IDocument
        {
            parameter.TagName = tagName;

            return parameter;
        }
    }
}
