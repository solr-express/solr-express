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
        /// Name of the field in the SOLR Schema
        /// </summary>
        public string Name { get; set; }
    }
}
