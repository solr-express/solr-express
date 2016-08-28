namespace SolrExpress.Core.Search.Parameter
{
    /// <summary>
    /// Signatures to use in query field parameter
    /// </summary>
    public interface IQueryFieldParameter : ISearchParameter
    {
        /// <summary>
        /// Configure current instance
        /// </summary>
        /// <param name="expression">Query used to make the query field</param>
        IQueryFieldParameter Configure(string expression);

        /// <summary>
        /// Query used to make the query field
        /// </summary>
        string Expression { get; }
    }
}
