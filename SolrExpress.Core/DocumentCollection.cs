using SolrExpress.Core.Query;
using System;

namespace SolrExpress.Core
{
    /// <summary>
    /// SOLR document collection
    /// </summary>
    public class DocumentCollection<TDocument>
        where TDocument : IDocument
    {
        /// <summary>
        /// Lazy solr queryable instance to provide create queries in SOLR
        /// </summary>
        private Lazy<SolrQueryable<TDocument>> _select;

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
            this._select = new Lazy<SolrQueryable<TDocument>>(() => new SolrQueryable<TDocument>(this.Provider, this.Resolver, this._configuration));
        }

        /// <summary>
        /// Add informed documents in SOLR collection
        /// If a doc with same id exists in collection, the document is updated
        /// </summary>
        /// <param name="documents">Documents to add</param>
        public void Add(params TDocument[] documents)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Remove informed documents from SOLR collection
        /// </summary>
        /// <param name="documents">Documents to add</param>
        public void Remove(params TDocument[] documents)
        {
            throw new NotImplementedException();
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
        public SolrQueryable<TDocument> Select => _select.Value;
    }
}
