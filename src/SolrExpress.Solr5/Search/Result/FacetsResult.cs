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
        private JsonReader _jsonReader;

        IEnumerable<IFacetItem> IFacetsResult<TDocument>.Data { get; set; }

        private IFacetParameter<TDocument> GetFacetParameter(IList<IFacetParameter<TDocument>> searchParameters, string facetName)
        {
            return searchParameters
                 .FirstOrDefault(parameter =>
                 {
                     var searchItemFieldExpression = parameter as ISearchItemFieldExpression<TDocument>;
                     var facetRangeParameter = parameter as IFacetRangeParameter<TDocument>;
                     var facetQueryParameter = parameter as IFacetQueryParameter<TDocument>;

                     var fieldName = searchItemFieldExpression
                        ?.ExpressionBuilder
                        .GetFieldName(searchItemFieldExpression.FieldExpression);

                     var aliasName = searchItemFieldExpression
                        ?.ExpressionBuilder
                        .GetAliasName(searchItemFieldExpression.FieldExpression);

                     return
                        facetName.Equals(facetRangeParameter?.AliasName) ||
                        facetName.Equals(facetQueryParameter?.AliasName) ||
                        facetName.Equals(aliasName) ||
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

        private void ProcessFacetFieldBuckets(string root, IFacetParameter<TDocument> facetParameter, JsonToken currentToken, string currentPath, string facetName, IFacetItem facetItem)
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
                    this.GetFacetItems(
                        initialPath,
                        facetParameter.Facets,
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

        private void ProcessFacetRangeBuckets(string root, IFacetRangeParameter<TDocument> facetParameter, JsonToken currentToken, string currentPath, string facetName, IFacetItem facetItem, Type fieldType)
        {
            this._jsonReader.Read();// Starts array

            while (this._jsonReader.Path.StartsWith($"{root}.{facetName}.buckets["))
            {
                var initialPath = this._jsonReader.Path;

                this._jsonReader.Read();// "val" property
                var rawMinimumValue = this._jsonReader.ReadAsString();

                this._jsonReader.Read();// "count" property
                var count = (long)this._jsonReader.ReadAsInt32();

                var value = this.GetFacetItemRangeValue(fieldType, rawMinimumValue);

                var maximumValue = this.GetMaximumValue(fieldType, value, facetParameter.Gap);

                value.SetMaximumValue(maximumValue);
                value.Quantity = count;

                // Go to next token to verify subfacet
                this._jsonReader.Read();

                // Subfacets
                if (this._jsonReader.TokenType != JsonToken.EndObject)
                {
                    this.GetFacetItems(
                        initialPath,
                        facetParameter.Facets,
                        this._jsonReader.TokenType,
                        initialPath,
                        out var facetItems);

                    value.Facets = facetItems;
                }

                ((List<IFacetItemRangeValue>)((FacetItemRange)facetItem).Values).Add(value);

                // Try closes bucket object
                while (this._jsonReader.TokenType != JsonToken.EndObject)
                {
                    this._jsonReader.Read();
                }

                this._jsonReader.Read();// Starts next bucket object
            }

            this._jsonReader.Read();// Ends array

            if (this._jsonReader.Path.Equals($"{root}.{facetName}.before"))
            {
                var facetItemRangeValue = this.GetFacetItemRangeValue(fieldType, rawMaximumValue: facetParameter.Start);

                this._jsonReader.Read();// Ends property
                this._jsonReader.Read();// "count" property
                facetItemRangeValue.Quantity = (long)this._jsonReader.ReadAsInt32();

                ((List<IFacetItemRangeValue>)((FacetItemRange)facetItem).Values).Insert(0, facetItemRangeValue);

                this._jsonReader.Read();// Ends property
                this._jsonReader.Read();// Ends object
            }

            if (this._jsonReader.Path.Equals($"{root}.{facetName}.after"))
            {
                // TODO: Calculate right value using "Gap" over "Start" until "End" (or "End" if "HardEnd"=true)
                var facetItemRangeValue = this.GetFacetItemRangeValue(fieldType, facetParameter.End);
                var maximumValue = this.GetMaximumValue(fieldType, facetItemRangeValue, facetParameter.Gap);

                facetItemRangeValue.SetMinimumValue(maximumValue);

                this._jsonReader.Read();// Ends property
                this._jsonReader.Read();// "count" property
                facetItemRangeValue.Quantity = (long)this._jsonReader.ReadAsInt32();

                ((List<IFacetItemRangeValue>)((FacetItemRange)facetItem).Values).Add(facetItemRangeValue);

                this._jsonReader.Read();// Ends property
                this._jsonReader.Read();// Ends object
            }
        }

        private IFacetItem GetFacetItem(string root, IList<IFacetParameter<TDocument>> facetParameters, JsonToken currentToken, string currentPath)
        {
            var initialPath = this._jsonReader.Path;
            var facetName = (string)this._jsonReader.Value;
            var facetParameter = this.GetFacetParameter(facetParameters, facetName);
            IFacetItem facetItem = null;

            this._jsonReader.Read();// Closes property
            this._jsonReader.Read();// Go to checker element

            // Facet query
            if ((string)this._jsonReader.Value == "count")
            {
                facetItem = new FacetItemQuery(facetName, (long)this._jsonReader.ReadAsInt32());

                this._jsonReader.Read();// Closes count property

                // Subfacets
                if (this._jsonReader.TokenType != JsonToken.EndObject)
                {
                    this.GetFacetItems(
                        initialPath,
                        facetParameter.Facets.ToList(),
                        this._jsonReader.TokenType,
                        initialPath,
                        out var facetItems);

                    ((FacetItemQuery)facetItem).Facets = facetItems;
                }
            }
            else // Facet field or facet range
            {
                var facetRangeParameter = (facetParameter as IFacetRangeParameter<TDocument>);

                this._jsonReader.Read();// Closes buckets property

                if (facetRangeParameter != null)
                {
                    var fieldType = facetRangeParameter
                        .ExpressionBuilder
                        .GetPropertyType(facetRangeParameter.FieldExpression);

                    facetItem = new FacetItemRange(facetName);
                    this.ProcessFacetRangeBuckets(root, facetRangeParameter, currentToken, currentPath, facetName, facetItem, fieldType);
                }
                else
                {
                    facetItem = new FacetItemField(facetName);
                    this.ProcessFacetFieldBuckets(root, facetParameter, currentToken, currentPath, facetName, facetItem);
                }
            }

            // Closes facet item object
            while (!this._jsonReader.Path.Equals($"{root}.{facetName}") && this._jsonReader.TokenType != JsonToken.EndObject)
            {
                this._jsonReader.Read();
            }

            return facetItem;
        }

        private void GetFacetItems(string root, IList<IFacetParameter<TDocument>> facetParameters, JsonToken currentToken, string currentPath, out IEnumerable<IFacetItem> facetItems)
        {
            facetItems = new List<IFacetItem>();

            // Skips "count" property
            while (this._jsonReader.Path.Equals($"{root}.count"))
            {
                this._jsonReader.Read();
            }

            while (!this._jsonReader.Path.Equals(root) && this._jsonReader.TokenType != JsonToken.EndObject)
            {
                var facetItem = this.GetFacetItem(root, facetParameters, currentToken, currentPath);

                ((List<IFacetItem>)facetItems).Add(facetItem);

                this._jsonReader.Read();
            }
        }

        void ISearchResult<TDocument>.Execute(IList<ISearchParameter> searchParameters, JsonToken currentToken, string currentPath, JsonReader jsonReader)
        {
            this._jsonReader = jsonReader;

            if (currentToken == JsonToken.StartObject && currentPath.Equals("facets"))
            {
                this._jsonReader.Read();// Go to first property

                var facetParameters = searchParameters
                    .Select(parameter => parameter as IFacetParameter<TDocument>)
                    .Where(parameter => parameter != null)
                    .ToList();

                this.GetFacetItems(
                    "facets",
                    facetParameters,
                    currentToken,
                    currentPath,
                    out var facetItems);

                ((IFacetsResult<TDocument>)this).Data = facetItems;
            }
        }
    }
}
