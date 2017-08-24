namespace SolrExpress.Search.Parameter
{
    /// <summary>
    /// Write type parameter
    /// </summary>
    public interface IWriteTypeParameter<TDocument> : ISearchParameter
        where TDocument : Document
    {
        /// <summary>
        /// Write type used in SOLR's result
        /// </summary>
        WriteType Value { get; set; }
    }
}
