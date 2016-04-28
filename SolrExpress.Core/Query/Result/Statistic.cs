using System;

namespace SolrExpress.Core.Query.Result
{
    public sealed class Statistic
    {
        /// <summary>
        /// True if search result return empty result, false otherwise
        /// </summary>
        public bool IsEmpty { get; set; }

        /// <summary>
        /// Total quantity of documents in the result
        /// </summary>
        public long DocumentCount { get; set; }

        /// <summary>
        /// Time to SOLR process requested search
        /// </summary>
        public TimeSpan ElapsedTime { get; set; }
    }
}
