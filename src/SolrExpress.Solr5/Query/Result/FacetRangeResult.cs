using Newtonsoft.Json.Linq;
using SolrExpress.Core;
using SolrExpress.Core.Extension.Internal;
using SolrExpress.Core.Query.Parameter;
using SolrExpress.Core.Query.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace SolrExpress.Solr5.Query.Result
{
    /// <summary>
    /// Facet range data builder
    /// </summary>
    public sealed class FacetRangeResult<TDocument> : IFacetRangeResult<TDocument>, IConvertJsonObject
        where TDocument : IDocument
    {
        /// <summary>
        /// Get a FacetRange instance basead in the informed JTokenType
        /// </summary>
        /// <param name="type">Type used to return the instance</param>
        /// <returns>A FacetRange instance</returns>
        private FacetRange CreateFacetRange(Type type)
        {
            if (type == typeof(float))
            {
                return new FacetRange<float>();
            }

            if (type == typeof(decimal))
            {
                return new FacetRange<decimal>();
            }

            if (type == typeof(DateTime))
            {
                return new FacetRange<DateTime>();
            }

            return new FacetRange<int>();
        }

        /// <summary>
        /// Get facet value in strong type
        /// </summary>
        /// <param name="facetType">Facet type</param>
        /// <param name="value">Facet value</param>
        /// <returns></returns>
        private object GetFacetValue(Type facetType, string value)
        {
            if (facetType == typeof(float))
            {
                return Convert.ToSingle(value);
            }

            if (facetType == typeof(decimal))
            {
                return Convert.ToDecimal(value);
            }

            if (facetType == typeof(DateTime))
            {
                return Convert.ToDateTime(value);
            }

            return Convert.ToInt32(value);
        }

        /// <summary>
        /// Returns maximum value of gap
        /// </summary>
        /// <param name="facetType">Facet type</param>
        /// <param name="minimumValue">Minimum value of gap</param>
        /// <param name="gapValue">Gap of the facet range</param>
        /// <returns></returns>
        /// <summary>
        /// Returns maximum value of gap
        /// </summary>
        /// <param name="facetType">Facet type</param>
        /// <param name="minimumValue">Minimum value of gap</param>
        /// <param name="gapValue">Gap of the facet range</param>
        /// <returns></returns>
        private object CalculateMaximumValue(Type facetType, JToken minimumValue, object gapValue)
        {
            var value = minimumValue.ToObject(facetType);

            if (facetType == typeof(float))
            {
                return ((float)value) + Convert.ToSingle(gapValue);
            }

            if (facetType == typeof(decimal))
            {
                return ((decimal)value) + Convert.ToDecimal(gapValue);
            }

            if (facetType == typeof(DateTime))
            {
                return ((DateTime)value).Add((TimeSpan)gapValue);
            }

            return ((int)value) + Convert.ToInt32(gapValue);
        }

        /// <summary>
        /// Process gap value in gap object
        /// </summary>
        /// <param name="gap">Gap string returned from Solr</param>
        /// <returns>.Net object equivalent</returns>
        private object GetGapValue(string gap)
        {
            int objInt;
            float objSingle;

            if (int.TryParse(gap, out objInt))
            {
                return objInt;
            }

            if (float.TryParse(gap, out objSingle))
            {
                return objSingle;
            }

            // Assuming than gap is DateTime type
            var gapNumber = int.Parse(Regex.Replace(gap, "[^0-9]", string.Empty, RegexOptions.IgnoreCase));

            var keys = new Dictionary<string, DateTime>
            {
                ["MILISECOND"] = DateTime.MinValue.AddMilliseconds(1),
                ["SECOND"] = DateTime.MinValue.AddSeconds(1),
                ["MINUTE"] = DateTime.MinValue.AddMinutes(1),
                ["HOUR"] = DateTime.MinValue.AddHours(1),
                ["DAY"] = DateTime.MinValue.AddDays(1),
                ["WEAK"] = DateTime.MinValue.AddDays(7),
                ["MONTH"] = DateTime.MinValue.AddMonths(1),
                ["YEAR"] = DateTime.MinValue.AddYears(1)
            };

            var key = keys.FirstOrDefault(q => gap.Contains(q.Key));

            return new TimeSpan(key.Value.Ticks * gapNumber);
        }

        /// <summary>
        /// Execute the parse of the JSON object in facet range list
        /// </summary>
        /// <param name="parameters">List of the parameters arranged in the queryable class</param>
        /// <param name="jsonObject">JSON object used in the parse</param>
        public void Execute(List<IParameter> parameters, JObject jsonObject)
        {
            if (jsonObject["facets"] == null)
            {
                throw new UnexpectedJsonFormatException(jsonObject.ToString());
            }

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
                this.Data = new List<FacetKeyValue<FacetRange>>();
                return;
            }

            this.Data = list
                .Select(item =>
                {
                    var jProperty = ((JProperty)item);

                    var facet = new FacetKeyValue<FacetRange>
                    {
                        Name = jProperty.Name,
                        Data = new Dictionary<FacetRange, long>()
                    };

                    var facetParameter = (IFacetRangeParameter<TDocument>)parameters.First(q =>
                    {
                        return
                            (q is IFacetRangeParameter<TDocument>) &&
                            ((IFacetRangeParameter<TDocument>)q).AliasName.Equals(facet.Name);
                    });

                    var facetType = facetParameter.Expression.GetPropertyTypeFromExpression();

                    var gapValue = this.GetGapValue(facetParameter.Gap);

                    var firstValue = this.CreateFacetRange(facetType);
                    firstValue.SetMaximumValue(this.GetFacetValue(facetType, facetParameter.Start));
                    facet.Data.Add(firstValue, jProperty.Value["before"]["count"].ToObject<long>());

                    var facetData = jProperty
                        .Value["buckets"]
                        .ToDictionary(
                            k =>
                            {
                                var result = this.CreateFacetRange(facetType);

                                result.SetMinimumValue(k["val"].ToObject(facetType));
                                result.SetMaximumValue(this.CalculateMaximumValue(facetType, k["val"], gapValue));

                                return result;
                            },
                            v => v["count"].ToObject<long>());

                    var lastValue = this.CreateFacetRange(facetType);
                    lastValue.SetMaximumValue(this.GetFacetValue(facetType, facetParameter.End));
                    facet.Data.Add(lastValue, jProperty.Value["after"]["count"].ToObject<long>());

                    return facet;
                })
                .ToList();
        }

        /// <summary>
        /// Facet data
        /// </summary>
        public List<FacetKeyValue<FacetRange>> Data { get; set; }
    }
}
