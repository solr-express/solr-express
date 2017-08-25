using SimpleInjector;

namespace SolrExpress.Benchmarks.Helper
{
    public sealed class SolrExpressServiceProvider<TDocument> : ISolrExpressServiceProvider<TDocument>
        where TDocument : Document
    {
        private readonly Container _container = new Container();

        ISolrExpressServiceProvider<TDocument> ISolrExpressServiceProvider<TDocument>.AddSingleton<TService>(TService implementationInstance)
        {
            if (implementationInstance == null)
            {
                this._container.Register<TService>(Lifestyle.Singleton);
            }
            else
            {
                this._container.Register(() => implementationInstance, Lifestyle.Singleton);
            }

            return this;
        }

        ISolrExpressServiceProvider<TDocument> ISolrExpressServiceProvider<TDocument>.AddSingleton<TService, UImplementation>(UImplementation implementationInstance)
        {
            if (implementationInstance == null)
            {
                this._container.Register<TService, UImplementation>(Lifestyle.Singleton);
            }
            else
            {
                this._container.Register<TService>(() => implementationInstance, Lifestyle.Singleton);
            }

            return this;
        }

        ISolrExpressServiceProvider<TDocument> ISolrExpressServiceProvider<TDocument>.AddTransient<TService>(TService implementationInstance)
        {
            if (implementationInstance == null)
            {
                this._container.Register<TService>(Lifestyle.Transient);
            }
            else
            {
                this._container.Register(() => implementationInstance, Lifestyle.Transient);
            }

            return this;
        }

        ISolrExpressServiceProvider<TDocument> ISolrExpressServiceProvider<TDocument>.AddTransient<TService, UImplementation>(UImplementation implementationInstance)
        {
            if (implementationInstance == null)
            {
                this._container.Register<TService, UImplementation>(Lifestyle.Transient);
            }
            else
            {
                this._container.Register<TService>(() => implementationInstance, Lifestyle.Transient);
            }

            return this;
        }

        TService ISolrExpressServiceProvider<TDocument>.GetService<TService>()
        {
            return this._container.GetInstance<TService>();
        }
    }
}
