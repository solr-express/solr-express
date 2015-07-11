using Newtonsoft.Json;
using SolrExpress.Core.ParameterValue;
using SolrExpress.Solr5.Builder;
using SolrExpress.Solr5.Parameter;
using System;

namespace BasicUse
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using (var ctx = new SolrContext())
            {
                ctx.TechProducts.Parameter(new QueryParameter(new QueryAll()));
                ctx.TechProducts.Parameter(new LimitParameter(3));

                var result = ctx.TechProducts.Execute();

                var documents = result.Get(new DocumentBuilder<TechProduct>()).Data;

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
