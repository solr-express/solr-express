using SolrExpress.Search.Query;
using System;

namespace SolrExpress.Search.Parameter
{
    public abstract class BaseBoostParameter<TDocument> : IBoostParameter<TDocument>, IEquatable<BaseBoostParameter<TDocument>>
        where TDocument : Document
    {
        public BoostFunctionType BoostFunctionType { get; set; }
        public SearchQuery<TDocument> Query { get; set; }

        /// <summary>
        /// Determines whether the specified object is equal to the current object
        /// </summary>
        /// <param name="obj">The object to compare with the current object</param>
        /// <returns>True if the specified object is equal to the current object; otherwise, false</returns>
        public override bool Equals(object obj)
        {
            if (obj is IBoostParameter<TDocument> parameter)
            {
                return
                    this.BoostFunctionType.Equals(parameter.BoostFunctionType) &&
                    this.Query.Execute().Equals(parameter.Query.Execute());
            }

            return false;
        }

        public bool Equals(BaseBoostParameter<TDocument> other)
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
                .Create(this.BoostFunctionType, this.Query.Execute())
                .GetHashCode();
        }
    }
}
