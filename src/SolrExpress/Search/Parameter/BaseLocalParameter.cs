using SolrExpress.Search.Query;
using SolrExpress.Utility;
using System;

namespace SolrExpress.Search.Parameter
{
    public abstract class BaseLocalParameter<TDocument> : ILocalParameter<TDocument>, IEquatable<BaseLocalParameter<TDocument>>
        where TDocument : Document
    {
        public string Name { get; set; }
        public SearchQuery<TDocument> Query { get; set; }
        public string Value { get; set; }

        /// <summary>
        /// Determines whether the specified object is equal to the current object
        /// </summary>
        /// <param name="obj">The object to compare with the current object</param>
        /// <returns>True if the specified object is equal to the current object; otherwise, false</returns>
        public override bool Equals(object obj)
        {
            if (obj is ILocalParameter<TDocument> parameter)
            {
                var thisQuery = this.Query?.Execute() ?? string.Empty;
                var thatQuery = parameter.Query?.Execute() ?? string.Empty;

                return
                    this.Name.IsEquals(parameter.Name) &&
                    thisQuery.IsEquals(thatQuery) &&
                    this.Value.IsEquals(parameter.Value);
            }

            return false;
        }

        public bool Equals(BaseLocalParameter<TDocument> other)
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
                    this.Name,
                    this.Query?.Execute(),
                    this.Value
                )
                .GetHashCode();
        }
    }
}
