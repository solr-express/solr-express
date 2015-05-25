using Newtonsoft.Json.Linq;
using SolrExpress.QueryBuilder;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SolrExpress.Solr5.Builder
{
    /// <summary>
    /// Facet range data builder
    /// </summary>
    public class FacetRangeResultBuilder : IResultBuilder
    {
        /// <summary>
        /// Execute the parse of the JSON object in facet range list
        /// </summary>
        /// <param name="jsonObject">JSON object used in the parse</param>
        public void Execute(JObject jsonObject)
        {
            if (jsonObject["facets"] != null)
            {
                this.Data = new List<FacetKeyValue<FacetRange>>();

                var list = jsonObject["facets"].Children().Where(q =>
                    q is JProperty &&
                    q.Values().Count() == 3 &&
                    ((JProperty)q).Value is JObject &&
                    ((JProperty)q).Value["after"] != null &&
                    ((JProperty)q).Value["before"] != null &&
                    ((JProperty)q).Value["buckets"] != null);

                if (list.Any())
                {
                    foreach (var item in list)
                    {
                        var jTokenType = ((JProperty)(item)).Value["buckets"][0]["val"].Type;

                        var facet = new FacetKeyValue<FacetRange>()
                        {
                            Name = ((JProperty)item).Name,
                            Data = ((JProperty)(item)).Value["buckets"]
                                .ToDictionary(
                                    k =>
                                    {
                                        FacetRange result;

                                        switch (jTokenType)
                                        {
                                            case JTokenType.Integer:
                                                result = new FacetRange<int>()
                                                {
                                                    MinimumValue = k["val"].ToObject<int>()
                                                };
                                                break;
                                            case JTokenType.Float:
                                                result = new FacetRange<float>()
                                                {
                                                    MinimumValue = k["val"].ToObject<float>()
                                                };
                                                break;
                                            case JTokenType.Date:
                                                result = new FacetRange<DateTime>()
                                                {
                                                    MinimumValue = k["val"].ToObject<DateTime>()
                                                };
                                                break;
                                            default:
                                                result = new FacetRange<object>()
                                                {
                                                    MinimumValue = k["val"].ToObject<object>()
                                                };
                                                break;
                                        }

                                        return result;
                                    },
                                    v => v["count"].ToObject<long>())
                        };

                        FacetRange lowest;
                        FacetRange higher;

                        switch (jTokenType)
                        {
                            case JTokenType.Integer:
                                lowest = new FacetRange<int>();
                                higher = new FacetRange<int>();
                                break;
                            case JTokenType.Float:
                                lowest = new FacetRange<float>();
                                higher = new FacetRange<float>();
                                break;
                            case JTokenType.Date:
                                lowest = new FacetRange<DateTime>();
                                higher = new FacetRange<DateTime>();
                                break;
                            default:
                                lowest = new FacetRange<object>();
                                higher = new FacetRange<object>();
                                break;
                        }

                        facet.Data.Add(lowest, ((JProperty)(item)).Value["before"]["count"].ToObject<long>());
                        facet.Data.Add(higher, ((JProperty)(item)).Value["after"]["count"].ToObject<long>());

                        // TODO: Implement max value

                        this.Data.Add(facet);
                    }
                }

                return;
            }

            throw new Exception.UnexpectedJsonFormatException(jsonObject.ToString());
        }

        public List<FacetKeyValue<FacetRange>> Data { get; set; }
    }
}
