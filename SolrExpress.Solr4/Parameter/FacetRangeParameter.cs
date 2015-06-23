using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using SolrExpress.Core.Enumerator;
using SolrExpress.Core.Helper;
using SolrExpress.Core.Query;

namespace SolrExpress.Solr4.Parameter
{
    public sealed class FacetRangeParameter<T> : IParameter<List<string>>, IValidation
      where T : IDocument
    {
        private readonly Expression<Func<T, object>> _expression;
        private readonly string _aliasName;
        private readonly string _gap;
        private readonly string _start;
        private readonly string _end;
        private readonly SolrFacetSortType? _sortType;

        /// <summary>
        /// Create a facet parameter
        /// </summary>
        /// <param name="expression">Expression used to find the property name</param>
        /// <param name="aliasName">Name of the alias added in the query</param>
        /// <param name="gap">Size of each range bucket to make the facet</param>
        /// <param name="start">Lower bound to make the facet</param>
        /// <param name="end">Upper bound to make the facet</param>
        /// <param name="sortType">Sort type of the result of the facet</param>
        public FacetRangeParameter(Expression<Func<T, object>> expression, string aliasName, string gap = null, string start = null, string end = null, SolrFacetSortType? sortType = null)
        {
            this._expression = expression;
            this._aliasName = aliasName;
            this._gap = gap;
            this._start = start;
            this._end = end;
            this._sortType = sortType;
        }

        /// <summary>
        /// True to indicate multiple instances of the parameter, otherwise false
        /// </summary>
        public bool AllowMultipleInstances { get { return true; } }

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

            container.Add(string.Format("facet.range={{!ex=dt key={0}}}{1}", this._aliasName, fieldName));

            if (!string.IsNullOrWhiteSpace(this._gap))
            {
                container.Add(string.Format("f.{0}.facet.range.gap={1}", fieldName, this._gap));
            }
            if (!string.IsNullOrWhiteSpace(this._start))
            {
                container.Add(string.Format("f.{0}.facet.range.start={1}", fieldName, this._start));
            }
            if (!string.IsNullOrWhiteSpace(this._end))
            {
                container.Add(string.Format("f.{0}.facet.range.end={1}", fieldName, this._end));
            }

            container.Add(string.Format("f.{0}.facet.range.other=before", fieldName));
            container.Add(string.Format("f.{0}.facet.range.other=after", fieldName));

            if (this._sortType.HasValue)
            {
                string typeName;
                string dummy;

                // TODO: In SOLR 4, we can't choise between ascending or descending sort. Make a choise here, throws a exception case the SolrFacetSortType equals *Descending or not throws???
                UtilHelper.GetSolrFacetSort(this._sortType.Value, out typeName, out dummy);

                container.Add(string.Format("f.{0}.facet.range.sort={1}", fieldName, typeName));
            }

            container.Add(string.Format("f.{0}.facet.mincount=1", fieldName));
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
                errorMessage = "A field must be \"indexed=true\" to be used in a facet";
            }
        }
    }
}
