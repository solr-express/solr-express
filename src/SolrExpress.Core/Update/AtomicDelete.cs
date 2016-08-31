using Newtonsoft.Json.Linq;
using SolrExpress.Core.Utility;
using System;
using System.Collections.Generic;

namespace SolrExpress.Core.Update
{
    [Obsolete("Necessita de teste para caso seja _documentIds==0")]
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
        public string Execute()
        {
            JProperty jProperty;

            if (this._documentIds.Count == 1)
            {
                jProperty = new JProperty("delete", this._documentIds[0]);
            }
            else
            {
                jProperty = new JProperty("delete", $"({string.Join(" OR ", this._documentIds)})");
            }

            var jObject = new JObject(jProperty);

            return jObject.ToString();
        }
    }
}
