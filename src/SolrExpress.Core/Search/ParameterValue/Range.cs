using SolrExpress.Core.Extension.Internal;
using SolrExpress.Core.Search.Parameter;
using SolrExpress.Core.Utility;
using System;
using System.Globalization;
using System.Linq.Expressions;

namespace SolrExpress.Core.Search.ParameterValue
{
    /// <summary>
    /// Single value parameter
    /// </summary>
    public sealed class Range<TDocument, TValue> : ISearchParameterValue, IValidation
        where TDocument : IDocument
        where TValue : struct
    {
        /// <summary>
        /// Create a range solr parameter value
        /// </summary>
        /// <param name="expression">Expression used to find the property name</param>
        /// <param name="from">From value in a range filter</param>
        /// <param name="to">To value in a range filter</param>
        public Range(Expression<Func<TDocument, object>> expression, TValue? from = null, TValue? to = null)
        {
            Checker.IsNull(expression);

            this.Expression = expression;
            this.From = from;
            this.To = to;
        }

        /// <summary>
        /// Execute parameter value generator
        /// </summary>
        /// <returns>Result generated value</returns>
        public string Execute()
        {
            var fieldName = this.Expression.GetFieldNameFromExpression();

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
                fromValue = this.From?.ToString() ?? "*";
                toValue = this.To?.ToString() ?? "*";
            }
            else
            {
                fromValue = this.From != null ? ((IFormattable)this.From.Value).ToString(format, CultureInfo.InvariantCulture) : "*";
                toValue = this.To != null ? ((IFormattable)this.To.Value).ToString(format, CultureInfo.InvariantCulture) : "*";
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

            var solrFieldAttribute = this.Expression.GetSolrFieldAttributeFromPropertyInfo();

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
        /// From value in a range filter
        /// </summary>
        public TValue? From { get; private set; }

        /// <summary>
        /// To value in a range filter
        /// </summary>
        public TValue? To { get; private set; }
    }
}
