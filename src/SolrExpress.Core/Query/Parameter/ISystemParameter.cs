namespace SolrExpress.Core.Query.Parameter
{
    /// <summary>
    /// Signatures to use in internal use
    /// </summary>
    internal interface ISystemParameter : IParameter
    {
        /// <summary>
        /// Configure current instance
        /// </summary>
        ISystemParameter Configure();
    }
}
