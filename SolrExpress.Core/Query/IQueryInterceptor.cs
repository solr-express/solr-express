namespace SolrExpress.Core.Query
{
    public interface IQueryInterceptor
    {
        void Execute(ref string query);
    }
}
