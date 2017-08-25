using Newtonsoft.Json.Linq;
using SolrExpress.Update;
using SolrExpress.Utility;

namespace SolrExpress.Solr5.Update
{
    public sealed class AtomicDelete<TDocument> : IAtomicDelete
        where TDocument : Document
    {
        public JObject Execute(params string[] documentIds)
        {
            Checker.IsNull(documentIds);

            if (documentIds.Length == 0)
            {
                return null;
            }

            var wrapper = new
            {
                delete = documentIds,
                commit = new { }
            };

            return JObject.FromObject(wrapper);
        }
    }
}
