namespace SolrExpress.Core.Parameter
{
    /// <summary>
    /// Signatures to use in query field parameter
    /// </summary>
    public interface IQueryFieldParameter : IParameter
    {
        /// <summary>
        /// Configure current instance
        /// </summary>
        /// <param name="expression">Query used to make the query field</param>
        IQueryFieldParameter Configure(string expression);
    }
}
