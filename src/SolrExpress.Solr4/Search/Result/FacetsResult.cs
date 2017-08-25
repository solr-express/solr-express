using DaleNewman;
using Newtonsoft.Json;
using SolrExpress.Search.Parameter;
using SolrExpress.Search.Result;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace SolrExpress.Solr4.Search.Result
{
    public sealed class FacetsResult<TDocument> : IFacetsResult<TDocument>
        where TDocument : Document
    {
        public IEnumerable<IFacetItem> Data { get; private set; }

        /// <summary>
        /// Execute processing of facet fields
        /// </summary>
        private static FacetItemField ProcessFacetFields(JsonReader jsonReader)
        {
            var facetName = (string)jsonReader.Value;
            var facetItem = new FacetItemField(facetName);

            jsonReader.Read();// Start array
            jsonReader.Read();// First element

            while (jsonReader.TokenType != JsonToken.EndArray)
            {
                if (jsonReader.Path.StartsWith($"facet_counts.facet_fields.{facetName}"))
                {
                    var value = new FacetItemFieldValue
                    {
                        Key = (string)jsonReader.Value,
                        Quantity = (long)jsonReader.ReadAsInt32()
                    };

                    ((List<FacetItemFieldValue>)facetItem.Values).Add(value);
                }

                jsonReader.Read();
            }

            return facetItem;
        }

        /// <summary>
        /// Execute processing facet queries
        /// </summary>
        private static IEnumerable<FacetItemQuery> ProcessFacetQueries(JsonReader jsonReader)
        {
            var facetItemQueries = new List<FacetItemQuery>();

            jsonReader.Read();// Start object
            jsonReader.Read();// Start property name

            while (jsonReader.TokenType != JsonToken.EndObject)
            {
                var facetItem = new FacetItemQuery(
                    (string)jsonReader.Value,
                    (long)jsonReader.ReadAsInt32());

                facetItemQueries.Add(facetItem);

                jsonReader.Read();
            }

            return facetItemQueries.ToArray();
        }

        private static IFacetRangeParameter<TDocument> GetFacetRangeParameter(IEnumerable<ISearchParameter> searchParameters, string facetName)
        {
            return (IFacetRangeParameter<TDocument>)searchParameters
                 .Where(parameter => parameter is IFacetRangeParameter<TDocument>)
                 .FirstOrDefault(parameter =>
                 {
                     var facetRangeParameter = (IFacetRangeParameter<TDocument>)parameter;

                     var fieldName = facetRangeParameter
                         .ExpressionBuilder
                         .GetFieldName(facetRangeParameter.FieldExpression);

                     return
                         facetName.Equals(facetRangeParameter.AliasName) ||
                         facetName.Equals(fieldName);
                 });
        }

        private static object GetMaximumValue(Type fieldType, IFacetItemRangeValue item, string facetGap)
        {
            if (string.IsNullOrWhiteSpace(facetGap))
            {
                return null;
            }

            if (typeof(DateTime) == fieldType)
            {
                // Prepare to DateMath
                facetGap = facetGap
                    .Replace("YEARS", "y")
                    .Replace("YEAR", "y")
                    .Replace("MONTHS", "M")
                    .Replace("MONTH", "M")
                    .Replace("DAYS", "d")
                    .Replace("DAY", "d")
                    .Replace("WEEKS", "w")
                    .Replace("WEEK", "w")
                    .Replace("HOURS", "h")
                    .Replace("HOUR", "h")
                    .Replace("MINUTES", "m")
                    .Replace("MINUTE", "m")
                    .Replace("SECONDS", "s")
                    .Replace("SECOND", "s");

                var minimumValue = ((FacetItemRangeValue<DateTime>)item).MinimumValue;
                return DateMath.Apply(minimumValue ?? DateTime.MinValue, facetGap);
            }

            if (typeof(decimal) == fieldType
               || typeof(float) == fieldType
               || typeof(double) == fieldType)
            {
                var minimumValue = ((FacetItemRangeValue<decimal>)item).MinimumValue;
                return (minimumValue ?? decimal.MinValue) + decimal.Parse(facetGap, CultureInfo.InvariantCulture);
            }

            {
                var minimumValue = ((FacetItemRangeValue<int>)item).MinimumValue;
                return (minimumValue ?? int.MinValue) + int.Parse(facetGap, CultureInfo.InvariantCulture);
            }
        }

        private static object GetParsedRangeValue(Type fieldType, string value = null)
        {
            if (typeof(DateTime) == fieldType)
            {
                if (!string.IsNullOrWhiteSpace(value) && DateTime.TryParse(value, out var typedValue))
                {
                    return typedValue;
                }

                return null;
            }
            if (typeof(decimal) == fieldType
                || typeof(float) == fieldType
                || typeof(double) == fieldType)
            {
                return string.IsNullOrWhiteSpace(value) ? (decimal?)null : decimal.Parse(value, CultureInfo.InvariantCulture);
            }

            return string.IsNullOrWhiteSpace(value) ? (int?)null : int.Parse(value);
        }

        private static IFacetItemRangeValue GetFacetItemRangeValue(Type fieldType, string rawMinimumValue = null, string rawMaximumValue = null)
        {
            if (typeof(DateTime) == fieldType)
            {
                var minimumValue = (DateTime?)GetParsedRangeValue(fieldType, rawMinimumValue);
                var maximumValue = (DateTime?)GetParsedRangeValue(fieldType, rawMaximumValue);

                return new FacetItemRangeValue<DateTime>(minimumValue, maximumValue);
            }
            if (typeof(decimal) == fieldType
                || typeof(float) == fieldType
                || typeof(double) == fieldType)
            {
                var minimumValue = (decimal?)GetParsedRangeValue(fieldType, rawMinimumValue);
                var maximumValue = (decimal?)GetParsedRangeValue(fieldType, rawMaximumValue);

                return new FacetItemRangeValue<decimal>(minimumValue, maximumValue);
            }
            else
            {
                var minimumValue = (int?)GetParsedRangeValue(fieldType, rawMinimumValue);
                var maximumValue = (int?)GetParsedRangeValue(fieldType, rawMaximumValue);

                return new FacetItemRangeValue<int>(minimumValue, maximumValue);
            }
        }

        /// <summary>
        /// Execute processing facet ranges
        /// </summary>
        /// <param name="searchParameters"></param>
        /// <param name="jsonReader"></param>
        private static FacetItemRange ProcessFacetRanges(IEnumerable<ISearchParameter> searchParameters, JsonReader jsonReader)
        {
            var facetName = (string)jsonReader.Value;
            var facetRangeParameter = GetFacetRangeParameter(searchParameters, facetName);
            var fieldType = facetRangeParameter
                .ExpressionBuilder
                .GetPropertyType(facetRangeParameter.FieldExpression);

            var facetItem = new FacetItemRange(facetName);

            string gapValue = null;
            string startValue = null;
            string endValue = null;
            long? quantityBefore = null;
            long? quantityAfter = null;

            jsonReader.Read();// Start array
            jsonReader.Read();// First element

            while (jsonReader.Path.StartsWith($"facet_counts.facet_ranges.{facetName}") && jsonReader.TokenType != JsonToken.EndObject)
            {
                if (jsonReader.Path.StartsWith($"facet_counts.facet_ranges.{facetName}.counts["))
                {
                    var rangeValue = GetFacetItemRangeValue(fieldType, jsonReader.Value.ToString());
                    rangeValue.Quantity = (long)jsonReader.ReadAsInt32();

                    ((List<IFacetItemRangeValue>)facetItem.Values).Add(rangeValue);
                }
                if (jsonReader.Path.StartsWith($"facet_counts.facet_ranges.{facetName}.gap"))
                {
                    gapValue = jsonReader.ReadAsString();
                }
                if (jsonReader.Path.StartsWith($"facet_counts.facet_ranges.{facetName}.start"))
                {
                    startValue = jsonReader.ReadAsString();
                }
                if (jsonReader.Path.StartsWith($"facet_counts.facet_ranges.{facetName}.end"))
                {
                    endValue = jsonReader.ReadAsString();
                }
                if (jsonReader.Path.StartsWith($"facet_counts.facet_ranges.{facetName}.before"))
                {
                    quantityBefore = (long)jsonReader.ReadAsInt32();
                }
                if (jsonReader.Path.StartsWith($"facet_counts.facet_ranges.{facetName}.after"))
                {
                    quantityAfter = (long)jsonReader.ReadAsInt32();
                }

                jsonReader.Read();
            }

            if (!string.IsNullOrWhiteSpace(gapValue))
            {
                ((List<IFacetItemRangeValue>)facetItem.Values)
                    .ForEach(item =>
                    {
                        var maximumValue = GetMaximumValue(fieldType, item, gapValue);
                        item.SetMaximumValue(maximumValue);
                    });
            }

            if (!string.IsNullOrWhiteSpace(startValue) && quantityBefore.HasValue)
            {
                var rangeValue = GetFacetItemRangeValue(fieldType, rawMaximumValue: startValue);
                rangeValue.Quantity = quantityBefore.Value;

                ((List<IFacetItemRangeValue>)facetItem.Values).Insert(0, rangeValue);
            }

            // ReSharper disable once InvertIf

            if (!string.IsNullOrWhiteSpace(endValue) && quantityAfter.HasValue)
            {
                var rangeValue = GetFacetItemRangeValue(fieldType, endValue);
                rangeValue.Quantity = quantityAfter.Value;

                ((List<IFacetItemRangeValue>)facetItem.Values).Add(rangeValue);
            }

            return facetItem;
        }

        void ISearchResult<TDocument>.Execute(IList<ISearchParameter> searchParameters, JsonToken currentToken, string currentPath, JsonReader jsonReader)
        {
            if (!currentPath.StartsWith("facet_counts."))
            {
                return;
            }

            this.Data = this.Data ?? new List<IFacetItem>();

            var facetItems = (List<IFacetItem>)((IFacetsResult<TDocument>)this).Data;

            if (jsonReader.Path.StartsWith("facet_counts.facet_fields."))
            {
                facetItems.Add(ProcessFacetFields(jsonReader));
            }
            if (jsonReader.Path.StartsWith("facet_counts.facet_queries"))
            {
                facetItems.AddRange(ProcessFacetQueries(jsonReader));
            }
            if (jsonReader.Path.StartsWith("facet_counts.facet_ranges."))
            {
                facetItems.Add(ProcessFacetRanges(searchParameters, jsonReader));
            }
        }
    }
}
