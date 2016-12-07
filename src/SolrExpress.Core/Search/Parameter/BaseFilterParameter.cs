using SolrExpress.Core.Utility;

namespace SolrExpress.Core.Search.Parameter
{
    public abstract class BaseFilterParameter<TDocument> : IFilterParameter<TDocument>
        where TDocument : IDocument
    {
        /// <summary>
        /// True to indicate multiple instances of the parameter, otherwise false
        /// </summary>
        public bool AllowMultipleInstances { get; } = true;

        /// <summary>
        /// Value of the filter
        /// </summary>
        public ISearchParameterValue<TDocument> Value { get; private set; }

        /// <summary>
        /// Tag name to use in facet excluding list
        /// </summary>
        public string TagName { get; private set; }

        /// <summary>
        /// Expressions builder
        /// </summary>
        public IExpressionBuilder<TDocument> ExpressionBuilder { get; set; }

        /// <summary>
        /// Configure current instance
        /// </summary>
        /// <param name="value">Value of the filter</param>
        /// <param name="tagName">Tag name to use in facet excluding list</param>
        public IFilterParameter<TDocument> Configure(ISearchParameterValue<TDocument> value, string tagName = null)
        {
            Checker.IsNull(value);

            this.Value = value;
            this.TagName = tagName;

            return this;
        }
    }
}
