using SolrExpress.Core.Search.Parameter;
using SolrExpress.Core.Utility;
using System;
using System.Linq.Expressions;

namespace SolrExpress.Core.Search.ParameterValue
{
    /// <summary>
    /// Single value parameter
    /// </summary>
    public sealed class Single<TDocument> : ISearchParameterValue, IValidation
        where TDocument : IDocument
    {
        /// <summary>
        /// Create a single solr parameter value
        /// </summary>
        /// <param name="expression">Expression used to find the property name</param>
        /// <param name="value">Value of the filter</param>
        public Single(Expression<Func<TDocument, object>> expression, string value)
        {
            Checker.IsNull(expression);
            Checker.IsNullOrWhiteSpace(value);

            this.Expression = expression;
            this.Value = value;
        }

        /// <summary>
        /// Execute parameter value generator
        /// </summary>
        /// <returns>Result generated value</returns>
        public string Execute()
        {
            var fieldName = ExpressionUtility.GetFieldNameFromExpression(this.Expression);

            return $"{fieldName}:{this.Value}";
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

            var solrFieldAttribute = ExpressionUtility.GetSolrFieldAttributeFromPropertyInfo(this.Expression);

            if (solrFieldAttribute == null || solrFieldAttribute.Indexed)
            {
                return;
            }

            isValid = false;
            errorMessage = Resource.FieldMustBeIndexedTrueToBeUsedInAQueryException;
        }

        /// <summary>
        /// Expression used to find the property name
        /// </summary>
        public Expression<Func<TDocument, object>> Expression { get; private set; }

        /// <summary>
        /// Value of the filter
        /// </summary>
        public string Value { get; private set; }
    }
}
