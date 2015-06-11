using SolrExpress.Core.Enumerator;
using SolrExpress.Core.Helper;
using SolrExpress.Core.Query;
using System;
using System.Linq.Expressions;
using System.Text;

namespace SolrExpress.Solr4.Parameter
{
    public sealed class FacetFieldParameter<T> : IParameter<StringBuilder>
        where T : IDocument
    {
        private readonly Expression<Func<T, object>> _expression;
        private readonly SolrFacetSortType? _sortType;

        /// <summary>
        /// Create a facet parameter
        /// </summary>
        /// <param name="expression">Expression used to find the property name</param>
        /// <param name="sortType">Sort type of the result of the facet</param>
        public FacetFieldParameter(Expression<Func<T, object>> expression, SolrFacetSortType? sortType = null)
        {
            this._expression = expression;
            this._sortType = sortType;
        }

        /// <summary>
        /// True to indicate multiple instances of the parameter, otherwise false
        /// </summary>
        public bool AllowMultipleInstances { get { return true; } }

        /// <summary>
        /// Execute the creation of the parameter "facet.field"
        /// </summary>
        /// <param name="container">Container to parameters to request to SOLR</param>
        public void Execute(StringBuilder container)
        {
            // TODO: Do better! Don't use "ToString" method to verify values, find a better way to do this
            if (!container.ToString().Contains("facet=true"))
            {
                container.Append("facet=true&");
            }

            var fieldName = UtilHelper.GetPropertyNameFromExpression(this._expression);

            container.AppendFormat("facet.field={0}&", fieldName);

            if (this._sortType.HasValue)
            {
                string typeName;
                string sortName;

                UtilHelper.GetSolrFacetSort(this._sortType.Value, out typeName, out sortName);

                container.AppendFormat("f.{0}.facet.sort={1} {2}", fieldName, typeName, sortName);
            }
        }
    }
}
