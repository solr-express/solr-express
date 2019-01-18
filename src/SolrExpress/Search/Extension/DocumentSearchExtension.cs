using SolrExpress.Search.Parameter;
using SolrExpress.Search.Parameter.Extension;
using SolrExpress.Search.Query;
using SolrExpress.Search.Result;
using SolrExpress.Utility;
using System;
using System.Linq.Expressions;

namespace SolrExpress.Search.Extension
{
    public static class DocumentSearchExtension
    {
        /// <summary>
        /// Create a not mapped parameter
        /// </summary>
        /// <param name="documentSearch">Document search engine</param>
        /// <param name="name">Name of parameter</param>
        /// <param name="value">Value of parameter</param>
        /// <returns>Document search engine</returns>
        public static DocumentSearch<TDocument> Any<TDocument>(this DocumentSearch<TDocument> documentSearch, string name, string value)
            where TDocument : Document
        {
            Checker.IsNullOrWhiteSpace(name);
            Checker.IsNullOrWhiteSpace(value);

            var parameter = documentSearch.ServiceProvider.GetService<IAnyParameter>();
            parameter.Name(name);
            parameter.Value(value);

            documentSearch.Add(parameter);

            return documentSearch;
        }

        /// <summary>
        /// Create a not mapped parameter
        /// </summary>
        /// <param name="documentSearch">Document search engine</param>
        /// <param name="conditional">Conditional to add parameter</param>
        /// <param name="name">Name of parameter</param>
        /// <param name="value">Value of parameter</param>
        /// <returns>Document search engine</returns>
        public static DocumentSearch<TDocument> AnyIf<TDocument>(this DocumentSearch<TDocument> documentSearch, Func<bool> conditional, string name, string value)
            where TDocument : Document
        {
            Checker.IsNull(conditional);

            if (conditional.Invoke())
            {
                documentSearch.Any(name, value);
            }

            return documentSearch;
        }

        /// <summary>
        /// Create a boost parameter
        /// </summary>
        /// <param name="documentSearch">Document search engine</param>
        /// <param name="query">Query used to make boost</param>
        /// <param name="instance">Instance of boost ready to configure</param>
        /// <returns>Document search engine</returns>
        public static DocumentSearch<TDocument> Boost<TDocument>(this DocumentSearch<TDocument> documentSearch, Action<SearchQuery<TDocument>> query, Action<IBoostParameter<TDocument>> instance = null)
            where TDocument : Document
        {
            Checker.IsNull(query);

            var parameter = documentSearch.ServiceProvider.GetService<IBoostParameter<TDocument>>();
            var search = documentSearch.ServiceProvider.GetService<SearchQuery<TDocument>>();
            parameter.Query(search);

            query.Invoke(search);
            instance?.Invoke(parameter);

            documentSearch.Add(parameter);

            return documentSearch;
        }

        /// <summary>
        /// Create a boost parameter
        /// </summary>
        /// <param name="documentSearch">Document search engine</param>
        /// <param name="conditional">Conditional to add parameter</param>
        /// <param name="query">Query used to make boost</param>
        /// <param name="instance">Instance of boost ready to configure</param>
        /// <returns>Document search engine</returns>
        public static DocumentSearch<TDocument> BoostIf<TDocument>(this DocumentSearch<TDocument> documentSearch, Func<bool> conditional, Action<SearchQuery<TDocument>> query, Action<IBoostParameter<TDocument>> instance = null)
            where TDocument : Document
        {
            Checker.IsNull(conditional);

            if (conditional.Invoke())
            {
                documentSearch.Boost(query, instance);
            }

            return documentSearch;
        }

        /// <summary>
        /// Create a facet field parameter
        /// </summary>
        /// <param name="documentSearch">Document search engine</param>
        /// <param name="fieldExpression">Expression used to find field name</param>
        /// <param name="instance">Instance of facet ready to configure</param>
        /// <returns>Document search engine</returns>
        public static DocumentSearch<TDocument> FacetField<TDocument>(this DocumentSearch<TDocument> documentSearch, Expression<Func<TDocument, object>> fieldExpression, Action<IFacetFieldParameter<TDocument>> instance = null)
            where TDocument : Document
        {
            Checker.IsNull(fieldExpression);

            var parameter = documentSearch.ServiceProvider.GetService<IFacetFieldParameter<TDocument>>();
            parameter.FieldExpression(fieldExpression);

            instance?.Invoke(parameter);

            documentSearch.Add(parameter);

            // ReSharper disable once InvertIf
            if (!documentSearch.Contains<IFacetsResult<TDocument>>())
            {
                var facetsResult = documentSearch.ServiceProvider.GetService<IFacetsResult<TDocument>>();
                documentSearch.Add(facetsResult);
            }

            return documentSearch;
        }

        /// <summary>
        /// Create a facet field parameter
        /// </summary>
        /// <param name="documentSearch">Document search engine</param>
        /// <param name="conditional">Conditional to add parameter</param>
        /// <param name="fieldExpression">Expression used to find field name</param>
        /// <param name="instance">Instance of facet ready to configure</param>
        /// <returns>Document search engine</returns>
        public static DocumentSearch<TDocument> FacetFieldIf<TDocument>(this DocumentSearch<TDocument> documentSearch, Func<bool> conditional, Expression<Func<TDocument, object>> fieldExpression, Action<IFacetFieldParameter<TDocument>> instance = null)
            where TDocument : Document
        {
            Checker.IsNull(conditional);

            if (conditional.Invoke())
            {
                documentSearch.FacetField(fieldExpression, instance);
            }

            return documentSearch;
        }

