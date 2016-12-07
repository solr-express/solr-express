using SolrExpress.Core.Utility;

namespace SolrExpress.Core.Search.Parameter
{
    public abstract class BaseQueryParameter<TDocument> : IQueryParameter<TDocument>
        where TDocument : IDocument
    {
        /// <summary>
        /// True to indicate multiple instances of the parameter, otherwise false
        /// </summary>
        public bool AllowMultipleInstances { get; } = false;

        /// <summary>
        /// Parameter to include in the query
        /// </summary>
        public ISearchParameterValue<TDocument> Value { get; private set; }

        /// <summary>
        /// Expressions builder
        /// </summary>
        public IExpressionBuilder<TDocument> ExpressionBuilder { get; set; }

        /// <summary>
        /// Configure current instance
        /// </summary>
        /// <param name="value">Parameter to include in the query</param>
        public IQueryParameter<TDocument> Configure(ISearchParameterValue<TDocument> value)
        {
            Checker.IsNull(value);

            this.Value = value;

            return this;
        }
    }
}
