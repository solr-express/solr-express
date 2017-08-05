using Newtonsoft.Json.Linq;
using SolrExpress.Update;
using SolrExpress.Utility;

namespace SolrExpress.Solr5.Update
{
    public sealed class AtomicDelete<TDocument> : IAtomicDelete<TDocument>
        where TDocument : Document
    {
        string IAtomicDelete<TDocument>.Execute(params string[] documentIds)
        {
            Checker.IsNull(documentIds);

            if (documentIds.Length == 0)
            {
                return string.Empty;
            }

            var wrapper = new
            {
                delete = documentIds,
                commit = new { }
            };

            var jObject = JObject.FromObject(wrapper);
            
            return jObject.ToString();
        }
    }
}
