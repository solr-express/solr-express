using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SolrExpress.Configuration
{
    /// <summary>
    /// Solr document configurations
    /// </summary>
    /// <typeparam name="TDocument"></typeparam>
    public sealed class SolrDocumentConfiguration<TDocument>
        where TDocument : Document
    {
        private readonly IList<SolrFieldConfiguration<TDocument>> _solrFieldConfigurationList = new List<SolrFieldConfiguration<TDocument>>();

        internal IList<SolrFieldConfiguration<TDocument>> GetSolrFieldConfigurationList()
        {
            return this._solrFieldConfigurationList;
        }

        /// <summary>
        /// Field to configure
        /// </summary>
        /// <param name="fieldExpression">Expression used to find field name</param>
        /// <returns>Field configuration</returns>
        public SolrFieldConfiguration<TDocument> Field(Expression<Func<TDocument, object>> fieldExpression)
        {
            var fieldConfiguration = new SolrFieldConfiguration<TDocument>
            {
                FieldExpression = fieldExpression
            };

            this._solrFieldConfigurationList.Add(fieldConfiguration);

            return fieldConfiguration;
        }
    }
}
