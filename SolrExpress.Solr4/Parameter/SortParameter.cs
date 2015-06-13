using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using SolrExpress.Core.Helper;
using SolrExpress.Core.Query;

namespace SolrExpress.Solr4.Parameter
{
    public sealed class SortParameter<T> : IParameter<List<string>>
        where T : IDocument
    {
        private readonly Expression<Func<T, object>> _expression;
        private readonly bool _ascendent;

        /// <summary>
        /// Create a sort parameter
        /// </summary>
        /// <param name="expression">Expression used to find the property name</param>
        /// <param name="ascendent">True to ascendent order, otherwise false</param>
        public SortParameter(Expression<Func<T, object>> expression, bool ascendent)
        {
            this._expression = expression;
            this._ascendent = ascendent;
        }

        /// <summary>
        /// True to indicate multiple instances of the parameter, otherwise false
        /// </summary>
        public bool AllowMultipleInstances { get { return true; } }

        /// <summary>
        /// Execute the creation of the parameter "sort"
        /// </summary>
        /// <param name="container">Container to parameters to request to SOLR</param>
        public void Execute(List<string> container)
        {
            var fieldName = UtilHelper.GetFieldNameFromExpression(this._expression);

            var value = string.Concat(fieldName, " ", this._ascendent ? "asc" : "desc");

            var sort = container.FirstOrDefault(q => q.StartsWith("sort="));

            if (!string.IsNullOrWhiteSpace(sort))
            {
                container.Remove(sort);

                sort = string.Concat(",", sort, value);
            }
            else
            {
                sort = string.Concat("sort=", value);
            }

            container.Add(sort);
        }
    }
}
