using Newtonsoft.Json.Linq;
using SolrExpress.Core.Helper;
using SolrExpress.Core.Query;
using System;
using System.Diagnostics.Contracts;
using System.Linq.Expressions;

namespace SolrExpress.Solr5.Parameter
{
    public sealed class FieldsParameter<T> : IParameter<JObject>, IValidation
        where T : IDocument
    {
        private readonly Expression<Func<T, object>>[] _expressions;

        /// <summary>
        /// Create a fields parameter
        /// </summary>
        /// <param name="expressions">Expression used to find the property name</param>
        public FieldsParameter(params Expression<Func<T, object>>[] expressions)
        {
            Contract.Requires<ArgumentNullException>(expressions != null);

            this._expressions = expressions;
        }

        /// <summary>
        /// True to indicate multiple instances of the parameter, otherwise false
        /// </summary>
        public bool AllowMultipleInstances { get { return true; } }

        /// <summary>
        /// Execute the creation of the parameter "sort"
        /// </summary>
        /// <param name="jObject">JSON object with parameters to request to SOLR</param>
        public void Execute(JObject jObject)
        {
            var jArray = (JArray)jObject["fields"] ?? new JArray();

            foreach (var expression in this._expressions)
            {
                var value = UtilHelper.GetFieldNameFromExpression(expression);

                jArray.Add(value);
            }

            jObject["fields"] = jArray;
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
                var solrFieldAttribute = UtilHelper.GetSolrFieldAttributeFromPropertyInfo(expression);

                if (solrFieldAttribute != null && !solrFieldAttribute.Stored)
                {
                    isValid = false;
                    errorMessage = "A field must be \"stored=true\" to be used in field list";

                    break;
                }
            }
        }
    }
}
