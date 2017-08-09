using Newtonsoft.Json;
using SolrExpress.Search.Parameter;
using SolrExpress.Search.Result;
using SolrExpress.Serialization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace SolrExpress.Solr4.Search.Result
{
    public class FacetsResult<TDocument> : IFacetsResult<TDocument>
        where TDocument : Document
    {
        IEnumerable<IFacetItem> IFacetsResult<TDocument>.Data { get; set; }

        private IFacetRangeParameter<TDocument> GetFacetRangeParameter(List<ISearchParameter> searchParameters, string facetName)
        {
            return (IFacetRangeParameter<TDocument>)searchParameters
                 .Where(q => q is IFacetRangeParameter<TDocument>)
                 .FirstOrDefault(q =>
                 {
                     var facetRangeParameter = ((IFacetRangeParameter<TDocument>)q);

                     var fieldName = facetRangeParameter
                         .ExpressionBuilder
                         .GetFieldName(facetRangeParameter.FieldExpression);

                     return
                         facetName.Equals(facetRangeParameter.AliasName) ||
                         facetName.Equals(fieldName);
                 });
        }

        private object GetParsedRangeValue(Type fieldType, string value = null)
        {
            if (typeof(DateTime).Equals(fieldType))
            {
                if (string.IsNullOrWhiteSpace(value)
                    && DateTime.TryParse(value, CultureInfo.InvariantCulture, DateTimeStyles.None, out var typedValue))
                {
                    return typedValue;
                }

                //return string.IsNullOrWhiteSpace(value) ? (DateTime?)null : DateTime.Parse(value, CultureInfo.InvariantCulture);
                return null;
            }
            else if (typeof(decimal).Equals(fieldType)
                || typeof(float).Equals(fieldType)
                || typeof(double).Equals(fieldType))
            {
                return string.IsNullOrWhiteSpace(value) ? (decimal?)null : decimal.Parse(value, CultureInfo.InvariantCulture);
            }

            return string.IsNullOrWhiteSpace(value) ? (int?)null : int.Parse(value);
        }

        private IFacetItemRangeValue GetFacetItemRangeValue(Type fieldType, string rawMinimumValue = null, string rawMaximumValue = null)
        {
            if (typeof(DateTime).Equals(fieldType))
            {
                var minimumValue = (DateTime?)this.GetParsedRangeValue(fieldType, rawMinimumValue);
                var maximumValue = (DateTime?)this.GetParsedRangeValue(fieldType, rawMaximumValue);

                return new FacetItemRangeValue<DateTime>(minimumValue, maximumValue);
            }
            else if (typeof(decimal).Equals(fieldType)
                || typeof(float).Equals(fieldType)
                || typeof(double).Equals(fieldType))
            {
                var minimumValue = (decimal?)this.GetParsedRangeValue(fieldType, rawMinimumValue);
                var maximumValue = (decimal?)this.GetParsedRangeValue(fieldType, rawMaximumValue);

                return new FacetItemRangeValue<decimal>(minimumValue, maximumValue);
            }
            else
            {
                var minimumValue = (int?)this.GetParsedRangeValue(fieldType, rawMinimumValue);
                var maximumValue = (int?)this.GetParsedRangeValue(fieldType, rawMaximumValue);

                return new FacetItemRangeValue<int>(minimumValue, maximumValue);
            }
        }

        private void ExecuteFacetFields(JsonReader jsonReader)
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

            ((List<IFacetItem>)((IFacetsResult<TDocument>)this).Data).Add(facetItem);
        }

        private void ExecuteFacetQueries(JsonReader jsonReader)
        {
            jsonReader.Read();// Start object
            jsonReader.Read();// Start property name

            while (jsonReader.TokenType != JsonToken.EndObject)
            {
                var facetItem = new FacetItemQuery(
                    (string)jsonReader.Value,
                    (long)jsonReader.ReadAsInt32());

                ((List<IFacetItem>)((IFacetsResult<TDocument>)this).Data).Add(facetItem);

                jsonReader.Read();
            }
        }

        private void ExecuteFacetRanges(List<ISearchParameter> searchParameters, JsonReader jsonReader)
        {
            var facetName = (string)jsonReader.Value;
            var facetRangeParameter = this.GetFacetRangeParameter(searchParameters, facetName);
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
                    var rangeValue = this.GetFacetItemRangeValue(fieldType, jsonReader.Value.ToString());
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
                        var typedGapValue = this.GetParsedRangeValue(fieldType, gapValue);
                        item.CalculateMaximumValueUsingGap(typedGapValue);
                    });
            }

            if (!string.IsNullOrWhiteSpace(startValue) && quantityBefore.HasValue)
            {
                var rangeValue = this.GetFacetItemRangeValue(fieldType, rawMaximumValue: startValue);
                rangeValue.Quantity = quantityBefore.Value;

                ((List<IFacetItemRangeValue>)facetItem.Values).Insert(0, rangeValue);
            }

            if (!string.IsNullOrWhiteSpace(endValue) && quantityAfter.HasValue)
            {
                var rangeValue = this.GetFacetItemRangeValue(fieldType, endValue);
                rangeValue.Quantity = quantityAfter.Value;

                ((List<IFacetItemRangeValue>)facetItem.Values).Add(rangeValue);
            }

            ((List<IFacetItem>)((IFacetsResult<TDocument>)this).Data).Add(facetItem);
        }

        void ISearchResult.Execute(List<ISearchParameter> searchParameters, JsonToken currentToken, string currentPath, JsonReader jsonReader)
        {
            if (currentPath.StartsWith("facet_counts."))
            {
                var jsonSerializer = new JsonSerializer();
                jsonSerializer.Converters.Add(new GeoCoordinateConverter());
                jsonSerializer.Converters.Add(new DateTimeConverter());
                jsonSerializer.ContractResolver = new CustomContractResolver();

                if (((IFacetsResult<TDocument>)this).Data == null)
                {
                    ((IFacetsResult<TDocument>)this).Data = new List<IFacetItem>();
                }

                if (jsonReader.Path.StartsWith("facet_counts.facet_fields."))
                {
                    this.ExecuteFacetFields(jsonReader);
                }
                if (jsonReader.Path.StartsWith("facet_counts.facet_queries"))
                {
                    this.ExecuteFacetQueries(jsonReader);
                }
                if (jsonReader.Path.StartsWith("facet_counts.facet_ranges."))
                {
                    this.ExecuteFacetRanges(searchParameters, jsonReader);
                }
            }
        }
    }
}
