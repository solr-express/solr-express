using SolrExpress.Core;
using SolrExpress.Core.Extension.Internal;
using SolrExpress.Core.Query.Parameter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SolrExpress.Solr4.Query.Parameter
{
    public sealed class SortParameter<TDocument> : ISortParameter<TDocument>, IParameter<List<string>>
        where TDocument : IDocument
    {
        private Expression<Func<TDocument, object>> _expression;
        private bool _ascendent;

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
            var fieldName = this._expression.GetFieldNameFromExpression();

            var value = $"{fieldName} {(this._ascendent ? "asc" : "desc")}";

            var sort = container.FirstOrDefault(q => q.StartsWith("sort="));

            if (!string.IsNullOrWhiteSpace(sort))
            {
                container.Remove(sort);

                sort = $",{sort}{value}";
            }
            else
            {
                sort = $"sort={value}";
            }

            container.Add(sort);
        }

        /// <summary>
        /// Configure current instance
        /// </summary>
        /// <param name="expression">Expression used to find the property name</param>
        /// <param name="ascendent">True to ascendent order, otherwise false</param>
        public ISortParameter<TDocument> Configure(Expression<Func<TDocument, object>> expression, bool ascendent)
        {
            Checker.IsNull(expression);

            this._expression = expression;
            this._ascendent = ascendent;

            return this;
        }
    }
}
