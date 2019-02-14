using SolrExpress.Search;
using SolrExpress.Update;
using SolrExpress.Utility;

namespace SolrExpress
{
    /// <summary>
    /// SOLR document collection
    /// </summary>
    public class DocumentCollection<TDocument>
        where TDocument : Document
    {
        private readonly ISolrExpressServiceProvider<TDocument> _serviceProvider;

        public DocumentCollection(ISolrExpressServiceProvider<TDocument> serviceProvider)
        {
            Checker.IsNull(serviceProvider);

            this._serviceProvider = serviceProvider;
        }

        /// <summary>
        /// Document search provider
        /// </summary>
        public DocumentCollectionSearch<TDocument> Select() => this._serviceProvider.GetService<DocumentCollectionSearch<TDocument>>();

        /// <summary>
        /// Document update provider
        /// </summary>
        public DocumentCollectionUpdate<TDocument> Update() => this._serviceProvider.GetService<DocumentCollectionUpdate<TDocument>>();
    }
}
