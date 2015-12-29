using SearchUI.Models;
using SolrExpress.Core.Constant;
using SolrExpress.Core.Entity;
using SolrExpress.Core.Query;
using SolrExpress.Solr5.Builder;
using SolrExpress.Solr5.Parameter;
using SolrExpress.Solr5.Query;
using System;

namespace SearchUI.Context
{
    public class SolrContext : IDisposable
    {
        public SolrContext()
        {
            var provider = new Provider("http://localhost:8983/solr/techproducts");
            var parameterFactory = new ParameterFactory<TechProduct>();
            var builderFactory = new BuilderFactory<TechProduct>();
            var configuration = new SolrQueryConfiguration
            {
                FailFast = true,
                Handler = RequestHandler.SELECT
            };

            this.TechProducts = new SolrQueryable<TechProduct>(provider, parameterFactory, builderFactory, configuration);
        }

        public SolrQueryable<TechProduct> TechProducts { get; private set; }

        public void Dispose()
        {
        }
    }
}