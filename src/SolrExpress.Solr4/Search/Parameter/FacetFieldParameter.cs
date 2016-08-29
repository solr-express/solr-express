using SolrExpress.Core;
using SolrExpress.Core.Extension.Internal;
using SolrExpress.Core.Search;
using SolrExpress.Core.Search.Parameter;
using SolrExpress.Core.Utility;
using SolrExpress.Solr4.Extension.Internal;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SolrExpress.Solr4.Search.Parameter
{
    public sealed class FacetFieldParameter<TDocument> : IFacetFieldParameter<TDocument>, ISearchParameter<List<string>>, IValidation
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
        /// Execute the creation of the parameter "facet.field"
        /// </summary>
        /// <param name="container">Container to parameters to request to SOLR</param>
        public void Execute(List<string> container)
        {
            if (!container.Contains("facet=true"))
            {
                container.Add("facet=true");
            }

            var aliasName = this.Expression.GetPropertyNameFromExpression();
            var fieldName = this.Expression.GetFieldNameFromExpression();
            var facetField = this.Excludes.GetSolrFacetWithExcludes(aliasName, fieldName);

            container.Add($"facet.field={facetField}");

            if (this.SortType.HasValue)
            {
                string typeName;
                string dummy;

                Checker.IsTrue<UnsupportedSortTypeException>(this.SortType.Value == FacetSortType.CountDesc || this.SortType.Value == FacetSortType.IndexDesc);

                ExpressionUtility.GetSolrFacetSort(this.SortType.Value, out typeName, out dummy);

                container.Add($"f.{fieldName}.facet.sort={typeName}");
            }

            container.Add($"f.{fieldName}.facet.mincount=1");

            if (this.Limit.HasValue)
            {
                container.Add($"f.{fieldName}.facet.limit={this.Limit.Value}");
            }
        }

        /// <summary>
        /// Check for the parameter validation
        /// </summary>
        /// <param name="isValid">True if is valid, otherwise false</param>
        /// <param name="errorMessage">The error message, if applicable</param>
        public void Validate(out bool isValid, out string errorMessage)
        {
            isValid = true;
            errorMessage = string.Empty;

            var solrFieldAttribute = this.Expression.GetSolrFieldAttributeFromPropertyInfo();

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
