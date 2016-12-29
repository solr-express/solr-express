using SolrExpress.Core.Utility;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace SolrExpress.Core.Search.Parameter
{
    public abstract class BaseFieldsParameter<TDocument> : IFieldsParameter<TDocument>, IValidation
        where TDocument : IDocument
    {
        protected IExpressionBuilder<TDocument> _expressionBuilder;

        public BaseFieldsParameter(IExpressionBuilder<TDocument> expressionBuilder)
        {
            this._expressionBuilder = expressionBuilder;
        }

        /// <summary>
        /// True to indicate multiple instances of the parameter, otherwise false
        /// </summary>
        public bool AllowMultipleInstances { get; } = true;

        /// <summary>
        /// Expression used to find the property name
        /// </summary>
        public Expression<Func<TDocument, object>>[] Expressions { get; private set; }

        public IExpressionBuilder<TDocument> ExpressionBuilder { get; set; }

        /// <summary>
        /// Check for the parameter validation
        /// </summary>
        /// <param name="isValid">True if is valid, otherwise false</param>
        /// <param name="errorMessage">The error message, if applicable</param>
        public void Validate(out bool isValid, out string errorMessage)
        {
            isValid = true;
            errorMessage = string.Empty;

            var withError = this
                .Expressions
                .Select(expression => this._expressionBuilder.GetSolrFieldAttributeFromPropertyInfo(expression))
                .Any(solrFieldAttribute => (!solrFieldAttribute?.Stored) ?? true);

            if (!withError)
            {
                return;
            }

            isValid = false;
            errorMessage = Resource.FieldMustBeStoredTrueToBeUsedInFieldsException;
        }

        /// <summary>
        /// Configure current instance
        /// </summary>
        /// <param name="expressions">Expression used to find the property name</param>
        public IFieldsParameter<TDocument> Configure(params Expression<Func<TDocument, object>>[] expressions)
        {
            Checker.IsNull(expressions);
            Checker.IsEmpty(expressions);

            this.Expressions = expressions;

            return this;
        }
    }
}
