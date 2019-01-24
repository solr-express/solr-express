using SolrExpress.Search.Query;
using SolrExpress.Utility;
using System;

namespace SolrExpress.Search.Parameter
{
    public abstract class BaseFilterParameter<TDocument> : IFilterParameter<TDocument>, IEquatable<BaseFilterParameter<TDocument>>
        where TDocument : Document
    {
        public SearchQuery<TDocument> Query { get; set; }
        public string TagName { get; set; }

        /// <summary>
        /// Determines whether the specified object is equal to the current object
        /// </summary>
        /// <param name="obj">The object to compare with the current object</param>
        /// <returns>True if the specified object is equal to the current object; otherwise, false</returns>
        public override bool Equals(object obj)
        {
            if (obj is IFilterParameter<TDocument> parameter)
            {
                var thisQuery = this.Query?.Execute() ?? string.Empty;
                var thatQuery = parameter.Query?.Execute() ?? string.Empty;

                return
                    thisQuery.IsEquals(thatQuery) &&
                    this.TagName.IsEquals(parameter.TagName);
            }

            return false;
        }

        public bool Equals(BaseFilterParameter<TDocument> other)
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
                .Create
                (
                    this.Query?.Execute(),
                    this.TagName
                )
                .GetHashCode();
        }
    }
}
