using Newtonsoft.Json;
using SolrExpress.Core.Extension;
using SolrExpress.Core.Query.ParameterValue;
using System;
using System.Collections.Generic;

namespace Sample.SimpleUse
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using (var ctx = new SolrContext())
            {
                List<TechProduct> documents;

                ctx
                    .TechProducts
                    .Select
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
            }

            Console.Read();
        }
    }
}
