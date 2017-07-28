using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SolrExpress.Serialization;
using SolrExpress.Utility;

namespace SolrExpress.Update
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

            var jArray = JArray.FromObject(documents, jsonSerializer);

            return jArray.ToString();
        }
    }
}