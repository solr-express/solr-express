namespace SolrExpress.Search.Parameter
{
    /// <summary>
    /// Signatures to use in parameters necessary by system
    /// </summary>
    internal interface ISystemParameter<TDocument> : ISearchParameter
        where TDocument : Document
    {
    }
}
