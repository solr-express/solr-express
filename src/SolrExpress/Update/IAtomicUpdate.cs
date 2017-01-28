namespace SolrExpress.Update
{
    /// <summary>
    /// Signatures to add informed documents in SOLR collection
    /// </summary>
    public interface IAtomicUpdate<TDocument>
        where TDocument : IDocument
    {
        /// <summary>
        /// Add informed documents in SOLR collection
        /// If a doc with same id exists in collection, the document is updated
        /// </summary>
        /// <param name="documents">Documents to add</param>
        string Execute(params TDocument[] documents);
    }
}
