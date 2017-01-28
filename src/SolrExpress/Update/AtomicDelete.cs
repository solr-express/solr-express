using Newtonsoft.Json.Linq;
using SolrExpress.Utility;
using System.Linq;

namespace SolrExpress.Update
{
    public sealed class AtomicDelete<TDocument> : IAtomicDelete<TDocument>
        where TDocument : IDocument
    {
        string IAtomicDelete<TDocument>.Execute(params string[] documentIds)
        {
            Checker.IsNull(documentIds);
            Checker.IsEmpty(documentIds);

            JProperty jProperty;

            if (documentIds.Count() == 1)
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
