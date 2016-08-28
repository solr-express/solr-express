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
        public DocumentCollection(DocumentCollectionOptions<TDocument> options)
        {
            Checker.IsNull(options);

            this.Options = options;
        }

        /// <summary>
        /// Solr queryable instance to provide create queries in SOLR
        /// </summary>
        public ISolrSearch<TDocument> Select() => new SolrSearch<TDocument>(this.Options);

        /// <summary>
        /// Solr queryable instance to provide create queries in SOLR
        /// </summary>
        public ISolrAtomicUpdate<TDocument> Update() => new SolrAtomicUpdate<TDocument>(this.Options);

        /// <summary>
        /// SolrExpress options
        /// </summary>
        public DocumentCollectionOptions<TDocument> Options { get; private set; }
    }
}
