namespace SolrExpress.Search.Behaviour
{
    /// <summary>
    /// Signautes to change behaviour about dynamic field
    /// </summary>
    public interface IChangeDynamicFieldBehaviour<TDocument> : IChangeBehaviour, ISearchItemFieldExpression<TDocument>
        where TDocument : Document
    {
        /// <summary>
        /// Indicates prefix name in dynamic field configurations
        /// </summary>
        string DynamicFieldPrefix { get; set; }

        /// <summary>
        /// Indicates suffix name in dynamic field configurations
        /// </summary>
        string DynamicFieldSuffix { get; set; }
    }
}
