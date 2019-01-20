using SolrExpress.Builder;
using System;
using System.Linq.Expressions;

namespace SolrExpress.Search.Parameter
{
    public abstract class BaseSortParameter<TDocument> : ISortParameter<TDocument>
        where TDocument : Document
    {
        public bool Ascendent { get; set; }
        public ExpressionBuilder<TDocument> ExpressionBuilder { get; set; }
        public Expression<Func<TDocument, object>> FieldExpression { get; set; }

        /// <summary>
        /// Determines whether the specified object is equal to the current object
        /// </summary>
        /// <param name="obj">The object to compare with the current object</param>
        /// <returns>True if the specified object is equal to the current object; otherwise, false</returns>
        public override bool Equals(object obj)
        {
            throw new System.NotImplementedException();
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
