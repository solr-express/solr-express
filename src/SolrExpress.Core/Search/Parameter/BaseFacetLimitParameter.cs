using SolrExpress.Core.Utility;

namespace SolrExpress.Core.Search.Parameter
{
    public abstract class BaseFacetLimitParameter<TDocument> : IFacetLimitParameter<TDocument>
        where TDocument : IDocument
    {
        /// <summary>
        /// True to indicate multiple instances of the parameter, otherwise false
        /// </summary>
        public bool AllowMultipleInstances { get; } = false;

        public IExpressionBuilder<TDocument> ExpressionBuilder { get; set; }

        /// <summary>
        /// Value of limit
        /// </summary>
        public int Value { get; private set; }

        /// <summary>
        /// Configure current instance
        /// </summary>
        /// <param name="value">Value of limit</param>
        public IFacetLimitParameter<TDocument> Configure(int value)
        {
            this.Value = value;

            return this;
        }
    }
}
