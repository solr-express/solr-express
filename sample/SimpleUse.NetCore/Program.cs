using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using SolrExpress;
using SolrExpress.Configuration;
using SolrExpress.DI.CoreClr;
using SolrExpress.Options;
using SolrExpress.Search.Extension;
using SolrExpress.Search.Parameter.Extension;
using SolrExpress.Search.Result.Extension;
using SolrExpress.Solr5.Extension;
using System;

namespace SimpleUse.NetCore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var services = new ServiceCollection()
                .AddSolrExpress<TechProduct>(builder => builder
                    .ConfigureDocument(q =>
                    {
                        q.Field(t => t.Name).HasName("name");
                        q.Field(t => t.Manufacturer).HasName("manu");
                        q.Field(t => t.ManufacturerId).HasName("manu_id_s");
                        q.Field(t => t.Categories).HasName("cat");
                        q.Field(t => t.Features).HasName("features");
                        q.Field(t => t.Price).HasName("price");
                        q.Field(t => t.Popularity).HasName("popularity");
                        q.Field(t => t.InStock).HasName("inStock");
                        q.Field(t => t.ManufacturedateIn).HasName("manufacturedate_dt");
                        q.Field(t => t.StoredAt).HasName("store");
                    })
                    .UseOptions(q => q.HasHostAddress("http://localhost:8983/solr/techproducts"))
                    .UseSolr5());

            var serviceProvider = services.BuildServiceProvider();

            var techProducts = serviceProvider.GetRequiredService<DocumentCollection<TechProduct>>();

            techProducts
                .Select()
                .Fields(d => d.Id, d => d.Manufacturer)
                .FacetField(d => d.Categories)
                .Execute()
                .Information(out var information)
                .Document(out var documents)
                .Facets(out var facets);

            Console.WriteLine("Informations");
            Console.WriteLine(JsonConvert.SerializeObject(information, Formatting.Indented));
            Console.WriteLine(new string('-', 50));

            Console.WriteLine("Documents");
            Console.WriteLine(JsonConvert.SerializeObject(documents, Formatting.Indented));
            Console.WriteLine(new string('-', 50));

            Console.WriteLine("Facets");
            Console.WriteLine(JsonConvert.SerializeObject(facets, Formatting.Indented));
            Console.WriteLine(new string('-', 50));

            Console.Read();
        }
    }
}
