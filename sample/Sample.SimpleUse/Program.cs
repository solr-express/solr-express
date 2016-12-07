#if NETCOREAPP1_0
using Microsoft.Extensions.DependencyInjection;
#endif
using Newtonsoft.Json;
using SolrExpress.Core;
using SolrExpress.Core.Extension;
using SolrExpress.Core.Search.ParameterValue;
using SolrExpress.Solr5.Extension;
using System;
using System.Collections.Generic;

namespace Sample.SimpleUse
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IDocumentCollection<TechProduct> techProducts;

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

            techProducts = serviceProvider.GetRequiredService<IDocumentCollection<TechProduct>>();
#endif

            IEnumerable<TechProduct> documents;

            var select = techProducts
                .Select()
                .QueryAll()
                .Limit(3)
                .FacetField(q => q.Manufacturer)
                .FacetField(q => q.InStock)
                .FacetRange("Price", q => q.Price, "10", "10", "100")
                .FacetRange("Popularity", q => q.Popularity, "1", "1", "10")
                .FacetRange("ManufacturedateIn", q => q.ManufacturedateIn, "+1MONTH", "NOW-10YEARS", "NOW")
                .FacetQuery("StoreIn1000km", new Spatial<TechProduct>(SolrSpatialFunctionType.Geofilt, q => q.StoredAt, new GeoCoordinate(35.0752M, -97.032M), 1000M));

            select
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
