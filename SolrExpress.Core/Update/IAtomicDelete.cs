using Newtonsoft.Json.Linq;
using System;

namespace SolrExpress.Core.Update
{
    /// <summary>
    /// Signatures to remove informed documents from SOLR collection
    /// </summary>
    public interface IAtomicDelete : IDisposable
    {
    }

    /// <summary>
    /// Signatures to remove informed documents from SOLR collection
    /// </summary>
    public interface IAtomicDelete<TDocument> : IAtomicDelete
        where TDocument : IDocument
    {
        /// <summary>
        /// Remove informed documents from SOLR collection
        /// </summary>
        /// <param name="documentIds">Document IDs to remove</param>
        void Configure(params string[] documentIds);

        /// <summary>
        /// Create atomic update command
        /// </summary>
        /// <param name="jObject">Container to parameters to request to SOLR</param>
        void Execute(JObject jObject);
    }
}
