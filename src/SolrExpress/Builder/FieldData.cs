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
        /// Type of POCO property
        /// </summary>
        public Type PropertyType { get; internal set; }

        /// <summary>
        /// Field's schema
        /// </summary>
        public FieldSchema FieldSchema { get; internal set; }

        /// <summary>
        /// If true, field is configured like dynamic field
        /// </summary>
        public bool IsDynamicField { get; internal set; }

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
