using SolrExpress.Search.Query;

namespace SolrExpress.Search.Parameter.Extension
{
    /// <summary>
    /// Extensions to configure in filter parameter
    /// </summary>
    public static class IFilterParameterExtension
    {
        /// <summary>
        /// Configure value of filter
        /// </summary>
		/// <param name="parameter">Parameter to configure</param>
        /// <param name="query">Value of filter</param>
        public static IFilterParameter<TDocument> Query<TDocument>(this IFilterParameter<TDocument> parameter, SearchQuery<TDocument> query)
            where TDocument : Document
        {
            parameter.Query = query;

            return parameter;
        }

        /// <summary>
        /// Configure tag name to use in facet excluding list
        /// </summary>
		/// <param name="parameter">Parameter to configure</param>
        /// <param name="value">Tag name to use in facet excluding list</param>
        public static IFilterParameter<TDocument> TagName<TDocument>(this IFilterParameter<TDocument> parameter, string value)
            where TDocument : Document
        {
            parameter.TagName = value;

            return parameter;
        }
    }
}
