using System;

namespace SolrExpress.Core.Update
{
    /// <summary>
    /// Signatures to remove informed documents from SOLR collection
    /// </summary>
    public interface IAtomicDelete<TDocument> : IDisposable
        where TDocument : IDocument
    {
        /// <summary>
        /// Remove informed documents from SOLR collection
        /// </summary>
        /// <param name="documents">Documents to add</param>
        void Execute(params TDocument[] documents);

        /// <summary>
        /// Remove informed documents from SOLR collection
        /// </summary>
        /// <param name="documentIds">Document IDs to remove</param>
        void Execute(params long[] documentIds);
    }
}