        /// <summary>
        /// Create a facet query parameter
        /// </summary>
        /// <param name="documentSearch">Document search engine</param>
        /// <param name="aliasName">Name of alias added in query</param>
        /// <param name="query">Query used to make facet</param>
        /// <param name="instance">Instance of facet ready to configure</param>
        /// <returns>Document search engine</returns>
        public static DocumentSearch<TDocument> FacetQuery<TDocument>(this DocumentSearch<TDocument> documentSearch, string aliasName, Action<SearchQuery<TDocument>> query, Action<IFacetQueryParameter<TDocument>> instance = null)
            where TDocument : Document
        {
            Checker.IsNull(query);

            var parameter = documentSearch.ServiceProvider.GetService<IFacetQueryParameter<TDocument>>();
            parameter.AliasName(aliasName);
            var search = documentSearch.ServiceProvider.GetService<SearchQuery<TDocument>>();
            query.Invoke(search);
            parameter.Query(search);

            instance?.Invoke(parameter);

            documentSearch.Add(parameter);

            // ReSharper disable once InvertIf
            if (!documentSearch.Contains<IFacetsResult<TDocument>>())
            {
                var facetsResult = documentSearch.ServiceProvider.GetService<IFacetsResult<TDocument>>();
                documentSearch.Add(facetsResult);
            }

            return documentSearch;
        }

        /// <summary>
        /// Create a facet query parameter
        /// </summary>
        /// <param name="documentSearch">Document search engine</param>
        /// <param name="conditional">Conditional to add parameter</param>
        /// <param name="aliasName">Name of alias added in query</param>
        /// <param name="query">Query used to make facet</param>
        /// <param name="instance">Instance of facet ready to configure</param>
        /// <returns>Document search engine</returns>
        public static DocumentSearch<TDocument> FacetQueryIf<TDocument>(this DocumentSearch<TDocument> documentSearch, Func<bool> conditional, string aliasName, Action<SearchQuery<TDocument>> query, Action<IFacetQueryParameter<TDocument>> instance = null)
            where TDocument : Document
        {
            Checker.IsNull(conditional);

            if (conditional.Invoke())
            {
                documentSearch.FacetQuery(aliasName, query, instance);
            }

            return documentSearch;
        }

        /// <summary>
        /// Create a facet range parameter
        /// </summary>
        /// <param name="documentSearch">Document search engine</param>
        /// <param name="aliasName">Name of alias added in query</param>
        /// <param name="fieldExpression">Expression used to find field name</param>
        /// <param name="gap">Size of each range bucket to make facet</param>
        /// <param name="start">Lower bound to make facet</param>
        /// <param name="end">Upper bound to make facet</param>
        /// <param name="instance">Instance of parameter ready to configure</param>
        /// <returns>Document search engine</returns>
        public static DocumentSearch<TDocument> FacetRange<TDocument>(this DocumentSearch<TDocument> documentSearch, string aliasName, Expression<Func<TDocument, object>> fieldExpression, string gap, string start, string end, Action<IFacetRangeParameter<TDocument>> instance = null)
            where TDocument : Document
        {
            var parameter = documentSearch.ServiceProvider.GetService<IFacetRangeParameter<TDocument>>();
            parameter.AliasName(aliasName);
            parameter.FieldExpression(fieldExpression);
            parameter.Gap(gap);
            parameter.Start(start);
            parameter.End(end);

            instance?.Invoke(parameter);

            documentSearch.Add(parameter);

            // ReSharper disable once InvertIf
            if (!documentSearch.Contains<IFacetsResult<TDocument>>())
            {
                var facetsResult = documentSearch.ServiceProvider.GetService<IFacetsResult<TDocument>>();
                documentSearch.Add(facetsResult);
            }

            return documentSearch;
        }

        /// <summary>
        /// Create a facet range parameter
        /// </summary>
        /// <param name="documentSearch">Document search engine</param>
        /// <param name="conditional">Conditional to add parameter</param>
        /// <param name="aliasName">Name of alias added in query</param>
        /// <param name="fieldExpression">Expression used to find field name</param>
        /// <param name="gap">Size of each range bucket to make facet</param>
        /// <param name="start">Lower bound to make facet</param>
        /// <param name="end">Upper bound to make facet</param>
        /// <param name="instance">Instance of parameter ready to configure</param>
        /// <returns>Document search engine</returns>
        public static DocumentSearch<TDocument> FacetRangeIf<TDocument>(this DocumentSearch<TDocument> documentSearch, Func<bool> conditional, string aliasName, Expression<Func<TDocument, object>> fieldExpression, string gap, string start, string end, Action<IFacetRangeParameter<TDocument>> instance = null)
            where TDocument : Document
        {
            Checker.IsNull(conditional);

            if (conditional.Invoke())
            {
                documentSearch.FacetRange(aliasName, fieldExpression, gap, start, end, instance);
            }

            return documentSearch;
        }

