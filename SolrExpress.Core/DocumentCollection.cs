using SolrExpress.Core.Query;
using SolrExpress.Core.Update;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SolrExpress.Core
{
    /// <summary>
    /// SOLR document collection
    /// </summary>
    public class DocumentCollection<TDocument>
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
        /// Default constructor of the class
        /// </summary>
        /// <param name="provider">Provider used to resolve expression</param>
        /// <param name="resolver">Resolver used to resolve classes dependency</param>
        /// <param name="configuration">Configurations about SolrQueriable behavior</param>
        public DocumentCollection(IProvider provider, IResolver resolver, Configuration configuration)
        {
            Checker.IsNull(provider);
            Checker.IsNull(resolver);
            Checker.IsNull(configuration);

            this.Provider = provider;
            this.Resolver = resolver;
            this._configuration = configuration;
        }

        /// <summary>
        /// Add informed documents in SOLR collection
        /// If a doc with same id exists in collection, the document is updated
        /// </summary>
        /// <param name="documents">Documents to add</param>
        public DocumentCollection<TDocument> Add(params TDocument[] documents)
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
        public DocumentCollection<TDocument> Delete(params string[] documentIds)
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
            if (this._documentsToAdd.Any())
            {
                using (var update = this.Resolver.GetInstance<IAtomicUpdate<TDocument>>())
                {
                    update.Execute(this._documentsToAdd.ToArray());
                }
            }

            if (this._documentsToDelete.Any())
            {
                using (var update = this.Resolver.GetInstance<IAtomicDelete<TDocument>>())
                {
                    update.Execute(this._documentsToDelete.ToArray());
                }
            }

            this._documentsToAdd = new List<TDocument>();
            this._documentsToDelete = new List<string>();
        }

        /// <summary>
        /// Provider used to resolve the expression
        /// </summary>
        public IProvider Provider { get; private set; }

        /// <summary>
        /// Resolver used to resolve classes dependency
        /// </summary>
        public IResolver Resolver { get; private set; }

        /// <summary>
        /// Solr queryable instance to provide create queries in SOLR
        /// </summary>
        public SolrQueryable<TDocument> Select => new SolrQueryable<TDocument>(this.Provider, this.Resolver, this._configuration);
    }
}
