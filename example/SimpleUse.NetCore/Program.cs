using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using SolrExpress;
using SolrExpress.DI.CoreClr;
using SolrExpress.Search.Behaviour.Extension;
using SolrExpress.Search.Parameter.Extension;
using SolrExpress.Search.Result;
using SolrExpress.Search.Result.Extension;
using SolrExpress.Solr5.Extension;
using System;
using System.Collections.Generic;

namespace SimpleUse.NetCore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var services = new ServiceCollection()
                .AddSolrExpress<TechProduct>(builder => builder
                    .UseHostAddress("http://localhost:8983/solr/techproducts")
                    .UseSolr5());

            var serviceProvider = services.BuildServiceProvider();

            var techProducts = serviceProvider.GetRequiredService<DocumentCollection<TechProduct>>();

            // Initial search settings (configure to result facet field Categories and filter by field id using value "205325092")
            var searchResults = techProducts
                .Select()
                .ChangeDynamicFieldBehaviour(q => q.InStock, suffixName: "_xpto")
                .Fields(d => d.Id, d => d.Manufacturer)
                .FacetField(d => d.Categories)
                .Filter(d => d.Id, "205325092")
                .Execute();

            // Get general information about search, documents and facets from search result
            searchResults
                .Information(out var information)
                .Document(out var documents)
                .Facets(out var facets);

            foreach (var document in documents)
            {
                Console.WriteLine(JsonConvert.SerializeObject(document));

                Console.WriteLine(new string('-', 50));
            }
        }
    }
}
