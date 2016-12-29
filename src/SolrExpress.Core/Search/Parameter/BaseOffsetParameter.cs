using System;
using SolrExpress.Core.Utility;

namespace SolrExpress.Core.Search.Parameter
{
    public abstract class BaseOffsetParameter<TDocument> : IOffsetParameter<TDocument>
        where TDocument : IDocument
    {
        /// <summary>
        /// True to indicate multiples instance of the parameter, otherwise false
        /// </summary>
        public bool AllowMultipleInstances { get; } = false;

        public IExpressionBuilder<TDocument> ExpressionBuilder { get; set; }

        /// <summary>
        /// Value of limit
        /// </summary>
        public long Value { get; private set; }

        /// <summary>
        /// Configure current instance
        /// </summary>
        /// <param name="value">Value of limit</param>
        /// <returns></returns>
        public IOffsetParameter<TDocument> Configure(long value)
        {
            this.Value = value;

            return this;
        }
    }
}
