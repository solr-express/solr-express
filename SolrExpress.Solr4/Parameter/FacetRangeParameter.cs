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
    public sealed class FacetRangeParameter<TDocument> : IFacetRangeParameter, IParameter<List<string>>, IValidation
      where TDocument : IDocument
    {
        private readonly Expression<Func<TDocument, object>> _expression;
        private readonly string _aliasName;
        private readonly string _gap;
        private readonly string _start;
        private readonly string _end;
        private readonly SolrFacetSortType? _sortType;
        private readonly string[] _excludes;

        /// <summary>
        /// Create a facet parameter
        /// </summary>
        /// <param name="aliasName">Name of the alias added in the query</param>
        /// <param name="expression">Expression used to find the property name</param>
        /// <param name="gap">Size of each range bucket to make the facet</param>
        /// <param name="start">Lower bound to make the facet</param>
        /// <param name="end">Upper bound to make the facet</param>
        /// <param name="sortType">Sort type of the result of the facet</param>
        /// <param name="excludes">List of tags to exclude in facet calculation</param>
        public FacetRangeParameter(string aliasName, Expression<Func<TDocument, object>> expression, string gap = null, string start = null, string end = null, SolrFacetSortType? sortType = null, params string[] excludes)
        {
            ThrowHelper<ArgumentNullException>.If(string.IsNullOrWhiteSpace(aliasName));
            ThrowHelper<ArgumentNullException>.If(expression == null);

            this._aliasName = aliasName;
            this._expression = expression;
            this._gap = gap;
            this._start = start;
            this._end = end;
            this._sortType = sortType;
            this._excludes = excludes;
        }

        /// <summary>
        /// True to indicate multiple instances of the parameter, otherwise false
        /// </summary>
        public bool AllowMultipleInstances { get; } = true;

        /// <summary>
        /// Execute the creation of the parameter "facet.range"
        /// </summary>
        /// <param name="container">Container to parameters to request to SOLR</param>
        public void Execute(List<string> container)
        {
            if (!container.Contains("facet=true"))
            {
                container.Add("facet=true");
            }

            var fieldName = UtilHelper.GetFieldNameFromExpression(this._expression);

            container.Add($"facet.range={UtilHelper.GetSolrFacetWithExcludesSolr4(this._aliasName, fieldName, this._excludes)}");

            if (!string.IsNullOrWhiteSpace(this._gap))
            {
                container.Add($"f.{fieldName}.facet.range.gap={this._gap}");
            }
            if (!string.IsNullOrWhiteSpace(this._start))
            {
                container.Add($"f.{fieldName}.facet.range.start={this._start}");
            }
            if (!string.IsNullOrWhiteSpace(this._end))
            {
                container.Add($"f.{fieldName}.facet.range.end={this._end}");
            }

            container.Add($"f.{fieldName}.facet.range.other=before");
            container.Add($"f.{fieldName}.facet.range.other=after");

            if (this._sortType.HasValue)
            {
                string typeName;
                string dummy;

                if (this._sortType.Value == SolrFacetSortType.CountDesc || this._sortType.Value == SolrFacetSortType.IndexDesc)
                {
                    throw new UnsupportedSortTypeException();
                }

                UtilHelper.GetSolrFacetSort(this._sortType.Value, out typeName, out dummy);

                container.Add($"f.{fieldName}.facet.range.sort={typeName}");
            }

            container.Add($"f.{fieldName}.facet.mincount=1");
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
                errorMessage = Resource.FieldMustBeIndexedTrueToBeUsedInAFacetException;
            }
            else
            {
                var propertyType = UtilHelper.GetPropertyTypeFromExpression(this._expression);

                switch (propertyType.ToString())
                {
                    case "System.Int32":
                    case "System.Int64":
                    case "System.Single":
                    case "System.Double":
                    case "System.Decimal":
                    case "System.DateTime":
                        break;
                    default:
                        isValid = false;

                        errorMessage = Resource.FieldMustBeNumericOrDateTimeToBeUsedInFacetRangeException;
                        break;
                }
            }
        }
    }
}
