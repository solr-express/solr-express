using Newtonsoft.Json.Linq;
using SolrExpress.Core;
using SolrExpress.Core.Result;
using System.Collections.Generic;
using System.Linq;

namespace SolrExpress.Solr5.Result
{
    /// <summary>
    /// Facet field data builder
    /// </summary>
    public sealed class FacetFieldResult<TDocument> : IFacetFieldResult<TDocument>, IConvertJsonObject
        where TDocument : IDocument
    {
        /// <summary>
        /// Execute the JSON object parse in facet field list
        /// </summary>
        /// <param name="jsonObject">JSON object used in the parse</param>
        public void Execute(JObject jsonObject)
        {
            Checker.IsTrue<UnexpectedJsonFormatException>(jsonObject["facets"] == null, jsonObject.ToString());

            this.Data = new List<FacetKeyValue<string>>();

            var list = jsonObject["facets"]
                .Children()
                .Where(q =>
                    q is JProperty &&
                    q.Values().Count() == 1 &&
                    ((JProperty)q).Value is JObject &&
                    ((JProperty)q).Value["buckets"] != null)
                .ToList();

            if (!list.Any())
            {
                return;
            }

            var facets = list
                .Select(item => new FacetKeyValue<string>()
                {
                    Name = ((JProperty)item).Name,
                    Data = ((JProperty)(item)).Value["buckets"]
                        .ToDictionary(
                            k => k["val"].ToObject<string>(),
                            v => v["count"].ToObject<long>())
                });

            this.Data.AddRange(facets);
        }

        /// <summary>
        /// Facet data
        /// </summary>
        public List<FacetKeyValue<string>> Data { get; set; }
    }
}
