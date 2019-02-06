namespace SolrExpress.Search.Parameter.Extension
{
    /// <summary>
    /// Extensions to configure in minimum should match parameter
    /// </summary>
    public static class IMinimumShouldMatchParameterExtension
    {
        /// <summary>
        /// Configure value used to make mm parameter
        /// </summary>
		/// <param name="parameter">Parameter to configure</param>
        /// <param name="str">Value used to make mm parameter</param>
        public static IMinimumShouldMatchParameter<TDocument> Value<TDocument>(this IMinimumShouldMatchParameter<TDocument> parameter, string str)
            where TDocument : Document
        {
            parameter.Value = str;

            return parameter;
        }
    }
}
