using Autofac;

namespace SolrExpress.DI.Autofac
{
    public class SolrExpressServiceProvider<TDocument> : ISolrExpressServiceProvider<TDocument>
        where TDocument : IDocument
    {
        private readonly ContainerBuilder _kernel = new ContainerBuilder();
        private IContainer _container = null;

        ISolrExpressServiceProvider<TDocument> ISolrExpressServiceProvider<TDocument>.AddSingleton<TService>(TService implementationInstance)
        {
            this._kernel.RegisterType<TService>().As<TService>().SingleInstance();

            return this;
        }

        ISolrExpressServiceProvider<TDocument> ISolrExpressServiceProvider<TDocument>.AddSingleton<TService, UImplementation>(UImplementation implementationInstance)
        {
            this._kernel.Register(q => implementationInstance).As<TService>().SingleInstance();

            return this;
        }

        ISolrExpressServiceProvider<TDocument> ISolrExpressServiceProvider<TDocument>.AddTransient<TService>(TService implementationInstance)
        {
            this._kernel.RegisterType<TService>().As<TService>().InstancePerLifetimeScope();

            return this;
        }

        ISolrExpressServiceProvider<TDocument> ISolrExpressServiceProvider<TDocument>.AddTransient<TService, UImplementation>(UImplementation implementationInstance)
        {
            this._kernel.Register(q => implementationInstance).As<TService>().InstancePerLifetimeScope();

            return this;
        }

        TService ISolrExpressServiceProvider<TDocument>.GetService<TService>()
        {
            this._container = this._container ?? this._kernel.Build();

            return this._container.Resolve<TService>();
        }
    }
}
