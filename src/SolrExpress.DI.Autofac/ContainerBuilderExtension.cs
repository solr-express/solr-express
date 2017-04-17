using Autofac;
using SolrExpress.Utility;
using System;

namespace SolrExpress.DI.Autofac
{
    /// <summary>
    /// Configure SolrExpress DI using Autofac engine
    /// </summary>
    public static class ContainerBuilderExtension
    {
        /// <summary>
        /// Add SolrExpress services
        /// </summary>
        /// <param name="container">Container used in SimpleInjector engine</param>
        /// <returns>Container used in SimpleInjector engine</returns>
        public static ContainerBuilder AddSolrExpress<TDocument>(this ContainerBuilder container, Action<SolrExpressBuilder<TDocument>> builder)
            where TDocument : IDocument
        {
            var solrExpressServiceProvider = new SolrExpressServiceProvider<TDocument>();
            var solrExpressBuilder = new SolrExpressBuilder<TDocument>(solrExpressServiceProvider);

            container.Register<ISolrExpressServiceProvider<TDocument>>(q => solrExpressServiceProvider).SingleInstance();
            container.RegisterType<DocumentCollection<TDocument>>().SingleInstance();

            CoreDependecyInjection.Configure(solrExpressServiceProvider, solrExpressBuilder.Options);

            builder.Invoke(solrExpressBuilder);

            return container;
        }
    }
}
