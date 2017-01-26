namespace SolrExpress.Search.Parameter
{
    /// <summary>
    /// Extensions to configure in query parameter
    /// </summary>
    public static class IQueryParameterExtension
    {
        /// <summary>
        /// Configure parameter to include in query
        /// </summary>
		/// <param name="parameter">Parameter to congigure</param>
        /// <param name="value">Parameter to include in query</param>
        public static IQueryParameter<TDocument> Value<TDocument>(this IQueryParameter<TDocument> parameter, ISearchQuery<TDocument> value)
            where TDocument : IDocument
        {
            parameter.Value = value;

            return parameter;
        }
    }
}
