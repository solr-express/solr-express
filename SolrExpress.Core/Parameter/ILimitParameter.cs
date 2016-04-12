namespace SolrExpress.Core.Parameter
{
    /// <summary>
    /// Signatures to use in limit parameter
    /// </summary>
    public interface ILimitParameter : IParameter
    {
        /// <summary>
        /// Value of limit
        /// </summary>
        int Value { get; set; }
    }
}
