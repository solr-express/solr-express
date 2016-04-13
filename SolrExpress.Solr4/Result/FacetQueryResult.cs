using Newtonsoft.Json.Linq;
using SolrExpress.Core;
using SolrExpress.Core.Result;
using System.Collections.Generic;
using System.Linq;

namespace SolrExpress.Solr4.Builder
{
    /// <summary>
    /// Facet query data builder
    /// </summary>
    public sealed class FacetQueryResult<TDocument> : IFacetQueryResult<TDocument>, IConvertJsonObject
        where TDocument : IDocument
    {
        /// <summary>
        /// Execute the parse of the JSON object in facet query list
        /// </summary>
        /// <param name="jsonObject">JSON object used in the parse</param>
        public void Execute(JObject jsonObject)
        {
            Checker.IsTrue<UnexpectedJsonFormatException>(jsonObject["facet_counts"]?["facet_queries"] == null, jsonObject.ToString());

            var list = jsonObject["facet_counts"]["facet_queries"]
                .Children()
                .ToList();

            this.Data = list.ToDictionary(
                k => ((JProperty)k).Name,
                v => ((JProperty)v).Value.ToObject<long>());
        }

        public Dictionary<string, long> Data { get; set; }
    }
}
