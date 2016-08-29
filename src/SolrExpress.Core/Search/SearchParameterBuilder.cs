using SolrExpress.Core.DependencyInjection;
using SolrExpress.Core.Search.Parameter;
using SolrExpress.Core.Search.ParameterValue;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SolrExpress.Core.Search
{
    public static class SearchParameterBuilder
    {
        /// <summary>
        /// Create a facet field parameter
        /// </summary>
        /// <param name="expression">Expression used to find the property name</param>
        /// <param name="sortType">Sort type of the result of the facet</param>
        /// <param name="limit">Limit of itens in facet's result</param>
        /// <param name="excludes">List of tags to exclude in facet calculation</param>
        public static IFacetFieldParameter<TDocument> FacetField<TDocument>(Expression<Func<TDocument, object>> expression, FacetSortType? sortType = null, int? limit = null, params string[] excludes)
            where TDocument : IDocument
        {
            return ApplicationServices
                .Current
                .GetService<IFacetFieldParameter<TDocument>>()
                .Configure(expression, sortType, limit, excludes);
        }

        /// <summary>
        /// Create a facet query parameter
        /// </summary>
        /// <param name="aliasName">Name of the alias added in the query</param>
        /// <param name="query">Query used to make the facet</param>
        /// <param name="sortType">Sort type of the result of the facet</param>
        /// <param name="excludes">List of tags to exclude in facet calculation</param>
        public static IFacetQueryParameter<TDocument> FacetQuery<TDocument>(string aliasName, ISearchParameterValue query, FacetSortType? sortType = null, params string[] excludes)
            where TDocument : IDocument
        {
            return ApplicationServices
                .Current
                .GetService<IFacetQueryParameter<TDocument>>()
                .Configure(aliasName, query, sortType, excludes);
        }

        /// <summary>
        /// Create a facet range parameter
        /// </summary>
        /// <param name="aliasName">Name of the alias added in the query</param>
        /// <param name="expression">Expression used to find the property name</param>
        /// <param name="gap">Size of each range bucket to make the facet</param>
        /// <param name="start">Lower bound to make the facet</param>
        /// <param name="end">Upper bound to make the facet</param>
        /// <param name="sortType">Sort type of the result of the facet</param>
        public static IFacetRangeParameter<TDocument> FacetRange<TDocument>(string aliasName, Expression<Func<TDocument, object>> expression, string gap = null, string start = null, string end = null, FacetSortType? sortType = null)
            where TDocument : IDocument
        {
            return ApplicationServices
                .Current
                .GetService<IFacetRangeParameter<TDocument>>()
                .Configure(aliasName, expression, gap, start, end, sortType);
        }

        /// <summary>
        /// Create a fields parameter
        /// </summary>
        /// <param name="expressions">Expression used to find the property name</param>
        public static IFieldsParameter<TDocument> Fields<TDocument>(params Expression<Func<TDocument, object>>[] expressions)
            where TDocument : IDocument
        {
            return ApplicationServices
                .Current
                .GetService<IFieldsParameter<TDocument>>()
                .Configure(expressions);
        }

        /// <summary>
        /// Create a filter parameter
        /// </summary>
        /// <param name="expression">Expression used to find the property name</param>
        /// <param name="value">Value of the filter</param>
        /// <param name="tagName">Tag name to use in facet excluding list</param>
        public static IFilterParameter<TDocument> Filter<TDocument>(Expression<Func<TDocument, object>> expression, string value, string tagName = null)
            where TDocument : IDocument
        {
            var paramaterValue = new Single<TDocument>(expression, value);

            return ApplicationServices
                .Current
                .GetService<IFilterParameter<TDocument>>()
                .Configure(paramaterValue, tagName);
        }

        /// <summary>
        /// Create a filter parameter
        /// </summary>
        /// <param name="expression">Expression used to find the property name</param>
        /// <param name="from">From value in a range filter</param>
        /// <param name="to">To value in a range filter</param>
        /// <param name="tagName">Tag name to use in facet excluding list</param>
        public static IFilterParameter<TDocument> Filter<TDocument, TValue>(Expression<Func<TDocument, object>> expression, TValue? from, TValue? to, string tagName = null)
            where TDocument : IDocument
            where TValue : struct
        {
            var paramaterValue = new Range<TDocument, TValue>(expression, from, to);

            return ApplicationServices
                .Current
                .GetService<IFilterParameter<TDocument>>()
                .Configure(paramaterValue, tagName);
        }

        /// <summary>
        /// Create a limit parameter
        /// </summary>
        /// <param name="value">Value of limit</param>
        public static ILimitParameter Limit(int value)
        {
            return ApplicationServices
                .Current
                .GetService<ILimitParameter>()
                .Configure(value);
        }

        /// <summary>
        /// Create a offset parameter
        /// </summary>
        /// <param name="value">Value of limit</param>
        public static IOffsetParameter Offset(int value)
        {
            return ApplicationServices
                .Current
                .GetService<IOffsetParameter>()
                .Configure(value);
        }

        /// <summary>
        /// Create a query parameter
        /// </summary>
        /// <param name="value">Parameter to include in the query</param>
        public static IQueryParameter<TDocument> Query<TDocument>(ISearchParameterValue value)
            where TDocument : IDocument
        {
            return ApplicationServices
                .Current
                .GetService<IQueryParameter<TDocument>>()
                .Configure(value);
        }

        /// <summary>
        /// Create a query parameter
        /// </summary>
        /// <param name="value">Parameter to include in the query</param>
        public static IQueryParameter<TDocument> Query<TDocument>(string value)
            where TDocument : IDocument
        {
            var paramaterValue = new Any(value);

            return ApplicationServices
                .Current
                .GetService<IQueryParameter<TDocument>>()
                .Configure(paramaterValue);
        }

        /// <summary>
        /// Create a query parameter
        /// </summary>
        /// <param name="expression">Expression used to find the property name</param>
        /// <param name="value">Value of the query</param>
        public static IQueryParameter<TDocument> Query<TDocument>(Expression<Func<TDocument, object>> expression, string value)
            where TDocument : IDocument
        {
            var paramaterValue = new Single<TDocument>(expression, value);

            return ApplicationServices
                .Current
                .GetService<IQueryParameter<TDocument>>()
                .Configure(paramaterValue);
        }

        /// <summary>
        /// Create a sort parameter
        /// </summary>
        /// <param name="expression">Expression used to find the property name</param>
        /// <param name="ascendent">True to ascendent order, otherwise false</param>
        public static ISortParameter<TDocument> Sort<TDocument>(Expression<Func<TDocument, object>> expression, bool ascendent)
            where TDocument : IDocument
        {
            return ApplicationServices
                .Current
                .GetService<ISortParameter<TDocument>>()
                .Configure(expression, ascendent);
        }

        /// <summary>
        /// Create a sort parameter
        /// </summary>
        /// <param name="ascendent">True to ascendent order, otherwise false</param>
        /// <param name="expressions">Expression used to find the property name</param>
        public static IEnumerable<ISortParameter<TDocument>> Sort<TDocument>(bool ascendent, params Expression<Func<TDocument, object>>[] expressions)
            where TDocument : IDocument
        {
            foreach (var expression in expressions)
            {
                yield return ApplicationServices
                    .Current
                    .GetService<ISortParameter<TDocument>>()
                    .Configure(expression, ascendent);
            }
        }

        /// <summary>
        /// Create a random sort parameter
        /// </summary>
        /// <param name="ascendent">True to ascendent order, otherwise false</param>
        /// <param name="expressions">Expression used to find the property name</param>
        public static IRandomSortParameter RandomSort(bool ascendent)
        {
            return ApplicationServices
                .Current
                .GetService<IRandomSortParameter>()
                .Configure(ascendent);
        }

        /// <summary>
        /// Create a facet limit parameter
        /// </summary>
        /// <param name="value">Value of limit</param>
        public static IFacetLimitParameter FacetLimit(int value)
        {
            return ApplicationServices
                .Current
                .GetService<IFacetLimitParameter>()
                .Configure(value);
        }

        /// <summary>
        /// Create a minimum should match parameter
        /// </summary>
        /// <param name="expression">Expression used to make the mm parameter</param>
        public static IMinimumShouldMatchParameter MinimumShouldMatch(string expression)
        {
            return ApplicationServices
                .Current
                .GetService<IMinimumShouldMatchParameter>()
                .Configure(expression);
        }

        /// <summary>
        /// Create a query field parameter
        /// </summary>
        /// <param name="expression">Expression used to make the mm parameter</param>
        public static IQueryFieldParameter QueryField(string expression)
        {
            return ApplicationServices
                .Current
                .GetService<IQueryFieldParameter>()
                .Configure(expression);
        }

        /// <summary>
        /// Create a query field parameter
        /// </summary>
        /// <param name="expression">Expression used to find the property name</param>
        /// <param name="functionType">Function used in the spatial filter</param>
        /// <param name="centerPoint">Center point to spatial filter</param>
        /// <param name="distance">Distance from the center point</param>
        public static ISpatialFilterParameter<TDocument> SpatialFilter<TDocument>(Expression<Func<TDocument, object>> expression, SolrSpatialFunctionType functionType, GeoCoordinate centerPoint, decimal distance)
            where TDocument : IDocument
        {
            return ApplicationServices
                .Current
                .GetService<ISpatialFilterParameter<TDocument>>()
                .Configure(expression, functionType, centerPoint, distance);
        }

        /// <summary>
        /// Create a any parameter
        /// </summary>
        /// <param name="name">Name of the parameter</param>
        /// <param name="value">Value of the parameter</param>
        public static IAnyParameter Any(string name, string value)
        {
            return ApplicationServices
                .Current
                .GetService<IAnyParameter>()
                .Configure(name, value);
        }

        /// <summary>
        /// Create a boost parameter
        /// </summary>
        /// <param name="query">Query used to make boost</param>
        /// <param name="boostFunctionType">Boost type used in calculation. Default is BoostFunctionType.Boost</param>
        public static IBoostParameter<TDocument> Boost<TDocument>(ISearchParameterValue query, BoostFunctionType? boostFunctionType = null)
            where TDocument : IDocument
        {
            return ApplicationServices
                .Current
                .GetService<IBoostParameter<TDocument>>()
                .Configure(query, boostFunctionType ?? BoostFunctionType.Boost);
        }
    }
}
