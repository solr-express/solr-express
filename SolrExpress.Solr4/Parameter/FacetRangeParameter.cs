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
    public sealed class FacetRangeParameter<TDocument> : IFacetRangeParameter<TDocument>, IParameter<List<string>>, IValidation
      where TDocument : IDocument
    {
        /// <summary>
        /// Create a facet parameter
        /// </summary>
        public FacetRangeParameter()
        {
        }

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
            : this()
        {
            this.AliasName = aliasName;
            this.Expression = expression;
            this.Gap = gap;
            this.Start = start;
            this.End = end;
            this.SortType = sortType;
            this.Excludes = excludes?.ToList();
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
            Checker.IsNullOrWhiteSpace(this.AliasName);
            Checker.IsNull(this.Expression);

            if (!container.Contains("facet=true"))
            {
                container.Add("facet=true");
            }

            var fieldName = this.Expression.GetFieldNameFromExpression();

            // TODO
            //container.Add($"facet.range={this.Expression.GetSolrFacetWithExcludesSolr4(this.AliasName, fieldName, this.Excludes)}");

            if (!string.IsNullOrWhiteSpace(this.Gap))
            {
                container.Add($"f.{fieldName}.facet.range.gap={this.Gap}");
            }
            if (!string.IsNullOrWhiteSpace(this.Start))
            {
                container.Add($"f.{fieldName}.facet.range.start={this.Start}");
            }
            if (!string.IsNullOrWhiteSpace(this.End))
            {
                container.Add($"f.{fieldName}.facet.range.end={this.End}");
            }

            container.Add($"f.{fieldName}.facet.range.other=before");
            container.Add($"f.{fieldName}.facet.range.other=after");

            if (this.SortType.HasValue)
            {
                string typeName;
                string dummy;

                Checker.IsTrue<UnsupportedSortTypeException>(this.SortType.Value == SolrFacetSortType.CountDesc || this.SortType.Value == SolrFacetSortType.IndexDesc);

                //TODO
                //UtilHelper.GetSolrFacetSort(this.SortType.Value, out typeName, out dummy);

                //container.Add($"f.{fieldName}.facet.range.sort={typeName}");
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
            else
            {
                var propertyType = this.Expression.GetPropertyTypeFromExpression();

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
        /// Name of the alias added in the query
        /// </summary>
        public string AliasName { get; set; }

        /// <summary>
        /// Expression used to find the property name
        /// </summary>
        public string End { get; set; }

        /// <summary>
        /// Size of each range bucket to make the facet
        /// </summary>
        public Expression<Func<TDocument, object>> Expression { get; set; }

        /// <summary>
        /// Lower bound to make the facet
        /// </summary>
        public string Gap { get; set; }

        /// <summary>
        /// Upper bound to make the facet
        /// </summary>
        public SolrFacetSortType? SortType { get; set; }

        /// <summary>
        /// Sort type of the result of the facet
        /// </summary>
        public string Start { get; set; }

        /// <summary>
        /// List of tags to exclude in facet calculation
        /// </summary>
        public List<string> Excludes { get; set; }
    }
}
