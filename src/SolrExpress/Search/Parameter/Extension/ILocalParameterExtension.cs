using SolrExpress.Search.Query;

namespace SolrExpress.Search.Parameter.Extension
{
    /// <summary>
    /// Extensions to configure local parameter
    /// </summary>
    public static class ILocalParameterExtension
    {
        /// <summary>
        /// Configure name of parameter added in query
        /// </summary>
        /// <param name="parameter">Parameter to configure</param>
        /// <param name="value">Name of parameter added in query</param>
        public static ILocalParameter<TDocument> Name<TDocument>(this ILocalParameter<TDocument> parameter, string value)
            where TDocument : Document
        {
            parameter.Name = value;

            return parameter;
        }

        /// <summary>
        /// Configure value to include in query
        /// </summary>
        /// <param name="parameter">Parameter to configure</param>
        /// <param name="query">Value to include in query</param>
        public static ILocalParameter<TDocument> Query<TDocument>(this ILocalParameter<TDocument> parameter, SearchQuery<TDocument> query)
            where TDocument : Document
        {
            parameter.Query = query;

            return parameter;
        }

        /// <summary>
        /// Configure plain value to include in query
        /// </summary>
        /// <param name="parameter">Parameter to configure</param>
        /// <param name="value">Plain value to include in query</param>
        public static ILocalParameter<TDocument> Value<TDocument>(this ILocalParameter<TDocument> parameter, string value)
            where TDocument : Document
        {
            parameter.Value = value;

            return parameter;
        }
    }
}
