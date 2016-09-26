using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SolrExpress.Core.Serialization;
using SolrExpress.Core.Utility;
using System.Collections.Generic;
using System.Linq;

namespace SolrExpress.Core.Update
{
    public sealed class AtomicUpdate<TDocument> : IAtomicUpdate<TDocument>
        where TDocument : IDocument
    {
        private List<TDocument> _documents = new List<TDocument>();

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
        }

        /// <summary>
        /// Add informed documents in SOLR collection
        /// If a doc with same id exists in collection, the document is updated
        /// </summary>
        /// <param name="documents">Documents to add</param>
        public void Configure(params TDocument[] documents)
        {
            Checker.IsNull(documents);
            Checker.IsEmpty(documents);

            this._documents.AddRange(documents);
        }

        /// <summary>
        /// Create atomic update command
        /// </summary>
        public string Execute()
        {
            if (this._documents.Any())
            {
                var jsonSerializer = JsonSerializer.Create();
                jsonSerializer.Converters.Add(new GeoCoordinateConverter());
                jsonSerializer.Converters.Add(new DateTimeConverter());
                jsonSerializer.ContractResolver = new CustomContractResolver();

                var jArray = JArray.FromObject(this._documents, jsonSerializer);

                return jArray.ToString();
            }

            return string.Empty;
        }
    }
}