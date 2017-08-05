using SolrExpress.Update;
using SolrExpress.Utility;
using System.Text;

namespace SolrExpress.Solr4.Update
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

            var result = new StringBuilder();
            result.AppendLine("{");

            foreach (var documentId in documentIds)
            {
                result.AppendLine($"\"delete\": \"{documentId}\",");
            }
            
            result.AppendLine("\"commit\":{}");
            result.AppendLine("}");

            return result.ToString();
        }
    }
}
