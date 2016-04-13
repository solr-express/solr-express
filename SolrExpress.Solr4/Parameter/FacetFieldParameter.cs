using SolrExpress.Core;
using SolrExpress.Core.Extension.Internal;
using SolrExpress.Core.Parameter;
using SolrExpress.Core.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SolrExpress.Solr4.Parameter
{
    public sealed class FacetFieldParameter<TDocument> : IFacetFieldParameter<TDocument>, IParameter<List<string>>, IValidation
        where TDocument : IDocument
    {
        /// <summary>
        /// Create a facet parameter
        /// </summary>
        public FacetFieldParameter()
        {
        }

        /// <summary>
        /// Create a facet parameter
        /// </summary>
        /// <param name="expression">Expression used to find the property name</param>
        /// <param name="sortType">Sort type of the result of the facet</param>
        /// <param name="limit">Limit of itens in facet's result</param>
        /// <param name="excludes">List of tags to exclude in facet calculation</param>
        public FacetFieldParameter(Expression<Func<TDocument, object>> expression, SolrFacetSortType? sortType = null, int? limit = null, params string[] excludes)
            : this()
        {
            this.Expression = expression;
            this.SortType = sortType;
            this.Limit = limit;
            this.Excludes = excludes?.ToList();
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
            Checker.IsNull(this.Expression);

            if (!container.Contains("facet=true"))
            {
                container.Add("facet=true");
            }

            var aliasName = this.Expression.GetPropertyNameFromExpression();
            var fieldName = this.Expression.GetFieldNameFromExpression();

            //TODO
            //container.Add($"facet.field={UtilHelper.GetSolrFacetWithExcludesSolr4(aliasName, fieldName, this.Excludes)}");

            if (this.SortType.HasValue)
            {
                string typeName;
                string dummy;

                Checker.IsTrue<UnsupportedSortTypeException>(this.SortType.Value == SolrFacetSortType.CountDesc || this.SortType.Value == SolrFacetSortType.IndexDesc);

                //TODO
                //UtilHelper.GetSolrFacetSort(this.SortType.Value, out typeName, out dummy);

                //container.Add($"f.{aliasName}.facet.sort={typeName}");
            }

            container.Add($"f.{aliasName}.facet.mincount=1");

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
            Checker.IsNull(this.Expression);

            isValid = true;
            errorMessage = string.Empty;

            var solrFieldAttribute = this.Expression.GetSolrFieldAttributeFromPropertyInfo();

            if (solrFieldAttribute != null && !solrFieldAttribute.Indexed)
            {
                isValid = false;
                errorMessage = Resource.FieldMustBeIndexedTrueToBeUsedInAFacetException;
            }
        }

        /// <summary>
        /// Expression used to find the property name
        /// </summary>
        public Expression<Func<TDocument, object>> Expression { get; set; }

        /// <summary>
        /// Sort type of the result of the facet
        /// </summary>
        public SolrFacetSortType? SortType { get; set; }

        /// <summary>
        /// Limit of itens in facet's result
        /// </summary>
        public int? Limit { get; set; }

        /// <summary>
        /// List of tags to exclude in facet calculation
        /// </summary>
        public List<string> Excludes { get; set; }
    }
}
