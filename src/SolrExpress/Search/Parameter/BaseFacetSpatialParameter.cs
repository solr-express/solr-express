using SolrExpress.Builder;
using SolrExpress.Search.Query;
using SolrExpress.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SolrExpress.Search.Parameter
{
    public abstract class BaseFacetSpatialParameter<TDocument> : IFacetSpatialParameter<TDocument>, IEquatable<BaseFacetSpatialParameter<TDocument>>
        where TDocument : Document
    {
        public string AliasName { get; set; }
        public GeoCoordinate CenterPoint { get; set; }
        public decimal Distance { get; set; }
        public string[] Excludes { get; set; }
        public ExpressionBuilder<TDocument> ExpressionBuilder { get; set; }
        public Expression<Func<TDocument, object>> FieldExpression { get; set; }
        public SpatialFunctionType FunctionType { get; set; }
        public int? Limit { get; set; }
        public int? Minimum { get; set; }
        public FacetSortType? SortType { get; set; }
        public ISolrExpressServiceProvider<TDocument> ServiceProvider { get; set; }
        public IList<IFacetParameter<TDocument>> Facets { get; set; }
        public SearchQuery<TDocument> Filter { get; set; }

        /// <summary>
        /// Determines whether the specified object is equal to the current object
        /// </summary>
        /// <param name="obj">The object to compare with the current object</param>
        /// <returns>True if the specified object is equal to the current object; otherwise, false</returns>
        public override bool Equals(object obj)
        {
            if (obj is IFacetSpatialParameter<TDocument> parameter)
            {
                var thisFieldExpression = this.ExpressionBuilder.GetFieldName(this.FieldExpression);
                var thatFieldExpression = parameter.ExpressionBuilder.GetFieldName(parameter.FieldExpression);

                var thisFilter = this.Filter?.Execute() ?? string.Empty;
                var thatFilter = parameter.Filter?.Execute() ?? string.Empty;

                var thisFacets = string.Concat(this.Facets?.Select(q => q.GetHashCode()) ?? Enumerable.Empty<int>());
                var thatFacets = string.Concat(parameter.Facets?.Select(q => q.GetHashCode()) ?? Enumerable.Empty<int>());

                return
                    this.AliasName.IsEquals(parameter.AliasName) &&
                    this.CenterPoint.IsEquals(parameter.CenterPoint) &&
                    this.Distance.IsEquals(parameter.Distance) &&
                    Array.Equals(this.Excludes, parameter.Excludes) &&
                    thisFieldExpression.IsEquals(thatFieldExpression) &&
                    this.FunctionType.IsEquals(parameter.FunctionType) &&
                    this.Limit.IsEquals(parameter.Limit) &&
                    this.Minimum.IsEquals(parameter.Minimum) &&
                    this.SortType.IsEquals(parameter.SortType) &&
                    thisFacets.IsEquals(thatFacets) &&
                    thisFilter.IsEquals(thatFilter);
            }

            return false;
        }

        public bool Equals(BaseFacetSpatialParameter<TDocument> other)
        {
            return this.Equals((object)other);
        }

        /// <summary>
        /// Serves as the default hash function
        /// </summary>
        /// <returns>A hash code for the current object</returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
