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
        internal string AliasName { get; set; }

        /// <summary>
        /// Type of POCO property
        /// </summary>
        internal Type PropertyType { get; set; }

        /// <summary>
        /// Field's schema
        /// </summary>
        internal FieldSchema FieldSchema { get; set; }

        /// <summary>
        /// If true, field is configured like dynamic field
        /// </summary>
        internal bool IsDynamicField { get; set; }

        /// <summary>
        /// Indicates prefix name in dynamic field configurations
        /// </summary>
        internal string DynamicFieldPrefixName { get; set; }

        /// <summary>
        /// Indicates suffix name in dynamic field configurations
        /// </summary>
        internal string DynamicFieldSuffixName { get; set; }
    }
}
