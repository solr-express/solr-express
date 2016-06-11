using System;

namespace SolrExpress.Core.Update
{
    /// <summary>
    /// Signatures to change documents from SOLR collection using Atomic Update
    /// </summary>
    public interface IAtomicInstruction : IDisposable
    {
        /// <summary>
        /// Create atomic update command
        /// </summary>
        string Execute();
    }
}
