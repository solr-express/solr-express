using SolrExpress.Core.Entity;
using SolrExpress.Core.Helper;
using SolrExpress.Core.Query;
using System;
using System.Globalization;
using System.Linq.Expressions;

namespace SolrExpress.Core.ParameterValue
{
    /// <summary>
    /// Single value parameter
    /// </summary>
    public sealed class RangeValue<TDocument, TValue> : IQueryParameterValue, IValidation
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
        public RangeValue(Expression<Func<TDocument, object>> expression, TValue? from = null, TValue? to = null)
        {
            ThrowHelper<ArgumentNullException>.If(expression == null);

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
            var fieldName = UtilHelper.GetFieldNameFromExpression(this._expression);

            if (typeof(TValue) == typeof(int))
            {
                return string.Format(
                    "{0}:[{1} TO {2}]",
                    fieldName,
                    this._from != null ? Convert.ToInt32(this._from.Value).ToString("0", CultureInfo.InvariantCulture) : "*",
                    this._to != null ? Convert.ToInt32(this._to.Value).ToString("0", CultureInfo.InvariantCulture) : "*");
            }

            if (typeof(TValue) == typeof(double))
            {
                return string.Format(
                    "{0}:[{1} TO {2}]",
                    fieldName,
                    this._from != null ? Convert.ToDouble(this._from.Value).ToString("0.#", CultureInfo.InvariantCulture) : "*",
                    this._to != null ? Convert.ToDouble(this._to.Value).ToString("0.#", CultureInfo.InvariantCulture) : "*");
            }

            if (typeof(TValue) == typeof(decimal))
            {
                return string.Format(
                    "{0}:[{1} TO {2}]",
                    fieldName,
                    this._from != null ? Convert.ToDecimal(this._from.Value).ToString("0.#", CultureInfo.InvariantCulture) : "*",
                    this._to != null ? Convert.ToDecimal(this._to.Value).ToString("0.#", CultureInfo.InvariantCulture) : "*");
            }

            if (typeof(TValue) == typeof(DateTime))
            {
                return string.Format(
                    "{0}:[{1} TO {2}]",
                    fieldName,
                    this._from != null ? Convert.ToDateTime(this._from.Value).ToString("yyyy-MM-dd'T'HH:mm:ss'Z'", CultureInfo.InvariantCulture) : "*",
                    this._to != null ? Convert.ToDateTime(this._to.Value).ToString("yyyy-MM-dd'T'HH:mm:ss'Z'", CultureInfo.InvariantCulture) : "*");
            }

            return string.Format(
                "{0}:[{1} TO {2}]",
                fieldName,
                this._from != null ? this._from.Value.ToString() : "*",
                this._to != null ? this._to.Value.ToString() : "*");
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

            var solrFieldAttribute = UtilHelper.GetSolrFieldAttributeFromPropertyInfo(this._expression);

            if (solrFieldAttribute != null && !solrFieldAttribute.Indexed)
            {
                isValid = false;
                errorMessage = Resource.FieldMustBeIndexedTrueToBeUsedInAQueryException;
            }
        }
    }
}
