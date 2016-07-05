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

            this.Provider = provider;
            this.Resolver = resolver;
            this.Configuration = configuration;
        }

        /// <summary>
        /// Add informed documents in SOLR collection
        /// If a doc with same id exists in collection, the document is updated
        /// </summary>
        /// <param name="documents">Documents to add</param>
        public SolrAtomicUpdate<TDocument> Add(params TDocument[] documents)
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
        public SolrAtomicUpdate<TDocument> Delete(params string[] documentIds)
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
                var atomicInstruction = this.Resolver.GetInstance<IAtomicUpdate<TDocument>>();
                atomicInstruction.Configure(this._documentsToAdd.ToArray());
                atomicInstructions.Add(atomicInstruction);
            }

            if (this._documentsToDelete.Any())
            {
                var atomicInstruction = this.Resolver.GetInstance<IAtomicDelete<TDocument>>();
                atomicInstruction.Configure(this._documentsToDelete.ToArray());
                atomicInstructions.Add(atomicInstruction);
            }

            foreach (var atomicInstruction in atomicInstructions)
            {
                var data = atomicInstruction.Execute();
                this.Provider.Post(RequestHandler.Update, data);
            }

            if (atomicInstructions.Any())
            {
                this.Provider.Post(RequestHandler.Update, "{\"commit\":{}}");
            }

            this._documentsToAdd = new List<TDocument>();
            this._documentsToDelete = new List<string>();
        }

        /// <summary>
        /// Configurations about SolrQueriable behavior
        /// </summary>
        public Configuration Configuration { get; private set; }

        /// <summary>
        /// Provider used to resolve the expression
        /// </summary>
        public IProvider Provider { get; private set; }

        /// <summary>
        /// Resolver used to resolve classes dependency
        /// </summary>
        public IResolver Resolver { get; private set; }
    }
}
