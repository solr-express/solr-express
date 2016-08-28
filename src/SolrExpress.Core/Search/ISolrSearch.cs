using System;
using System.Collections.Generic;

namespace SolrExpress.Core.Search
{
    /// <summary>
    /// Signatures to SOLR queryable
    /// </summary>
    public interface ISolrSearch<TDocument> : ICollection<ISearchItem>
        where TDocument : IDocument
    {
        /// <summary>
        /// Adds a search interceptor to the System.Collections.Generic.ICollection`1.
        /// </summary>
        /// <param name="builder">Builder to execute after interceptor creation</param>
        void Add<TQueryInterceptor>(Action<TQueryInterceptor> builder = null)
            where TQueryInterceptor : class, ISearchInterceptor, new();

        /// <summary>
        /// Adds a result interceptor to the System.Collections.Generic.ICollection`1.
        /// </summary>
        /// <param name="builder">Builder to execute after interceptor creation</param>
        void Add<TResultInterceptor>(Action<IResultInterceptor> builder = null)
            where TResultInterceptor : class, IResultInterceptor, new();

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
        /// SolrExpress options
        /// </summary>
        DocumentCollectionOptions<TDocument> Options { get; }
    }
}
