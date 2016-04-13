namespace SolrExpress.Core.Parameter
{
    /// <summary>
    /// Signatures to use infacet field parameter
    /// </summary>
    public interface IFacetFieldParameter<TDocument> : IParameter
        where TDocument : IDocument
    {
        FacetFieldSettings<TDocument> Settings { get; }
    }
}
