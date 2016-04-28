using System;
using System.Collections.Generic;
using System.Linq;

namespace SolrExpress.Core.Update
{
    /// <summary>
    /// SOLR atomic update container
    /// </summary>
    public sealed class SolrAtomicUpdate<TDocument>
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
        /// Configurations about SolrQueriable behavior
        /// </summary>
        private readonly Configuration _configuration;

        /// <summary>
        /// Provider used to resolve the expression
        /// </summary>
        private readonly IProvider _provider;

        /// <summary>
        /// Resolver used to resolve classes dependency
        /// </summary>
        private readonly IResolver _resolver;

        /// <summary>
        /// Default constructor of the class
        /// </summary>
        /// <param name="provider">Provider used to resolve expression</param>
        /// <param name="resolver">Resolver used to resolve classes dependency</param>
        /// <param name="configuration">Configurations about SolrQueriable behavior</param>
        public SolrAtomicUpdate(IProvider provider, IResolver resolver, Configuration configuration)
        {
            Checker.IsNull(provider);
            Checker.IsNull(resolver);
            Checker.IsNull(configuration);

            this._provider = provider;
            this._resolver = resolver;
            this._configuration = configuration;
        }

        /// <summary>
        /// Add informed documents in SOLR collection
        /// If a doc with same id exists in collection, the document is updated
        /// </summary>
        /// <param name="documents">Documents to add</param>
        public SolrAtomicUpdate<TDocument> Add(params TDocument[] documents)
        {
            Checker.IsNull(documents);
            Checker.IsTrue<ArgumentOutOfRangeException>(documents.Length == 0);

            this._documentsToAdd.AddRange(documents);

            return this;
        }

        /// <summary>
        /// Remove informed documents from SOLR collection
        /// </summary>
        /// <param name="documentIds">Document IDs to remove</param>
        public SolrAtomicUpdate<TDocument> Delete(params string[] documentIds)
        {
            Checker.IsNull(documentIds);
            Checker.IsTrue<ArgumentOutOfRangeException>(documentIds.Length == 0);

            this._documentsToDelete.AddRange(documentIds);

            return this;
        }

        /// <summary>
        /// Commit adds and removes in SOLR collection
        /// </summary>
        public void Commit()
        {
            IAtomicUpdate<TDocument> atomicUpdate = null;
            IAtomicDelete<TDocument> atomicDelete = null;

            if (this._documentsToAdd.Any())
            {
                atomicUpdate = this._resolver.GetInstance<IAtomicUpdate<TDocument>>();
                atomicUpdate.Configure(this._documentsToAdd.ToArray());
            }

            if (this._documentsToDelete.Any())
            {
                atomicDelete = this._resolver.GetInstance<IAtomicDelete<TDocument>>();
                atomicDelete.Configure(this._documentsToDelete.ToArray());
            }

            var query = this._provider.GetAtomicUpdateInstruction(atomicUpdate, atomicDelete);

            this._provider.Execute(RequestHandler.Update, query);

            this._documentsToAdd = new List<TDocument>();
            this._documentsToDelete = new List<string>();
        }
    }
}
