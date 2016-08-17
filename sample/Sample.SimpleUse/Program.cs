#if NETCOREAPP1_0
using Microsoft.Extensions.DependencyInjection;
#endif
using Newtonsoft.Json;
using SolrExpress.Core;
using SolrExpress.Core.Extension;
using SolrExpress.Core.Query.ParameterValue;
using SolrExpress.Solr5.Extension;
using System;
using System.Collections.Generic;

namespace Sample.SimpleUse
{
    public class Program
    {
        public static void Main(string[] args)
        {
            DocumentCollection<TechProduct> techProducts;

            // Emuling not use of Net Core DI services
#if NET40 || NET45
            techProducts = new DocumentCollectionBuilder<TechProduct>()
                .AddSolrExpress()
                .UseHostAddress("http://localhost:8983/solr/techproducts")
                .UseSolr5()
                .Create();
#else
            // Emuling use of Net Core DI services
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddSolrExpress<TechProduct>(builder => builder
                .UseHostAddress("http://localhost:8983/solr/techproducts")
                .UseSolr5());

            var serviceProvider = serviceCollection.BuildServiceProvider();

            techProducts = serviceProvider.GetRequiredService<DocumentCollection<TechProduct>>();
#endif

            List<TechProduct> documents;

            techProducts
                .Select()
                .Query(new QueryAll())
                .Limit(3)
                .Execute()
                .Document(out documents);

            foreach (var document in documents)
            {
                var json = JsonConvert.SerializeObject(document, Formatting.Indented);

                Console.WriteLine(json);
                Console.WriteLine(new string('-', 50));
            }

            Console.Read();
        }
    }
}
