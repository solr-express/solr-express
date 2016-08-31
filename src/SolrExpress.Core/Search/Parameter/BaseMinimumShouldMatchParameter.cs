using SolrExpress.Core.Utility;

namespace SolrExpress.Core.Search.Parameter
{
    public abstract class BaseMinimumShouldMatchParameter : IMinimumShouldMatchParameter
    {
        /// <summary>
        /// True to indicate multiple instances of the parameter, otherwise false
        /// </summary>
        public bool AllowMultipleInstances { get; } = false;

        /// <summary>
        /// Expression used to make the mm parameter
        /// </summary>
        public string Expression { get; private set; }

        /// <summary>
        /// Configure current instance
        /// </summary>
        /// <param name="expression">Expression used to make the mm parameter</param>
        public IMinimumShouldMatchParameter Configure(string expression)
        {
            Checker.IsNullOrWhiteSpace(expression);

            this.Expression = expression;

            return this;
        }
    }
}
