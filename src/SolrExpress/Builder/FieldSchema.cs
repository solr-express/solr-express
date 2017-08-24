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
        internal bool IsIndexed { get; set; }

        /// <summary>
        /// If true, actual value of the field can be retrieved by queries
        /// </summary>
        internal bool IsStored { get; set; }

        /// <summary>
        /// Name of field in the SOLR schema
        /// </summary>
        internal string FieldName { get; set; }
    }
}
