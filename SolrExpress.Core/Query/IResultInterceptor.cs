namespace SolrExpress.Core.Query
{
    public interface IResultInterceptor
    {
        void Execute(ref string json);
    }
}
