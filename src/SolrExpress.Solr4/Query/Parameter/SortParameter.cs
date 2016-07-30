using SolrExpress.Core;
using SolrExpress.Core.Extension.Internal;
using SolrExpress.Core.Query;
using SolrExpress.Core.Query.Parameter;
using SolrExpress.Solr4.Query.Parameter.Internal;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SolrExpress.Solr4.Query.Parameter
{
    public sealed class SortParameter<TDocument> : ISortParameter<TDocument>, IParameter<List<string>>, IValidation
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
        /// Execute creation of parameter "sort"
        /// </summary>
        /// <param name="container">Container to parameters to request to SOLR</param>
        public void Execute(List<string> container)
        {
            var fieldName = this.Expression.GetFieldNameFromExpression();

            var command = new SortCommand();
            command.Execute(fieldName, this.Ascendent, container);
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

        /// <summary>
        /// Check for the parameter validation
        /// </summary>
        /// <param name="isValid">True if is valid, otherwise false</param>
        /// <param name="errorMessage">The error message, if applicable</param>
        public void Validate(out bool isValid, out string errorMessage)
        {
            isValid = true;
            errorMessage = string.Empty;

            var solrFieldAttribute = this
                .Expression
                .GetSolrFieldAttributeFromPropertyInfo();

            var withError = (!solrFieldAttribute?.Indexed) ?? true;

            if (!withError)
            {
                return;
            }

            isValid = false;
            errorMessage = Resource.FieldMustBeIndexedTrueToBeUsedInASortException;
        }
    }
}
