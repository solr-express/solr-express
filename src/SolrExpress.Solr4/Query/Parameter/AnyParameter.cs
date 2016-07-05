using SolrExpress.Core;
using SolrExpress.Core.Query;
using SolrExpress.Core.Query.Parameter;
using System.Collections.Generic;

namespace SolrExpress.Solr4.Query.Parameter
{
    public sealed class AnyParameter : IAnyParameter, IParameter<List<string>>, IValidation
    {
        /// <summary>
        /// True to indicate multiple instances of the parameter, otherwise false
        /// </summary>
        public bool AllowMultipleInstances { get; } = true;

        /// <summary>
        /// Name of the parameter
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Value of the parameter
        /// </summary>
        public string Value { get; private set; }

        /// <summary>
        /// Execute the creation of the parameter
        /// </summary>
        /// <param name="container">Container to parameters to request to SOLR</param>
        public void Execute(List<string> container)
        {
            container.Add($"{this.Name}={this.Value}");
        }

        /// <summary>
        /// Configure current instance
        /// </summary>
        /// <param name="name">Name of the parameter</param>
        /// <param name="value">Value of the parameter</param>
        public IAnyParameter Configure(string name, string value)
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
