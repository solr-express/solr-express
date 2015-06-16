namespace SolrExpress.Core.Query
{
    /// <summary>
    /// Signatures to use in solr parameter
    /// </summary>
    public interface IParameter
    {
        /// <summary>
        /// True to indicate multiple instance of the parameter, otherwise false
        /// </summary>
        bool AllowMultipleInstances { get; }
    }

    /// <summary>
    /// Signatures to use in solr parameter
    /// </summary>
    public interface IParameter<T> : IParameter
    {
        /// <summary>
        /// Create the parameter
        /// </summary>
        /// <param name="container">Container to parameters to request to SOLR</param>
        void Execute(T container);
    }
}
