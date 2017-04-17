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
            var services =
                new ServiceCollection()
                .AddSolrExpress<TechProduct>(q => q.UseHostAddress("http://localhost:8983/solr/techproducts")
                .UseSolr5());

            var serviceProvider = services.BuildServiceProvider();

            var documentCollection = serviceProvider.GetRequiredService<DocumentCollection<TechProduct>>();

            IEnumerable<TechProduct> documents;
            IEnumerable<FacetKeyValue> facets;
            Information information;

            documentCollection
                .Select()
                .ChangeDynamicFieldBehaviour(q => q.InStock, suffixName: "_xpto")
                // TODO: Need implementations in Core
                //.Fields(d => d.Id, d => d.Manufacturer)
                //.FacetField(d => d.Categories)
                //.Filter(d => d.Id, "205325092")
                .Execute()
                .Information(out information)
                .Document(out documents)
                .Facets(out facets)
                .Execute();

            foreach (var document in documents)
            {
                Console.WriteLine(JsonConvert.SerializeObject(document));

                Console.WriteLine(new string('-', 50));
            }
        }
    }
}
