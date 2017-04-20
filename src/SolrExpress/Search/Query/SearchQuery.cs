using System;
using System.Globalization;
using System.Linq.Expressions;
using System.Text;

namespace SolrExpress.Search.Query
{
    /// <summary>
    /// Solr queries container
    /// </summary>
    public class SearchQuery
    {
        private readonly StringBuilder _sb = new StringBuilder();

        /// <summary>
        /// Add operator AND into expression stack
        /// </summary>
        /// <returns>It self</returns>
        internal SearchQuery AddOperatorAnd()
        {
            this._sb.Append(" AND ");

            return this;
        }

        /// <summary>
        /// Add operator OR into expression stack
        /// </summary>
        /// <returns>It self</returns>
        internal SearchQuery AddOperatorOr()
        {
            this._sb.Append(" OR ");

            return this;
        }

        /// <summary>
        /// Add symbol ( into expression stack
        /// </summary>
        /// <returns>It self</returns>
        internal SearchQuery AddParenthesisOpening()
        {
            this._sb.Append("(");

            return this;
        }

        /// <summary>
        /// Add symbol ) into expression stack
        /// </summary>
        /// <returns>It self</returns>
        internal SearchQuery AddParenthesisClosure()
        {
            this._sb.Append(")");

            return this;
        }

        /// <summary>
        /// Add field name into expression stack
        /// </summary>
        /// <param name="fieldExpression">Expressions used to find field name</param>
        /// <returns>It self</returns>
        internal SearchQuery AddField<TDocument>(Expression<Func<TDocument, object>> fieldExpression)
            where TDocument : IDocument
        {
            // TODO: Think about ExpressionBuilder and TDocument

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
            if (value is string && addQuotesInString)
            {
                this._sb.Append("\"");
                this._sb.Append(value);
                this._sb.Append("\"");
            }
            else if (value is decimal || value is double || value is float)
            {
                this._sb.Append(((IFormattable)value).ToString("0.#", CultureInfo.InvariantCulture));
            }
            else if (value is DateTime)
            {
                this._sb.Append(((IFormattable)value).ToString("yyyy-MM-dd'T'HH:mm:ss'Z'", CultureInfo.InvariantCulture));
            }
            else
            {
                this._sb.Append(value);
            }

            return this;
        }

        /// <summary>
        /// Add indicated value into expression stack
        /// </summary>
        /// <param name="from">Value "from" to add into expression stack</param>
        /// <param name="to">Value "to" to add into expression stack</param>
        /// <returns>It self</returns>
        internal SearchQuery AddRangeValue<T>(T from, T to)
        {
            this._sb.Append("[");
            this.AddValue(from);
            this._sb.Append(" TO ");
            this.AddValue(to);
            this._sb.Append("]");

            return this;
        }

        /// <summary>
        /// Execute parameter value generator
        /// </summary>
        /// <returns>Result generated value</returns>
        internal string Execute()
        {
            return this._sb.ToString();
        }
    }
}
