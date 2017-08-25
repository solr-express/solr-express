using Newtonsoft.Json.Linq;
using SolrExpress.Update;
using SolrExpress.Utility;
using System.Text;

namespace SolrExpress.Solr4.Update
{
    public sealed class AtomicDelete : IAtomicDelete
    {
        public JObject Execute(params string[] documentIds)
        {
            Checker.IsNull(documentIds);

            if (documentIds.Length == 0)
            {
                return null;
            }

            var result = new StringBuilder();
            result.AppendLine("{");

            foreach (var documentId in documentIds)
            {
                result.AppendLine($"\"delete\": \"{documentId}\",");
            }

            result.AppendLine("\"commit\":{}");
            result.AppendLine("}");

            return JObject.Parse(result.ToString());
        }
    }
}
