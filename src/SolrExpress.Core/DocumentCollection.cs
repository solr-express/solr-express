using SolrExpress.Core.DependencyInjection;
using SolrExpress.Core.Search;
using SolrExpress.Core.Update;
using SolrExpress.Core.Utility;

namespace SolrExpress.Core
{
    /// <summary>
    /// SOLR document collection
    /// </summary>
    public class DocumentCollection<TDocument> : IDocumentCollection<TDocument>
        where TDocument : IDocument
    {
        /// <summary>
        /// Default constructor of class
        /// </summary>
        /// <param name="options">SolrExpress options</param>
        /// <param name="engine">Services container</param>
        public DocumentCollection(DocumentCollectionOptions<TDocument> options, IEngine engine)
        {
            Checker.IsNull(options);
            Checker.IsNull(engine);

            this.Options = options;
            this.Engine = engine;
        }

        /// <summary>
        /// Solr queryable instance to provide create queries in SOLR
        /// </summary>
        public ISolrSearch<TDocument> Select() => this.Engine.GetService<ISolrSearch<TDocument>>();

        /// <summary>
        /// Solr queryable instance to provide create queries in SOLR
        /// </summary>
        public ISolrAtomicUpdate<TDocument> Update() => this.Engine.GetService<ISolrAtomicUpdate<TDocument>>();

        /// <summary>
        /// Services container
        /// </summary>
        public IEngine Engine { get; private set; }

        /// <summary>
        /// SolrExpress options
        /// </summary>
        public DocumentCollectionOptions<TDocument> Options { get; private set; }
    }
}
