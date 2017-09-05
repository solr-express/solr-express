namespace SolrExpress.Search.Parameter
{
    /// <summary>
    /// Cursor mark parameter
    /// </summary>
    public interface ICursorMarkParameter : ISearchParameter
    {
        /// <summary>
        /// Mark used to paging through the results
        /// </summary>
        string CursorMark { get; set; }
    }
}