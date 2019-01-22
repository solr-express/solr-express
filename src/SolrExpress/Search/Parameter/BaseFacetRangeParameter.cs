using SolrExpress.Builder;
using SolrExpress.Search.Query;
using SolrExpress.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SolrExpress.Search.Parameter
{
    public abstract class BaseFacetRangeParameter<TDocument> : IFacetRangeParameter<TDocument>
        where TDocument : Document
    {
        public string AliasName { get; set; }
        public bool CountAfter { get; set; }
        public bool CountBefore { get; set; }
        public string End { get; set; }
        public string[] Excludes { get; set; }
        public IList<IFacetParameter<TDocument>> Facets { get; set; }
        public ExpressionBuilder<TDocument> ExpressionBuilder { get; set; }
        public Expression<Func<TDocument, object>> FieldExpression { get; set; }
        public string Gap { get; set; }
        public int? Limit { get; set; }
        public int? Minimum { get; set; }
        public FacetSortType? SortType { get; set; }
        public string Start { get; set; }
        public ISolrExpressServiceProvider<TDocument> ServiceProvider { get; set; }
        public SearchQuery<TDocument> Filter { get; set; }
        public bool HardEnd { get; set; }

        /// <summary>
        /// Determines whether the specified object is equal to the current object
        /// </summary>
        /// <param name="obj">The object to compare with the current object</param>
        /// <returns>True if the specified object is equal to the current object; otherwise, false</returns>
        public override bool Equals(object obj)
        {
            if (obj is IFacetRangeParameter<TDocument> parameter)
            {
                var thisFacets = string.Concat(this.Facets?.Select(q => q.GetHashCode()) ?? Enumerable.Empty<int>());
                var thatFacets = string.Concat(parameter.Facets?.Select(q => q.GetHashCode()) ?? Enumerable.Empty<int>());

                var thisFilter = this.Filter?.Execute() ?? string.Empty;
                var thatFilter = parameter.Filter?.Execute() ?? string.Empty;

                return
                    this.AliasName.IsEquals(parameter.AliasName) &&
                    this.CountAfter.IsEquals(parameter.CountAfter) &&
                    this.CountBefore.IsEquals(parameter.CountBefore) &&
                    this.End.IsEquals(parameter.End) &&
                    Array.Equals(this.Excludes, parameter.Excludes) &&
                    thisFacets.IsEquals(thatFacets) &&
                    this.ExpressionBuilder.GetFieldName(this.FieldExpression).IsEquals(parameter.ExpressionBuilder.GetFieldName(parameter.FieldExpression)) &&
                    this.Gap.IsEquals(parameter.Gap) &&
                    this.Limit.IsEquals(parameter.Limit) &&
                    this.Minimum.IsEquals(parameter.Minimum) &&
                    this.SortType.IsEquals(parameter.SortType) &&
                    this.Start.IsEquals(parameter.Start) &&
                    thisFilter.IsEquals(thatFilter) &&
                    this.HardEnd.IsEquals(parameter.HardEnd);
            }

            return false;
        }

        /// <summary>
        /// Serves as the default hash function
        /// </summary>
        /// <returns>A hash code for the current object</returns>
        public override int GetHashCode()
        {
            return Tuple
                .Create(
                    this.AliasName?.GetHashCode(),
                    this.CountAfter.GetHashCode(),
                    this.CountBefore.GetHashCode(),
                    this.End?.GetHashCode(),
                    string.Concat(this.Excludes).GetHashCode(),
                    string.Concat(this.Facets?.Select(parameter => parameter.GetHashCode())),
                    this.ExpressionBuilder.GetFieldName(this.FieldExpression),
                    Tuple
                        .Create(
                            this.Gap?.GetHashCode(),
                            this.Limit?.GetHashCode(),
                            this.Minimum?.GetHashCode(),
                            this.SortType?.GetHashCode(),
                            this.Start?.GetHashCode(),
                            this.Filter?.Execute().GetHashCode(),
                            this.HardEnd.GetHashCode()
                        )
                        .GetHashCode()
                )
                .GetHashCode();
        }
    }
}