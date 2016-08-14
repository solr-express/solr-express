using SolrExpress.Core.Query;
using SolrExpress.Core.Update;

namespace SolrExpress.Core
{
    /// <summary>
    /// SOLR document collection
    /// </summary>
    public class DocumentCollection<TDocument>: IDocumentCollection<TDocument>
        where TDocument : IDocument
    {
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
            this.Configuration = configuration;
        }

        /// <summary>
        /// Solr queryable instance to provide create queries in SOLR
        /// </summary>
        public ISolrQueryable<TDocument> Select() => new SolrQueryable<TDocument>(this.Provider, this.Resolver, this.Configuration);

        /// <summary>
        /// Solr queryable instance to provide create queries in SOLR
        /// </summary>
        public ISolrAtomicUpdate<TDocument> Update() => new SolrAtomicUpdate<TDocument>(this.Provider, this.Resolver, this.Configuration);

        /// <summary>
        /// Provider used to resolve the expression
        /// </summary>
        public IProvider Provider { get; private set; }

        /// <summary>
        /// Resolver used to resolve classes dependency
        /// </summary>
        public IResolver Resolver { get; private set; }

        /// <summary>
        /// Configurations about SolrQueriable behavior
        /// </summary>
        public Configuration Configuration { get; private set; }
    }
}
