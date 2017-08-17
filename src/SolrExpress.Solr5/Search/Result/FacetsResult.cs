﻿using DaleNewman;
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
        private List<ISearchParameter> _searchParameters;
        private JsonReader _jsonReader;

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

        private void ProcessFacetFieldBuckets(string root, JsonToken currentToken, string currentPath, string facetName, IFacetItem facetItem)
        {
            this._jsonReader.Read();// Starts array

            while (this._jsonReader.Path.StartsWith($"{root}.{facetName}.buckets["))
            {
                var initialPath = this._jsonReader.Path;

                this._jsonReader.Read();// "val" property
                var key = this._jsonReader.ReadAsString();

                this._jsonReader.Read();// "count" property
                var count = this._jsonReader.ReadAsInt32();

                var value = new FacetItemFieldValue
                {
                    Key = key,
                    Quantity = (long)count
                };

                // Go to next token to verify subfacet
                this._jsonReader.Read();

                // Subfacets
                if (this._jsonReader.TokenType != JsonToken.EndObject)
                {
                    // Prepare to process subfacets
                    //this._jsonReader.Read();
                    //this._jsonReader.Read();
                    //this._jsonReader.Read();

                    this.GetFacetItems(
                        initialPath,
                        this._jsonReader.TokenType,
                        initialPath,
                        out var facetItems);

                    value.Facets = facetItems;
                }

                ((List<FacetItemFieldValue>)((FacetItemField)facetItem).Values).Add(value);

                // Try closes bucket object
                while (this._jsonReader.TokenType != JsonToken.EndObject)
                {
                    this._jsonReader.Read();
                }

                this._jsonReader.Read();// Starts next bucket object
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

        private void ProcessFacetRangeBuckets(string facetName, Type fieldType, IFacetRangeParameter<TDocument> facetRangeParameter, IFacetItem facetItem)
        {
            this._jsonReader.Read();// Starts array

            while (this._jsonReader.Path.StartsWith($"facets.{facetName}.buckets["))
            {
                var starterPath = this._jsonReader.Path;

                this._jsonReader.Read();// "val" property
                var rawMinimumValue = this._jsonReader.ReadAsString();
                this._jsonReader.Read();// "count" property
                var quantity = (long)this._jsonReader.ReadAsInt32();

                var facetItemRangeValue = this.GetFacetItemRangeValue(fieldType, rawMinimumValue);
                var maximumValue = this.GetMaximumValue(fieldType, facetItemRangeValue, facetRangeParameter.Gap);

                facetItemRangeValue.SetMaximumValue(maximumValue);
                facetItemRangeValue.Quantity = quantity;

                ((List<IFacetItemRangeValue>)((FacetItemRange)facetItem).Values).Add(facetItemRangeValue);

                // Closes bucket object
                while (!this._jsonReader.Path.Equals(starterPath))
                {
                    this._jsonReader.Read();
                }

                this._jsonReader.Read();// Starts next bucket object
            }

            this._jsonReader.Read();// Ends array

            if (this._jsonReader.Path.Equals($"facets.{facetName}.before"))
            {
                var facetItemRangeValue = this.GetFacetItemRangeValue(fieldType, rawMaximumValue: facetRangeParameter.Start);

                this._jsonReader.Read();// Ends property
                this._jsonReader.Read();// "count" property
                facetItemRangeValue.Quantity = (long)this._jsonReader.ReadAsInt32();

                ((List<IFacetItemRangeValue>)((FacetItemRange)facetItem).Values).Insert(0, facetItemRangeValue);

                this._jsonReader.Read();// Ends property
                this._jsonReader.Read();// Ends object
            }

            if (this._jsonReader.Path.Equals($"facets.{facetName}.after"))
            {
                // TODO: Calculate right value using "Gap" over "Start" until "End" (or "End" if "HardEnd"=true)
                var facetItemRangeValue = this.GetFacetItemRangeValue(fieldType, facetRangeParameter.End);
                var maximumValue = this.GetMaximumValue(fieldType, facetItemRangeValue, facetRangeParameter.Gap);

                facetItemRangeValue.SetMinimumValue(maximumValue);

                this._jsonReader.Read();// Ends property
                this._jsonReader.Read();// "count" property
                facetItemRangeValue.Quantity = (long)this._jsonReader.ReadAsInt32();

                ((List<IFacetItemRangeValue>)((FacetItemRange)facetItem).Values).Add(facetItemRangeValue);

                this._jsonReader.Read();// Ends property
                this._jsonReader.Read();// Ends object
            }
        }

        private IFacetItem GetFacetItem(string root, JsonToken currentToken, string currentPath)
        {
            var facetName = (string)this._jsonReader.Value;
            IFacetItem facetItem = null;

            this._jsonReader.Read();// Closes property
            this._jsonReader.Read();// Go to checker element

            // Facet query
            if ((string)this._jsonReader.Value == "count")
            {
                facetItem = new FacetItemQuery(facetName, (long)this._jsonReader.ReadAsInt32());
            }
            else // Facet field or facet range
            {
                var searchParameter = this.GetSearchParameter(this._searchParameters, facetName);
                var facetRangeParameter = (searchParameter as IFacetRangeParameter<TDocument>);

                this._jsonReader.Read();// Closes buckets property

                if (facetRangeParameter != null)
                {
                    var fieldType = facetRangeParameter
                        .ExpressionBuilder
                        .GetPropertyType(facetRangeParameter.FieldExpression);

                    facetItem = new FacetItemRange(facetName);
                    this.ProcessFacetRangeBuckets(facetName, fieldType, facetRangeParameter, facetItem);
                }
                else
                {
                    facetItem = new FacetItemField(facetName);
                    this.ProcessFacetFieldBuckets(root, currentToken, currentPath, facetName, facetItem);
                }
            }

            // Closes facet item object
            while (!this._jsonReader.Path.Equals($"{root}.{facetName}") && this._jsonReader.TokenType != JsonToken.EndObject)
            {
                this._jsonReader.Read();
            }

            return facetItem;
        }

        private void GetFacetItems(string root, JsonToken currentToken, string currentPath, out IEnumerable<IFacetItem> facetItems)
        {
            facetItems = new List<IFacetItem>();

            // Skips "count" property
            while (this._jsonReader.Path.Equals($"{root}.count"))
            {
                this._jsonReader.Read();
            }

            while (!this._jsonReader.Path.Equals(root) && this._jsonReader.TokenType != JsonToken.EndObject)
            {
                var facetItem = this.GetFacetItem(root, currentToken, currentPath);

                ((List<IFacetItem>)facetItems).Add(facetItem);

                this._jsonReader.Read();
            }
        }

        void ISearchResult.Execute(List<ISearchParameter> searchParameters, JsonToken currentToken, string currentPath, JsonReader jsonReader)
        {
            this._searchParameters = searchParameters;
            this._jsonReader = jsonReader;

            if (currentToken == JsonToken.StartObject && currentPath.Equals("facets"))
            {
                this._jsonReader.Read();// Go to first property

                this.GetFacetItems(
                    "facets",
                    currentToken,
                    currentPath,
                    out var facetItems);

                ((IFacetsResult<TDocument>)this).Data = facetItems;
            }
        }
    }
}
