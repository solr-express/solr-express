using SolrExpress.Builder;
using System;
using System.Globalization;
using System.Linq.Expressions;
using System.Text;

namespace SolrExpress.Search.Query
{
    /// <summary>
    /// Solr queries container with TDocument link
    /// </summary>
    public class SearchQuery<TDocument>
        where TDocument : Document
    {
        private readonly ExpressionBuilder<TDocument> _expressionBuilder;
        private readonly StringBuilder _internalQuery = new StringBuilder();
        private string _internalLinkValue = string.Empty;

        public SearchQuery(ExpressionBuilder<TDocument> expressionBuilder)
        {
            this._expressionBuilder = expressionBuilder;
        }

        /// <summary>
        /// Add field name into expression stack
        /// </summary>
        /// <param name="fieldExpression">Expressions used to find field name</param>
        /// <returns>It self</returns>
        internal SearchQuery<TDocument> AddField(Expression<Func<TDocument, object>> fieldExpression)
        {
            this._internalQuery.Append(this._expressionBuilder.GetFieldName(fieldExpression));

            this._internalLinkValue = ":";

            return this;
        }

        /// <summary>
        /// Add operator AND into expression stack
        /// </summary>
        /// <returns>It self</returns>
        internal SearchQuery<TDocument> AddOperatorAnd()
        {
            this._internalQuery.Append(this._internalLinkValue);
            this._internalLinkValue = string.Empty;
            this._internalQuery.Append(" AND ");

            return this;
        }

        /// <summary>
        /// Add operator OR into expression stack
        /// </summary>
        /// <returns>It self</returns>
        internal SearchQuery<TDocument> AddOperatorOr()
        {
            this._internalQuery.Append(this._internalLinkValue);
            this._internalLinkValue = string.Empty;
            this._internalQuery.Append(" OR ");

            return this;
        }

        /// <summary>
        /// Add symbol ( into expression stack
        /// </summary>
        /// <returns>It self</returns>
        internal SearchQuery<TDocument> AddParenthesisOpening()
        {
            this._internalQuery.Append(this._internalLinkValue);
            this._internalLinkValue = string.Empty;
            this._internalQuery.Append("(");

            return this;
        }

        /// <summary>
        /// Add symbol ) into expression stack
        /// </summary>
        /// <returns>It self</returns>
        internal SearchQuery<TDocument> AddParenthesisClosure()
        {
            this._internalQuery.Append(this._internalLinkValue);
            this._internalLinkValue = string.Empty;
            this._internalQuery.Append(")");

            return this;
        }

        /// <summary>
        /// Add indicated value into expression stack
        /// </summary>
        /// <param name="value">Value to add into expression stack</param>
        /// <param name="addQuotesInString">True to add quotes before and after string values, otherwise false</param>
        /// <returns>It self</returns>
        internal SearchQuery<TDocument> AddValue<T>(T value, bool addQuotesInString = true)
        {
            this._internalQuery.Append(this._internalLinkValue);
            this._internalLinkValue = string.Empty;

            if (value is string && addQuotesInString)
            {
                this._internalQuery.Append("\"");
                this._internalQuery.Append(value);
                this._internalQuery.Append("\"");
            }
            else if (value is decimal || value is double || value is float)
            {
                this._internalQuery.Append(((IFormattable)value).ToString("G", CultureInfo.InvariantCulture));
            }
            else if (value is DateTime)
            {
                this._internalQuery.Append(((IFormattable)value).ToString("yyyy-MM-dd'T'HH:mm:ss'Z'", CultureInfo.InvariantCulture));
            }
            else
            {
                this._internalQuery.Append(value);
            }

            return this;
        }

        /// <summary>
        /// Add indicated value into expression stack
        /// </summary>
        /// <param name="from">Value "from" to add into expression stack</param>
        /// <param name="to">Value "to" to add into expression stack</param>
        /// <returns>It self</returns>
        internal SearchQuery<TDocument> AddRangeValue<TValue>(TValue? from, TValue? to)
            where TValue : struct
        {
            this._internalQuery.Append(this._internalLinkValue);
            this._internalLinkValue = string.Empty;

            this._internalQuery.Append("[");

            if (from.HasValue)
            {
                this.AddValue(from.Value);
            }
            else
            {
                this.AddValue("*", false);
            }

            this._internalQuery.Append(" TO ");

            if (to.HasValue)
            {
                this.AddValue(to.Value);
            }
            else
            {
                this.AddValue("*", false);
            }

            this._internalQuery.Append("]");

            return this;
        }

        /// <summary>
        /// Execute parameter value generator
        /// </summary>
        /// <returns>Result generated value</returns>
        internal string Execute()
        {
            return this._internalQuery.ToString();
        }
    }
}
