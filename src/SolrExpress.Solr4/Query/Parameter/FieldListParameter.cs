using SolrExpress.Core;
using SolrExpress.Core.Extension.Internal;
using SolrExpress.Core.Query;
using SolrExpress.Core.Query.Parameter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SolrExpress.Solr4.Query.Parameter
{
    public sealed class FieldListParameter<TDocument> : IFieldsParameter<TDocument>, IParameter<List<string>>, IValidation
        where TDocument : IDocument
    {
        /// <summary>
        /// True to indicate multiple instances of the parameter, otherwise false
        /// </summary>
        public bool AllowMultipleInstances { get; } = true;

        /// <summary>
        /// Expression used to find the property name
        /// </summary>
        public Expression<Func<TDocument, object>>[] Expressions { get; private set; }

        /// <summary>
        /// Execute the creation of the parameter "fl"
        /// </summary>
        /// <param name="container">Container to parameters to request to SOLR</param>
        public void Execute(List<string> container)
        {
            foreach (var expression in this.Expressions)
            {
                var fieldName = expression.GetFieldNameFromExpression();

                var fieldList = container.FirstOrDefault(q => q.StartsWith("fl="));

                if (!string.IsNullOrWhiteSpace(fieldList))
                {
                    container.Remove(fieldList);

                    fieldList = $"{fieldList},{fieldName}";
                }
                else
                {
                    fieldList = $"fl={fieldName}";
                }

                container.Add(fieldList);
            }
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

            var withError = this
                .Expressions
                .Select(expression => expression.GetSolrFieldAttributeFromPropertyInfo())
                .Any(solrFieldAttribute => solrFieldAttribute != null && !solrFieldAttribute.Stored);

            if (!withError)
            {
                return;
            }

            isValid = false;
            errorMessage = Resource.FieldMustBeStoredTrueToBeUsedInFieldsException;
        }

        /// <summary>
        /// Configure current instance
        /// </summary>
        /// <param name="expressions">Expression used to find the property name</param>
        public IFieldsParameter<TDocument> Configure(params Expression<Func<TDocument, object>>[] expressions)
        {
            Checker.IsNull(expressions);
            Checker.IsEmpty(expressions);

            this.Expressions = expressions;

            return this;
        }
    }
}
