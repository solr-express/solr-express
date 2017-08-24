using System;
using System.Linq.Expressions;

namespace SolrExpress.Search.Query.Extension
{
    /// <summary>
    /// Extensions to configure search queries
    /// </summary>
    public static class SearchQueryExtension
    {
        /// <summary>
        /// Create a search query to find all informed values (conditional AND)
        /// </summary>
        /// <param name="searchQuery">Search query instance used to make query</param>
        /// <param name="values">Values used in search</param>
        /// <returns>Search query configured</returns>
        public static SearchQuery<TDocument> All<TDocument, TValue>(this SearchQuery<TDocument> searchQuery, params TValue[] values)
            where TDocument : Document
        {
            searchQuery.AddParenthesisOpening();

            for (var index = 0; index < values.Length - 1; index++)
            {
                searchQuery
                    .AddValue(values[index])
                    .AddOperatorAnd();
            }

            searchQuery
                .AddValue(values[values.Length - 1])
                .AddParenthesisClosure();

            return searchQuery;
        }

        /// <summary>
        /// Create a search query to find all informed values (conditional OR)
        /// </summary>
        /// <param name="searchQuery">Search query instance used to make query</param>
        /// <param name="values">Values used in search</param>
        /// <returns>Search query configured</returns>
        public static SearchQuery<TDocument> Any<TDocument, TValue>(this SearchQuery<TDocument> searchQuery, params TValue[] values)
            where TDocument : Document
        {
            if (values.Length == 0)
            {
                return searchQuery;
            }

            searchQuery.AddParenthesisOpening();

            for (var index = 0; index < values.Length - 1; index++)
            {
                searchQuery
                    .AddValue(values[index])
                    .AddOperatorOr();
            }

            searchQuery
                .AddValue(values[values.Length - 1])
                .AddParenthesisClosure();

            return searchQuery;
        }

        /// <summary>
        /// Create a search query to find something starts with informed value
        /// </summary>
        /// <param name="searchQuery">Search query instance used to make query</param>
        /// <param name="value">Value used in search</param>
        /// <returns>Search query configured</returns>
        public static SearchQuery<TDocument> StartsWith<TDocument>(this SearchQuery<TDocument> searchQuery, string value)
            where TDocument : Document
        {
            searchQuery.AddValue($"/{value}.*/");

            return searchQuery;
        }

        /// <summary>
        /// Create a search query to find exact informed value
        /// </summary>
        /// <param name="searchQuery">Search query instance used to make query</param>
        /// <param name="value">Value used in search</param>
        /// <returns>Search query configured</returns>
        public static SearchQuery<TDocument> EqualsTo<TDocument, TValue>(this SearchQuery<TDocument> searchQuery, TValue value)
            where TDocument : Document
        {
            searchQuery.AddValue(value);

            return searchQuery;
        }

        /// <summary>
        /// Create a search query to find negate informed value
        /// </summary>
        /// <param name="searchQuery">Search query instance used to make query</param>
        /// <param name="value">Value used in search</param>
        /// <returns>Search query configured</returns>
        public static SearchQuery<TDocument> NotEqualsTo<TDocument, TValue>(this SearchQuery<TDocument> searchQuery, TValue value)
            where TDocument : Document
        {
            searchQuery
                .AddValue("-", false)
                .AddParenthesisOpening()
                .AddValue(value)
                .AddParenthesisClosure();

            return searchQuery;
        }

        /// <summary>
        /// Create a search query to find someting in informed range
        /// </summary>
        /// <param name="searchQuery">Search query instance used to make query</param>
        /// <param name="valueFrom">Value used in search</param>
        /// <param name="valueTo">Value used in search</param>
        /// <returns>Search query configured</returns>
        public static SearchQuery<TDocument> InRange<TDocument, TValue>(this SearchQuery<TDocument> searchQuery, TValue valueFrom, TValue valueTo)
            where TDocument : Document
            where TValue : struct
        {
            searchQuery.AddRangeValue(valueFrom, (TValue?)valueTo);

            return searchQuery;
        }

