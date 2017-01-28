using System;
using SolrExpress.Search;
using SolrExpress.Update;

namespace SolrExpress
{
    /// <summary>
    /// SOLR document collection
    /// </summary>
    public class DocumentCollection<TDocument>
        where TDocument : IDocument
    {
        /// <summary>
        /// Document search provider
        /// </summary>
        public DocumentSearch<TDocument> Select()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Document update provider
        /// </summary>
        public DocumentUpdate<TDocument> Update()
        {
            throw new NotImplementedException();
        }
    }
}