        /// <summary>
        /// Create a facet limit parameter
        /// </summary>
        /// <param name="documentSearch">Document search engine</param>
        /// <param name="value">Value of limit</param>
        /// <returns>Document search engine</returns>
        public static DocumentSearch<TDocument> FacetLimit<TDocument>(this DocumentSearch<TDocument> documentSearch, int value)
            where TDocument : Document
        {
            var parameter = documentSearch.ServiceProvider.GetService<IFacetLimitParameter<TDocument>>();
            parameter.Value(value);

            documentSearch.Add(parameter);

            return documentSearch;
        }

        /// <summary>
        /// Create a facet limit parameter
        /// </summary>
        /// <param name="documentSearch">Document search engine</param>
        /// <param name="conditional">Conditional to add parameter</param>
        /// <param name="value">Value of limit</param>
        /// <returns>Document search engine</returns>
        public static DocumentSearch<TDocument> FacetLimitIf<TDocument>(this DocumentSearch<TDocument> documentSearch, Func<bool> conditional, int value)
            where TDocument : Document
        {
            Checker.IsNull(conditional);

            if (conditional.Invoke())
            {
                documentSearch.FacetLimit(value);
            }

            return documentSearch;
        }

        /// <summary>
        /// Create a facet range parameter
        /// </summary>
        /// <param name="documentSearch">Document search engine</param>
        /// <param name="aliasName">Name of alias added in query</param>
        /// <param name="fieldExpression">Expression used to find field name</param>
        /// <param name="centerPoint">Center point to spatial filter</param>
        /// <param name="distance">Distance from center point</param>
        /// <param name="instance">Instance of parameter ready to configure</param>
        /// <returns>Document search engine</returns>
        public static DocumentSearch<TDocument> FacetSpatial<TDocument>(this DocumentSearch<TDocument> documentSearch, string aliasName, Expression<Func<TDocument, object>> fieldExpression, GeoCoordinate centerPoint, decimal distance, Action<IFacetSpatialParameter<TDocument>> instance = null)
            where TDocument : Document
        {
            var parameter = documentSearch.ServiceProvider.GetService<IFacetSpatialParameter<TDocument>>();
            parameter.AliasName(aliasName);
            parameter.FieldExpression(fieldExpression);
            parameter.CenterPoint(centerPoint);
            parameter.Distance(distance);

            instance?.Invoke(parameter);

            documentSearch.Add(parameter);

            // ReSharper disable once InvertIf
            if (!documentSearch.Contains<IFacetsResult<TDocument>>())
            {
                var facetsResult = documentSearch.ServiceProvider.GetService<IFacetsResult<TDocument>>();
                documentSearch.Add(facetsResult);
            }

            return documentSearch;
        }

        /// <summary>
        /// Create a facet range parameter
        /// </summary>
        /// <param name="documentSearch">Document search engine</param>
        /// <param name="conditional">Conditional to add parameter</param>
        /// <param name="aliasName">Name of alias added in query</param>
        /// <param name="fieldExpression">Expression used to find field name</param>
        /// <param name="centerPoint">Center point to spatial filter</param>
        /// <param name="distance">Distance from center point</param>
        /// <param name="instance">Instance of parameter ready to configure</param>
        /// <returns>Document search engine</returns>
        public static DocumentSearch<TDocument> FacetSpatialIf<TDocument>(this DocumentSearch<TDocument> documentSearch, Func<bool> conditional, string aliasName, Expression<Func<TDocument, object>> fieldExpression, GeoCoordinate centerPoint, decimal distance, Action<IFacetSpatialParameter<TDocument>> instance = null)
            where TDocument : Document
        {
            Checker.IsNull(conditional);

            if (conditional.Invoke())
            {
                documentSearch.FacetSpatial(aliasName, fieldExpression, centerPoint, distance, instance);
            }

            return documentSearch;
        }

        /// <summary>
        /// Create a fields parameter
        /// </summary>
        /// <param name="documentSearch">Document search engine</param>
        /// <param name="fieldExpressions">Expressions used to find fields name</param>
        /// <returns>Document search engine</returns>
        public static DocumentSearch<TDocument> Fields<TDocument>(this DocumentSearch<TDocument> documentSearch, params Expression<Func<TDocument, object>>[] fieldExpressions)
            where TDocument : Document
        {
            var parameter = documentSearch.ServiceProvider.GetService<IFieldsParameter<TDocument>>();
            parameter.FieldExpressions(fieldExpressions);

            documentSearch.Add(parameter);

            return documentSearch;
        }

        /// <summary>
        /// Create a fields parameter
        /// </summary>
        /// <param name="documentSearch">Document search engine</param>
        /// <param name="conditional">Conditional to add parameter</param>
        /// <param name="fieldExpressions">Expressions used to find fields name</param>
        /// <returns>Document search engine</returns>
        public static DocumentSearch<TDocument> FieldsIf<TDocument>(this DocumentSearch<TDocument> documentSearch, Func<bool> conditional, params Expression<Func<TDocument, object>>[] fieldExpressions)
            where TDocument : Document
        {
            Checker.IsNull(conditional);

            if (conditional.Invoke())
            {
                documentSearch.Fields(fieldExpressions);
            }

            return documentSearch;
        }

