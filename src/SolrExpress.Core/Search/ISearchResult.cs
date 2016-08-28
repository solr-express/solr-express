using SolrExpress.Core.Search.Result;

namespace SolrExpress.Core.Search
{
    /// <summary>
    /// Signatures to SOLR query result with fluent API
    /// </summary>
    public interface ISearchResult<TDocument>
        where TDocument : IDocument
    {
        /// <summary>
        /// Get a instance of the informed type with parsed json resulted from the search
        /// </summary>
        /// <typeparam name="T">Concrete class that implements the IResultBuilder interface</typeparam>
        /// <returns>Instance of T ready to be used</returns>
        T Get<T>(T result)
            where T : IResult;
    }
}
