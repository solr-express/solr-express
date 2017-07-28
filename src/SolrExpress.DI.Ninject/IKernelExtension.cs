using Ninject;
using SolrExpress.Utility;
using System;

namespace SolrExpress.DI.Ninject
{
    /// <summary>
    /// Configure SolrExpress DI using SimpleInjector engine
    /// </summary>
    public static class IKernelExtension
    {
        /// <summary>
        /// Add SolrExpress services
        /// </summary>
        /// <param name="container">Container used in SimpleInjector engine</param>
        /// <returns>Container used in SimpleInjector engine</returns>
        public static IKernel AddSolrExpress<TDocument>(this IKernel container, Action<SolrExpressBuilder<TDocument>> builder)
            where TDocument : Document
        {
            var solrExpressServiceProvider = new SolrExpressServiceProvider<TDocument>();
            var solrExpressBuilder = new SolrExpressBuilder<TDocument>(solrExpressServiceProvider);

            container.Bind<ISolrExpressServiceProvider<TDocument>>().ToMethod((context) => solrExpressServiceProvider).InSingletonScope();
            container.Bind<DocumentCollection<TDocument>>().To<DocumentCollection<TDocument>>().InSingletonScope();

            CoreDependecyInjection.Configure(solrExpressServiceProvider, solrExpressBuilder.Options);

            builder.Invoke(solrExpressBuilder);

            return container;
        }
    }
}
