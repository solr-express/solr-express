using SolrExpress.Core.Query;
using SolrExpress.Core.Update;

namespace SolrExpress.Core
{
    /// <summary>
    /// Signatures to SOLR document collection
    /// </summary>
    public interface IDocumentCollection<TDocument>
        where TDocument : IDocument
    {
        /// <summary>
        /// Solr queryable instance to provide create queries in SOLR
        /// </summary>
        ISolrQueryable<TDocument> Select();

        /// <summary>
        /// Solr queryable instance to provide create queries in SOLR
        /// </summary>
        ISolrAtomicUpdate<TDocument> Update();

        /// <summary>
        /// SolrExpress options
        /// </summary>
        DocumentCollectionOptions<TDocument> Options { get; }
    }
}
