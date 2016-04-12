namespace SolrExpress.Core.Parameter
{
    /// <summary>
    /// Signatures to use in offset parameter
    /// </summary>
    public interface IOffsetParameter : IParameter
    {
        /// <summary>
        /// Value of limit
        /// </summary>
        int Value { get; set; }
    }
}
