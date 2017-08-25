using SolrExpress.Utility;
using System;

namespace SolrExpress
{
    /// <summary>
    /// Attribute used to indicate field configurations
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class SolrFieldAttribute : Attribute
    {
        public SolrFieldAttribute(string name)
        {
            Checker.IsNullOrWhiteSpace(name);

            this.Name = name;
        }

        /// <summary>
        /// Name of field in the SOLR schema
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// If true, indicates than field is a magic field
        /// </summary>
        public bool IsMagicField { get; set; }

        /// <summary>
        /// If true, field in SOLR schema is configured like dynamic field
        /// </summary>
        public bool IsDynamicField { get; set; }

        /// <summary>
        /// Indicates prefix name in dynamic field configurations
        /// </summary>
        public string DynamicFieldPrefixName { get; set; }

        /// <summary>
        /// Indicates suffix name in dynamic field configurations
        /// </summary>
        public string DynamicFieldSuffixName { get; set; }
    }
}
