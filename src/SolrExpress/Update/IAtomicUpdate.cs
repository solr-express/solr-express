using Newtonsoft.Json.Linq;

namespace SolrExpress.Update
{
    /// <summary>
    /// Atomic update informed documents in SOLR collection
    /// </summary>
    public interface IAtomicUpdate<TDocument>
        where TDocument : Document
    {
        /// <summary>
        /// Add informed documents in SOLR collection
        /// If a doc with same id exists in collection, the document is updated
        /// </summary>
        /// <param name="documents">Documents to add</param>
        JObject Execute(params DocumentUpdate<TDocument>[] documents);
    }
}
