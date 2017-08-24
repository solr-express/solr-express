using Newtonsoft.Json.Linq;

namespace SolrExpress.Update
{
    /// <summary>
    /// Atomic delete informed documents from SOLR collection
    /// </summary>
    public interface IAtomicDelete<TDocument>
        where TDocument : Document
    {
        /// <summary>
        /// Remove informed documents from SOLR collection
        /// </summary>
        /// <param name="documentIds">Document IDs to remove</param>
        JObject Execute(params string[] documentIds);
    }
}
