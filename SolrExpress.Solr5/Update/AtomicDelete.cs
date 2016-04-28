using Newtonsoft.Json.Linq;
using SolrExpress.Core;
using SolrExpress.Core.Update;
using System.Collections.Generic;

namespace SolrExpress.Solr5.Update
{
    public sealed class AtomicDelete<TDocument> : IAtomicDelete<TDocument>
        where TDocument : IDocument
    {
        private List<string> _documentIds = new List<string>();

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

            this._documentIds.AddRange(documentIds);
        }

        /// <summary>
        /// Create atomic update command
        /// </summary>
        /// <param name="jObject">Container to parameters to request to SOLR</param>
        public void Execute(JObject jObject)
        {
            JProperty jProperty;

            if (this._documentIds.Count == 1)
            {
                jProperty = new JProperty("id", this._documentIds[0]);
            }
            else
            {
                jProperty = new JProperty("id", $"({string.Join(" OR ", this._documentIds)})");
            }

            var delete = new JObject(jProperty);

            jObject["delete"] = delete;

            this._documentIds = new List<string>();
        }
    }
}
