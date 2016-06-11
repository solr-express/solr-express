using Newtonsoft.Json.Linq;
using System;

namespace SolrExpress.Core.Update
{
    /// <summary>
    /// Signatures to remove informed documents from SOLR collection
    /// </summary>
    public interface IAtomicDelete<TDocument> : IAtomicInstruction
        where TDocument : IDocument
    {
        /// <summary>
        /// Remove informed documents from SOLR collection
        /// </summary>
        /// <param name="documentIds">Document IDs to remove</param>
        void Configure(params string[] documentIds);
    }
}
