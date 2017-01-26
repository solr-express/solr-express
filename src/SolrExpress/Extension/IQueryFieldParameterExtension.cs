using SolrExpress.Search.Parameter;

namespace SolrExpress.Extension
{
    /// <summary>
    /// Extensions to configure in query field parameter
    /// </summary>
    public static class IQueryFieldParameterExtension
    {
        /// <summary>
        /// Configure query used to make query field
        /// </summary>
		/// <param name="parameter">Parameter to congigure</param>
        /// <param name="expression">Query used to make query field</param>
        public static IQueryFieldParameter<TDocument> Expression<TDocument>(this IQueryFieldParameter<TDocument> parameter, string expression)
            where TDocument : IDocument
        {
            parameter.Expression = expression;

            return parameter;
        }
    }
}
