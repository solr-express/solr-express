using Newtonsoft.Json.Linq;
using SolrExpress.Core.Exception;
using SolrExpress.Core.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using SolrExpress.Core.Entity;
using SolrExpress.Core.Helper;

namespace SolrExpress.Solr4.Builder
{
    /// <summary>
    /// Facet range data builder
    /// </summary>
    public sealed class FacetRangeResultBuilder : IResultBuilder, IConvertJsonObject
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
            var first = facetData.First();
            var second = facetData.Skip(1).FirstOrDefault();
            var last = facetData.Last();

            var gap = GenericHelper.Subtract(((FacetRange<TFacetKey>)second.Key).MinimumValue, ((FacetRange<TFacetKey>)first.Key).MinimumValue);

            foreach (var range in facetData)
            {
                ((FacetRange<TFacetKey>)range.Key).MaximumValue = GenericHelper.Addition(((FacetRange<TFacetKey>)range.Key).MinimumValue, gap);
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
            if (jsonObject["facet_counts"] == null || jsonObject["facet_counts"]["facet_ranges"] == null)
            {
                throw new UnexpectedJsonFormatException(jsonObject.ToString());
            }

            var list = jsonObject["facet_counts"]["facet_ranges"]
                .Children()
                .ToList();

            this.Data = new List<FacetKeyValue<FacetRange>>();

            foreach (var item in list)
            {
                var facet = new FacetKeyValue<FacetRange>()
                {
                    Name = ((JProperty)item).Name,
                    Data = new Dictionary<FacetRange, long>()
                };

                var facetData = new Dictionary<FacetRange, long>();

                var array = (JArray)((JProperty)item).Value["counts"];

                var jTokenType = array[1].Type;

                for (int i = 0; i < array.Count; i += 2)
                {
                    var result = this.GetFacetRangeByType(jTokenType);

                    result.SetMinimumValue(array[i + 1].ToObject(result.GetKeyType()));

                    facetData.Add(result, array[i + 1].ToObject<long>());
                }

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

                facet.Data.Add(before, ((JProperty)(item)).Value["before"].ToObject<long>());

                foreach (var facetDataItem in facetData)
                {
                    facet.Data.Add(facetDataItem.Key, facetDataItem.Value);
                }

                facet.Data.Add(after, ((JProperty)(item)).Value["after"].ToObject<long>());

                this.Data.Add(facet);
            }
        }

        public List<FacetKeyValue<FacetRange>> Data { get; set; }
    }
}
