using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SolrExpress.Serialization;
using SolrExpress.Update;
using SolrExpress.Utility;

namespace SolrExpress.Solr5.Update
{
    public sealed class AtomicUpdate<TDocument> : IAtomicUpdate<TDocument>
        where TDocument : Document
    {
        public JObject Execute(params TDocument[] documents)
        {
            Checker.IsNull(documents);

            if (documents.Length == 0)
            {
                return null;
            }

            var jsonSerializer = JsonSerializer.Create();
            jsonSerializer.Converters.Add(new GeoCoordinateConverter());
            jsonSerializer.Converters.Add(new DateTimeConverter());
            jsonSerializer.ContractResolver = new CustomContractResolver();
            jsonSerializer.NullValueHandling = NullValueHandling.Ignore;

            var wrapper = new
            {
                add = documents,
                commit = new { }
            };

            return JObject.FromObject(wrapper, jsonSerializer);
        }
    }
}