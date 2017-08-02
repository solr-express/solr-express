using SolrExpress.Search.Query;

namespace SolrExpress.Search.Parameter
{
    /// <summary>
    /// Signatures to use boost parameter
    /// </summary>
    public interface IBoostParameter<TDocument> : ISearchParameter
        where TDocument : Document
    {
        /// <summary>
        /// Query used to make boost
        /// </summary>
        SearchQuery<TDocument> Query { get; set; }

        /// <summary>
        /// Boost type used in calculation
        /// </summary>
        BoostFunctionType BoostFunctionType { get; set; }
    }
}
