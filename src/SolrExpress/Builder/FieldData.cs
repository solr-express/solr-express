using System;

namespace SolrExpress.Builder
{
    /// <summary>
    /// Field informations about some field
    /// </summary>
    internal class FieldData
    {
        /// <summary>
        /// Name of alias of field in queries
        /// </summary>
        public string AliasName { get; internal set; }

        /// <summary>
        /// Name of field in the SOLR schema
        /// </summary>
        public string FieldName { get; internal set; }

        /// <summary>
        /// Type of POCO property
        /// </summary>
        public Type PropertyType { get; internal set; }

        /// <summary>
        /// If true, field in SOLR schema is configured like dynamic field
        /// </summary>
        public bool IsDynamicField { get; internal set; }

        /// <summary>
        /// If true, value of the field can be used in queries to retrieve matching documents
        /// </summary>
        public bool IsIndexed { get; internal set; }

        /// <summary>
        /// If true, actual value of the field can be retrieved by queries
        /// </summary>
        public bool IsStored { get; internal set; }

        /// <summary>
        /// Indicates prefix name in dynamic field configurations
        /// </summary>
        public string DynamicFieldPrefixName { get; internal set; }

        /// <summary>
        /// Indicates suffix name in dynamic field configurations
        /// </summary>
        public string DynamicFieldSuffixName { get; internal set; }
    }
}
