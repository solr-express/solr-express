using SolrExpress.Utility;
using System.Collections.Generic;
using System.Linq;

namespace SolrExpress.Update
{
    public class DocumentUpdate<TDocument>
        where TDocument : IDocument
    {
        private readonly SolrExpressOptions<TDocument> _options;
        private readonly List<TDocument> _documentsToAdd = new List<TDocument>();
        private readonly List<string> _documentsToDelete = new List<string>();

        /// <summary>
        /// Default constructor of class
        /// </summary>
        /// <param name="options">SolrExpress options</param>
        public DocumentUpdate(SolrExpressOptions<TDocument> options)
        {
            Checker.IsNull(options);

            this._options = options;
        }

        /// <summary>
        /// Add informed documents in SOLR collection
        /// If a doc with same id exists in collection, the document is updated
        /// </summary>
        /// <param name="documents">Documents to add</param>
        public DocumentUpdate<TDocument> Add(params TDocument[] documents)
        {
            Checker.IsNull(documents);
            Checker.IsEmpty(documents);

            this._documentsToAdd.AddRange(documents);

            return this;
        }

        /// <summary>
        /// Remove informed documents from SOLR collection
        /// </summary>
        /// <param name="documentIds">Document IDs to remove</param>
        public DocumentUpdate<TDocument> Delete(params string[] documentIds)
        {
            Checker.IsNull(documentIds);
            Checker.IsEmpty(documentIds);

            this._documentsToDelete.AddRange(documentIds);

            return this;
        }

        /// <summary>
        /// Commit adds and removes in SOLR collection
        /// </summary>
        public void Commit()
        {
            if (this._documentsToAdd.Any())
            {
                //TODO: DI
                IAtomicUpdate<TDocument> atomicUpdate = null;
                var data = atomicUpdate.Execute(this._documentsToAdd.ToArray());
                //TODO: Need SOlr connection
                //solrConnection.Post(this.Options.Security, RequestHandler.Update, data);
                //solrConnection.Post(this.Options.Security, RequestHandler.Update, "{\"commit\":{}}");
            }

            if (this._documentsToDelete.Any())
            {
                //TODO: DI
                IAtomicDelete<TDocument> atomicDelete = null;
                var data = atomicDelete.Execute(this._documentsToDelete.ToArray());
                //TODO: Need SOlr connection
                //solrConnection.Post(this.Options.Security, RequestHandler.Update, data);
                //solrConnection.Post(this.Options.Security, RequestHandler.Update, "{\"commit\":{}}");
            }

            this._documentsToAdd.Clear();
            this._documentsToDelete.Clear();
        }
    }
}