        /// <summary>
        /// Create a search query to find someting greater than informed value
        /// </summary>
        /// <param name="searchQuery">Search query instance used to make query</param>
        /// <param name="value">Value used in search</param>
        /// <returns>Search query configured</returns>
        public static SearchQuery<TDocument> GreaterThan<TDocument, TValue>(this SearchQuery<TDocument> searchQuery, TValue value)
            where TDocument : Document
            where TValue : struct
        {
            searchQuery
                .AddRangeValue((TValue?)value, null);

            return searchQuery;
        }

        /// <summary>
        /// Create a search query to find someting less than informed value
        /// </summary>
        /// <param name="searchQuery">Search query instance used to make query</param>
        /// <param name="value">Value used in search</param>
        /// <returns>Search query configured</returns>
        public static SearchQuery<TDocument> LessThan<TDocument, TValue>(this SearchQuery<TDocument> searchQuery, TValue value)
            where TDocument : Document
            where TValue : struct
        {
            searchQuery
                .AddRangeValue(null, (TValue?)value);

            return searchQuery;
        }

        /// <summary>
        /// Create a search query expression using OR operator
        /// </summary>
        /// <param name="searchQuery">Search query instance used to make query</param>
        /// <param name="searchQueryExtend">Search query used inside group OR</param>
        /// <returns>Search query configured</returns>
        public static SearchQuery<TDocument> Or<TDocument>(this SearchQuery<TDocument> searchQuery, Action<SearchQuery<TDocument>> searchQueryExtend)
            where TDocument : Document
        {
            searchQuery
                .AddOperatorOr()
                .AddParenthesisOpening();

            searchQueryExtend.Invoke(searchQuery);

            searchQuery.AddParenthesisClosure();

            return searchQuery;
        }

        /// <summary>
        /// Create a search query expression using OR operator
        /// </summary>
        /// <param name="searchQuery">Search query instance used to make query</param>
        /// <param name="searchQueryExtend">Search query used inside group AND</param>
        /// <returns>Search query configured</returns>
        public static SearchQuery<TDocument> And<TDocument>(this SearchQuery<TDocument> searchQuery, Action<SearchQuery<TDocument>> searchQueryExtend)
            where TDocument : Document
        {
            searchQuery
                .AddOperatorAnd()
                .AddParenthesisOpening();

            searchQueryExtend.Invoke(searchQuery);

            searchQuery.AddParenthesisClosure();

            return searchQuery;
        }

        /// <summary>
        /// Create a search query expression using NOT group
        /// </summary>
        /// <param name="searchQuery">Search query instance used to make query</param>
        /// <param name="searchQueryExtend">Search query used inside group AND</param>
        /// <returns>Search query configured</returns>
        public static SearchQuery<TDocument> Not<TDocument>(this SearchQuery<TDocument> searchQuery, Action<SearchQuery<TDocument>> searchQueryExtend)
            where TDocument : Document
        {
            searchQuery
                .AddValue("-", false)
                .AddParenthesisOpening();

            searchQueryExtend.Invoke(searchQuery);

            searchQuery.AddParenthesisClosure();

            return searchQuery;
        }

        /// <summary>
        /// Create a search query expression using isolating in a group
        /// </summary>
        /// <param name="searchQuery">Search query instance used to make query</param>
        /// <param name="searchQueryExtend">Search query used inside group AND</param>
        /// <returns>Search query configured</returns>
        public static SearchQuery<TDocument> Group<TDocument>(this SearchQuery<TDocument> searchQuery, Action<SearchQuery<TDocument>> searchQueryExtend)
            where TDocument : Document
        {
            searchQuery
                .AddParenthesisOpening();

            searchQueryExtend.Invoke(searchQuery);

            searchQuery.AddParenthesisClosure();

            return searchQuery;
        }

        /// <summary>
        /// Create a search query using field name from expression
        /// </summary>
        /// <param name="searchQuery">Search query instance used to make query</param>
        /// <param name="fieldExpression">Expressions used to find field name</param>
        /// <returns>Search query configured</returns>
        public static SearchQuery<TDocument> Field<TDocument>(this SearchQuery<TDocument> searchQuery, Expression<Func<TDocument, object>> fieldExpression)
            where TDocument : Document
        {
            searchQuery.AddField(fieldExpression);

            return searchQuery;
        }
    }
}
