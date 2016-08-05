namespace SolrExpress.Core.Query.Parameter
{
    /// <summary>
    /// Signatures to use in internal use
    /// </summary>
    public interface ISystemParameter : IParameter
    {
        /// <summary>
        /// Configure current instance
        /// </summary>
        ISystemParameter Configure();
    }
}
