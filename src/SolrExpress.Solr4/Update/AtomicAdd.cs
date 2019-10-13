using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SolrExpress.Configuration;
using SolrExpress.Serialization;
using SolrExpress.Update;
using SolrExpress.Utility;
using System.Linq;
using System.Text;

namespace SolrExpress.Solr4.Update
{
    public sealed class AtomicAdd<TDocument> : IAtomicAdd<TDocument>
        where TDocument : Document
    {
        private readonly SolrDocumentConfiguration<TDocument> _configuration;

        public AtomicAdd(SolrDocumentConfiguration<TDocument> configuration)
        {
            this._configuration = configuration;
        }

        public JObject Execute(params TDocument[] documents)
        {
            Checker.IsNull(documents);
            Checker.IsTrue<UnsupportedFeatureException>(documents.Count() > 1);

            if (documents.Length == 0)
            {
                return null;
            }

            var jsonSerializer = JsonSerializer.Create();
            jsonSerializer.Converters.Add(new GeoCoordinateConverter());
            jsonSerializer.Converters.Add(new DateTimeConverter());
            jsonSerializer.ContractResolver = new DocumentContractResolver<TDocument>(this._configuration);
            jsonSerializer.NullValueHandling = NullValueHandling.Ignore;

            var result = new StringBuilder();
            result.AppendLine("{");

            foreach (var document in documents)
            {
                var json = JObject.FromObject(document, jsonSerializer);

                result.AppendLine("\"add\":{\"doc\":");
                result.AppendLine(json.ToString());
                result.AppendLine(",\"overwrite\":true},");
            }

            result.AppendLine("\"commit\":{}");
            result.AppendLine("}");

            return JObject.Parse(result.ToString());
        }
    }
}
