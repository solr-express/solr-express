namespace SolrExpress.Core
{
    /// <summary>
    /// Signatures of classes dependency resolver
    /// </summary>
    public interface IResolver
    {
        T Get<T>()
            where T : class;
    }
}
