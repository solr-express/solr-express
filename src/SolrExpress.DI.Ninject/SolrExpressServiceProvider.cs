using Ninject;

namespace SolrExpress.DI.Ninject
{
    public class SolrExpressServiceProvider<TDocument> : ISolrExpressServiceProvider<TDocument>
        where TDocument : Document
    {
        private readonly IKernel _kernel = new StandardKernel();

        ISolrExpressServiceProvider<TDocument> ISolrExpressServiceProvider<TDocument>.AddSingleton<TService>(TService implementationInstance)
        {
            this._kernel.Bind<TService>().ToMethod(context => implementationInstance).InSingletonScope();

            return this;
        }

        ISolrExpressServiceProvider<TDocument> ISolrExpressServiceProvider<TDocument>.AddSingleton<TService, UImplementation>(UImplementation implementationInstance)
        {
            this._kernel.Bind<TService>().ToMethod(context => implementationInstance).InSingletonScope();

            return this;
        }

        ISolrExpressServiceProvider<TDocument> ISolrExpressServiceProvider<TDocument>.AddTransient<TService>(TService implementationInstance)
        {
            this._kernel.Bind<TService>().ToMethod(context => implementationInstance).InTransientScope();

            return this;
        }

        ISolrExpressServiceProvider<TDocument> ISolrExpressServiceProvider<TDocument>.AddTransient<TService, UImplementation>(UImplementation implementationInstance)
        {
            this._kernel.Bind<TService>().ToMethod(context => implementationInstance).InTransientScope();

            return this;
        }

        TService ISolrExpressServiceProvider<TDocument>.GetService<TService>()
        {
            return (TService)this._kernel.GetService(typeof(TService));
        }
    }
}
