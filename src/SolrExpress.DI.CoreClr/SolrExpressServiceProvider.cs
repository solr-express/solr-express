using Microsoft.Extensions.DependencyInjection;
using System;

namespace SolrExpress.DI.CoreClr
{
    public sealed class SolrExpressServiceProvider<TDocument> : ISolrExpressServiceProvider<TDocument>
        where TDocument : IDocument
    {
        private readonly IServiceCollection _serviceCollection = new ServiceCollection();
        private IServiceProvider _serviceProvider = null;

        ISolrExpressServiceProvider<TDocument> ISolrExpressServiceProvider<TDocument>.AddSingleton<TService>(TService implementationInstance)
        {
            if (implementationInstance == null)
            {
                this._serviceCollection.AddSingleton<TService>();
            }
            else
            {
                this._serviceCollection.AddSingleton(implementationInstance);
            }

            return this;
        }

        ISolrExpressServiceProvider<TDocument> ISolrExpressServiceProvider<TDocument>.AddSingleton<TService, UImplementation>(UImplementation implementationInstance)
        {
            if (implementationInstance == null)
            {
                this._serviceCollection.AddSingleton<TService, UImplementation>();
            }
            else
            {
                this._serviceCollection.AddSingleton<TService, UImplementation>(q => implementationInstance);
            }

            return this;
        }

        ISolrExpressServiceProvider<TDocument> ISolrExpressServiceProvider<TDocument>.AddTransient<TService>(TService implementationInstance)
        {
            if (implementationInstance == null)
            {
                this._serviceCollection.AddTransient<TService>();
            }
            else
            {
                this._serviceCollection.AddTransient(q => implementationInstance);
            }

            return this;
        }

        ISolrExpressServiceProvider<TDocument> ISolrExpressServiceProvider<TDocument>.AddTransient<TService, UImplementation>(UImplementation implementationInstance)
        {
            if (implementationInstance == null)
            {
                this._serviceCollection.AddTransient<TService, UImplementation>();
            }
            else
            {
                this._serviceCollection.AddTransient<TService, UImplementation>(q => implementationInstance);
            }

            return this;
        }

        TService ISolrExpressServiceProvider<TDocument>.GetService<TService>()
        {
            this._serviceProvider = this._serviceProvider ?? this._serviceCollection.BuildServiceProvider();

            return this._serviceProvider.GetService<TService>();
        }
    }
}
