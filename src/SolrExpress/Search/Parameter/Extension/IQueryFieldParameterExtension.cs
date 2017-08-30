namespace SolrExpress.Search.Parameter.Extension
{
    /// <summary>
    /// Extensions to configure field parameter
    /// </summary>
    public static class IQueryFieldParameterExtension
    {
        /// <summary>
        /// Configure query used to make query field
        /// </summary>
		/// <param name="parameter">Parameter to configure</param>
        /// <param name="expression">Query used to make query field</param>
        public static IQueryFieldParameter<TDocument> Expression<TDocument>(this IQueryFieldParameter<TDocument> parameter, string expression)
            where TDocument : Document
        {
            parameter.Expression = expression;

            return parameter;
        }
    }
}
