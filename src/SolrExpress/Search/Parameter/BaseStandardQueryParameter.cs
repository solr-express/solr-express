using SolrExpress.Search.Query;
using SolrExpress.Utility;
using System;

namespace SolrExpress.Search.Parameter
{
    public abstract class BaseStandardQueryParameter<TDocument> : IStandardQueryParameter<TDocument>, IEquatable<BaseStandardQueryParameter<TDocument>>
        where TDocument : Document
    {
        public SearchQuery<TDocument> Value { get; set; }

        /// <summary>
        /// Determines whether the specified object is equal to the current object
        /// </summary>
        /// <param name="obj">The object to compare with the current object</param>
        /// <returns>True if the specified object is equal to the current object; otherwise, false</returns>
        public override bool Equals(object obj)
        {
            if (obj is IStandardQueryParameter<TDocument> parameter)
            {
                var thisValue = this.Value?.Execute() ?? string.Empty;
                var thatValue = parameter.Value?.Execute() ?? string.Empty;

                return thisValue.IsEquals(thatValue);
            }

            return false;
        }

        public bool Equals(BaseStandardQueryParameter<TDocument> other)
        {
            return this.Equals((object)other);
        }

        /// <summary>
        /// Serves as the default hash function
        /// </summary>
        /// <returns>A hash code for the current object</returns>
        public override int GetHashCode()
        {
            var thisValue = this.Value?.Execute() ?? string.Empty;

            return thisValue.GetHashCode();
        }
    }
}
