using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SolrExpress.Core;
using SolrExpress.Core.Extension;
using SolrExpress.Solr5.Extension;

namespace Sample.WebApi.Net46
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container
        public void ConfigureServices(IServiceCollection services)
        {
            var techProducts = new DocumentCollectionBuilder<TechProduct>()
                .AddSolrExpress()
                .UseOptions(new DocumentCollectionOptions<TechProduct>()
                {
                    CheckAnyParameter = true,
                    FailFast = true
                })
                .UseHostAddress("http://localhost:8983/solr/gettingstarted")
                .UseSolr5()
                .Create();

            services.AddSingleton<IDocumentCollection<TechProduct>>(q => techProducts);

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseMvc();
        }
    }
}
