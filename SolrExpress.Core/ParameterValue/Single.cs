using SolrExpress.Core.Extension.Internal;
using SolrExpress.Core.Query;
using System;
using System.Linq.Expressions;

namespace SolrExpress.Core.ParameterValue
{
    /// <summary>
    /// Single value parameter
    /// </summary>
    public sealed class Single<TDocument> : IQueryParameterValue, IValidation
        where TDocument : IDocument
    {
        private readonly Expression<Func<TDocument, object>> _expression;
        private readonly string _value;

        /// <summary>
        /// Create a single solr parameter value
        /// </summary>
        /// <param name="expression">Expression used to find the property name</param>
        /// <param name="value">Value of the filter</param>
        public Single(Expression<Func<TDocument, object>> expression, string value)
        {
            Checker.IsNull(expression);
            Checker.IsNullOrWhiteSpace(value);

            this._expression = expression;
            this._value = value;
        }

        /// <summary>
        /// Execute parameter value generator
        /// </summary>
        /// <returns>Result generated value</returns>
        public string Execute()
        {
            var fieldName = this._expression.GetFieldNameFromExpression();

            return $"{fieldName}:{this._value}";
        }

        /// <summary>
        /// Check for the parameter validation
        /// </summary>
        /// <param name="isValid">True if is valid, otherwise false</param>
        /// <param name="errorMessage">The error message, if applicable</param>
        public void Validate(out bool isValid, out string errorMessage)
        {
            isValid = true;
            errorMessage = string.Empty;

            var solrFieldAttribute = this._expression.GetSolrFieldAttributeFromPropertyInfo();

            if (solrFieldAttribute != null && !solrFieldAttribute.Indexed)
            {
                isValid = false;
                errorMessage = Resource.FieldMustBeIndexedTrueToBeUsedInAQueryException;
            }
        }
    }
}
