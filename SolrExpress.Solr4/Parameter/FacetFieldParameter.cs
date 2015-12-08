using SolrExpress.Core;
using SolrExpress.Core.Entity;
using SolrExpress.Core.Enumerator;
using SolrExpress.Core.Exception;
using SolrExpress.Core.Helper;
using SolrExpress.Core.Parameter;
using SolrExpress.Core.Query;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SolrExpress.Solr4.Parameter
{
    public sealed class FacetFieldParameter<TDocument> : IFacetFieldParameter, IParameter<List<string>>, IValidation
        where TDocument : IDocument
    {
        private readonly Expression<Func<TDocument, object>> _expression;
        private readonly SolrFacetSortType? _sortType;
        private readonly int? _limit;
        private readonly string[] _excludes;

        /// <summary>
        /// Create a facet parameter
        /// </summary>
        /// <param name="expression">Expression used to find the property name</param>
        /// <param name="sortType">Sort type of the result of the facet</param>
        /// <param name="limit">Limit of itens in facet's result</param>
        /// <param name="excludes">List of tags to exclude in facet calculation</param>
        public FacetFieldParameter(Expression<Func<TDocument, object>> expression, SolrFacetSortType? sortType = null, int? limit = null, params string[] excludes)
        {
            ThrowHelper<ArgumentNullException>.If(expression == null);

            this._expression = expression;
            this._sortType = sortType;
            this._limit = limit;
            this._excludes = excludes;
        }

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

            var aliasName = UtilHelper.GetPropertyNameFromExpression(this._expression);
            var fieldName = UtilHelper.GetFieldNameFromExpression(this._expression);

            container.Add($"facet.field={UtilHelper.GetSolrFacetWithExcludesSolr4(aliasName, fieldName, this._excludes)}");

            if (this._sortType.HasValue)
            {
                string typeName;
                string dummy;

                if (this._sortType.Value == SolrFacetSortType.CountDesc || this._sortType.Value == SolrFacetSortType.IndexDesc)
                {
                    throw new UnsupportedSortTypeException();
                }

                UtilHelper.GetSolrFacetSort(this._sortType.Value, out typeName, out dummy);

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

            var solrFieldAttribute = UtilHelper.GetSolrFieldAttributeFromPropertyInfo(this._expression);

            if (solrFieldAttribute != null && !solrFieldAttribute.Indexed)
            {
                isValid = false;
                errorMessage = Resource.FieldMustBeIndexedTrueToBeUsedInAFacet;
            }
        }
    }
}
