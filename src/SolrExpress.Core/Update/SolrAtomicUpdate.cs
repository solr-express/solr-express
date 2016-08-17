using SolrExpress.Core.DependencyInjection;
using System.Collections.Generic;
using System.Linq;

namespace SolrExpress.Core.Update
{
    /// <summary>
    /// SOLR atomic update container
    /// </summary>
    public sealed class SolrAtomicUpdate<TDocument> : ISolrAtomicUpdate<TDocument>
        where TDocument : IDocument
    {
        /// <summary>
        /// Documents to add in SOLR collection
        /// </summary>
        private List<TDocument> _documentsToAdd = new List<TDocument>();

        /// <summary>
        /// Documents to delete from SOLR collection
        /// </summary>
        private List<string> _documentsToDelete = new List<string>();

        /// <summary>
        /// SOLR connection
        /// </summary>
        private readonly ISolrConnection _solrConnection;

        /// <summary>
        /// Default constructor of class
        /// </summary>
        /// <param name="serviceProvider">Classes dependency provider</param>
        /// <param name="options">SolrExpress options</param>
        public SolrAtomicUpdate(DocumentCollectionOptions<TDocument> options)
        {
            Checker.IsNull(options);

            this.Options = options;

            this._solrConnection = ApplicationServices.Current.GetService<ISolrConnection>();
            this._solrConnection.SolrHost = this.Options.HostAddress;
        }

        /// <summary>
        /// Add informed documents in SOLR collection
        /// If a doc with same id exists in collection, the document is updated
        /// </summary>
        /// <param name="documents">Documents to add</param>
        public ISolrAtomicUpdate<TDocument> Add(params TDocument[] documents)
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
        public ISolrAtomicUpdate<TDocument> Delete(params string[] documentIds)
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
            var atomicInstructions = new List<IAtomicInstruction>();

            if (this._documentsToAdd.Any())
            {
                var atomicInstruction = ApplicationServices.Current.GetService<IAtomicUpdate<TDocument>>();
                atomicInstruction.Configure(this._documentsToAdd.ToArray());
                atomicInstructions.Add(atomicInstruction);
            }

            if (this._documentsToDelete.Any())
            {
                var atomicInstruction = ApplicationServices.Current.GetService<IAtomicDelete<TDocument>>();
                atomicInstruction.Configure(this._documentsToDelete.ToArray());
                atomicInstructions.Add(atomicInstruction);
            }

            foreach (var atomicInstruction in atomicInstructions)
            {
                var data = atomicInstruction.Execute();
                this._solrConnection.Post(RequestHandler.Update, data);
            }

            if (atomicInstructions.Any())
            {
                this._solrConnection.Post(RequestHandler.Update, "{\"commit\":{}}");
            }

            this._documentsToAdd = new List<TDocument>();
            this._documentsToDelete = new List<string>();
        }

        /// <summary>
        /// Configurations about SolrQueriable behavior
        /// </summary>
        public DocumentCollectionOptions<TDocument> Options { get; private set; }
    }
}
