using SolrExpress.Core.Query.Result;

namespace SolrExpress.Core.Query
{
    /// <summary>
    /// Signatures to SOLR query result with fluent API
    /// </summary>
    public interface IQueryResult<TDocument>
        where TDocument : IDocument
    {
        /// <summary>
        /// Resolver used to resolve classes dependency
        /// </summary>
        IResolver Resolver { get; }

        /// <summary>
        /// Get a instance of the informed type with parsed json resulted from the search
        /// </summary>
        /// <typeparam name="T">Concrete class that implements the IResultBuilder interface</typeparam>
        /// <returns>Instance of T ready to be used</returns>
        T Get<T>(T result)
            where T : IResult;
    }
}
