using Newtonsoft.Json.Linq;

namespace SolrExpress.Update
{
    /// <summary>
    /// Atomic delete informed documents from SOLR collection
    /// </summary>
    public interface IAtomicDelete
    {
        /// <summary>
        /// Remove informed documents from SOLR collection
        /// </summary>
        /// <param name="documentIds">Document IDs to remove</param>
        JObject Execute(params string[] documentIds);
    }
}
