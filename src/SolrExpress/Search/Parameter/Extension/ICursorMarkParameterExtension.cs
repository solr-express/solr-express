namespace SolrExpress.Search.Parameter.Extension
{
    /// <summary>
    /// Extensions to configure boost parameter
    /// </summary>
    public static class ICursorMarkParameterExtension
    {
        /// <summary>
        /// Configure mark used to paging through the results
        /// </summary>
        /// <param name="parameter">Parameter to configure</param>
        /// <param name="value">Mark used to paging through the results</param>
        public static ICursorMarkParameter CursorMark(this ICursorMarkParameter parameter, string value)
        {
            parameter.CursorMark = value;

            return parameter;
        }
    }
}
