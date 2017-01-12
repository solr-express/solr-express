using SolrExpress.Core.Search.ParameterValue;
using SolrExpress.Core.Utility;
using System;
using System.Linq.Expressions;

namespace SolrExpress.Core.Search.Parameter
{
    public abstract class BaseFacetSpatialParameter<TDocument> : IFacetSpatialParameter<TDocument>, IValidation
        where TDocument : IDocument
    {
        protected IExpressionBuilder<TDocument> _expressionBuilder;

        public BaseFacetSpatialParameter(IExpressionBuilder<TDocument> expressionBuilder)
        {
            this._expressionBuilder = expressionBuilder;
        }

        /// <summary>
        /// True to indicate multiple instances of the parameter, otherwise false
        /// </summary>
        public bool AllowMultipleInstances { get; } = true;

        /// <summary>
        /// Name of the alias added in the query
        /// </summary>
        public string AliasName { get; private set; }

        /// <summary>
        /// Function used in the spatial filter
        /// </summary>
        public SolrSpatialFunctionType FunctionType { get; private set; }

        /// <summary>
        /// Expression used to find the property name
        /// </summary>
        public Expression<Func<TDocument, object>> Expression { get; private set; }

        /// <summary>
        /// Center point to spatial filter
        /// </summary>
        public GeoCoordinate CenterPoint { get; private set; }

        /// <summary>
        /// Distance from the center point
        /// </summary>
        public decimal Distance { get; private set; }

        /// <summary>
        /// Sort type of the result of the facet
        /// </summary>
        public FacetSortType? SortType { get; private set; }

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
            Checker.IsNullOrWhiteSpace(this.AliasName);
            Checker.IsNull(this.Expression);

            isValid = true;
            errorMessage = string.Empty;

            var solrFieldAttribute = this._expressionBuilder.GetSolrFieldAttributeFromPropertyInfo(this.Expression);

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
        /// <param name="aliasName">Name of the alias added in the query</param>
        /// <param name="functionType">Function used in the spatial filter</param>
        /// <param name="expression">Expression used to find the property name</param>
        /// <param name="centerPoint">Center point to spatial filter</param>
        /// <param name="distance">Distance from the center point</param>
        /// <param name="sortType">Sort type of the result of the facet</param>
        /// <param name="excludes">List of tags to exclude in facet calculation</param>
        public IFacetSpatialParameter<TDocument> Configure(string aliasName, SolrSpatialFunctionType functionType, Expression<Func<TDocument, object>> expression, GeoCoordinate centerPoint, decimal distance, FacetSortType? sortType = null, params string[] excludes)
        {
            this.AliasName = aliasName;
            this.FunctionType = functionType;
            this.Expression = expression;
            this.CenterPoint = centerPoint;
            this.Distance = distance;
            this.SortType = sortType;
            this.Excludes = excludes;

            return this;
        }
    }
}
