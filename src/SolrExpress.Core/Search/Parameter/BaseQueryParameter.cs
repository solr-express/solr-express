using SolrExpress.Core.Utility;

namespace SolrExpress.Core.Search.Parameter
{
    public abstract class BaseQueryParameter<TDocument> : IQueryParameter<TDocument>
        where TDocument : IDocument
    {
        protected IExpressionBuilder<TDocument> _expressionBuilder;

        public BaseQueryParameter(IExpressionBuilder<TDocument> expressionBuilder)
        {
            this._expressionBuilder = expressionBuilder;
        }
        
        /// <summary>
        /// True to indicate multiple instances of the parameter, otherwise false
        /// </summary>
        public bool AllowMultipleInstances { get; } = false;

        /// <summary>
        /// Parameter to include in the query
        /// </summary>
        public ISearchParameterValue<TDocument> Value { get; private set; }

        /// <summary>
        /// Configure current instance
        /// </summary>
        /// <param name="value">Parameter to include in the query</param>
        public IQueryParameter<TDocument> Configure(ISearchParameterValue<TDocument> value)
        {
            Checker.IsNull(value);

            this.Value = value;
            this.Value.ExpressionBuilder = this._expressionBuilder;

            return this;
        }
    }
}
