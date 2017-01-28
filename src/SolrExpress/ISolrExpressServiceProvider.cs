namespace SolrExpress
{
    /// <summary>
    /// Signatures to DI engine
    /// </summary>
    public interface ISolrExpressServiceProvider<TDocument>
        where TDocument : IDocument
    {
        /// <summary>
        /// Create a new instance of T using internal DI engine
        /// </summary>
        /// <returns>Instance of T</returns>
        T GetService<T>();
    }
}
