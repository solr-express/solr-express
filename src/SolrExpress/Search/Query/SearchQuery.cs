using System;
using System.Globalization;
using System.Text;

namespace SolrExpress.Search.Query
{
    /// <summary>
    /// Solr queries container
    /// </summary>
    public class SearchQuery
    {
        protected readonly StringBuilder _InternalQuery = new StringBuilder();
        protected string _InternalLinkValue = string.Empty;

        /// <summary>
        /// Add operator AND into expression stack
        /// </summary>
        /// <returns>It self</returns>
        internal SearchQuery AddOperatorAnd()
        {
            this._InternalQuery.Append(this._InternalLinkValue);
            this._InternalLinkValue = string.Empty;
            this._InternalQuery.Append(" AND ");

            return this;
        }

        /// <summary>
        /// Add operator OR into expression stack
        /// </summary>
        /// <returns>It self</returns>
        internal SearchQuery AddOperatorOr()
        {
            this._InternalQuery.Append(this._InternalLinkValue);
            this._InternalLinkValue = string.Empty;
            this._InternalQuery.Append(" OR ");

            return this;
        }

        /// <summary>
        /// Add symbol ( into expression stack
        /// </summary>
        /// <returns>It self</returns>
        internal SearchQuery AddParenthesisOpening()
        {
            this._InternalQuery.Append(this._InternalLinkValue);
            this._InternalLinkValue = string.Empty;
            this._InternalQuery.Append("(");

            return this;
        }

        /// <summary>
        /// Add symbol ) into expression stack
        /// </summary>
        /// <returns>It self</returns>
        internal SearchQuery AddParenthesisClosure()
        {
            this._InternalQuery.Append(this._InternalLinkValue);
            this._InternalLinkValue = string.Empty;
            this._InternalQuery.Append(")");

            return this;
        }

        /// <summary>
        /// Add indicated value into expression stack
        /// </summary>
        /// <param name="value">Value to add into expression stack</param>
        /// <param name="addQuotesInString">True to add quotes before and after string values, otherwise false</param>
        /// <returns>It self</returns>
        internal SearchQuery AddValue<T>(T value, bool addQuotesInString = true)
        {
            this._InternalQuery.Append(this._InternalLinkValue);
            this._InternalLinkValue = string.Empty;

            if (value is string && addQuotesInString)
            {
                this._InternalQuery.Append("\"");
                this._InternalQuery.Append(value);
                this._InternalQuery.Append("\"");
            }
            else if (value is decimal || value is double || value is float)
            {
                this._InternalQuery.Append(((IFormattable)value).ToString("G", CultureInfo.InvariantCulture));
            }
            else if (value is DateTime)
            {
                this._InternalQuery.Append(((IFormattable)value).ToString("yyyy-MM-dd'T'HH:mm:ss'Z'", CultureInfo.InvariantCulture));
            }
            else
            {
                this._InternalQuery.Append(value);
            }

            return this;
        }

        /// <summary>
        /// Add indicated value into expression stack
        /// </summary>
        /// <param name="from">Value "from" to add into expression stack</param>
        /// <param name="to">Value "to" to add into expression stack</param>
        /// <returns>It self</returns>
        internal SearchQuery AddRangeValue<TValue>(TValue? from, TValue? to)
            where TValue : struct
        {
            this._InternalQuery.Append(this._InternalLinkValue);
            this._InternalLinkValue = string.Empty;

            this._InternalQuery.Append("[");

            if (from.HasValue)
            {
                this.AddValue(from.Value);
            }
            else
            {
                this.AddValue("*", false);
            }

            this._InternalQuery.Append(" TO ");

            if (to.HasValue)
            {
                this.AddValue(to.Value);
            }
            else
            {
                this.AddValue("*", false);
            }

            this._InternalQuery.Append("]");

            return this;
        }

        /// <summary>
        /// Execute parameter value generator
        /// </summary>
        /// <returns>Result generated value</returns>
        internal string Execute()
        {
            return this._InternalQuery.ToString();
        }
    }
}
