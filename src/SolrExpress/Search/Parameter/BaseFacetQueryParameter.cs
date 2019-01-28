using SolrExpress.Search.Query;
using SolrExpress.Utility;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SolrExpress.Search.Parameter
{
    public abstract class BaseFacetQueryParameter<TDocument> : IFacetQueryParameter<TDocument>, IEquatable<BaseFacetQueryParameter<TDocument>>
        where TDocument : Document
    {
        public string AliasName { get; set; }
        public string[] Excludes { get; set; }
        public SearchQuery<TDocument> Query { get; set; }
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
            if (obj is IFacetQueryParameter<TDocument> parameter)
            {
                var thisFacets = string.Concat(this.Facets?.Select(q => q.GetHashCode()) ?? Enumerable.Empty<int>());
                var thatFacets = string.Concat(parameter.Facets?.Select(q => q.GetHashCode()) ?? Enumerable.Empty<int>());

                var thisFilter = this.Filter?.Execute() ?? string.Empty;
                var thatFilter = parameter.Filter?.Execute() ?? string.Empty;

                var thisQuery = this.Query?.Execute() ?? string.Empty;
                var thatQuery = parameter.Query?.Execute() ?? string.Empty;

                return
                    this.AliasName.IsEquals(parameter.AliasName) &&
                    Array.Equals(this.Excludes, parameter.Excludes) &&
                    thisQuery.IsEquals(thatQuery) &&
                    this.Limit.IsEquals(parameter.Limit) &&
                    this.Minimum.IsEquals(parameter.Minimum) &&
                    this.SortType.IsEquals(parameter.SortType) &&
                    thisFacets.IsEquals(thatFacets) &&
                    thisFilter.IsEquals(thatFilter);
            }

            return false;
        }

        public bool Equals(BaseFacetQueryParameter<TDocument> other)
        {
            return this.Equals((object)other);
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
                    string.Concat(this.Excludes).GetHashCode(),
                    this.Query?.Execute(),
                    this.Limit?.GetHashCode(),
                    this.Minimum?.GetHashCode(),
                    this.SortType?.GetHashCode(),
                    string.Concat(this.Facets?.Select(parameter => parameter.GetHashCode())),
                    this.Filter?.Execute().GetHashCode()
                )
                .GetHashCode();
        }
    }
}
