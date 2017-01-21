using System.Collections.Generic;

namespace SolrExpress.Search
{
    /// <summary>
    /// Document search engine
    /// </summary>
    /// <typeparam name="TDocument"></typeparam>
    public class DocumentSearch<TDocument>
        where TDocument : IDocument
    {
        /// <summary>
        /// Add an item to search
        /// </summary>
        /// <param name="item">Parameter to add in the query</param>
        public DocumentSearch<TDocument> Add(ISearchItem item)
        {
            return this;
        }

        /// <summary>
        /// Add items to search
        /// </summary>
        /// <param name="items">Parameter to add in the query</param>
        public DocumentSearch<TDocument> AddRange(IEnumerable<ISearchItem> items)
        {
            return this;
        }
    }
}
