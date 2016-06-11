using Newtonsoft.Json.Linq;
using SolrExpress.Core;
using SolrExpress.Core.Query.Result;
using System;

namespace SolrExpress.Solr5.Query.Result
{
    /// <summary>
    /// Statistic data builder
    /// </summary>
    public sealed class StatisticResult<TDocument> : IStatisticResult<TDocument>, IConvertJsonObject
        where TDocument : IDocument
    {
        /// <summary>
        /// Execute the statistic parse of the json
        /// </summary>
        /// <param name="jsonObject">JSON object used in the parse</param>
        public void Execute(JObject jsonObject)
        {
            if (jsonObject["response"]?["numFound"] == null || jsonObject["responseHeader"]?["QTime"] == null)
            {
                throw new UnexpectedJsonFormatException(jsonObject.ToString());
            }

            var qTime = jsonObject["responseHeader"]["QTime"].ToObject<int>();
            var documentCount = jsonObject["response"]["numFound"].ToObject<long>();

            this.Data = new Statistic
            {
                DocumentCount = documentCount,
                IsEmpty = documentCount.Equals(0),
                ElapsedTime = new TimeSpan(0, 0, 0, 0, qTime)
            };
        }

        /// <summary>
        /// Facet data
        /// </summary>
        public Statistic Data { get; set; }
    }
}
