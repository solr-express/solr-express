using SolrExpress.Builder;
using System;
using System.Linq.Expressions;

namespace SolrExpress.Search.Parameter
{
    public abstract class BaseDefaultFieldParameter<TDocument> : IDefaultFieldParameter<TDocument>
        where TDocument : Document
    {
        public ExpressionBuilder<TDocument> ExpressionBuilder { get; set; }
        public Expression<Func<TDocument, object>> FieldExpression { get; set; }

        /// <summary>
        /// Determines whether the specified object is equal to the current object
        /// </summary>
        /// <param name="obj">The object to compare with the current object</param>
        /// <returns>True if the specified object is equal to the current object; otherwise, false</returns>
        public override bool Equals(object obj)
        {
            if (obj is IDefaultFieldParameter<TDocument> parameter)
            {
                var thisExpression = this.ExpressionBuilder.GetFieldName(this.FieldExpression);
                var otherExpression = parameter.ExpressionBuilder.GetFieldName(parameter.FieldExpression);

                return thisExpression.Equals(otherExpression);
            }

            return false;
        }

        /// <summary>
        /// Serves as the default hash function
        /// </summary>
        /// <returns>A hash code for the current object</returns>
        public override int GetHashCode()
        {
            var expression = this.ExpressionBuilder.GetFieldName(this.FieldExpression);

            return expression.GetHashCode();
        }
    }
}
