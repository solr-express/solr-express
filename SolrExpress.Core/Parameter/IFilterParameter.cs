using SolrExpress.Core.ParameterValue;

namespace SolrExpress.Core.Parameter
{
    /// <summary>
    /// Signatures to use in filter parameter
    /// </summary>
    public interface IFilterParameter<TDocument> : IParameter
        where TDocument : IDocument
    {
        /// <summary>
        /// Configure current instance
        /// </summary>
        /// <param name="value">Value of the filter</param>
        /// <param name="tagName">Tag name to use in facet excluding list</param>
        IFilterParameter<TDocument> Configure(IQueryParameterValue value, string tagName = null);
    }
}
