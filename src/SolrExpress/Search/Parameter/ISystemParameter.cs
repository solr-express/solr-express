namespace SolrExpress.Search.Parameter
{
    /// <summary>
    /// Parameters necessary by system
    /// </summary>
    internal interface ISystemParameter<TDocument> : ISearchParameter
        where TDocument : Document
    {
    }
}
