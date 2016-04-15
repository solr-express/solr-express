namespace SolrExpress.Core.Parameter
{
    public interface IQueryFieldParameter : IParameter
    {
        /// <summary>
        /// Configure current instance
        /// </summary>
        /// <param name="expression">Query used to make the query field</param>
        IQueryFieldParameter Configure(string expression);
    }
}
