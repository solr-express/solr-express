using SolrExpress.Core;
using SolrExpress.Core.Query;
using SolrExpress.Solr5;
using SolrExpress.Solr5.Extension;
using System;

namespace Sample.Ui.Context
{
    public class SolrContext : IDisposable
    {
        public SolrContext()
        {
            var provider = new Provider("http://localhost:8983/solr/techproducts");
            var resolver = new SimpleResolver().Configure();
            var configuration = new Configuration
            {
                FailFast = true
            };

            this.TechProducts = new Queryable<TechProduct>(provider, resolver, configuration);
        }

        public Queryable<TechProduct> TechProducts { get; private set; }

        public void Dispose()
        {
        }
    }
}