        /// <summary>
        /// Create a filter parameter in commom case (field equals value, field with value in collection)
        /// </summary>
        /// <param name="documentSearch">Document search engine</param>
        /// <param name="fieldExpression">Expression used to find field name</param>
        /// <param name="values">Values to find</param>
        /// <returns>Document search engine</returns>
        public static DocumentSearch<TDocument> Filter<TDocument, TValue>(this DocumentSearch<TDocument> documentSearch, Expression<Func<TDocument, object>> fieldExpression, params TValue[] values)
            where TDocument : Document
        {
            var parameter = documentSearch.ServiceProvider.GetService<IFilterParameter<TDocument>>();
            var search = documentSearch.ServiceProvider.GetService<SearchQuery<TDocument>>();
            search.Field(fieldExpression);
            search.Any(values);

            parameter.Query(search);

            documentSearch.Add(parameter);

            return documentSearch;
        }

        /// <summary>
        /// Create a filter parameter in commom case (field equals value, field with value in collection)
        /// </summary>
        /// <param name="documentSearch">Document search engine</param>
        /// <param name="conditional">Conditional to add parameter</param>
        /// <param name="fieldExpression">Expression used to find field name</param>
        /// <param name="values">Values to find</param>
        /// <returns>Document search engine</returns>
        public static DocumentSearch<TDocument> FilterIf<TDocument, TValue>(this DocumentSearch<TDocument> documentSearch, Func<bool> conditional, Expression<Func<TDocument, object>> fieldExpression, params TValue[] values)
            where TDocument : Document
        {
            Checker.IsNull(conditional);

            if (conditional.Invoke())
            {
                documentSearch.Filter(fieldExpression, values);
            }

            return documentSearch;
        }

        /// <summary>
        /// Create a filter parameter
        /// </summary>
        /// <param name="documentSearch">Document search engine</param>
        /// <param name="fieldExpression">Expression used to find field name</param>
        /// <param name="query">Query used to make filter</param>
        /// <param name="instance">Instance of parameter ready to configure</param>
        /// <returns>Document search engine</returns>
        public static DocumentSearch<TDocument> Filter<TDocument>(this DocumentSearch<TDocument> documentSearch, Expression<Func<TDocument, object>> fieldExpression, Action<SearchQuery<TDocument>> query, Action<IFilterParameter<TDocument>> instance = null)
            where TDocument : Document
        {
            Checker.IsNull(query);

            var parameter = documentSearch.ServiceProvider.GetService<IFilterParameter<TDocument>>();
            var search = documentSearch.ServiceProvider.GetService<SearchQuery<TDocument>>();

            search.Field(fieldExpression);
            parameter.Query(search);

            query.Invoke(search);
            instance?.Invoke(parameter);

            documentSearch.Add(parameter);

            return documentSearch;
        }

        /// <summary>
        /// Create a filter parameter
        /// </summary>
        /// <param name="documentSearch">Document search engine</param>
        /// <param name="conditional">Conditional to add parameter</param>
        /// <param name="fieldExpression">Expression used to find field name</param>
        /// <param name="query">Query used to make filter</param>
        /// <param name="instance">Instance of parameter ready to configure</param>
        /// <returns>Document search engine</returns>
        public static DocumentSearch<TDocument> FilterIf<TDocument>(this DocumentSearch<TDocument> documentSearch, Func<bool> conditional, Expression<Func<TDocument, object>> fieldExpression, Action<SearchQuery<TDocument>> query, Action<IFilterParameter<TDocument>> instance = null)
            where TDocument : Document
        {
            Checker.IsNull(conditional);

            if (conditional.Invoke())
            {
                documentSearch.Filter(fieldExpression, query, instance);
            }

            return documentSearch;
        }

        /// <summary>
        /// Create a limit parameter
        /// </summary>
        /// <param name="documentSearch">Document search engine</param>
        /// <param name="value">Value of limit</param>
        /// <returns>Document search engine</returns>
        public static DocumentSearch<TDocument> Limit<TDocument>(this DocumentSearch<TDocument> documentSearch, long value)
            where TDocument : Document
        {
            var parameter = documentSearch.ServiceProvider.GetService<ILimitParameter<TDocument>>();
            parameter.Value(value);

            documentSearch.Add(parameter);

            return documentSearch;
        }

        /// <summary>
        /// Create a limit parameter
        /// </summary>
        /// <param name="documentSearch">Document search engine</param>
        /// <param name="conditional">Conditional to add parameter</param>
        /// <param name="value">Value of limit</param>
        /// <returns>Document search engine</returns>
        public static DocumentSearch<TDocument> LimitIf<TDocument>(this DocumentSearch<TDocument> documentSearch, Func<bool> conditional, long value)
            where TDocument : Document
        {
            Checker.IsNull(conditional);

            if (conditional.Invoke())
            {
                documentSearch.Limit(value);
            }

            return documentSearch;
        }

