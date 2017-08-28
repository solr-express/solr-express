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

        /// <summary>
        /// Create a search query to find all informed values (conditional AND)
        /// </summary>
        /// <param name="values">Values used in search</param>
        /// <returns>Search query configured</returns>
        public SearchQuery<TDocument> All<TValue>(params TValue[] values)
        {
            this.AddParenthesisOpening();

            for (var index = 0; index < values.Length - 1; index++)
            {
                this
                    .AddValue(values[index])
                    .AddOperatorAnd();
            }

            this
                .AddValue(values[values.Length - 1])
                .AddParenthesisClosure();

            return this;
        }

        /// <summary>
        /// Create a search query to find some of informed values (conditional OR)
        /// </summary>
        /// <param name="values">Values used in search</param>
        /// <returns>Search query configured</returns>
        public SearchQuery<TDocument> Any<TValue>(params TValue[] values)
        {
            if (values.Length == 0)
            {
                return this;
            }

            this.AddParenthesisOpening();

            for (var index = 0; index < values.Length - 1; index++)
            {
                this
                    .AddValue(values[index])
                    .AddOperatorOr();
            }

            this
                .AddValue(values[values.Length - 1])
                .AddParenthesisClosure();

            return this;
        }

        /// <summary>
        /// Create a search query to find something starts with informed value
        /// </summary>
        /// <param name="value">Value used in search</param>
        /// <returns>Search query configured</returns>
        public SearchQuery<TDocument> StartsWith(string value)
        {
            return this.AddValue($"/{value}.*/");
        }

        /// <summary>
        /// Create a search query to find exact informed value
        /// </summary>
        /// <param name="value">Value used in search</param>
        /// <returns>Search query configured</returns>
        public SearchQuery<TDocument> EqualsTo<TValue>(TValue value)
        {
            return this.AddValue(value);
        }

        /// <summary>
        /// Create a search query to find negate informed value
        /// </summary>
        /// <param name="value">Value used in search</param>
        /// <returns>Search query configured</returns>
        public SearchQuery<TDocument> NotEqualsTo<TValue>(TValue value)
        {
            return this
                .AddValue("-", false)
                .AddParenthesisOpening()
                .AddValue(value)
                .AddParenthesisClosure();
        }

        /// <summary>
        /// Create a search query to find someting in informed range
        /// </summary>
        /// <param name="valueFrom">Value used in search</param>
        /// <param name="valueTo">Value used in search</param>
        /// <returns>Search query configured</returns>
        public SearchQuery<TDocument> InRange<TValue>(TValue valueFrom, TValue valueTo)
            where TValue : struct
        {
            return this.AddRangeValue(valueFrom, (TValue?)valueTo);
        }

        /// <summary>
        /// Create a search query to find someting greater than informed value
        /// </summary>
        /// <param name="value">Value used in search</param>
        /// <returns>Search query configured</returns>
        public SearchQuery<TDocument> GreaterThan<TValue>(TValue value)
            where TValue : struct
        {
            return this.AddRangeValue((TValue?)value, null);
        }

        /// <summary>
        /// Create a search query to find someting less than informed value
        /// </summary>
        /// <param name="value">Value used in search</param>
        /// <returns>Search query configured</returns>
        public SearchQuery<TDocument> LessThan<TValue>(TValue value)
            where TValue : struct
        {
            return this.AddRangeValue(null, (TValue?)value);
        }

        /// <summary>
        /// Create a search query expression using OR operator
        /// </summary>
        /// <param name="searchQueryExtend">Search query used inside group OR</param>
        /// <returns>Search query configured</returns>
        public SearchQuery<TDocument> Or(Action<SearchQuery<TDocument>> searchQueryExtend)
        {
            this
                .AddOperatorOr()
                .AddParenthesisOpening();

            searchQueryExtend.Invoke(this);

            return this.AddParenthesisClosure();
        }

        /// <summary>
        /// Create a search query expression using OR operator
        /// </summary>
        /// <param name="searchQueryExtend">Search query used inside group AND</param>
        /// <returns>Search query configured</returns>
        public SearchQuery<TDocument> And(Action<SearchQuery<TDocument>> searchQueryExtend)
        {
            this
                .AddOperatorAnd()
                .AddParenthesisOpening();

            searchQueryExtend.Invoke(this);

            return this.AddParenthesisClosure();
        }

        /// <summary>
        /// Create a search query expression using NOT group
        /// </summary>
        /// <param name="searchQueryExtend">Search query used inside group AND</param>
        /// <returns>Search query configured</returns>
        public SearchQuery<TDocument> Not(Action<SearchQuery<TDocument>> searchQueryExtend)
        {
            this
                .AddValue("-", false)
                .AddParenthesisOpening();

            searchQueryExtend.Invoke(this);

            return this.AddParenthesisClosure();
        }

        /// <summary>
        /// Create a search query expression isolating in a group
        /// </summary>
        /// <param name="searchQueryExtend">Search query used inside group AND</param>
        /// <returns>Search query configured</returns>
        public SearchQuery<TDocument> Group(Action<SearchQuery<TDocument>> searchQueryExtend)
        {
            this
                .AddParenthesisOpening();

            searchQueryExtend.Invoke(this);

            return this.AddParenthesisClosure();
        }

        /// <summary>
        /// Create a search query using field name from expression
        /// </summary>
        /// <param name="fieldExpression">Expressions used to find field name</param>
        /// <returns>Search query configured</returns>
        public SearchQuery<TDocument> Field(Expression<Func<TDocument, object>> fieldExpression)
        {
            return this.AddField(fieldExpression);
        }
    }
}
