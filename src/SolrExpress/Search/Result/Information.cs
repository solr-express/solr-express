using System;

namespace SolrExpress.Search.Result
{
    /// <summary>
    /// Information about search result
    /// </summary>
    public sealed class Information
    {
        /// <summary>
        /// Maximum size of any individual subset
        /// </summary>
        public long PageSize { get; set; }

        /// <summary>
        /// One-based index of this subset within the superset
        /// </summary>
        public long? PageNumber { get; set; }

        /// <summary>
        /// Total number of subsets within the superset
        /// </summary>
        public long? PageCount { get; set; }

        /// <summary>
        /// Returns true if this is not the first subset within the superset, otherwise false
        /// </summary>
        public bool? HasPreviousPage { get; set; }

        /// <summary>
        /// Returns true if this is not the last subset within the superset, otherwise false
        /// </summary>
        public bool HasNextPage { get; set; }

        /// <summary>
        /// Returns true if this is the first subset within the superset, otherwise false
        /// </summary>
        public bool? IsFirstPage { get; set; }

        /// <summary>
        /// Returns true if this is the last subset within the superset, otherwise false
        /// </summary>
        public bool IsLastPage { get; set; }

        /// <summary>
        /// Total quantity of documents in the result
        /// </summary>
        public long DocumentCount { get; set; }

        /// <summary>
        /// Time to SOLR process requested search
        /// </summary>
        public TimeSpan ElapsedTime { get; set; }

        /// <summary>
        /// Value for cursorMark on the next call to continue paging through the results
        /// </summary>
        public string NextCursorMark { get; set; }
    }
}
