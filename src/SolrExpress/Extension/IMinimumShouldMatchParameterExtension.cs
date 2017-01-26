using SolrExpress.Search.Parameter;

namespace SolrExpress.Extension
{
    /// <summary>
    /// Extensions to configure in minimum should match parameter
    /// </summary>
    public static class IMinimumShouldMatchParameterExtension
    {
        /// <summary>
        /// Configure value used to make mm parameter
        /// </summary>
		/// <param name="parameter">Parameter to congigure</param>
        /// <param name="value">Value used to make mm parameter</param>
        public static IMinimumShouldMatchParameter<TDocument> Value<TDocument>(this IMinimumShouldMatchParameter<TDocument> parameter, string value)
            where TDocument : IDocument
        {
            parameter.Value = value;

            return parameter;
        }
    }
}
