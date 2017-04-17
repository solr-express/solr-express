namespace SolrExpress.Search.Parameter.Extension
{
    /// <summary>
    /// Extensions to configure in facet limit parameter
    /// </summary>
    public static class IFacetLimitParameterExtension
    {
        /// <summary>
        /// Configure value of limit
        /// </summary>
		/// <param name="parameter">Parameter to congigure</param>
        /// <param name="value">Value of limit</param>
        public static IFacetLimitParameter<TDocument> Value<TDocument>(this IFacetLimitParameter<TDocument> parameter, long value)
            where TDocument : IDocument
        {
            parameter.Value = value;

            return parameter;
        }
    }
}