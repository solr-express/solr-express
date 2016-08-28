using SolrExpress.Core.DependencyInjection;
using SolrExpress.Core.Search;
using SolrExpress.Core.Search.Parameter;
using SolrExpress.Core.Search.ParameterValue;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SolrExpress.Core.Extension
{
    [Obsolete("Unify")]
    public static class ISolrSearchExtensions
    {
        /// <summary>
        /// Create a facet field parameter
        /// </summary>
        /// <param name="expression">Expression used to find the property name</param>
        /// <param name="sortType">Sort type of the result of the facet</param>
        /// <param name="limit">Limit of itens in facet's result</param>
        /// <param name="excludes">List of tags to exclude in facet calculation</param>
        public static ISolrSearch<TDocument> FacetField<TDocument>(this ISolrSearch<TDocument> search, Expression<Func<TDocument, object>> expression, FacetSortType? sortType = null, int? limit = null, params string[] excludes)
            where TDocument : IDocument
        {
            var parameter = ApplicationServices
                .Current
                .GetService<IFacetFieldParameter<TDocument>>()
                .Configure(expression, sortType, limit, excludes);
            
            search.Add(parameter);
            return search;
        }

        /// <summary>
        /// Create a facet query parameter
        /// </summary>
        /// <param name="aliasName">Name of the alias added in the query</param>
        /// <param name="query">Query used to make the facet</param>
        /// <param name="sortType">Sort type of the result of the facet</param>
        /// <param name="excludes">List of tags to exclude in facet calculation</param>
        public static ISolrSearch<TDocument> FacetQuery<TDocument>(this ISolrSearch<TDocument> search, string aliasName, ISearchParameterValue query, FacetSortType? sortType = null, params string[] excludes)
            where TDocument : IDocument
        {
            var parameter = ApplicationServices
                .Current
                .GetService<IFacetQueryParameter<TDocument>>()
                .Configure(aliasName, query, sortType, excludes);

            search.Add(parameter);
            return search;
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
        public static ISolrSearch<TDocument> FacetRange<TDocument>(this ISolrSearch<TDocument> search, string aliasName, Expression<Func<TDocument, object>> expression, string gap = null, string start = null, string end = null, FacetSortType? sortType = null)
            where TDocument : IDocument
        {
            var parameter = ApplicationServices
                .Current
                .GetService<IFacetRangeParameter<TDocument>>()
                .Configure(aliasName, expression, gap, start, end, sortType);

            search.Add(parameter);
            return search;
        }

        /// <summary>
        /// Create a fields parameter
        /// </summary>
        /// <param name="expressions">Expression used to find the property name</param>
        public static ISolrSearch<TDocument> Fields<TDocument>(this ISolrSearch<TDocument> search, params Expression<Func<TDocument, object>>[] expressions)
            where TDocument : IDocument
        {
            var parameter = ApplicationServices
                .Current
                .GetService<IFieldsParameter<TDocument>>()
                .Configure(expressions);

            search.Add(parameter);
            return search;
        }

        /// <summary>
        /// Create a filter parameter
        /// </summary>
        /// <param name="expression">Expression used to find the property name</param>
        /// <param name="value">Value of the filter</param>
        /// <param name="tagName">Tag name to use in facet excluding list</param>
        public static ISolrSearch<TDocument> Filter<TDocument>(this ISolrSearch<TDocument> search, Expression<Func<TDocument, object>> expression, string value, string tagName = null)
            where TDocument : IDocument
        {
            var paramaterValue = new Single<TDocument>(expression, value);

            var parameter = ApplicationServices
                .Current
                .GetService<IFilterParameter<TDocument>>()
                .Configure(paramaterValue, tagName);

            search.Add(parameter);
            return search;
        }

        /// <summary>
        /// Create a filter parameter
        /// </summary>
        /// <param name="expression">Expression used to find the property name</param>
        /// <param name="from">From value in a range filter</param>
        /// <param name="to">To value in a range filter</param>
        /// <param name="tagName">Tag name to use in facet excluding list</param>
        public static ISolrSearch<TDocument> Filter<TDocument, TValue>(this ISolrSearch<TDocument> search, Expression<Func<TDocument, object>> expression, TValue? from, TValue? to, string tagName = null)
            where TDocument : IDocument
            where TValue : struct
        {
            var paramaterValue = new Range<TDocument, TValue>(expression, from, to);

            var parameter = ApplicationServices
                .Current
                .GetService<IFilterParameter<TDocument>>()
                .Configure(paramaterValue, tagName);

            search.Add(parameter);
            return search;
        }

        /// <summary>
        /// Create a limit parameter
        /// </summary>
        /// <param name="value">Value of limit</param>
        public static ISolrSearch<TDocument> Limit<TDocument>(this ISolrSearch<TDocument> search, int value)
            where TDocument : IDocument
        {
            var parameter = ApplicationServices
                .Current
                .GetService<ILimitParameter>()
                .Configure(value);

            search.Add(parameter);
            return search;
        }

        /// <summary>
        /// Create a offset parameter
        /// </summary>
        /// <param name="value">Value of limit</param>
        public static ISolrSearch<TDocument> Offset<TDocument>(this ISolrSearch<TDocument> search, int value)
            where TDocument : IDocument
        {
            var parameter = ApplicationServices
                .Current
                .GetService<IOffsetParameter>()
                .Configure(value);

            search.Add(parameter);
            return search;
        }

        /// <summary>
        /// Create a query parameter
        /// </summary>
        /// <param name="value">Parameter to include in the query</param>
        public static ISolrSearch<TDocument> Query<TDocument>(this ISolrSearch<TDocument> search, ISearchParameterValue value)
            where TDocument : IDocument
        {
            var parameter = ApplicationServices
                .Current
                .GetService<IQueryParameter<TDocument>>()
                .Configure(value);

            search.Add(parameter);
            return search;
        }

        /// <summary>
        /// Create a query parameter
        /// </summary>
        /// <param name="value">Parameter to include in the query</param>
        public static ISolrSearch<TDocument> Query<TDocument>(this ISolrSearch<TDocument> search, string value)
            where TDocument : IDocument
        {
            var paramaterValue = new Any(value);

            var parameter = ApplicationServices
                .Current
                .GetService<IQueryParameter<TDocument>>()
                .Configure(paramaterValue);

            search.Add(parameter);
            return search;
        }

        /// <summary>
        /// Create a query parameter
        /// </summary>
        /// <param name="expression">Expression used to find the property name</param>
        /// <param name="value">Value of the query</param>
        public static ISolrSearch<TDocument> Query<TDocument>(this ISolrSearch<TDocument> search, Expression<Func<TDocument, object>> expression, string value)
            where TDocument : IDocument
        {
            var paramaterValue = new Single<TDocument>(expression, value);

            var parameter = ApplicationServices
                .Current
                .GetService<IQueryParameter<TDocument>>()
                .Configure(paramaterValue);

            search.Add(parameter);
            return search;
        }

        /// <summary>
        /// Create a sort parameter
        /// </summary>
        /// <param name="expression">Expression used to find the property name</param>
        /// <param name="ascendent">True to ascendent order, otherwise false</param>
        public static ISolrSearch<TDocument> Sort<TDocument>(this ISolrSearch<TDocument> search, Expression<Func<TDocument, object>> expression, bool ascendent)
            where TDocument : IDocument
        {
            var parameter = ApplicationServices
                .Current
                .GetService<ISortParameter<TDocument>>()
                .Configure(expression, ascendent);

            search.Add(parameter);
            return search;
        }

        /// <summary>
        /// Create a sort parameter
        /// </summary>
        /// <param name="ascendent">True to ascendent order, otherwise false</param>
        /// <param name="expressions">Expression used to find the property name</param>
        public static ISolrSearch<TDocument> Sort<TDocument>(this ISolrSearch<TDocument> search, bool ascendent, params Expression<Func<TDocument, object>>[] expressions)
            where TDocument : IDocument
        {
            foreach (var expression in expressions)
            {
                var parameter = ApplicationServices
                    .Current
                    .GetService<ISortParameter<TDocument>>()
                    .Configure(expression, ascendent);

                search.Add(parameter);
            }

            return search;
        }

        /// <summary>
        /// Create a random sort parameter
        /// </summary>
        /// <param name="ascendent">True to ascendent order, otherwise false</param>
        /// <param name="expressions">Expression used to find the property name</param>
        public static ISolrSearch<TDocument> RandomSort<TDocument>(this ISolrSearch<TDocument> search, bool ascendent)
            where TDocument : IDocument
        {
            var parameter = ApplicationServices
                .Current
                .GetService<IRandomSortParameter>()
                .Configure(ascendent);

            search.Add(parameter);
            return search;
        }

        /// <summary>
        /// Create a facet limit parameter
        /// </summary>
        /// <param name="value">Value of limit</param>
        public static ISolrSearch<TDocument> FacetLimit<TDocument>(this ISolrSearch<TDocument> search, int value)
            where TDocument : IDocument
        {
            var parameter = ApplicationServices
                .Current
                .GetService<IFacetLimitParameter>()
                .Configure(value);

            search.Add(parameter);
            return search;
        }

        /// <summary>
        /// Create a minimum should match parameter
        /// </summary>
        /// <param name="expression">Expression used to make the mm parameter</param>
        public static ISolrSearch<TDocument> MinimumShouldMatch<TDocument>(this ISolrSearch<TDocument> search, string expression)
            where TDocument : IDocument
        {
            var parameter = ApplicationServices
                .Current
                .GetService<IMinimumShouldMatchParameter>()
                .Configure(expression);

            search.Add(parameter);
            return search;
        }

        /// <summary>
        /// Create a query field parameter
        /// </summary>
        /// <param name="expression">Expression used to make the mm parameter</param>
        public static ISolrSearch<TDocument> QueryField<TDocument>(this ISolrSearch<TDocument> search, string expression)
            where TDocument : IDocument
        {
            var parameter = ApplicationServices
                .Current
                .GetService<IQueryFieldParameter>()
                .Configure(expression);

            search.Add(parameter);
            return search;
        }

        /// <summary>
        /// Create a query field parameter
        /// </summary>
        /// <param name="expression">Expression used to find the property name</param>
        /// <param name="functionType">Function used in the spatial filter</param>
        /// <param name="centerPoint">Center point to spatial filter</param>
        /// <param name="distance">Distance from the center point</param>
        public static ISolrSearch<TDocument> SpatialFilter<TDocument>(this ISolrSearch<TDocument> search, Expression<Func<TDocument, object>> expression, SolrSpatialFunctionType functionType, GeoCoordinate centerPoint, decimal distance)
            where TDocument : IDocument
        {
            var parameter = ApplicationServices
                .Current
                .GetService<ISpatialFilterParameter<TDocument>>()
                .Configure(expression, functionType, centerPoint, distance);

            search.Add(parameter);
            return search;
        }

        /// <summary>
        /// Create a any parameter
        /// </summary>
        /// <param name="name">Name of the parameter</param>
        /// <param name="value">Value of the parameter</param>
        public static ISolrSearch<TDocument> Any<TDocument>(this ISolrSearch<TDocument> search, string name, string value)
            where TDocument : IDocument
        {
            var parameter = ApplicationServices
                .Current
                .GetService<IAnyParameter>()
                .Configure(name, value);

            search.Add(parameter);
            return search;
        }

        /// <summary>
        /// Create a boost parameter
        /// </summary>
        /// <param name="query">Query used to make boost</param>
        /// <param name="boostFunctionType">Boost type used in calculation. Default is BoostFunctionType.Boost</param>
        public static ISolrSearch<TDocument> Boost<TDocument>(this ISolrSearch<TDocument> search, ISearchParameterValue query, BoostFunctionType? boostFunctionType = null)
            where TDocument : IDocument
        {
            var parameter = ApplicationServices
                .Current
                .GetService<IBoostParameter<TDocument>>()
                .Configure(query, boostFunctionType ?? BoostFunctionType.Boost);

            search.Add(parameter);
            return search;
        }
    }
}
