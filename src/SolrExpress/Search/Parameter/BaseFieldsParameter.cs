using SolrExpress.Builder;
using System;
using System.Linq.Expressions;
using System.Text;

namespace SolrExpress.Search.Parameter
{
    public abstract class BaseFieldsParameter<TDocument> : IFieldsParameter<TDocument>, IEquatable<BaseFieldsParameter<TDocument>>
        where TDocument : Document
    {
        public ExpressionBuilder<TDocument> ExpressionBuilder { get; set; }
        public Expression<Func<TDocument, object>>[] FieldExpressions { get; set; }

        /// <summary>
        /// Determines whether the specified object is equal to the current object
        /// </summary>
        /// <param name="obj">The object to compare with the current object</param>
        /// <returns>True if the specified object is equal to the current object; otherwise, false</returns>
        public override bool Equals(object obj)
        {
            if (obj is IFieldsParameter<TDocument> parameter)
            {
                var thisExpression = new StringBuilder();
                var otherExpression = new StringBuilder();

                foreach (var item in this.FieldExpressions)
                {
                    thisExpression.Append(this.ExpressionBuilder.GetFieldName(item));
                }

                foreach (var item in parameter.FieldExpressions)
                {
                    otherExpression.Append(parameter.ExpressionBuilder.GetFieldName(item));
                }

                return thisExpression.ToString().Equals(otherExpression.ToString());
            }

            return false;
        }

        public bool Equals(BaseFieldsParameter<TDocument> other)
        {
            return this.Equals((object)other);
        }

        /// <summary>
        /// Serves as the default hash function
        /// </summary>
        /// <returns>A hash code for the current object</returns>
        public override int GetHashCode()
        {
            var expressions = new StringBuilder();

            foreach (var item in this.FieldExpressions)
            {
                expressions.Append(this.ExpressionBuilder.GetFieldName(item));
            }

            return expressions.ToString().GetHashCode();
        }
    }
}
