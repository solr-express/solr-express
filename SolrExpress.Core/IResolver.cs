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
        TConcrete Get<TConcrete>()
            where TConcrete : class;

        /// <summary>
        /// Get concrete class that implements informed interface
        /// </summary>
        /// <param name="settings">Settings to configure concrete class</param>
        /// <returns>Concrete class</returns>
        TConcrete Get<TConcrete>(ISettings settings)
            where TConcrete : class;
    }
}
