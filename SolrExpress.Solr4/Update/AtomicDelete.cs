using System;
using SolrExpress.Core;
using SolrExpress.Core.Update;

namespace SolrExpress.Solr4.Update
{
    public sealed class AtomicDelete<TDocument> : IAtomicDelete<TDocument>
        where TDocument : IDocument
    {
        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
        }

        /// <summary>
        /// Remove informed documents from SOLR collection
        /// </summary>
        /// <param name="documentIds">Document IDs to remove</param>
        public void Execute(params string[] documentIds)
        {
            throw new NotImplementedException();
        }
    }
}
