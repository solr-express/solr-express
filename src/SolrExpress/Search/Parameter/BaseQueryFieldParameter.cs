using SolrExpress.Utility;
using System;

namespace SolrExpress.Search.Parameter
{
    public abstract class BaseQueryFieldParameter<TDocument> : IQueryFieldParameter<TDocument>, IEquatable<BaseQueryFieldParameter<TDocument>>
        where TDocument : Document
    {
        public string Expression { get; set; }

        /// <summary>
        /// Determines whether the specified object is equal to the current object
        /// </summary>
        /// <param name="obj">The object to compare with the current object</param>
        /// <returns>True if the specified object is equal to the current object; otherwise, false</returns>
        public override bool Equals(object obj)
        {
            if (obj is IQueryFieldParameter<TDocument> parameter)
            {
                return this.Expression.IsEquals(parameter.Expression);
            }

            return false;
        }

        public bool Equals(BaseQueryFieldParameter<TDocument> other)
        {
            return this.Equals((object)other);
        }

        /// <summary>
        /// Serves as the default hash function
        /// </summary>
        /// <returns>A hash code for the current object</returns>
        public override int GetHashCode()
        {
            return this.Expression.GetHashCode();
        }
    }
}
