using SimpleInjector;
using SolrExpress.DI.SimpleInjector;
using SolrExpress.Solr5.Extension;

namespace SimpleUse.Net46WithSimpleInjector
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var container = new Container();

            container.AddSolrExpress<TechProduct>(q => q
                .UseHostAddress("http://localhost:8983/solr/techproducts")
                .UseSolr5());
        }
    }
}
