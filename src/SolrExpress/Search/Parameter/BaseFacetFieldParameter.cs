using SolrExpress.Builder;
using SolrExpress.Search.Query;
using SolrExpress.Utility;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SolrExpress.Search.Parameter
{
    public abstract class BaseFacetFieldParameter<TDocument> : IFacetFieldParameter<TDocument>, IEquatable<BaseFacetFieldParameter<TDocument>>
        where TDocument : Document
    {
        public string[] Excludes { get; set; }
        public ExpressionBuilder<TDocument> ExpressionBuilder { get; set; }
        public Expression<Func<TDocument, object>> FieldExpression { get; set; }
        public int? Limit { get; set; }
        public int? Minimum { get; set; }
        public FacetSortType? SortType { get; set; }
        public ISolrExpressServiceProvider<TDocument> ServiceProvider { get; set; }
        public IList<IFacetParameter<TDocument>> Facets { get; set; }
        public SearchQuery<TDocument> Filter { get; set; }
        public FacetMethodType? MethodType { get; set; }
        public string Prefix { get; set; }

        /// <summary>
        /// Determines whether the specified object is equal to the current object
        /// </summary>
        /// <param name="obj">The object to compare with the current object</param>
        /// <returns>True if the specified object is equal to the current object; otherwise, false</returns>
        public override bool Equals(object obj)
        {
            if (obj is BaseFacetFieldParameter<TDocument> parameter)
            {
                var thisFieldExpression = this.ExpressionBuilder.GetFieldName(this.FieldExpression);
                var thatFieldExpression = parameter.ExpressionBuilder.GetFieldName(parameter.FieldExpression);

                var thisFilter = this.Filter?.Execute() ?? string.Empty;
                var thatFilter = parameter.Filter?.Execute() ?? string.Empty;

                return
                    Array.Equals(this.Excludes, parameter.Excludes) &&
                    thisFieldExpression.IsEquals(thatFieldExpression) &&
                    this.Limit.IsEquals(parameter.Limit) &&
                    this.Minimum.IsEquals(parameter.Minimum) &&
                    this.SortType.IsEquals(parameter.SortType) &&
                    thisFilter.IsEquals(thatFilter) &&
                    this.MethodType.IsEquals(parameter.MethodType) &&
                    this.Prefix.IsEquals(parameter.Prefix);
            }

            return false;
        }

        public bool Equals(BaseFacetFieldParameter<TDocument> other)
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
