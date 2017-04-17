namespace SolrExpress.Search.Behaviour
{
    /// <summary>
    /// Signatures to change behaviour of search item
    /// </summary>
    public interface IChangeBehaviour : ISearchItem
    {
        /// <summary>
        /// Execute changes in behaviour of search item
        /// </summary>
        void Execute();
    }
}
