using SolrExpress.Core;
using SolrExpress.Core.Extension.Internal;
using SolrExpress.Core.Query;
using SolrExpress.Core.Query.Parameter;
using SolrExpress.Solr4.Extension.Internal;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SolrExpress.Solr4.Parameter
{
    public sealed class FacetFieldParameter<TDocument> : IFacetFieldParameter<TDocument>, IParameter<List<string>>, IValidation
        where TDocument : IDocument
    {
        private Expression<Func<TDocument, object>> _expression;
        private SolrFacetSortType? _sortType;
        private int? _limit;
        private string[] _excludes;

        /// <summary>
        /// True to indicate multiple instances of the parameter, otherwise false
        /// </summary>
        public bool AllowMultipleInstances { get; } = true;

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

            var aliasName = this._expression.GetPropertyNameFromExpression();
            var fieldName = this._expression.GetFieldNameFromExpression();
            var facetField = this._excludes.GetSolrFacetWithExcludes(aliasName, fieldName);

            container.Add($"facet.field={facetField}");

            if (this._sortType.HasValue)
            {
                string typeName;
                string dummy;

                Checker.IsTrue<UnsupportedSortTypeException>(this._sortType.Value == SolrFacetSortType.CountDesc || this._sortType.Value == SolrFacetSortType.IndexDesc);

                this._sortType.Value.GetSolrFacetSort(out typeName, out dummy);

                container.Add($"f.{aliasName}.facet.sort={typeName}");
            }

            container.Add($"f.{aliasName}.facet.mincount=1");

            if (this._limit.HasValue)
            {
                container.Add($"f.{fieldName}.facet.limit={this._limit.Value}");
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

            var solrFieldAttribute = this._expression.GetSolrFieldAttributeFromPropertyInfo();

            if (solrFieldAttribute != null && !solrFieldAttribute.Indexed)
            {
                isValid = false;
                errorMessage = Resource.FieldMustBeIndexedTrueToBeUsedInAFacetException;
            }
        }

        /// <summary>
        /// Configure current instance
        /// </summary>
        /// <param name="expression">Expression used to find the property name</param>
        /// <param name="sortType">Sort type of the result of the facet</param>
        /// <param name="limit">Limit of itens in facet's result</param>
        /// <param name="excludes">List of tags to exclude in facet calculation</param>
        public IFacetFieldParameter<TDocument> Configure(Expression<Func<TDocument, object>> expression, SolrFacetSortType? sortType = null, int? limit = null, params string[] excludes)
        {
            Checker.IsNull(expression);

            this._expression = expression;
            this._sortType = sortType;
            this._limit = limit;
            this._excludes = excludes;

            return this;
        }
    }
}
