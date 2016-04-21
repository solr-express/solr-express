namespace SolrExpress.Core
{
    /// <summary>
    /// Signatures of classes dependency resolver
    /// </summary>
    public interface IResolver
    {
        /// <summary>
        /// Get concrete class that implements informed interface
        /// </summary>
        /// <returns>Concrete class</returns>
        T Get<T>();
    }
}
