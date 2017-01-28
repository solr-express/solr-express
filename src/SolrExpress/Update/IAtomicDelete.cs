namespace SolrExpress.Update
{
    /// <summary>
    /// Signatures to remove informed documents from SOLR collection
    /// </summary>
    public interface IAtomicDelete<TDocument>
        where TDocument : IDocument
    {
        /// <summary>
        /// Remove informed documents from SOLR collection
        /// </summary>
        /// <param name="documentIds">Document IDs to remove</param>
        string Execute(params string[] documentIds);
    }
}