        /// <summary>
        /// Create a minimum should match
        /// </summary>
        /// <param name="documentSearch">Document search engine</param>
        /// <param name="value">Expression used to make mm parameter</param>
        /// <returns>Document search engine</returns>
        public static DocumentSearch<TDocument> MinimumShouldMatch<TDocument>(this DocumentSearch<TDocument> documentSearch, string value)
            where TDocument : Document
        {
            var parameter = documentSearch.ServiceProvider.GetService<IMinimumShouldMatchParameter<TDocument>>();
            parameter.Value(value);

            documentSearch.Add(parameter);

            return documentSearch;
        }

        /// <summary>
        /// Create a minimum should match
        /// </summary>
        /// <param name="documentSearch">Document search engine</param>
        /// <param name="conditional">Conditional to add parameter</param>
        /// <param name="value">Expression used to make mm parameter</param>
        /// <returns>Document search engine</returns>
        public static DocumentSearch<TDocument> MinimumShouldMatchIf<TDocument>(this DocumentSearch<TDocument> documentSearch, Func<bool> conditional, string value)
            where TDocument : Document
        {
            Checker.IsNull(conditional);

            if (conditional.Invoke())
            {
                documentSearch.MinimumShouldMatch(value);
            }

            return documentSearch;
        }

        /// <summary>
        /// Create a offset parameter
        /// </summary>
        /// <param name="documentSearch">Document search engine</param>
        /// <param name="value">Value of offset</param>
        /// <returns>Document search engine</returns>
        public static DocumentSearch<TDocument> Offset<TDocument>(this DocumentSearch<TDocument> documentSearch, long value)
            where TDocument : Document
        {
            var parameter = documentSearch.ServiceProvider.GetService<IOffsetParameter<TDocument>>();
            parameter.Value(value);

            documentSearch.Add(parameter);

            return documentSearch;
        }

        /// <summary>
        /// Create a offset parameter
        /// </summary>
        /// <param name="documentSearch">Document search engine</param>
        /// <param name="conditional">Conditional to add parameter</param>
        /// <param name="value">Value of offset</param>
        /// <returns>Document search engine</returns>
        public static DocumentSearch<TDocument> OffsetIf<TDocument>(this DocumentSearch<TDocument> documentSearch, Func<bool> conditional, long value)
            where TDocument : Document
        {
            Checker.IsNull(conditional);

            if (conditional.Invoke())
            {
                documentSearch.Offset(value);
            }

            return documentSearch;
        }

        /// <summary>
        /// Create a limit and a offset parameters
        /// </summary>
        /// <param name="documentSearch">Document search engine</param>
        /// <param name="itemsPerPage">Quantity of items in one page</param>
        /// <param name="currentPage">Current page</param>
        /// <returns>Document search engine</returns>
        public static DocumentSearch<TDocument> Page<TDocument>(this DocumentSearch<TDocument> documentSearch, long itemsPerPage, long currentPage)
            where TDocument : Document
        {
            documentSearch.Limit(itemsPerPage);
            documentSearch.Offset((currentPage - 1) * itemsPerPage);

            return documentSearch;
        }

        /// <summary>
        /// Create a limit and a offset parameters
        /// </summary>
        /// <param name="documentSearch">Document search engine</param>
        /// <param name="conditional">Conditional to add parameter</param>
        /// <param name="itemsPerPage">Quantity of items in one page</param>
        /// <param name="currentPage">Current page</param>
        /// <returns>Document search engine</returns>
        public static DocumentSearch<TDocument> PageIf<TDocument>(this DocumentSearch<TDocument> documentSearch, Func<bool> conditional, long itemsPerPage, long currentPage)
            where TDocument : Document
        {
            Checker.IsNull(conditional);

            if (conditional.Invoke())
            {
                documentSearch.Page(itemsPerPage, currentPage);
            }

            return documentSearch;
        }

        /// <summary>
        /// Create a query field parameter
        /// </summary>
        /// <param name="documentSearch">Document search engine</param>
        /// <param name="expression">Query used to make query field</param>
        /// <returns>Document search engine</returns>
        public static DocumentSearch<TDocument> QueryField<TDocument>(this DocumentSearch<TDocument> documentSearch, string expression)
            where TDocument : Document
        {
            var parameter = documentSearch.ServiceProvider.GetService<IQueryFieldParameter<TDocument>>();
            parameter.Expression(expression);

            documentSearch.Add(parameter);

            return documentSearch;
        }

        /// <summary>
        /// Create a query field parameter
        /// </summary>
        /// <param name="documentSearch">Document search engine</param>
        /// <param name="conditional">Conditional to add parameter</param>
        /// <param name="expression">Query used to make query field</param>
        /// <returns>Document search engine</returns>
        public static DocumentSearch<TDocument> QueryFieldIf<TDocument>(this DocumentSearch<TDocument> documentSearch, Func<bool> conditional, string expression)
            where TDocument : Document
        {
            Checker.IsNull(conditional);

            if (conditional.Invoke())
            {
                documentSearch.QueryField(expression);
            }

            return documentSearch;
        }

