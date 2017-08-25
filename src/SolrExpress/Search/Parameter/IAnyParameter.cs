namespace SolrExpress.Search.Parameter
{
    /// <summary>
    /// Any parameter
    /// </summary>
    public interface IAnyParameter : ISearchParameter
    {
        /// <summary>
        /// Name of parameter
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Value of parameter
        /// </summary>
        string Value { get; set; }
    }
}
