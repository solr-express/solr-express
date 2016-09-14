using SolrExpress.Core.DependencyInjection;

namespace SolrExpress.Core.Update
{
    /// <summary>
    /// Signatures of SOLR atomic update container
    /// </summary>
    public interface ISolrAtomicUpdate<TDocument>
        where TDocument : IDocument
    {
        /// <summary>
        /// Add informed documents in SOLR collection
        /// If a doc with same id exists in collection, the document is updated
        /// </summary>
        /// <param name="documents">Documents to add</param>
        ISolrAtomicUpdate<TDocument> Add(params TDocument[] documents);

        /// <summary>
        /// Remove informed documents from SOLR collection
        /// </summary>
        /// <param name="documentIds">Document IDs to remove</param>
        ISolrAtomicUpdate<TDocument> Delete(params string[] documentIds);

        /// <summary>
        /// Commit adds and removes in SOLR collection
        /// </summary>
        void Commit();

        /// <summary>
        /// SolrExpress options
        /// </summary>
        DocumentCollectionOptions<TDocument> Options { get; }

        /// <summary>
        /// Services container
        /// </summary>
        IEngine Engine { get; }
    }
}
