namespace SolrExpress.Search.Parameter.Extension
{
    /// <summary>
    /// Extensions to configure write type parameter
    /// </summary>
    public static class IWriteTypeParameterExtension
    {
        /// <summary>
        /// Configure write type used in SOLR's result
        /// </summary>
		/// <param name="parameter">Parameter to configure</param>
        /// <param name="writeType">Write type used in SOLR's result</param>
        public static IWriteTypeParameter<TDocument> Value<TDocument>(this IWriteTypeParameter<TDocument> parameter, WriteType writeType)
            where TDocument : Document
        {
            parameter.Value = writeType;

            return parameter;
        }
    }
}
