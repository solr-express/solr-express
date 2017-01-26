namespace SolrExpress.Search.Parameter
{
    /// <summary>
    /// Extensions to configure any parameter
    /// </summary>
    public static class IAnyParameterExtension
    {
        /// <summary>
        /// Configure name of parameter
        /// </summary>
		/// <param name="parameter">Parameter to congigure</param>
        /// <param name="name">Name of parameter</param>
        public static IAnyParameter<TDocument> Name<TDocument>(this IAnyParameter<TDocument> parameter, string name)
            where TDocument : IDocument
        {
            parameter.Name = name;

            return parameter;
        }

        /// <summary>
        /// Configure value of parameter
        /// </summary>
		/// <param name="parameter">Parameter to congigure</param>
        /// <param name="value">Value of parameter</param>
        public static IAnyParameter<TDocument> Value<TDocument>(this IAnyParameter<TDocument> parameter, string value)
            where TDocument : IDocument
        {
            parameter.Value = value;

            return parameter;
        }
    }
}