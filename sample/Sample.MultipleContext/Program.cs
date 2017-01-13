using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using SolrExpress.Core;
using SolrExpress.Core.Extension;
using SolrExpress.Core.Search;
using SolrExpress.Core.Search.ParameterValue;
using SolrExpress.Core.Search.Result;
using SolrExpress.Solr5.Extension;
using System;
using System.Collections.Generic;

namespace Sample.MultipleContext
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Emulating use of Net Core DI services
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddSolrExpress<TechProduct>(builder => builder
                .UseHostAddress("http://localhost:8983/solr/techproducts")
                .UseSolr5());

            serviceCollection.AddSolrExpress<TechProduct2>(builder => builder
                .UseHostAddress("http://localhost:8983/solr/techproducts")
                .UseSolr5());

            var serviceProvider = serviceCollection.BuildServiceProvider();

            // Emulating use of DI provided by Net Core DI
            var documentCollection = serviceProvider.GetRequiredService<IDocumentCollection<TechProduct>>();
            var documentCollection2 = serviceProvider.GetRequiredService<IDocumentCollection<TechProduct2>>();

            IEnumerable<TechProduct> techProducts;
            IEnumerable<TechProduct2> techProducts2;

            documentCollection
                .Select()
                .QueryAll()
                .Limit(3)
                .Execute()
                .Document(out techProducts);

            documentCollection2
                .Select()
                .QueryAll()
                .Limit(3)
                .Execute()
                .Document(out techProducts2);

            foreach (var document in techProducts)
            {
                var json = JsonConvert.SerializeObject(document, Formatting.Indented);

                Console.WriteLine(json);
                Console.WriteLine(new string('-', 50));
            }

            foreach (var document in techProducts2)
            {
                var json = JsonConvert.SerializeObject(document, Formatting.Indented);

                Console.WriteLine(json);
                Console.WriteLine(new string('-', 50));
            }

            Console.Read();
        }
    }
}
