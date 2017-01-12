using SolrExpress.Core.Utility;

namespace SolrExpress.Core.Search.Parameter
{
    public abstract class BaseQueryFieldParameter<TDocument> : IQueryFieldParameter<TDocument>
        where TDocument : IDocument
    {
        /// <summary>
        /// True to indicate multiple instances of the parameter, otherwise false
        /// </summary>
        public bool AllowMultipleInstances { get; } = false;

        /// <summary>
        /// Query used to make the query field
        /// </summary>
        public string Expression { get; private set; }
        
        /// <summary>
        /// Configure current instance
        /// </summary>
        /// <param name="expression">Query used to make the query field</param>
        public IQueryFieldParameter<TDocument> Configure(string expression)
        {
            Checker.IsNullOrWhiteSpace(expression);

            this.Expression = expression;

            return this;
        }
    }
}
