using SolrExpress.Core.Helper;
using SolrExpress.Core.Query;
using System;
using System.Globalization;
using System.Linq.Expressions;

namespace SolrExpress.Solr5.ParameterValue
{
    /// <summary>
    /// Single value parameter
    /// </summary>
    public sealed class RangeValue<TDocument, TValue> : IQueryParameterValue
        where TDocument : IDocument
        where TValue : struct
    {
        private readonly string _value;

        /// <summary>
        /// Create a range solr parameter value
        /// </summary>
        /// <param name="expression">Expression used to find the property name</param>
        /// <param name="from">From value in a range filter</param>
        /// <param name="to">To value in a range filter</param>
        public RangeValue(Expression<Func<TDocument, object>> expression, TValue? from, TValue? to)
        {
            var fieldName = UtilHelper.GetPropertyNameFromExpression(expression);

            if (typeof(TValue) == typeof(int))
            {
                this._value = string.Format(
                    "{0}:[{1} TO {2}]",
                    fieldName,
                    from != null ? Convert.ToInt32(from.Value).ToString("0", CultureInfo.InvariantCulture) : "*",
                    to != null ? Convert.ToInt32(to.Value).ToString("0", CultureInfo.InvariantCulture) : "*");
            }
            else if (typeof(TValue) == typeof(double))
            {
                this._value = string.Format(
                    "{0}:[{1} TO {2}]",
                    fieldName,
                    from != null ? Convert.ToDouble(from.Value).ToString("0.#", CultureInfo.InvariantCulture) : "*",
                    to != null ? Convert.ToDouble(to.Value).ToString("0.#", CultureInfo.InvariantCulture) : "*");
            }
            else if (typeof(TValue) == typeof(decimal))
            {
                this._value = string.Format(
                    "{0}:[{1} TO {2}]",
                    fieldName,
                    from != null ? Convert.ToDecimal(from.Value).ToString("0.#", CultureInfo.InvariantCulture) : "*",
                    to != null ? Convert.ToDecimal(to.Value).ToString("0.#", CultureInfo.InvariantCulture) : "*");
            }
            else if (typeof(TValue) == typeof(DateTime))
            {
                this._value = string.Format(
                    "{0}:[{1} TO {2}]",
                    fieldName,
                    from != null ? Convert.ToDateTime(from.Value).ToString("yyyy-MM-dd'T'HH:mm:ss'Z'", CultureInfo.InvariantCulture) : "*",
                    to != null ? Convert.ToDateTime(to.Value).ToString("yyyy-MM-dd'T'HH:mm:ss'Z'", CultureInfo.InvariantCulture) : "*");
            }
            else
            {
                this._value = string.Format(
                    "{0}:[{1} TO {2}]",
                    fieldName,
                    from != null ? from.Value.ToString() : "*",
                    to != null ? to.Value.ToString() : "*");
            }
        }

        /// <summary>
        /// Execute parameter value generator
        /// </summary>
        /// <returns>Result of the value generator</returns>
        public string Execute()
        {
            return _value;
        }
    }
}
