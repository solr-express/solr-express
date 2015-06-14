using System;
using Newtonsoft.Json.Linq;
using SolrExpress.Core.Exception;
using SolrExpress.Core.Query;

namespace SolrExpress.Solr4.Builder
{
    /// <summary>
    /// Statistic data builder
    /// </summary>
    public sealed class StatisticResultBuilder : IResultBuilder, IConvertJsonObject
    {
        /// <summary>
        /// Execute the parse of the JSON object in statistic
        /// </summary>
        /// <param name="jsonObject">JSON object used in the parse</param>
        public void Execute(JObject jsonObject)
        {
            if ((jsonObject["response"] != null) && (jsonObject["response"]["numFound"] != null) &&
                (jsonObject["responseHeader"] != null) && (jsonObject["responseHeader"]["QTime"] != null))
            {
                this.DocumentCount = jsonObject["response"]["numFound"].ToObject<long>();
                this.IsEmpty = this.DocumentCount.Equals(0);

                var qTime = jsonObject["responseHeader"]["QTime"].ToObject<int>();
                this.TimeToExecution = new TimeSpan(0, 0, 0, 0, qTime);

                return;
            }

            throw new UnexpectedJsonFormatException(jsonObject.ToString());
        }

        /// <summary>
        /// True if search result return empty result, false otherwise
        /// </summary>
        public bool IsEmpty { get; private set; }

        /// <summary>
        /// Total quantity of documents in the result
        /// </summary>
        public long DocumentCount { get; private set; }

        /// <summary>
        /// Time to SOLR process the requested search
        /// </summary>
        public TimeSpan TimeToExecution { get; private set; }
    }
}
