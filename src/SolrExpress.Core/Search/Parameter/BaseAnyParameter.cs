using SolrExpress.Core.Utility;
using System.Collections.Generic;

namespace SolrExpress.Core.Search.Parameter
{
    public abstract class BaseAnyParameter<TDocument> : IAnyParameter<TDocument>, IValidation
        where TDocument : IDocument
    {
        /// <summary>
        /// True to indicate multiple instances of the parameter, otherwise false
        /// </summary>
        public bool AllowMultipleInstances { get; } = true;

        public IExpressionBuilder<TDocument> ExpressionBuilder { get; set; }

        /// <summary>
        /// Name of the parameter
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Value of the parameter
        /// </summary>
        public string Value { get; private set; }

        /// <summary>
        /// Configure current instance
        /// </summary>
        /// <param name="name">Name of the parameter</param>
        /// <param name="value">Value of the parameter</param>
        public IAnyParameter<TDocument> Configure(string name, string value)
        {
            this.Name = name;
            this.Value = value;

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
            errorMessage = null;

            var specificParameters = new List<string> {
                "facet.field",
                "facet.limit",
                "facet.query",
                "facet.range",
                "fl",
                "fq",
                "mm",
                "q",
                "qf",
                "rows",
                "sort",
                "start"
            };

            if (specificParameters.Contains(this.Name.ToLowerInvariant()))
            {
                isValid = false;
                errorMessage = string.Format(Resource.UseSpecificParameterRatherThanAnyException, this.Name);
            }
        }
    }
}
