using SolrExpress.Search.Parameter;
using SolrExpress.Utility;
using System;
using System.Linq.Expressions;

namespace SolrExpress.Search
{
    public static class DocumentSearchExtension
    {
        /// <summary>
        /// Create a facet field parameter
        /// </summary>
        /// <param name="documentSearch">Document search engine</param>
        /// <param name="fieldExpression">Expression used to find the property name</param>
        /// <param name="facet">Instance of facet ready to configure</param>
        public static DocumentSearch<TDocument> FacetField<TDocument>(this DocumentSearch<TDocument> documentSearch, Expression<Func<TDocument, object>> fieldExpression, Action<IFacetFieldParameter<TDocument>> facet = null)
            where TDocument : IDocument
        {
            Checker.IsNull(fieldExpression);

            // TODO: Get from DI Engine
            IFacetFieldParameter<TDocument> parameter = null;
            parameter.FieldExpression = fieldExpression;

            facet?.Invoke(parameter);

            documentSearch.Add(parameter);

            return documentSearch;
        }
    }
}
