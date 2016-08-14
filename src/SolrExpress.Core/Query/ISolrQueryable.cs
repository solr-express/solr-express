using SolrExpress.Core.Query.Parameter;

namespace SolrExpress.Core.Query
{
    /// <summary>
    /// Signatures to SOLR queryable
    /// </summary>
    public interface ISolrQueryable<TDocument>
        where TDocument : IDocument
    {

        /// <summary>
        /// Add a parameters to the query
        /// </summary>
        /// <param name="parameters">Parameters to add in the query</param>
        /// <returns>Itself</returns>
        ISolrQueryable<TDocument> Parameter(params IParameter[] parameters);

        /// <summary>
        /// Add a parameter to the query
        /// </summary>
        /// <param name="parameter">Parameter to add in the query</param>
        /// <returns>Itself</returns>
        ISolrQueryable<TDocument> Parameter(IParameter parameter);

        /// <summary>
        /// Add a query interceptor to the queryable
        /// </summary>
        /// <param name="interceptors">Query interceptors to add in the queryable</param>
        /// <returns>Itself</returns>
        ISolrQueryable<TDocument> QueryInterceptor(params IQueryInterceptor[] interceptors);

        /// <summary>
        /// Add a query interceptor to the queryable
        /// </summary>
        /// <param name="interceptor">Query interceptor to add in the queryable</param>
        /// <returns>Itself</returns>
        ISolrQueryable<TDocument> QueryInterceptor(IQueryInterceptor interceptor);

        /// <summary>
        /// Add a query interceptor to the queryable
        /// </summary>
        /// <returns>Itself</returns>
        ISolrQueryable<TDocument> QueryInterceptor<TQueryInterceptor>()
            where TQueryInterceptor : class, IQueryInterceptor, new();

        /// <summary>
        /// Add a result interceptor to the queryable
        /// </summary>
        /// <param name="interceptors">Result interceptors to add in the queryable</param>
        /// <returns>Itself</returns>
        ISolrQueryable<TDocument> ResultInterceptor(params IResultInterceptor[] interceptors);

        /// <summary>
        /// Add a result interceptor to the queryable
        /// </summary>
        /// <param name="interceptor">The result interceptor to add in the queryable</param>
        /// <returns>Itself</returns>
        ISolrQueryable<TDocument> ResultInterceptor(IResultInterceptor interceptor);

        /// <summary>
        /// Add a result interceptor to the queryable
        /// </summary>
        /// <returns>Itself</returns>
        ISolrQueryable<TDocument> ResultInterceptor<TResultInterceptor>()
            where TResultInterceptor : class, IResultInterceptor, new();

        /// <summary>
        /// Handler name used in solr request
        /// </summary>
        /// <param name="name">Name to be used</param>
        /// <returns>Itself</returns>
        ISolrQueryable<TDocument> Handler(string name);

        /// <summary>
        /// Execute the search in the solr with informed parameters
        /// </summary>
        /// <returns>Solr result</returns>
        QueryResult<TDocument> Execute();

        /// <summary>
        /// Configurations about SolrQueriable behavior
        /// </summary>
        Configuration Configuration { get; }

        /// <summary>
        /// Provider used to resolve the expression
        /// </summary>
        IProvider Provider { get; }

        /// <summary>
        /// Resolver used to resolve classes dependency
        /// </summary>
        IResolver Resolver { get; }
    }
}