        /// <summary>
        /// Create a query parameter in commom case (field equals value, field with value in collection)
        /// </summary>
        /// <param name="documentSearch">Document search engine</param>
        /// <param name="fieldExpression">Expression used to find field name</param>
        /// <param name="values">Values to find</param>
        /// <returns>Document search engine</returns>
        public static DocumentSearch<TDocument> Query<TDocument, TValue>(this DocumentSearch<TDocument> documentSearch, Expression<Func<TDocument, object>> fieldExpression, params TValue[] values)
            where TDocument : Document
        {
            var parameter = documentSearch.ServiceProvider.GetService<IQueryParameter<TDocument>>();
            var search = documentSearch.ServiceProvider.GetService<SearchQuery<TDocument>>();
            search.AddField(fieldExpression);
            search.Any(values);

            parameter.Value(search);

            documentSearch.Add(parameter);

            return documentSearch;
        }

        /// <summary>
        /// Create a query parameter in commom case (field equals value, field with value in collection)
        /// </summary>
        /// <param name="documentSearch">Document search engine</param>
        /// <param name="conditional">Conditional to add parameter</param>
        /// <param name="fieldExpression">Expression used to find field name</param>
        /// <param name="values">Values to find</param>
        /// <returns>Document search engine</returns>
        public static DocumentSearch<TDocument> QueryIf<TDocument, TValue>(this DocumentSearch<TDocument> documentSearch, Func<bool> conditional, Expression<Func<TDocument, object>> fieldExpression, params TValue[] values)
            where TDocument : Document
        {
            Checker.IsNull(conditional);

            if (conditional.Invoke())
            {
                documentSearch.Query(fieldExpression, values);
            }

            return documentSearch;
        }

        /// <summary>
        /// Create a query parameter in commom case (value to be processed using QueryField)
        /// </summary>
        /// <param name="documentSearch">Document search engine</param>
        /// <param name="values">Values to find</param>
        /// <returns>Document search engine</returns>
        public static DocumentSearch<TDocument> Query<TDocument, TValue>(this DocumentSearch<TDocument> documentSearch, params TValue[] values)
            where TDocument : Document
        {
            var parameter = documentSearch.ServiceProvider.GetService<IQueryParameter<TDocument>>();
            var search = documentSearch.ServiceProvider.GetService<SearchQuery<TDocument>>();
            search.Any(values);

            parameter.Value(search);

            documentSearch.Add(parameter);

            return documentSearch;
        }

        /// <summary>
        /// Create a query parameter in commom case (value to be processed using QueryField)
        /// </summary>
        /// <param name="documentSearch">Document search engine</param>
        /// <param name="conditional">Conditional to add parameter</param>
        /// <param name="values">Values to find</param>
        /// <returns>Document search engine</returns>
        public static DocumentSearch<TDocument> QueryIf<TDocument, TValue>(this DocumentSearch<TDocument> documentSearch, Func<bool> conditional, params TValue[] values)
            where TDocument : Document
        {
            Checker.IsNull(conditional);

            if (conditional.Invoke())
            {
                documentSearch.Query(values);
            }

            return documentSearch;
        }

        /// <summary>
        /// Create a query parameter
        /// </summary>
        /// <param name="documentSearch">Document search engine</param>
        /// <param name="fieldExpression">Expression used to find field name</param>
        /// <param name="query">Query used to make filter</param>
        /// <param name="instance">Instance of parameter ready to configure</param>
        /// <returns>Document search engine</returns>
        public static DocumentSearch<TDocument> Query<TDocument>(this DocumentSearch<TDocument> documentSearch, Expression<Func<TDocument, object>> fieldExpression, Action<SearchQuery<TDocument>> query, Action<IQueryParameter<TDocument>> instance = null)
            where TDocument : Document
        {
            Checker.IsNull(query);

            var parameter = documentSearch.ServiceProvider.GetService<IQueryParameter<TDocument>>();
            var search = documentSearch.ServiceProvider.GetService<SearchQuery<TDocument>>();

            search.AddField(fieldExpression);
            parameter.Value(search);

            query.Invoke(search);
            instance?.Invoke(parameter);

            documentSearch.Add(parameter);

            return documentSearch;
        }

        /// <summary>
        /// Create a query parameter
        /// </summary>
        /// <param name="documentSearch">Document search engine</param>
        /// <param name="conditional">Conditional to add parameter</param>
        /// <param name="fieldExpression">Expression used to find field name</param>
        /// <param name="query">Query used to make filter</param>
        /// <param name="instance">Instance of parameter ready to configure</param>
        /// <returns>Document search engine</returns>
        public static DocumentSearch<TDocument> QueryIf<TDocument>(this DocumentSearch<TDocument> documentSearch, Func<bool> conditional, Expression<Func<TDocument, object>> fieldExpression, Action<SearchQuery<TDocument>> query, Action<IQueryParameter<TDocument>> instance = null)
            where TDocument : Document
        {
            Checker.IsNull(conditional);

            if (conditional.Invoke())
            {
                documentSearch.Query(fieldExpression, query, instance);
            }

            return documentSearch;
        }

        /// <summary>
        /// Create a query parameter to get all (*:*)
        /// </summary>
        /// <param name="documentSearch">Document search engine</param>
        /// <returns>Document search engine</returns>
        public static DocumentSearch<TDocument> QueryAll<TDocument>(this DocumentSearch<TDocument> documentSearch)
            where TDocument : Document
        {
            var parameter = documentSearch.ServiceProvider.GetService<IQueryParameter<TDocument>>();
            var search = documentSearch.ServiceProvider.GetService<SearchQuery<TDocument>>();
            search.AddValue("*:*", false);

            parameter.Value(search);

            documentSearch.Add(parameter);

            return documentSearch;
        }

