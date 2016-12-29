using SolrExpress.Core.DependencyInjection;
using SolrExpress.Core.Search.Parameter;
using SolrExpress.Core.Search.ParameterValue;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SolrExpress.Core.Search
{
    /// <summary>
    /// Search parameter builder
    /// </summary>
    public class SearchParameterBuilder<TDocument> : ISearchParameterBuilder<TDocument>
        where TDocument : IDocument
    {
        /// <summary>
        /// Default constructor of class
        /// </summary>
        /// <param name="engine">Instance of IEngine used to DI</param>
        public SearchParameterBuilder(IEngine engine)
        {
            this.Engine = engine;
        }

        /// <summary>
        /// Create a facet field parameter
        /// </summary>
        /// <param name="expression">Expression used to find the property name</param>
        /// <param name="sortType">Sort type of the result of the facet</param>
        /// <param name="limit">Limit of itens in facet's result</param>
        /// <param name="excludes">List of tags to exclude in facet calculation</param>
        public IFacetFieldParameter<TDocument> FacetField(Expression<Func<TDocument, object>> expression, FacetSortType? sortType = null, int? limit = null, params string[] excludes)
        {
            return this
                .Engine
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
        public IFacetQueryParameter<TDocument> FacetQuery(string aliasName, ISearchParameterValue<TDocument> query, FacetSortType? sortType = null, params string[] excludes)
        {
            return this
                .Engine
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
        public IFacetRangeParameter<TDocument> FacetRange(string aliasName, Expression<Func<TDocument, object>> expression, string gap = null, string start = null, string end = null, FacetSortType? sortType = null)
        {
            return this
                .Engine
                .GetService<IFacetRangeParameter<TDocument>>()
                .Configure(aliasName, expression, gap, start, end, sortType);
        }

        /// <summary>
        /// Create a fields parameter
        /// </summary>
        /// <param name="expressions">Expression used to find the property name</param>
        public IFieldsParameter<TDocument> Fields(params Expression<Func<TDocument, object>>[] expressions)
        {
            return this
                .Engine
                .GetService<IFieldsParameter<TDocument>>()
                .Configure(expressions);
        }

        /// <summary>
        /// Create a filter parameter
        /// </summary>
        /// <param name="expression">Expression used to find the property name</param>
        /// <param name="value">Value of the filter</param>
        /// <param name="tagName">Tag name to use in facet excluding list</param>
        public IFilterParameter<TDocument> Filter(Expression<Func<TDocument, object>> expression, string value, string tagName = null)
        {
            var paramaterValue = new Single<TDocument>(expression, value);

            return this
                .Engine
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
        public IFilterParameter<TDocument> Filter<TValue>(Expression<Func<TDocument, object>> expression, TValue? from = null, TValue? to = null, string tagName = null)
                        where TValue : struct
        {
            var paramaterValue = new Range<TDocument, TValue>(expression, from, to);

            return this
                .Engine
                .GetService<IFilterParameter<TDocument>>()
                .Configure(paramaterValue, tagName);
        }

        /// <summary>
        /// Create a limit parameter
        /// </summary>
        /// <param name="value">Value of limit</param>
        public ILimitParameter<TDocument> Limit(int value)
        {
            return this
                .Engine
                .GetService<ILimitParameter<TDocument>>()
                .Configure(value);
        }

        /// <summary>
        /// Create a offset parameter
        /// </summary>
        /// <param name="value">Value of limit</param>
        public IOffsetParameter<TDocument> Offset(int value)
        {
            return this
                .Engine
                .GetService<IOffsetParameter<TDocument>>()
                .Configure(value);
        }

        /// <summary>
        /// Create a query parameter
        /// </summary>
        /// <param name="value">Parameter to include in the query</param>
        public IQueryParameter<TDocument> Query(ISearchParameterValue<TDocument> value)
        {
            return this
                .Engine
                .GetService<IQueryParameter<TDocument>>()
                .Configure(value);
        }

        /// <summary>
        /// Create a query parameter
        /// </summary>
        /// <param name="value">Parameter to include in the query</param>
        public IQueryParameter<TDocument> Query(string value)
        {
            var paramaterValue = new Any<TDocument>(value);

            return this
                .Engine
                .GetService<IQueryParameter<TDocument>>()
                .Configure(paramaterValue);
        }

        /// <summary>
        /// Create a query parameter
        /// </summary>
        /// <param name="expression">Expression used to find the property name</param>
        /// <param name="value">Value of the query</param>
        public IQueryParameter<TDocument> Query(Expression<Func<TDocument, object>> expression, string value)
        {
            var paramaterValue = new Single<TDocument>(expression, value);

            return this
                .Engine
                .GetService<IQueryParameter<TDocument>>()
                .Configure(paramaterValue);
        }

        /// <summary>
        /// Create a sort parameter
        /// </summary>
        /// <param name="expression">Expression used to find the property name</param>
        /// <param name="ascendent">True to ascendent order, otherwise false</param>
        public ISortParameter<TDocument> Sort(Expression<Func<TDocument, object>> expression, bool ascendent)
        {
            return this
                .Engine
                .GetService<ISortParameter<TDocument>>()
                .Configure(expression, ascendent);
        }

        /// <summary>
        /// Create a sort parameter
        /// </summary>
        /// <param name="ascendent">True to ascendent order, otherwise false</param>
        /// <param name="expressions">Expression used to find the property name</param>
        public IEnumerable<ISortParameter<TDocument>> Sort(bool ascendent, params Expression<Func<TDocument, object>>[] expressions)
        {
            foreach (var expression in expressions)
            {
                yield return this
                    .Engine
                    .GetService<ISortParameter<TDocument>>()
                    .Configure(expression, ascendent);
            }
        }

        /// <summary>
        /// Create a random sort parameter
        /// </summary>
        /// <param name="ascendent">True to ascendent order, otherwise false</param>
        /// <param name="expressions">Expression used to find the property name</param>
        public IRandomSortParameter<TDocument> RandomSort(bool ascendent)
        {
            return this
                .Engine
                .GetService<IRandomSortParameter<TDocument>>()
                .Configure(ascendent);
        }

        /// <summary>
        /// Create a facet limit parameter
        /// </summary>
        /// <param name="value">Value of limit</param>
        public IFacetLimitParameter<TDocument> FacetLimit(int value)
        {
            return this
                .Engine
                .GetService<IFacetLimitParameter<TDocument>>()
                .Configure(value);
        }

        /// <summary>
        /// Create a minimum should match parameter
        /// </summary>
        /// <param name="expression">Expression used to make the mm parameter</param>
        public IMinimumShouldMatchParameter<TDocument> MinimumShouldMatch(string expression)
        {
            return this
                .Engine
                .GetService<IMinimumShouldMatchParameter<TDocument>>()
                .Configure(expression);
        }

        /// <summary>
        /// Create a query field parameter
        /// </summary>
        /// <param name="expression">Expression used to make the mm parameter</param>
        public IQueryFieldParameter<TDocument> QueryField(string expression)
        {
            return this
                .Engine
                .GetService<IQueryFieldParameter<TDocument>>()
                .Configure(expression);
        }

        /// <summary>
        /// Create a query field parameter
        /// </summary>
        /// <param name="expression">Expression used to find the property name</param>
        /// <param name="functionType">Function used in the spatial filter</param>
        /// <param name="centerPoint">Center point to spatial filter</param>
        /// <param name="distance">Distance from the center point</param>
        public ISpatialFilterParameter<TDocument> SpatialFilter(Expression<Func<TDocument, object>> expression, SolrSpatialFunctionType functionType, GeoCoordinate centerPoint, decimal distance)
        {
            return this
                .Engine
                .GetService<ISpatialFilterParameter<TDocument>>()
                .Configure(expression, functionType, centerPoint, distance);
        }

        /// <summary>
        /// Create a any parameter
        /// </summary>
        /// <param name="name">Name of the parameter</param>
        /// <param name="value">Value of the parameter</param>
        public IAnyParameter<TDocument> Any(string name, string value)
        {
            return this
                .Engine
                .GetService<IAnyParameter<TDocument>>()
                .Configure(name, value);
        }

        /// <summary>
        /// Create a boost parameter
        /// </summary>
        /// <param name="query">Query used to make boost</param>
        /// <param name="boostFunctionType">Boost type used in calculation. Default is BoostFunctionType.Boost</param>
        public IBoostParameter<TDocument> Boost(ISearchParameterValue<TDocument> query, BoostFunctionType? boostFunctionType = null)
        {
            return this
                .Engine
                .GetService<IBoostParameter<TDocument>>()
                .Configure(query, boostFunctionType ?? BoostFunctionType.Boost);
        }

        /// <summary>
        /// Instance of IEngine used to DI
        /// </summary>
        /// <returns>Instance of IEngine</returns>
        public IEngine Engine { get; private set; }
    }
}
