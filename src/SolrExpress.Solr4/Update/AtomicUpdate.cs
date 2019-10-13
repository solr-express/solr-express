using Newtonsoft.Json.Linq;
using SolrExpress.Update;

namespace SolrExpress.Solr4.Update
{
    public sealed class AtomicUpdate<TDocument> : IAtomicUpdate<TDocument>
        where TDocument : Document
    {
        public JObject Execute(params DocumentUpdate<TDocument>[] documents)
        {
            throw new UnsupportedFeatureException();
        }
    }
}