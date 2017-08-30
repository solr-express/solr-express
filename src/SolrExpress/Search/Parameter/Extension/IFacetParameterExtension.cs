using SolrExpress.Search.Query;
using SolrExpress.Utility;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SolrExpress.Search.Parameter.Extension
{
    public static class IFacetParameterExtension
    {
        /// <summary>
        /// Create a facet field parameter
        /// </summary>
        /// <param name="facetParameter">Facet parameter</param>
        /// <param name="fieldExpression">Expression used to find field name</param>
        /// <param name="instance">Instance of facet ready to configure</param>
        /// <returns>Document search engine</returns>
        public static IFacetFieldParameter<TDocument> FacetField<TDocument>(this IFacetParameter<TDocument> facetParameter, Expression<Func<TDocument, object>> fieldExpression, Action<IFacetFieldParameter<TDocument>> instance = null)
            where TDocument : Document
        {
            Checker.IsNull(fieldExpression);

            var subFacetParameter = facetParameter.ServiceProvider.GetService<IFacetFieldParameter<TDocument>>();
            subFacetParameter.FieldExpression(fieldExpression);

            instance?.Invoke(subFacetParameter);

            if (facetParameter.Facets == null)
            {
                facetParameter.Facets = new List<IFacetParameter<TDocument>>();
            }
            facetParameter.Facets.Add(subFacetParameter);

            return subFacetParameter;
        }

        /// <summary>
        /// Create a facet query parameter
        /// </summary>
        /// <param name="facetParameter">Facet parameter</param>
        /// <param name="aliasName">Name of alias added in query</param>
        /// <param name="query">Query used to make facet</param>
        /// <param name="instance">Instance of facet ready to configure</param>
        /// <returns>Document search engine</returns>
        public static IFacetQueryParameter<TDocument> FacetQuery<TDocument>(this IFacetParameter<TDocument> facetParameter, string aliasName, Action<SearchQuery<TDocument>> query, Action<IFacetQueryParameter<TDocument>> instance = null)
            where TDocument : Document
        {
            Checker.IsNull(query);

            var subFacetParameter = facetParameter.ServiceProvider.GetService<IFacetQueryParameter<TDocument>>();
            subFacetParameter.AliasName(aliasName);
            var search = facetParameter.ServiceProvider.GetService<SearchQuery<TDocument>>();
            query.Invoke(search);
            subFacetParameter.Query(search);

            instance?.Invoke(subFacetParameter);

            if (facetParameter.Facets == null)
            {
                facetParameter.Facets = new List<IFacetParameter<TDocument>>();
            }
            facetParameter.Facets.Add(subFacetParameter);

            return subFacetParameter;
        }

        /// <summary>
        /// Create a facet range parameter
        /// </summary>
        /// <param name="facetParameter">Facet parameter</param>
        /// <param name="aliasName">Name of alias added in query</param>
        /// <param name="fieldExpression">Expression used to find field name</param>
        /// <param name="gap">Size of each range bucket to make facet</param>
        /// <param name="start">Lower bound to make facet</param>
        /// <param name="end">Upper bound to make facet</param>
        /// <param name="instance">Instance of parameter ready to configure</param>
        /// <returns>Document search engine</returns>
        public static IFacetRangeParameter<TDocument> FacetRange<TDocument>(this IFacetParameter<TDocument> facetParameter, string aliasName, Expression<Func<TDocument, object>> fieldExpression, string gap, string start, string end, Action<IFacetRangeParameter<TDocument>> instance = null)
            where TDocument : Document
        {
            var subFacetParameter = facetParameter.ServiceProvider.GetService<IFacetRangeParameter<TDocument>>();
            subFacetParameter.AliasName(aliasName);
            subFacetParameter.FieldExpression(fieldExpression);
            subFacetParameter.Gap(gap);
            subFacetParameter.Start(start);
            subFacetParameter.End(end);

            instance?.Invoke(subFacetParameter);

            if (facetParameter.Facets == null)
            {
                facetParameter.Facets = new List<IFacetParameter<TDocument>>();
            }
            facetParameter.Facets.Add(subFacetParameter);

            return subFacetParameter;
        }
    }
}
