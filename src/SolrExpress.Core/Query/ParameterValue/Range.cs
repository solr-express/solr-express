using SolrExpress.Core.Extension.Internal;
using System;
using System.Globalization;
using System.Linq.Expressions;

namespace SolrExpress.Core.Query.ParameterValue
{
    /// <summary>
    /// Single value parameter
    /// </summary>
    public sealed class Range<TDocument, TValue> : IQueryParameterValue, IValidation
        where TDocument : IDocument
        where TValue : struct
    {
        private readonly Expression<Func<TDocument, object>> _expression;
        private readonly TValue? _from;
        private readonly TValue? _to;

        /// <summary>
        /// Create a range solr parameter value
        /// </summary>
        /// <param name="expression">Expression used to find the property name</param>
        /// <param name="from">From value in a range filter</param>
        /// <param name="to">To value in a range filter</param>
        public Range(Expression<Func<TDocument, object>> expression, TValue? from = null, TValue? to = null)
        {
            Checker.IsNull(expression);

            this._expression = expression;
            this._from = from;
            this._to = to;
        }

        /// <summary>
        /// Execute parameter value generator
        /// </summary>
        /// <returns>Result generated value</returns>
        public string Execute()
        {
            var fieldName = this._expression.GetFieldNameFromExpression();

            string fromValue;
            string toValue;

            string format = null;

            if (typeof(TValue) == typeof(int))
            {
                format = "0";
            }
            else if (typeof(TValue) == typeof(double) || typeof(TValue) == typeof(decimal))
            {
                format = "0.#";
            }
            else if (typeof(TValue) == typeof(DateTime))
            {
                format = "yyyy-MM-dd'T'HH:mm:ss'Z'";
            }

            if (string.IsNullOrWhiteSpace(format))
            {
                fromValue = this._from?.ToString() ?? "*";
                toValue = this._to?.ToString() ?? "*";
            }
            else
            {
                fromValue = this._from != null ? ((IFormattable)this._from.Value).ToString(format, CultureInfo.InvariantCulture) : "*";
                toValue = this._to != null ? ((IFormattable)this._to.Value).ToString(format, CultureInfo.InvariantCulture) : "*";
            }

            return $"{fieldName}:[{fromValue} TO {toValue}]";
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

            if (solrFieldAttribute == null || solrFieldAttribute.Indexed)
            {
                return;
            }

            isValid = false;
            errorMessage = Resource.FieldMustBeIndexedTrueToBeUsedInAQueryException;
        }
    }
}
