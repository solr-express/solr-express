using SolrExpress.Core.Entity;

namespace SolrExpress.Core.Builder
{
    /// <summary>
    /// Signatures of SOLR builders factory 
    /// </summary>
    public interface IBuilderFactory<TDocument>
        where TDocument : IDocument
    {
        /// <summary>
        /// Get document list builder
        /// </summary>
        /// <param name="data">Documents list</param>
        IDocumentBuilder<TDocument> GetDocumentBuilder();

        /// <summary>
        /// Get facet field list builder
        /// </summary>        
        /// <param name="data">Facet field list</param>
        IFacetFieldResultBuilder<TDocument> GetFacetFieldBuilder();

        /// <summary>
        /// Get facet query list builder
        /// </summary>
        /// <param name="data">Facet query list</param>
        IFacetQueryResultBuilder<TDocument> GetFacetQueryBuilder();

        /// <summary>
        /// Get facet range list builder
        /// </summary>
        /// <param name="data">Facet range list</param>
        IFacetRangeResultBuilder<TDocument> GetFacetRangeBuilder();

        /// <summary>
        /// Get statistics builder
        /// </summary>
        /// <param name="statistic">Statics about search execution</param>
        IStatisticResultBuilder<TDocument> GetStatisticBuilder();
    }
}
