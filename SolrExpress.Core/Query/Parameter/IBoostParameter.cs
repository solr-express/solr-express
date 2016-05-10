using SolrExpress.Core.Query.ParameterValue;

namespace SolrExpress.Core.Query.Parameter
{
    /// <summary>
    /// Signatures to use boost parameter
    /// </summary>
    public interface IBoostParameter<TDocument> : IParameter
        where TDocument : IDocument
    {
        /// <summary>
        /// Configure current instance
        /// </summary>
        /// <param name="query">Query used to make boost</param>
        /// <param name="sortType">Boost type used in calculation</param>
        IBoostParameter<TDocument> Configure(IQueryParameterValue query, FacetSortType? sortType = null);
    }
}