        /// <summary>
        /// Create a query parameter to get all (*:*)
        /// </summary>
        /// <param name="documentSearch">Document search engine</param>
        /// <param name="conditional">Conditional to add parameter</param>
        /// <returns>Document search engine</returns>
        public static DocumentSearch<TDocument> QueryAllIf<TDocument>(this DocumentSearch<TDocument> documentSearch, Func<bool> conditional)
            where TDocument : Document
        {
            Checker.IsNull(conditional);

            if (conditional.Invoke())
            {
                documentSearch.QueryAll();
            }

            return documentSearch;
        }

        /// <summary>
        /// Create a sort parameter
        /// </summary>
        /// <param name="documentSearch">Document search engine</param>
        /// <param name="fieldExpression">Expressions used to find field name</param>
        /// <param name="ascendent">True to ascendent order, otherwise false</param>
        /// <returns>Document search engine</returns>
        public static DocumentSearch<TDocument> Sort<TDocument>(this DocumentSearch<TDocument> documentSearch, Expression<Func<TDocument, object>> fieldExpression, bool ascendent = true)
            where TDocument : Document
        {
            var parameter = documentSearch.ServiceProvider.GetService<ISortParameter<TDocument>>();
            parameter.FieldExpression(fieldExpression);
            parameter.Ascendent(ascendent);

            documentSearch.Add(parameter);

            return documentSearch;
        }

        /// <summary>
        /// Create a sort parameter
        /// </summary>
        /// <param name="documentSearch">Document search engine</param>
        /// <param name="conditional">Conditional to add parameter</param>
        /// <param name="fieldExpression">Expressions used to find field name</param>
        /// <param name="ascendent">True to ascendent order, otherwise false</param>
        /// <returns>Document search engine</returns>
        public static DocumentSearch<TDocument> SortIf<TDocument>(this DocumentSearch<TDocument> documentSearch, Func<bool> conditional, Expression<Func<TDocument, object>> fieldExpression, bool ascendent = true)
            where TDocument : Document
        {
            Checker.IsNull(conditional);

            if (conditional.Invoke())
            {
                documentSearch.Sort(fieldExpression, ascendent);
            }

            return documentSearch;
        }

        /// <summary>
        /// Create a sort parameter configured to do a random sort
        /// </summary>
        /// <param name="documentSearch">Document search engine</param>
        /// <returns>Document search engine</returns>
        public static DocumentSearch<TDocument> SortRandomly<TDocument>(this DocumentSearch<TDocument> documentSearch)
            where TDocument : Document
        {
            var parameter = documentSearch.ServiceProvider.GetService<ISortRandomlyParameter<TDocument>>();

            documentSearch.Add(parameter);

            return documentSearch;
        }

        /// <summary>
        /// Create a sort parameter configured to do a random sort
        /// </summary>
        /// <param name="documentSearch">Document search engine</param>
        /// <param name="conditional">Conditional to add parameter</param>
        /// <returns>Document search engine</returns>
        public static DocumentSearch<TDocument> SortRandomlyIf<TDocument>(this DocumentSearch<TDocument> documentSearch, Func<bool> conditional)
            where TDocument : Document
        {
            Checker.IsNull(conditional);

            if (conditional.Invoke())
            {
                documentSearch.SortRandomly();
            }

            return documentSearch;
        }

        /// <summary>
        /// Create a spatial filter parameter
        /// </summary>
        /// <param name="documentSearch">Document search engine</param>
        /// <param name="fieldExpression">Expression used to find field name</param>
        /// <param name="functionType">Function used in spatial filter</param>
        /// <param name="centerPoint">Center point to spatial filter</param>
        /// <param name="distance">Distance from center point</param>
        /// <returns>Document search engine</returns>
        public static DocumentSearch<TDocument> SpatialFilter<TDocument>(this DocumentSearch<TDocument> documentSearch, Expression<Func<TDocument, object>> fieldExpression, GeoCoordinate centerPoint, decimal distance, SpatialFunctionType functionType = SpatialFunctionType.Bbox)
            where TDocument : Document
        {
            var parameter = documentSearch.ServiceProvider.GetService<ISpatialFilterParameter<TDocument>>();
            parameter.FieldExpression(fieldExpression);
            parameter.FunctionType(functionType);
            parameter.CenterPoint(centerPoint);
            parameter.Distance(distance);

            documentSearch.Add(parameter);

            return documentSearch;
        }

