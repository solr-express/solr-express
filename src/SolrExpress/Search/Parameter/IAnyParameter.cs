namespace SolrExpress.Search.Parameter
{
    /// <summary>
    /// Signatures to use any parameter
    /// </summary>
    public interface IAnyParameter<TDocument> : ISearchParameter
        where TDocument : IDocument
    {
        /// <summary>
        /// Configure name of parameter
        /// </summary>
        /// <param name="name">Name of parameter</param>
        IAnyParameter<TDocument> Name(string name);

        /// <summary>
        /// Configure value of parameter
        /// </summary>
        /// <param name="value">Value of parameter</param>
        IAnyParameter<TDocument> Value(string value);
    }
}
