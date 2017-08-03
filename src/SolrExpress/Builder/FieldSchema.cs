namespace SolrExpress.Builder
{
    /// <summary>
    /// Field schema about some field
    /// </summary>
    internal class FieldSchema
    {
        /// <summary>
        /// If true, value of the field can be used in queries to retrieve matching documents
        /// </summary>
        public bool IsIndexed { get; internal set; }

        /// <summary>
        /// If true, actual value of the field can be retrieved by queries
        /// </summary>
        public bool IsStored { get; internal set; }

        /// <summary>
        /// Name of field in the SOLR schema
        /// </summary>
        public string FieldName { get; internal set; }
    }
}
