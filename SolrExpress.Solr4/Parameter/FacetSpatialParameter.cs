using SolrExpress.Core;
using SolrExpress.Core.Extension.Internal;
using SolrExpress.Core.Parameter;
using SolrExpress.Core.ParameterValue;
using SolrExpress.Core.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SolrExpress.Solr4.Parameter
{
    public sealed class FacetSpatialParameter<TDocument> : IFacetSpatialParameter<TDocument>, IParameter<List<string>>, IValidation
        where TDocument : IDocument
    {
        /// <summary>
        /// Create a facet parameter
        /// </summary>
        public FacetSpatialParameter()
        {
        }

        /// <summary>
        /// Create a facet parameter
        /// </summary>
        /// <param name="aliasName">Name of the alias added in the query</param>
        /// <param name="functionType">Function used in the spatial filter</param>
        /// <param name="expression">Expression used to find the property name</param>
        /// <param name="centerPoint">Center point to spatial filter</param>
        /// <param name="distance">Distance from the center point</param>
        /// <param name="sortType">Sort type of the result of the facet</param>
        /// <param name="excludes">List of tags to exclude in facet calculation</param>
        public FacetSpatialParameter(string aliasName, SolrSpatialFunctionType functionType, Expression<Func<TDocument, object>> expression, GeoCoordinate centerPoint, decimal distance, SolrFacetSortType? sortType = null, params string[] excludes)
            : this()
        {
            this.AliasName = aliasName;
            this.FunctionType = functionType;
            this.Expression = expression;
            this.CenterPoint = centerPoint;
            this.Distance = distance;
            this.SortType = sortType;
            this.Excludes = excludes?.ToList();
        }

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
            Checker.IsNullOrWhiteSpace(this.AliasName);
            Checker.IsNull(this.Expression);

            if (!container.Contains("facet=true"))
            {
                container.Add("facet=true");
            }

            var fieldName = this.Expression.GetFieldNameFromExpression();

            //TODO
            //var formule = UtilHelper.GetSolrSpatialFormule(
            //    this.FunctionType,
            //    fieldName,
            //    this.CenterPoint,
            //    this.Distance);

            //container.Add($"facet.query={UtilHelper.GetSolrFacetWithExcludesSolr4(this.AliasName, formule, this.Excludes)}");

            if (this.SortType.HasValue)
            {
                string typeName;
                string dummy;

                Checker.IsTrue<UnsupportedSortTypeException>(this.SortType.Value == SolrFacetSortType.CountDesc || this.SortType.Value == SolrFacetSortType.IndexDesc);

                //TODO
                //UtilHelper.GetSolrFacetSort(this.SortType.Value, out typeName, out dummy);

                //container.Add($"f.{this.AliasName}.facet.sort={typeName}");
            }

            container.Add($"f.{this.AliasName}.facet.mincount=1");
        }

        /// <summary>
        /// Check for the parameter validation
        /// </summary>
        /// <param name="isValid">True if is valid, otherwise false</param>
        /// <param name="errorMessage">The error message, if applicable</param>
        public void Validate(out bool isValid, out string errorMessage)
        {
            Checker.IsNullOrWhiteSpace(this.AliasName);
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
        /// Name of the alias added in the query
        /// </summary>
        public string AliasName { get; set; }

        /// <summary>
        /// Function used in the spatial filter
        /// </summary>
        public SolrSpatialFunctionType FunctionType { get; set; }

        /// <summary>
        /// Expression used to find the property name
        /// </summary>
        public Expression<Func<TDocument, object>> Expression { get; set; }

        /// <summary>
        /// Center point to spatial filter
        /// </summary>
        public GeoCoordinate CenterPoint { get; set; }

        /// <summary>
        /// Distance from the center point
        /// </summary>
        public decimal Distance { get; set; }

        /// <summary>
        /// Sort type of the result of the facet
        /// </summary>
        public SolrFacetSortType? SortType { get; set; }

        /// <summary>
        /// List of tags to exclude in facet calculation
        /// </summary>
        public List<string> Excludes { get; set; }
    }
}
