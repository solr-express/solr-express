using Newtonsoft.Json.Linq;
using SolrExpress.Core;
using SolrExpress.Core.Extension.Internal;
using SolrExpress.Core.Query.Parameter;
using System;
using System.Linq.Expressions;

namespace SolrExpress.Solr5.Query.Parameter
{
    public sealed class SortParameter<TDocument> : ISortParameter<TDocument>, IParameter<JObject>
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
        /// <param name="jObject">JSON object with parameters to request to SOLR</param>
        public void Execute(JObject jObject)
        {
            var jValue = (JValue)jObject["sort"] ?? new JValue((string)null);

            var fieldName = this._expression.GetFieldNameFromExpression();

            var value = $"{fieldName} {(this._ascendent ? "asc" : "desc")}";

            if (jValue.Value != null)
            {
                jValue.Value += $", {value}";
            }
            else
            {
                jValue.Value = value;
            }

            jObject["sort"] = jValue;
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
