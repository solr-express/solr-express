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
		/// <param name="parameter">Parameter to congigure</param>
        /// <param name="value">Parameter to include in standard query</param>
        public static IStandardQueryParameter<TDocument> Value<TDocument>(this IStandardQueryParameter<TDocument> parameter, SearchQuery<TDocument> value)
            where TDocument : Document
        {
            parameter.Value = value;

            return parameter;
        }
    }
}
