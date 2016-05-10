using SolrExpress.Core;
using SolrExpress.Core.Extension.Internal;
using SolrExpress.Core.Query;
using SolrExpress.Core.Query.Parameter;
using SolrExpress.Core.Query.ParameterValue;
using SolrExpress.Solr4.Extension.Internal;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SolrExpress.Solr4.Query.Parameter
{
    public sealed class FacetSpatialParameter<TDocument> : IFacetSpatialParameter<TDocument>, IParameter<List<string>>, IValidation
        where TDocument : IDocument
    {
        private string _aliasName;
        private SolrSpatialFunctionType _functionType;
        private Expression<Func<TDocument, object>> _expression;
        private GeoCoordinate _centerPoint;
        private decimal _distance;
        private FacetSortType? _sortType;
        private string[] _excludes;

        /// <summary>
        /// True to indicate multiple instances of the parameter, otherwise false
        /// </summary>
        public bool AllowMultipleInstances { get; } = true;

        /// <summary>
        /// Execute the creation of the parameter "sort"
        /// </summary>
        /// <param name="container">Container to parameters to request to SOLR</param>
        public void Execute(List<string> container)
        {
            Checker.IsNullOrWhiteSpace(this._aliasName);
            Checker.IsNull(this._expression);

            if (!container.Contains("facet=true"))
            {
                container.Add("facet=true");
            }

            var fieldName = this._expression.GetFieldNameFromExpression();
            var formule = this._functionType.GetSolrSpatialFormule(fieldName, this._centerPoint, this._distance);
            var facetName = this._excludes.GetSolrFacetWithExcludes(this._aliasName, formule);

            container.Add($"facet.query={facetName}");

            if (this._sortType.HasValue)
            {
                string typeName;
                string dummy;

                Checker.IsTrue<UnsupportedSortTypeException>(this._sortType.Value == FacetSortType.CountDesc || this._sortType.Value == FacetSortType.IndexDesc);

                this._sortType.Value.GetSolrFacetSort(out typeName, out dummy);

                container.Add($"f.{this._aliasName}.facet.sort={typeName}");
            }

            container.Add($"f.{this._aliasName}.facet.mincount=1");
        }

        /// <summary>
        /// Check for the parameter validation
        /// </summary>
        /// <param name="isValid">True if is valid, otherwise false</param>
        /// <param name="errorMessage">The error message, if applicable</param>
        public void Validate(out bool isValid, out string errorMessage)
        {
            Checker.IsNullOrWhiteSpace(this._aliasName);
            Checker.IsNull(this._expression);

            isValid = true;
            errorMessage = string.Empty;

            var solrFieldAttribute = this._expression.GetSolrFieldAttributeFromPropertyInfo();

            if (solrFieldAttribute == null || solrFieldAttribute.Indexed)
            {
                return;
            }

            isValid = false;
            errorMessage = Resource.FieldMustBeIndexedTrueToBeUsedInAFacetException;
        }

        /// <summary>
        /// Configure current instance
        /// </summary>
        /// <param name="aliasName">Name of the alias added in the query</param>
        /// <param name="functionType">Function used in the spatial filter</param>
        /// <param name="expression">Expression used to find the property name</param>
        /// <param name="centerPoint">Center point to spatial filter</param>
        /// <param name="distance">Distance from the center point</param>
        /// <param name="sortType">Sort type of the result of the facet</param>
        /// <param name="excludes">List of tags to exclude in facet calculation</param>
        public IFacetSpatialParameter<TDocument> Configure(string aliasName, SolrSpatialFunctionType functionType, Expression<Func<TDocument, object>> expression, GeoCoordinate centerPoint, decimal distance, FacetSortType? sortType = null, params string[] excludes)
        {
            this._aliasName = aliasName;
            this._functionType = functionType;
            this._expression = expression;
            this._centerPoint = centerPoint;
            this._distance = distance;
            this._sortType = sortType;
            this._excludes = excludes;

            return this;
        }
    }
}
