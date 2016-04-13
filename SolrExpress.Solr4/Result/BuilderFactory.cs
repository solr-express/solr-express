using SolrExpress.Core.Builder;
using SolrExpress.Core.Entity;

namespace SolrExpress.Solr4.Builder
{
    /// <summary>
    /// SOLR builders factory 
    /// </summary>
    public class BuilderFactory<TDocument> : IBuilderFactory<TDocument>
        where TDocument : IDocument
    {
        /// <summary>
        /// Get document list builder
        /// </summary>
        /// <param name="data">Documents list</param>
        public IDocumentBuilder<TDocument> GetDocumentBuilder()
        {
            return new DocumentBuilder<TDocument>();
        }

        /// <summary>
        /// Get facet field list builder
        /// </summary>        
        /// <param name="data">Facet field list</param>
        public IFacetFieldResultBuilder<TDocument> GetFacetFieldBuilder()
        {
            return new FacetFieldResultBuilder<TDocument>();
        }

        /// <summary>
        /// Get facet query list builder
        /// </summary>
        /// <param name="data">Facet query list</param>
        public IFacetQueryResultBuilder<TDocument> GetFacetQueryBuilder()
        {
            return new FacetQueryResultBuilder<TDocument>();
        }

        /// <summary>
        /// Get facet range list builder
        /// </summary>
        /// <param name="data">Facet range list</param>
        public IFacetRangeResultBuilder<TDocument> GetFacetRangeBuilder()
        {
            return new FacetRangeResultBuilder<TDocument>();
        }

        /// <summary>
        /// Get statistics builder
        /// </summary>
        /// <param name="statistic">Statics about search execution</param>
        public IStatisticResultBuilder<TDocument> GetStatisticBuilder()
        {
            return new StatisticResultBuilder<TDocument>();
        }
    }
}
