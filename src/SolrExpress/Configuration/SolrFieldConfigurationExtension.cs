namespace SolrExpress.Configuration
{
    public static class SolrFieldConfigurationExtension
    {
        /// <summary>
        /// Name of field in the SOLR schema
        /// </summary>
        /// <param name="solrFieldConfiguration">Solr field options</param>
        /// <param name="value">Value to set</param>
        /// <returns>Solr field options</returns>
        public static SolrFieldConfiguration<TDocument> HasName<TDocument>(this SolrFieldConfiguration<TDocument> solrFieldConfiguration, string value)
            where TDocument : Document
        {
            solrFieldConfiguration.Name = value;

            return solrFieldConfiguration;
        }

        /// <summary>
        /// If true, indicates than field is a magic field
        /// </summary>
        /// <param name="solrFieldConfiguration">Solr field options</param>
        /// <param name="value">Value to set</param>
        /// <returns>Solr field options</returns>
        public static SolrFieldConfiguration<TDocument> IsMagicField<TDocument>(this SolrFieldConfiguration<TDocument> solrFieldConfiguration, bool value)
            where TDocument : Document
        {
            solrFieldConfiguration.IsMagicField = value;

            return solrFieldConfiguration;
        }

        /// <summary>
        /// If true, field in SOLR schema is configured like dynamic field
        /// </summary>
        /// <param name="solrFieldConfiguration">Solr field options</param>
        /// <param name="value">Value to set</param>
        /// <returns>Solr field options</returns>
        public static SolrFieldConfiguration<TDocument> IsDynamicField<TDocument>(this SolrFieldConfiguration<TDocument> solrFieldConfiguration, bool value)
            where TDocument : Document
        {
            solrFieldConfiguration.IsDynamicField = value;

            return solrFieldConfiguration;
        }

        /// <summary>
        /// Indicates prefix name in dynamic field configurations
        /// </summary>
        /// <param name="solrFieldConfiguration">Solr field options</param>
        /// <param name="value">Value to set</param>
        /// <returns>Solr field options</returns>
        public static SolrFieldConfiguration<TDocument> HasDynamicFieldPrefixName<TDocument>(this SolrFieldConfiguration<TDocument> solrFieldConfiguration, string value)
            where TDocument : Document
        {
            solrFieldConfiguration.DynamicFieldPrefixName = value;

            return solrFieldConfiguration;
        }

        /// <summary>
        /// Indicates suffix name in dynamic field configurations
        /// </summary>
        /// <param name="solrFieldConfiguration">Solr field options</param>
        /// <param name="value">Value to set</param>
        /// <returns>Solr field options</returns>
        public static SolrFieldConfiguration<TDocument> HasDynamicFieldSuffixName<TDocument>(this SolrFieldConfiguration<TDocument> solrFieldConfiguration, string value)
            where TDocument : Document
        {
            solrFieldConfiguration.DynamicFieldSuffixName = value;

            return solrFieldConfiguration;
        }
    }
}
