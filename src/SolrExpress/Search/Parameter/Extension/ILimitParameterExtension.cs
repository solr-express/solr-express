namespace SolrExpress.Search.Parameter.Extension
{
    /// <summary>
    /// Extensions to configure in limit parameter
    /// </summary>
    public static class ILimitParameterExtension
    {
        /// <summary>
        /// Configure value of limit
        /// </summary>
		/// <param name="parameter">Parameter to congigure</param>
        /// <param name="value">Value of limit</param>
        public static ILimitParameter<TDocument> Value<TDocument>(this ILimitParameter<TDocument> parameter, long value)
            where TDocument : Document
        {
            parameter.Value = value;

            return parameter;
        }
    }
}
