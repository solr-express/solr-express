using SolrExpress.Search.Query;

namespace SolrExpress.Search.Parameter.Extension
{
    /// <summary>
    /// Extensions to configure parameter
    /// </summary>
    public static class IQueryParameterExtension
    {
        /// <summary>
        /// Configure parameter to include in query
        /// </summary>
		/// <param name="parameter">Parameter to configure</param>
        /// <param name="searchQuery">Parameter to include in query</param>
        public static IQueryParameter<TDocument> Value<TDocument>(this IQueryParameter<TDocument> parameter, SearchQuery<TDocument> searchQuery)
            where TDocument : Document
        {
            parameter.Value = searchQuery;

            return parameter;
        }
    }
}
