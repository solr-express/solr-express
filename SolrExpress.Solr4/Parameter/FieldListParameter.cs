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
    public sealed class FieldListParameter<TDocument> : IFieldsParameter<TDocument>, IParameter<List<string>>, IValidation
        where TDocument : IDocument
    {
        private Expression<Func<TDocument, object>>[] _expressions;

        /// <summary>
        /// True to indicate multiple instances of the parameter, otherwise false
        /// </summary>
        public bool AllowMultipleInstances { get; } = true;

        /// <summary>
        /// Execute the creation of the parameter "fl"
        /// </summary>
        /// <param name="container">Container to parameters to request to SOLR</param>
        public void Execute(List<string> container)
        {
            foreach (var expression in this._expressions)
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

            foreach (var expression in this._expressions)
            {
                var solrFieldAttribute = expression.GetSolrFieldAttributeFromPropertyInfo();

                if (solrFieldAttribute != null && !solrFieldAttribute.Stored)
                {
                    isValid = false;
                    errorMessage = Resource.FieldMustBeStoredTrueToBeUsedInFieldsException;
                    break;
                }
            }
        }

        /// <summary>
        /// Configure current instance
        /// </summary>
        /// <param name="expressions">Expression used to find the property name</param>
        public IFieldsParameter<TDocument> Configure(Expression<Func<TDocument, object>>[] expressions)
        {
            Checker.IsNull(expressions);

            this._expressions = expressions;

            return this;
        }
    }
}
