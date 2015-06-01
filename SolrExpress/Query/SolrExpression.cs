using SolrExpress.Helper;
using System;
using System.Globalization;
using System.Linq.Expressions;

//TODO: Create unit tests.... a lot of...
namespace SolrExpress.Query
{
    public class SolrExpression
    {
        protected string Value;

        public static SolrExpression operator &(SolrExpression expression1, SolrExpression expression2)
        {
            return expression1;
        }

        public static SolrExpression operator |(SolrExpression expression1, SolrExpression expression2)
        {
            return expression1;
        }

        public static bool operator true(SolrExpression expression1)
        {
            return true;
        }

        public static bool operator false(SolrExpression expression1)
        {
            return false;
        }

        public SolrExpression()
        {
        }

        /// <summary>
        /// Create a solr expression
        /// </summary>
        /// <param name="value">Value of the filter</param>
        public SolrExpression(string value)
        {
            this.Value = value;
        }

        /// <summary>
        /// Resolve internal expression
        /// </summary>
        /// <returns>Resolved internal expression</returns>
        internal string Resolve()
        {
            return this.Value;
        }
    }

    public class SolrExpression<T> : SolrExpression
        where T : IDocument
    {
        /// <summary>
        /// Create a solr expression
        /// </summary>
        /// <param name="expression">Expression used to find the property name</param>
        /// <param name="value">Value of the filter</param>
        public SolrExpression(Expression<Func<T, object>> expression, string value)
        {
            var fieldName = UtilHelper.GetPropertyNameFromExpression(expression);

            this.Value = string.Concat(fieldName, ":", value);
        }

        /// <summary>
        /// Create a solr expression
        /// </summary>
        /// <param name="expression">Expression used to find the property name</param>
        /// <param name="from">From value in a range filter</param>
        /// <param name="to">To value in a range filter</param>
        public SolrExpression(Expression<Func<T, object>> expression, DateTime? from, DateTime? to)
        {
            var fieldName = UtilHelper.GetPropertyNameFromExpression(expression);

            this.Value = string.Format(
                "{0}:[{1} TO {2}]",
                fieldName,
                from != null ? from.Value.ToString("yyyy-MM-dd'T'HH:mm:ss'Z'", CultureInfo.InvariantCulture) : "*",
                to != null ? to.Value.ToString("yyyy-MM-dd'T'HH:mm:ss'Z'", CultureInfo.InvariantCulture) : "*");
        }

        /// <summary>
        /// Create a solr expression
        /// </summary>
        /// <param name="expression">Expression used to find the property name</param>
        /// <param name="from">From value in a range filter</param>
        /// <param name="to">To value in a range filter</param>
        public SolrExpression(Expression<Func<T, object>> expression, int? from, int? to)
        {
            var fieldName = UtilHelper.GetPropertyNameFromExpression(expression);

            this.Value = string.Format(
                "{0}:[{1} TO {2}]",
                fieldName,
                from != null ? from.Value.ToString("0", CultureInfo.InvariantCulture) : "*",
                to != null ? to.Value.ToString("0", CultureInfo.InvariantCulture) : "*");
        }

        /// <summary>
        /// Create a solr expression
        /// </summary>
        /// <param name="expression">Expression used to find the property name</param>
        /// <param name="from">From value in a range filter</param>
        /// <param name="to">To value in a range filter</param>
        public SolrExpression(Expression<Func<T, object>> expression, double? from, double? to)
        {
            var fieldName = UtilHelper.GetPropertyNameFromExpression(expression);

            this.Value = string.Format(
                "{0}:[{1} TO {2}]",
                fieldName,
                from != null ? from.Value.ToString("0.#", CultureInfo.InvariantCulture) : "*",
                to != null ? to.Value.ToString("0.#", CultureInfo.InvariantCulture) : "*");
        }

        /// <summary>
        /// Create a solr expression
        /// </summary>
        /// <param name="expression">Expression used to find the property name</param>
        /// <param name="from">From value in a range filter</param>
        /// <param name="to">To value in a range filter</param>
        public SolrExpression(Expression<Func<T, object>> expression, decimal? from, decimal? to)
        {
            var fieldName = UtilHelper.GetPropertyNameFromExpression(expression);

            this.Value = string.Format(
                "{0}:[{1} TO {2}]",
                fieldName,
                from != null ? from.Value.ToString("0.#", CultureInfo.InvariantCulture) : "*",
                to != null ? to.Value.ToString("0.#", CultureInfo.InvariantCulture) : "*");
        }

        /// <summary>
        /// Create a solr expression
        /// </summary>
        /// <param name="expression">Expression used to find the property name</param>
        /// <param name="from">From value in a range filter</param>
        /// <param name="to">To value in a range filter</param>
        public SolrExpression(Expression<Func<T, object>> expression, GeoCoordinate? from, GeoCoordinate? to)
        {
            var fieldName = UtilHelper.GetPropertyNameFromExpression(expression);

            this.Value = string.Format(
                "{0}:[{1} TO {2}]",
                fieldName,
                from != null ? from.Value.ToString() : "*",
                to != null ? to.Value.ToString() : "*");
        }
    }
}
