using SolrExpress.Core.Utility;
using System;
using System.Linq.Expressions;

namespace SolrExpress.Core.Search.Parameter
{
    public abstract class BaseFacetRangeParameter<TDocument> : IFacetRangeParameter<TDocument>, IValidation
      where TDocument : IDocument
    {
        protected IExpressionBuilder<TDocument> _expressionBuilder;

        public BaseFacetRangeParameter(IExpressionBuilder<TDocument> expressionBuilder)
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
        /// Expression used to find the property name
        /// </summary>
        public Expression<Func<TDocument, object>> Expression { get; private set; }

        /// <summary>
        /// Size of each range bucket to make the facet
        /// </summary>
        public string Gap { get; private set; }

        /// <summary>
        /// Lower bound to make the facet
        /// </summary>
        public string Start { get; private set; }

        /// <summary>
        /// Upper bound to make the facet
        /// </summary>
        public string End { get; private set; }

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
            isValid = true;
            errorMessage = string.Empty;

            var solrFieldAttribute = this._expressionBuilder.GetSolrFieldAttributeFromPropertyInfo(this.Expression);

            if (solrFieldAttribute != null && !solrFieldAttribute.Indexed)
            {
                isValid = false;
                errorMessage = Resource.FieldMustBeIndexedTrueToBeUsedInAFacetException;
            }
            else
            {
                var propertyType = this._expressionBuilder.GetPropertyTypeFromExpression(this.Expression);

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

        /// <summary>
        /// Configure current instance
        /// </summary>
        /// <param name="aliasName">Name of the alias added in the query</param>
        /// <param name="expression">Expression used to find the property name</param>
        /// <param name="gap">Size of each range bucket to make the facet</param>
        /// <param name="start">Lower bound to make the facet</param>
        /// <param name="end">Upper bound to make the facet</param>
        /// <param name="sortType">Sort type of the result of the facet</param>
        /// <param name="excludes">List of tags to exclude in facet calculation</param>
        public IFacetRangeParameter<TDocument> Configure(string aliasName, Expression<Func<TDocument, object>> expression, string gap, string start, string end, FacetSortType? sortType = null, params string[] excludes)
        {
            Checker.IsNullOrWhiteSpace(aliasName);
            Checker.IsNull(expression);
            Checker.IsNull(gap);
            Checker.IsNull(start);
            Checker.IsNull(end);

            this.AliasName = aliasName;
            this.Expression = expression;
            this.Gap = gap;
            this.Start = start;
            this.End = end;
            this.SortType = sortType;
            this.Excludes = excludes;

            return this;
        }
    }
}
