using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SolrExpress.Serialization;
using SolrExpress.Update;
using SolrExpress.Utility;
using System.Text;

namespace SolrExpress.Solr4.Update
{
    public sealed class AtomicUpdate<TDocument> : IAtomicUpdate<TDocument>
        where TDocument : Document
    {
        string IAtomicUpdate<TDocument>.Execute(params TDocument[] documents)
        {
            Checker.IsNull(documents);

            if (documents.Length == 0)
            {
                return string.Empty;
            }

            var jsonSerializer = JsonSerializer.Create();
            jsonSerializer.Converters.Add(new GeoCoordinateConverter());
            jsonSerializer.Converters.Add(new DateTimeConverter());
            jsonSerializer.ContractResolver = new CustomContractResolver();
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

            return result.ToString();
        }
    }
}