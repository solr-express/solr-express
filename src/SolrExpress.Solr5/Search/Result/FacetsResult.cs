using DaleNewman;
using Newtonsoft.Json;
using SolrExpress.Search;
using SolrExpress.Search.Parameter;
using SolrExpress.Search.Result;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace SolrExpress.Solr5.Search.Result
{
    public class FacetsResult<TDocument> : IFacetsResult<TDocument>
        where TDocument : Document
    {
        IEnumerable<IFacetItem> IFacetsResult<TDocument>.Data { get; set; }

        private ISearchParameter GetSearchParameter(List<ISearchParameter> searchParameters, string facetName)
        {
            return searchParameters
                 .Where(parameter => parameter is IFacetRangeParameter<TDocument> || parameter is IFacetFieldParameter<TDocument>)
                 .FirstOrDefault(parameter =>
                 {
                     var searchItemFieldExpression = ((ISearchItemFieldExpression<TDocument>)parameter);

                     var fieldName = searchItemFieldExpression
                         .ExpressionBuilder
                         .GetFieldName(searchItemFieldExpression.FieldExpression);

                     var facetRangeParameter = parameter as IFacetRangeParameter<TDocument>;

                     return
                         facetName.Equals(facetRangeParameter?.AliasName) ||
                         facetName.Equals(fieldName);
                 });
        }

        private object GetParsedRangeValue(Type fieldType, string value = null)
        {
            if (typeof(DateTime).Equals(fieldType))
            {
                if (!string.IsNullOrWhiteSpace(value) && DateTime.TryParse(value, CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal, out var typedValue))
                {
                    return typedValue;
                }

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

        private void ProcessFacetFieldBuckets(JsonReader jsonReader, string facetName, IFacetItem facetItem)
        {
            jsonReader.Read();// Starts array

            while (jsonReader.Path.StartsWith($"facets.{facetName}.buckets["))
            {
                var starterPath = jsonReader.Path;

                jsonReader.Read();// "val" property
                var key = jsonReader.ReadAsString();

                jsonReader.Read();// "count" property
                var count = jsonReader.ReadAsInt32();

                var value = new FacetItemFieldValue
                {
                    Key = key,
                    Quantity = (long)count
                };

                ((List<FacetItemFieldValue>)((FacetItemField)facetItem).Values).Add(value);

                // Closes bucket object
                while (!jsonReader.Path.Equals(starterPath))
                {
                    jsonReader.Read();
                }

                jsonReader.Read();// Starts next bucket object
            }
        }

        private object GetMaximumValue(Type fieldType, IFacetItemRangeValue item, string facetGap)
        {
            if (string.IsNullOrWhiteSpace(facetGap))
            {
                return null;
            }

            if (typeof(DateTime).Equals(fieldType))
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
                return DateMath.Apply(minimumValue.HasValue ? minimumValue.Value : DateTime.MinValue, facetGap);
            }

            if (typeof(decimal).Equals(fieldType)
               || typeof(float).Equals(fieldType)
               || typeof(double).Equals(fieldType))
            {
                var minimumValue = ((FacetItemRangeValue<decimal>)item).MinimumValue;
                return (minimumValue.HasValue ? minimumValue.Value : decimal.MinValue)
                    + decimal.Parse(facetGap, CultureInfo.InvariantCulture);
            }

            {
                var minimumValue = ((FacetItemRangeValue<int>)item).MinimumValue;
                return (minimumValue.HasValue ? minimumValue.Value : int.MinValue)
                    + int.Parse(facetGap, CultureInfo.InvariantCulture);
            }
        }

        private void ProcessFacetRangeBuckets(JsonReader jsonReader, string facetName, Type fieldType, IFacetRangeParameter<TDocument> facetRangeParameter, IFacetItem facetItem)
        {
            jsonReader.Read();// Starts array

            while (jsonReader.Path.StartsWith($"facets.{facetName}.buckets["))
            {
                var starterPath = jsonReader.Path;

                jsonReader.Read();// "val" property
                var rawMinimumValue = jsonReader.ReadAsString();
                jsonReader.Read();// "count" property
                var quantity = (long)jsonReader.ReadAsInt32();

                var facetItemRangeValue = this.GetFacetItemRangeValue(fieldType, rawMinimumValue);
                var maximumValue = this.GetMaximumValue(fieldType, facetItemRangeValue, facetRangeParameter.Gap);

                facetItemRangeValue.SetMaximumValue(maximumValue);
                facetItemRangeValue.Quantity = quantity;

                ((List<IFacetItemRangeValue>)((FacetItemRange)facetItem).Values).Add(facetItemRangeValue);

                // Closes bucket object
                while (!jsonReader.Path.Equals(starterPath))
                {
                    jsonReader.Read();
                }

                jsonReader.Read();// Starts next bucket object
            }

            jsonReader.Read();// Ends array

            if (jsonReader.Path.Equals($"facets.{facetName}.before"))
            {
                var facetItemRangeValue = this.GetFacetItemRangeValue(fieldType, rawMaximumValue: facetRangeParameter.Start);

                jsonReader.Read();// Ends property
                jsonReader.Read();// "count" property
                facetItemRangeValue.Quantity = (long)jsonReader.ReadAsInt32();

                ((List<IFacetItemRangeValue>)((FacetItemRange)facetItem).Values).Insert(0, facetItemRangeValue);

                jsonReader.Read();// Ends property
                jsonReader.Read();// Ends object
            }

            if (jsonReader.Path.Equals($"facets.{facetName}.after"))
            {
                // TODO: Calculate right value using "Gap" over "Start" until "End" (or "End" if "HardEnd"=true)
                var facetItemRangeValue = this.GetFacetItemRangeValue(fieldType, facetRangeParameter.End);
                var maximumValue = this.GetMaximumValue(fieldType, facetItemRangeValue, facetRangeParameter.Gap);

                facetItemRangeValue.SetMinimumValue(maximumValue);

                jsonReader.Read();// Ends property
                jsonReader.Read();// "count" property
                facetItemRangeValue.Quantity = (long)jsonReader.ReadAsInt32();

                ((List<IFacetItemRangeValue>)((FacetItemRange)facetItem).Values).Add(facetItemRangeValue);

                jsonReader.Read();// Ends property
                jsonReader.Read();// Ends object
            }
        }

        private IFacetItem GetFacetItem(List<ISearchParameter> searchParameters, JsonReader jsonReader)
        {
            var facetName = (string)jsonReader.Value;
            IFacetItem facetItem = null;

            jsonReader.Read();// Closes property
            jsonReader.Read();// Go to checker element

            // Facet query
            if ((string)jsonReader.Value == "count")
            {
                facetItem = new FacetItemQuery(facetName, (long)jsonReader.ReadAsInt32());
            }
            else // Facet field or facet range
            {
                var searchParameter = this.GetSearchParameter(searchParameters, facetName);
                var facetRangeParameter = (searchParameter as IFacetRangeParameter<TDocument>);

                jsonReader.Read();// Closes buckets property

                if (facetRangeParameter != null)
                {
                    var fieldType = facetRangeParameter
                        .ExpressionBuilder
                        .GetPropertyType(facetRangeParameter.FieldExpression);

                    facetItem = new FacetItemRange(facetName);
                    this.ProcessFacetRangeBuckets(jsonReader, facetName, fieldType, facetRangeParameter, facetItem);
                }
                else
                {
                    facetItem = new FacetItemField(facetName);
                    this.ProcessFacetFieldBuckets(jsonReader, facetName, facetItem);
                }
            }

            // Closes facet item object
            while (!jsonReader.Path.Equals($"facets.{facetName}") && jsonReader.TokenType != JsonToken.EndObject)
            {
                jsonReader.Read();
            }

            return facetItem;
        }

        void ISearchResult.Execute(List<ISearchParameter> searchParameters, JsonToken currentToken, string currentPath, JsonReader jsonReader)
        {
            if (currentToken == JsonToken.StartObject)
            {
                if (currentPath.Equals("facets"))
                {
                    var facetItems = new List<IFacetItem>();

                    jsonReader.Read();// Go to first property

                    // Skips "count" property
                    while (jsonReader.Path.Equals("facets.count"))
                    {
                        jsonReader.Read();
                    }

                    while (!jsonReader.Path.Equals("facets") && jsonReader.TokenType != JsonToken.EndObject)
                    {
                        var facetItem = this.GetFacetItem(searchParameters, jsonReader);

                        facetItems.Add(facetItem);

                        jsonReader.Read();
                    }

                    ((IFacetsResult<TDocument>)this).Data = facetItems;
                }
            }
        }
    }
}
