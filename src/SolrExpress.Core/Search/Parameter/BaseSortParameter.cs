using SolrExpress.Core.Utility;
using System;
using System.Linq.Expressions;

namespace SolrExpress.Core.Search.Parameter
{
    public abstract class BaseSortParameter<TDocument> : ISortParameter<TDocument>, IValidation
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

            var solrFieldAttribute = ExpressionUtility.GetSolrFieldAttributeFromPropertyInfo(this.Expression);

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
