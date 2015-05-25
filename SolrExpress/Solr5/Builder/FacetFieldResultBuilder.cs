using Newtonsoft.Json.Linq;
using SolrExpress.QueryBuilder;
using System.Collections.Generic;
using System.Linq;

namespace SolrExpress.Solr5.Builder
{
    /// <summary>
    /// Facet field data builder
    /// </summary>
    public class FacetFieldResultBuilder : IResultBuilder
    {
        /// <summary>
        /// Execute the parse of the JSON object in facet field list
        /// </summary>
        /// <param name="jsonObject">JSON object used in the parse</param>
        public void Execute(JObject jsonObject)
        {
            if (jsonObject["facets"] != null)
            {
                this.Data = new List<FacetKeyValue<string>>();

                var list = jsonObject["facets"].Children().Where(q =>
                    q is JProperty &&
                    q.Values().Count() == 1 &&
                    ((JProperty)q).Value is JObject &&
                    ((JProperty)q).Value["buckets"] != null);

                if (list.Any())
                {
                    foreach (var item in list)
                    {
                        var facet = new FacetKeyValue<string>()
                        {
                            Name = ((JProperty)item).Name,
                            Data = ((JProperty)(item)).Value["buckets"]
                                .ToDictionary(
                                    k => k["val"].ToObject<string>(),
                                    v => v["count"].ToObject<long>())
                        };

                        this.Data.Add(facet);
                    }
                }

                return;
            }

            throw new Exception.UnexpectedJsonFormatException(jsonObject.ToString());
        }

        public List<FacetKeyValue<string>> Data { get; set; }
    }
}
