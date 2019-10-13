using SolrExpress.Search.Parameter;
using SolrExpress.Utility;
using System.Collections.Generic;
using System.Linq;

namespace SolrExpress.Update
{
    public class DocumentCollectionUpdate<TDocument>
        where TDocument : Document
    {
        private readonly ISolrConnection<TDocument> _solrConnection;
        private readonly List<TDocument> _documentsToAdd = new List<TDocument>();
        private readonly List<DocumentUpdate<TDocument>> _documentsToUpdate = new List<DocumentUpdate<TDocument>>();
        private readonly List<string> _documentsToDelete = new List<string>();

        /// <summary>
        /// Default constructor of class
        /// </summary>
        public DocumentCollectionUpdate(
            ISolrConnection<TDocument> solrConnection,
            ISolrExpressServiceProvider<TDocument> serviceProvider)
        {
            Checker.IsNull(solrConnection);
            Checker.IsNull(serviceProvider);

            this._solrConnection = solrConnection;
            this.ServiceProvider = serviceProvider;
        }

        /// <summary>
        /// Add informed documents in SOLR collection
        /// If a doc with same id exists in collection, the document is rewrite
        /// </summary>
        /// <param name="documents">Documents to add</param>
        public DocumentCollectionUpdate<TDocument> Add(params TDocument[] documents)
        {
            Checker.IsNull(documents);
            Checker.IsEmpty(documents);

            this._documentsToAdd.AddRange(documents);

            return this;
        }

        /// <summary>
        /// Update informed documents in SOLR collection
        /// </summary>
        /// <param name="documents">Documents to update</param>
        public DocumentCollectionUpdate<TDocument> Update(params DocumentUpdate<TDocument>[] documents)
        {
            Checker.IsNull(documents);
            Checker.IsEmpty(documents);

            this._documentsToUpdate.AddRange(documents);

            return this;
        }

        /// <summary>
        /// Remove informed documents from SOLR collection
        /// </summary>
        /// <param name="documentIds">Document IDs to remove</param>
        public DocumentCollectionUpdate<TDocument> Delete(params string[] documentIds)
        {
            Checker.IsNull(documentIds);
            Checker.IsEmpty(documentIds);

            this._documentsToDelete.AddRange(documentIds);

            return this;
        }

        /// <summary>
        /// Execute adds, updates and removes in SOLR collection
        /// </summary>
        public void Execute()
        {
            if (this._documentsToAdd.Any())
            {
                var atomicAdd = this.ServiceProvider.GetService<IAtomicAdd<TDocument>>();
                var data = atomicAdd.Execute(this._documentsToAdd.ToArray());
                if (data != null)
                {
                    this._solrConnection.Post(RequestHandler.Update, data);
                }
            }

            if (this._documentsToUpdate.Any())
            {
                var atomicUpdate = this.ServiceProvider.GetService<IAtomicUpdate<TDocument>>();
                var data = atomicUpdate.Execute(this._documentsToUpdate.ToArray());
                if (data != null)
                {
                    this._solrConnection.Post(RequestHandler.Update, data);
                }
            }

            if (this._documentsToDelete.Any())
            {
                var atomicDelete = this.ServiceProvider.GetService<IAtomicDelete>();
                var data = atomicDelete.Execute(this._documentsToDelete.ToArray());
                if (data != null)
                {
                    this._solrConnection.Post(RequestHandler.Update, data);
                }
            }

            this._documentsToAdd.Clear();
            this._documentsToDelete.Clear();
        }

        /// <summary>
        /// Services provider
        /// </summary>
        public ISolrExpressServiceProvider<TDocument> ServiceProvider { get; set; }
    }
}