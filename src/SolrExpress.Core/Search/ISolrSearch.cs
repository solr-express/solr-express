using SolrExpress.Core.DependencyInjection;
using System;

namespace SolrExpress.Core.Search
{
    /// <summary>
    /// Signatures to SOLR queryable
    /// </summary>
    public interface ISolrSearch<TDocument>
        where TDocument : IDocument
    {
        /// <summary>
        /// Add an item to search
        /// </summary>
        /// <param name="parameter">Parameter to add in the query</param>
        ISolrSearch<TDocument> Add(ISearchItem item);

        /// <summary>
        /// Adds a search interceptor to the System.Collections.Generic.ICollection`1.
        /// </summary>
        /// <param name="builder">Builder to execute after interceptor creation</param>
        ISolrSearch<TDocument> Add<TQueryInterceptor>(Action<TQueryInterceptor> builder = null)
            where TQueryInterceptor : class, ISearchInterceptor, new();

        /// <summary>
        /// Adds a result interceptor to the System.Collections.Generic.ICollection`1.
        /// </summary>
        /// <param name="builder">Builder to execute after interceptor creation</param>
        ISolrSearch<TDocument> Add<TResultInterceptor>(Action<IResultInterceptor> builder = null)
            where TResultInterceptor : class, IResultInterceptor, new();

        /// <summary>
        /// Determines whether an element is in current list
        /// </summary>
        /// <param name="item">Item to add</param>
        /// <returns>Returns true if item is found in list, otherwise false</returns>
        bool Contains(ISearchItem item);

        /// <summary>
        /// Handler name used in solr request
        /// </summary>
        /// <param name="name">Name to be used</param>
        /// <returns>Itself</returns>
        ISolrSearch<TDocument> SetHandler(string name);

        /// <summary>
        /// Execute the search in the solr with informed parameters
        /// </summary>
        /// <returns>Solr result</returns>
        ISearchResult<TDocument> Execute();

        /// <summary>
        /// Gets the number of elements
        /// </summary>
        int Count { get; }

        /// <summary>
        /// SolrExpress options
        /// </summary>
        DocumentCollectionOptions<TDocument> Options { get; }

        /// <summary>
        /// Services container
        /// </summary>
        IEngine Engine { get; }
    }
}
