using System;

namespace SolrExpress.Core
{
    /// <summary>
    /// Attribute used to indicate field configurations
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class SolrFieldAttribute : Attribute
    {
        public SolrFieldAttribute(string name)
        {
            name.ThrowIfIsNullOrWhiteSpace();

            this.Name = name;
        }

        /// <summary>
        /// Name of the field in the SOLR Schema
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// If true, the value of the field can be used in queries to retrieve matching documents
        /// </summary>
        public bool Indexed { get; set; }

        /// <summary>
        /// If true, the actual value of the field can be retrieved by queries
        /// </summary>
        public bool Stored { get; set; }

        /// <summary>
        /// If true, omits the norms associated with this field
        /// </summary>
        public bool OmitNorms { get; set; }
    }
}
