using SolrExpress.Builder;
using SolrExpress.Utility;
using System;
using System.Linq.Expressions;

namespace SolrExpress.Search.Parameter
{
    public abstract class BaseSortParameter<TDocument> : ISortParameter<TDocument>, IEquatable<BaseSortParameter<TDocument>>
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
            if (obj is ISortParameter<TDocument> parameter)
            {
                var thisFieldExpression = this.ExpressionBuilder.GetFieldName(this.FieldExpression) ?? string.Empty;
                var thatFieldExpression = parameter.ExpressionBuilder.GetFieldName(parameter.FieldExpression) ?? string.Empty;

                return
                    this.Ascendent.IsEquals(parameter.Ascendent) &&
                    thisFieldExpression.IsEquals(thatFieldExpression);
            }

            return false;
        }

        public bool Equals(BaseSortParameter<TDocument> other)
        {
            return this.Equals((object)other);
        }

        /// <summary>
        /// Serves as the default hash function
        /// </summary>
        /// <returns>A hash code for the current object</returns>
        public override int GetHashCode()
        {
            var thisFieldExpression = this.ExpressionBuilder.GetFieldName(this.FieldExpression) ?? string.Empty;

            return Tuple
                .Create
                (
                    this.Ascendent,
                    thisFieldExpression
                )
                .GetHashCode();
        }
    }
}
