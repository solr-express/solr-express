using Newtonsoft.Json.Linq;
using SolrExpress.Core.Exception;
using SolrExpress.Core.Query;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SolrExpress.Solr5.Builder
{
    /// <summary>
    /// Facet range data builder
    /// </summary>
    public sealed class FacetRangeResultBuilder : IResultBuilder
    {
        /// <summary>
        /// Get a FacetRange instance basead in the informed JTokenType
        /// </summary>
        /// <param name="type">JTokenType used to return the instance</param>
        /// <returns>A FacetRange instance</returns>
        private FacetRange GetFacetRangeByType(JTokenType type)
        {
            switch (type)
            {
                case JTokenType.Float:
                    return new FacetRange<float>();
                case JTokenType.Date:
                    return new FacetRange<DateTime>();
                default:
                    return new FacetRange<int>();
            }
        }

        private void ProcessGap<TFacetKey>(Dictionary<FacetRange, long> facetData, FacetRange facetBefore, FacetRange facetAfter)
            where TFacetKey : struct, IComparable
        {
            dynamic first = facetData.First();
            dynamic second = facetData.Skip(1).FirstOrDefault();
            var last = facetData.Last();

            // Do with dynamic because otherwise the error below occurring
            // Operator '-' cannot be applied to operands of type 'TFacetKey?' and 'TFacetKey?'
            // TODO: Is this behavior expected? Why is this exception being throwed?
            var gap = second.Key.MinimumValue - first.Key.MinimumValue;

            foreach (var range in facetData)
            {
                ((FacetRange<TFacetKey>)range.Key).MaximumValue = ((FacetRange<TFacetKey>)range.Key).MinimumValue + gap;
            }

            ((FacetRange<TFacetKey>)facetBefore).MaximumValue = ((FacetRange<TFacetKey>)first.Key).MinimumValue;
            ((FacetRange<TFacetKey>)facetAfter).MinimumValue = ((FacetRange<TFacetKey>)last.Key).MaximumValue;
        }

        /// <summary>
        /// Execute the parse of the JSON object in facet range list
        /// </summary>
        /// <param name="jsonObject">JSON object used in the parse</param>
        public void Execute(JObject jsonObject)
        {
            if (jsonObject["facets"] != null)
            {
                this.Data = new List<FacetKeyValue<FacetRange>>();

                var list = jsonObject["facets"]
                    .Children()
                    .Where(q =>
                        q is JProperty &&
                        q.Values().Count() == 3 &&
                        ((JProperty)q).Value is JObject &&
                        ((JProperty)q).Value["after"] != null &&
                        ((JProperty)q).Value["before"] != null &&
                        ((JProperty)q).Value["buckets"] != null)
                    .ToList();

                if (!list.Any())
                {
                    return;
                }

                foreach (var item in list)
                {
                    var jTokenType = ((JProperty)(item)).Value["buckets"][0]["val"].Type;

                    var facet = new FacetKeyValue<FacetRange>()
                    {
                        Name = ((JProperty)item).Name,
                        Data = new Dictionary<FacetRange, long>()
                    };

                    var facetData = ((JProperty)(item)).Value["buckets"]
                        .ToDictionary(
                            k =>
                            {
                                var result = this.GetFacetRangeByType(jTokenType);

                                result.SetMinimumValue(k["val"].ToObject(result.GetKeyType()));

                                return result;
                            },
                            v => v["count"].ToObject<long>());

                    var before = this.GetFacetRangeByType(jTokenType);
                    var after = this.GetFacetRangeByType(jTokenType);

                    switch (jTokenType)
                    {
                        case JTokenType.Float:
                            // TODO: Make unit test to this
                            this.ProcessGap<float>(facetData, before, after);
                            break;
                        case JTokenType.Date:
                            // TODO: Make unit test to this
                            this.ProcessGap<DateTime>(facetData, before, after);
                            break;
                        default:
                            this.ProcessGap<int>(facetData, before, after);
                            break;
                    }

                    facet.Data.Add(before, ((JProperty)(item)).Value["before"]["count"].ToObject<long>());

                    foreach (var facetDataItem in facetData)
                    {
                        facet.Data.Add(facetDataItem.Key, facetDataItem.Value);
                    }

                    facet.Data.Add(after, ((JProperty)(item)).Value["after"]["count"].ToObject<long>());

                    this.Data.Add(facet);
                }

                return;
            }

            throw new UnexpectedJsonFormatException(jsonObject.ToString());
        }

        public List<FacetKeyValue<FacetRange>> Data { get; set; }
    }
}
