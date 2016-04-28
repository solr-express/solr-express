using Newtonsoft.Json.Linq;
using SolrExpress.Core;
using SolrExpress.Core.Update;

namespace SolrExpress.Solr4.Update
{
    public sealed class AtomicDelete<TDocument> : IAtomicDelete<TDocument>
        where TDocument : IDocument
    {
        private string[] _documentIds;

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
        }

        /// <summary>
        /// Remove informed documents from SOLR collection
        /// </summary>
        /// <param name="documentIds">Document IDs to remove</param>
        public void Configure(params string[] documentIds)
        {
            Checker.IsNull(documentIds);
            Checker.IsEmpty(documentIds);

            this._documentIds = documentIds;
        }

        /// <summary>
        /// Create atomic update command
        /// </summary>
        /// <param name="jObject">Container to parameters to request to SOLR</param>
        public void Execute(JObject jObject)
        {
            JProperty jProperty;

            if (this._documentIds.Length == 1)
            {
                jProperty = new JProperty("id", this._documentIds[0]);
            }
            else
            {
                jProperty = new JProperty("id", $"({string.Join(" OR ", this._documentIds)})");
            }

            var delete = new JObject(jProperty);

            jObject["delete"] = delete;
        }
    }
}
