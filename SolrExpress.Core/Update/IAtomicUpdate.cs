using Newtonsoft.Json.Linq;
using System;

namespace SolrExpress.Core.Update
{
    /// <summary>
    /// Signatures to add informed documents in SOLR collection
    /// </summary>
    public interface IAtomicUpdate : IDisposable
    {
    }

    /// <summary>
    /// Signatures to add informed documents in SOLR collection
    /// </summary>
    public interface IAtomicUpdate<TDocument> : IAtomicUpdate
        where TDocument : IDocument
    {
        /// <summary>
        /// Add informed documents in SOLR collection
        /// If a doc with same id exists in collection, the document is updated
        /// </summary>
        /// <param name="documents">Documents to add</param>
        void Configure(params TDocument[] documents);

        /// <summary>
        /// Create atomic update command
        /// </summary>
        /// <param name="container">Container to parameters to request to SOLR</param>
        void Execute(JObject container);
    }
}