        /// <summary>
        /// Create a spatial filter parameter
        /// </summary>
        /// <param name="documentSearch">Document search engine</param>
        /// <param name="conditional">Conditional to add parameter</param>
        /// <param name="fieldExpression">Expression used to find field name</param>
        /// <param name="functionType">Function used in spatial filter</param>
        /// <param name="centerPoint">Center point to spatial filter</param>
        /// <param name="distance">Distance from center point</param>
        /// <returns>Document search engine</returns>
        public static DocumentSearch<TDocument> SpatialFilterIf<TDocument>(this DocumentSearch<TDocument> documentSearch, Func<bool> conditional, Expression<Func<TDocument, object>> fieldExpression, GeoCoordinate centerPoint, decimal distance, SpatialFunctionType functionType = SpatialFunctionType.Bbox)
            where TDocument : Document
        {
            Checker.IsNull(conditional);

            if (conditional.Invoke())
            {
                documentSearch.SpatialFilter(fieldExpression, centerPoint, distance, functionType);
            }

            return documentSearch;
        }

        /// <summary>
        /// Create a local parameter in commom case (a simple query)
        /// </summary>
        /// <param name="documentSearch">Document search engine</param>
        /// <param name="name">Name of parameter</param>
        /// <param name="fieldExpression">Expression used to find field name</param>
        /// <param name="query">Query used to make filter</param>
        /// <returns>Document search engine</returns>
        public static DocumentSearch<TDocument> LocalParameter<TDocument>(this DocumentSearch<TDocument> documentSearch, string name, Expression<Func<TDocument, object>> fieldExpression, Action<SearchQuery<TDocument>> query)
            where TDocument : Document
        {
            var parameter = documentSearch.ServiceProvider.GetService<ILocalParameter<TDocument>>();
            var search = documentSearch.ServiceProvider.GetService<SearchQuery<TDocument>>();
            search.Field(fieldExpression);

            query.Invoke(search);

            parameter.Name(name);
            parameter.Query(search);

            documentSearch.Add(parameter);

            return documentSearch;
        }

        /// <summary>
        /// Create a local parameter in commom case (a simple query)
        /// </summary>
        /// <param name="documentSearch">Document search engine</param>
        /// <param name="conditional">Conditional to add parameter</param>
        /// <param name="name">Name of parameter</param>
        /// <param name="fieldExpression">Expression used to find field name</param>
        /// <param name="query">Query used to make filter</param>
        /// <returns>Document search engine</returns>
        public static DocumentSearch<TDocument> LocalParameterIf<TDocument>(this DocumentSearch<TDocument> documentSearch, Func<bool> conditional, string name, Expression<Func<TDocument, object>> fieldExpression, Action<SearchQuery<TDocument>> query)
            where TDocument : Document
        {
            Checker.IsNull(conditional);

            if (conditional.Invoke())
            {
                documentSearch.LocalParameter(name, fieldExpression, query);
            }

            return documentSearch;
        }

        /// <summary>
        /// Create a local parameter in commom case (a plain value)
        /// </summary>
        /// <param name="documentSearch">Document search engine</param>
        /// <param name="name">Name of parameter</param>
        /// <param name="value">Plain value to include in query</param>
        /// <returns>Document search engine</returns>
        public static DocumentSearch<TDocument> LocalParameter<TDocument>(this DocumentSearch<TDocument> documentSearch, string name, string value)
            where TDocument : Document
        {
            var parameter = documentSearch.ServiceProvider.GetService<ILocalParameter<TDocument>>();

            parameter.Name(name);
            parameter.Value(value);

            documentSearch.Add(parameter);

            return documentSearch;
        }

        /// <summary>
        /// Create a local parameter in commom case (a plain value)
        /// </summary>
        /// <param name="documentSearch">Document search engine</param>
        /// <param name="conditional">Conditional to add parameter</param>
        /// <param name="name">Name of parameter</param>
        /// <param name="value">Plain value to include in query</param>
        /// <returns>Document search engine</returns>
        public static DocumentSearch<TDocument> LocalParameterIf<TDocument>(this DocumentSearch<TDocument> documentSearch, Func<bool> conditional, string name, string value)
            where TDocument : Document
        {
            Checker.IsNull(conditional);

            if (conditional.Invoke())
            {
                documentSearch.LocalParameter(name, value);
            }

            return documentSearch;
        }

        /// <summary>
        /// Create a cursor mark parameter
        /// </summary>
        /// <param name="documentSearch">Document search engine</param>
        /// <param name="cursorMark">Mark used to paging through the results</param>
        /// <returns>Document search engine</returns>
        public static DocumentSearch<TDocument> CursorMark<TDocument>(this DocumentSearch<TDocument> documentSearch, string cursorMark)
            where TDocument : Document
        {
            var parameter = documentSearch.ServiceProvider.GetService<ICursorMarkParameter>();

            parameter.CursorMark(cursorMark);

            documentSearch.Add(parameter);

            return documentSearch;
        }

        /// <summary>
        /// Create a cursor mark parameter
        /// </summary>
        /// <param name="documentSearch">Document search engine</param>
        /// <param name="conditional">Conditional to add parameter</param>
        /// <param name="cursorMark">Mark used to paging through the results</param>
        /// <returns>Document search engine</returns>
        public static DocumentSearch<TDocument> CursorMarkIf<TDocument>(this DocumentSearch<TDocument> documentSearch, Func<bool> conditional, string cursorMark)
            where TDocument : Document
        {
            Checker.IsNull(conditional);

            if (conditional.Invoke())
            {
                documentSearch.CursorMark(cursorMark);
            }

            return documentSearch;
        }
    }
}
