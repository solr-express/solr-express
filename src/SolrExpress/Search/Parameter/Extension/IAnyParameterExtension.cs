namespace SolrExpress.Search.Parameter.Extension
{
    /// <summary>
    /// Extensions to configure any parameter
    /// </summary>
    public static class IAnyParameterExtension
    {
        /// <summary>
        /// Configure name of parameter
        /// </summary>
		/// <param name="parameter">Parameter to configure</param>
        /// <param name="name">Name of parameter</param>
        public static IAnyParameter Name(this IAnyParameter parameter, string name)
        {
            parameter.Name = name;

            return parameter;
        }

        /// <summary>
        /// Configure value of parameter
        /// </summary>
		/// <param name="parameter">Parameter to configure</param>
        /// <param name="value">Value of parameter</param>
        public static IAnyParameter Value(this IAnyParameter parameter, string value)
        {
            parameter.Value = value;

            return parameter;
        }
    }
}