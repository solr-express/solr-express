namespace SolrExpress.Core.Parameter
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
    public interface IParameter<TObject> : IParameter
    {
        /// <summary>
        /// Create the parameter
        /// </summary>
        /// <param name="container">Container to parameters to request to SOLR</param>
        void Execute(TObject container);
    }
}
