using System;
using System.Linq.Expressions;

namespace SolrExpress.Configuration
{
    /// <summary>
    /// Solr field configurations
    /// </summary>
    public class SolrFieldConfiguration<TDocument>
        where TDocument : Document
    {
        /// <summary>
        /// Expression used to find field name
        /// </summary>
        internal Expression<Func<TDocument, object>> FieldExpression { get; set; }

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
