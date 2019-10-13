using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SolrExpress.Configuration;
using SolrExpress.Serialization;
using SolrExpress.Update;
using SolrExpress.Utility;

namespace SolrExpress.Solr5.Update
{
    public sealed class AtomicUpdate<TDocument> : IAtomicUpdate<TDocument>
        where TDocument : Document
    {
        private readonly SolrDocumentConfiguration<TDocument> _configuration;

        public AtomicUpdate(SolrDocumentConfiguration<TDocument> configuration)
        {
            this._configuration = configuration;
        }

        public JObject Execute(params DocumentUpdate<TDocument>[] documents)
        {
            Checker.IsNull(documents);

            if (documents.Length == 0)
            {
                return null;
            }

            var jsonSerializer = JsonSerializer.Create();
            jsonSerializer.Converters.Add(new GeoCoordinateConverter());
            jsonSerializer.Converters.Add(new DateTimeConverter());
            jsonSerializer.Converters.Add(new DocumentUpdateConverter<TDocument>(this._configuration));
            jsonSerializer.NullValueHandling = NullValueHandling.Ignore;

            var wrapper = new
            {
                update = documents,
                commit = new { }
            };

            return JObject.FromObject(wrapper, jsonSerializer);
        }
    }
}