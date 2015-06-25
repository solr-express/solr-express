namespace SolrExpress.Core.Query
{
    public interface IResultInterceptor
    {
        void Execute(string json);
    }
}
