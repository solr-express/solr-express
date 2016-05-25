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
        /// <summary>
        /// True to indicate multiple instances of the parameter, otherwise false
        /// </summary>
        public bool AllowMultipleInstances { get; } = true;

        /// <summary>
        /// Expression used to find the property name
        /// </summary>
        public Expression<Func<TDocument, object>> Expression { get; private set; }

        /// <summary>
        /// True to ascendent order, otherwise false
        /// </summary>
        public bool Ascendent { get; private set; }

        /// <summary>
        /// Execute the creation of the parameter "sort"
        /// </summary>
        /// <param name="container">Container to parameters to request to SOLR</param>
        public void Execute(List<string> container)
        {
            var fieldName = this.Expression.GetFieldNameFromExpression();

            var value = $"{fieldName} {(this.Ascendent ? "asc" : "desc")}";

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

            this.Expression = expression;
            this.Ascendent = ascendent;

            return this;
        }
    }
}
