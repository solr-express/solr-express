using Newtonsoft.Json.Linq;
using SolrExpress.Utility;

namespace SolrExpress.Update
{
    public sealed class AtomicDelete<TDocument> : IAtomicDelete<TDocument>
        where TDocument : IDocument
    {
        string IAtomicDelete<TDocument>.Execute(params string[] documentIds)
        {
            Checker.IsNull(documentIds);

            if (documentIds.Length == 0)
            {
                return string.Empty;
            }

            JProperty jProperty;

            if (documentIds.Length == 1)
            {
                jProperty = new JProperty("delete", documentIds[0]);
            }
            else
            {
                jProperty = new JProperty("delete", $"({string.Join(" OR ", documentIds)})");
            }

            var jObject = new JObject(jProperty);

            return jObject.ToString();
        }
    }
}
