using SolrExpress.Core.Utility;
using System;
using System.Linq.Expressions;

namespace SolrExpress.Core.Search.Parameter
{
    public abstract class BaseFacetFieldParameter<TDocument> : IFacetFieldParameter<TDocument>, IValidation
        where TDocument : IDocument
    {
        /// <summary>
        /// True to indicate multiple instances of the parameter, otherwise false
        /// </summary>
        public bool AllowMultipleInstances { get; } = true;

        /// <summary>
        /// Expression used to find the property name
        /// </summary>
        public Expression<Func<TDocument, object>> Expression { get; private set; }

        /// <summary>
        /// Sort type of the result of the facet
        /// </summary>
        public FacetSortType? SortType { get; private set; }

        /// <summary>
        /// Limit of itens in facet's result
        /// </summary>
        public int? Limit { get; private set; }

        /// <summary>
        /// List of tags to exclude in facet calculation
        /// </summary>
        public string[] Excludes { get; private set; }

        /// <summary>
        /// Check for the parameter validation
        /// </summary>
        /// <param name="isValid">True if is valid, otherwise false</param>
        /// <param name="errorMessage">The error message, if applicable</param>
        public void Validate(out bool isValid, out string errorMessage)
        {
            isValid = true;
            errorMessage = string.Empty;

            var solrFieldAttribute = ExpressionUtility.GetSolrFieldAttributeFromPropertyInfo(this.Expression);

            if (solrFieldAttribute?.Indexed ?? true)
            {
                return;
            }

            isValid = false;
            errorMessage = Resource.FieldMustBeIndexedTrueToBeUsedInAFacetException;
        }

        /// <summary>
        /// Configure current instance
        /// </summary>
        /// <param name="expression">Expression used to find the property name</param>
        /// <param name="sortType">Sort type of the result of the facet</param>
        /// <param name="limit">Limit of itens in facet's result</param>
        /// <param name="excludes">List of tags to exclude in facet calculation</param>
        public IFacetFieldParameter<TDocument> Configure(Expression<Func<TDocument, object>> expression, FacetSortType? sortType = null, int? limit = null, params string[] excludes)
        {
            Checker.IsNull(expression);

            this.Expression = expression;
            this.SortType = sortType;
            this.Limit = limit;
            this.Excludes = excludes;

            return this;
        }
    }
}
