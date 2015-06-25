namespace SolrExpress.Core.Query
{
    public interface IQueryInterceptor
    {
        void Execute(string query);
    }
}
