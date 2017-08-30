namespace SolrExpress.Search.Parameter.Extension
{
    /// <summary>
    /// Extensions to configure query parser parameter
    /// </summary>
    public static class IQueryParserParameterExtension
    {
        /// <summary>
        /// Configure query parser type used in SOLR's search
        /// </summary>
		/// <param name="parameter">Parameter to configure</param>
        /// <param name="value">Query parser type used in SOLR's search</param>
        public static IQueryParserParameter<TDocument> Value<TDocument>(this IQueryParserParameter<TDocument> parameter, QueryParserType value)
            where TDocument : Document
        {
            parameter.Value = value;

            return parameter;
        }
    }
}
