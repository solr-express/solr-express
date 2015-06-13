using Newtonsoft.Json.Linq;
using SolrExpress.Core.Exception;
using SolrExpress.Core.Query;
using System.Collections.Generic;
using System.Linq;

namespace SolrExpress.Solr5.Builder
{
    /// <summary>
    /// Facet field data builder
    /// </summary>
    public sealed class FacetFieldResultBuilder : IResultBuilder, IConvertJsonObject
    {
        /// <summary>
        /// Execute the JSON object parse in facet field list
        /// </summary>
        /// <param name="jsonObject">JSON object used in the parse</param>
        public void Execute(JObject jsonObject)
        {
            if (jsonObject["facets"] == null)
            {
                throw new UnexpectedJsonFormatException(jsonObject.ToString());
            }

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

        public List<FacetKeyValue<string>> Data { get; set; }
    }
}
