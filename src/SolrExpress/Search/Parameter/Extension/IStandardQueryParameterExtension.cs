using SolrExpress.Search.Query;

namespace SolrExpress.Search.Parameter.Extension
{
    /// <summary>
    /// Extensions to configure standard query parameter
    /// </summary>
    public static class IStandardQueryParameterExtension
    {
        /// <summary>
        /// Configure parameter to include in standard query
        /// </summary>
		/// <param name="parameter">Parameter to configure</param>
        /// <param name="searchQuery">Parameter to include in standard query</param>
        public static IStandardQueryParameter<TDocument> Value<TDocument>(this IStandardQueryParameter<TDocument> parameter, SearchQuery<TDocument> searchQuery)
            where TDocument : Document
        {
            parameter.Value = searchQuery;

            return parameter;
        }
    }
}
