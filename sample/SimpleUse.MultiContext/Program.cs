using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using SolrExpress;
using SolrExpress.DI.CoreClr;
using SolrExpress.Search.Extension;
using SolrExpress.Search.Result.Extension;
using SolrExpress.Solr5.Extension;
using System;

namespace SimpleUse.MultiContext
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var services = new ServiceCollection()
                .AddSolrExpress<TechProduct1>(builder => builder
                    .UseHostAddress("http://localhost:8983/solr/techproducts")
                    .UseSolr5());
            services
                .AddSolrExpress<TechProduct2>(builder => builder
                    .UseHostAddress("http://localhost:8983/solr/techproducts")
                    .UseSolr5());

            var serviceProvider = services.BuildServiceProvider();

            var techProducts1 = serviceProvider.GetRequiredService<DocumentCollection<TechProduct1>>();
            techProducts1
                .Select()
                .Page(1, 1)
                .Execute()
                .Information(out var information1)
                .Document(out var documents1);

            Console.WriteLine("Informations");
            Console.WriteLine(JsonConvert.SerializeObject(information1, Formatting.Indented));
            Console.WriteLine(new string('-', 50));

            Console.WriteLine("Documents");
            Console.WriteLine(JsonConvert.SerializeObject(documents1, Formatting.Indented));
            Console.WriteLine(new string('-', 50));

            var techProducts2 = serviceProvider.GetRequiredService<DocumentCollection<TechProduct2>>();
            techProducts2
                .Select()
                .Page(1, 1)
                .Execute()
                .Information(out var information2)
                .Document(out var documents2);

            Console.WriteLine("Informations");
            Console.WriteLine(JsonConvert.SerializeObject(information2, Formatting.Indented));
            Console.WriteLine(new string('-', 50));

            Console.WriteLine("Documents");
            Console.WriteLine(JsonConvert.SerializeObject(documents2, Formatting.Indented));
            Console.WriteLine(new string('-', 50));

            Console.Read();
        }
    }
}
