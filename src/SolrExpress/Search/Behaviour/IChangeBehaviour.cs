namespace SolrExpress.Search.Behaviour
{
    /// <summary>
    /// Change behaviour of search item
    /// </summary>
    public interface IChangeBehaviour : ISearchItem
    {
        /// <summary>
        /// Execute changes in behaviour of search item
        /// </summary>
        void Execute();
    }
}
