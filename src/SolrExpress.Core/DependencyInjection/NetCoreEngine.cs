#if NETCOREAPP1_0
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;

namespace SolrExpress.Core.DependencyInjection
{
    internal class NetCoreEngine : IEngine
    {
        /// <summary>
        /// Object used in lock
        /// </summary>
        private readonly object _lockObject = new object();

        /// <summary>
        /// Service provider instance
        /// </summary>
        private IServiceProvider _serviceProvider;

        /// <summary>
        /// Collection of service descriptors
        /// </summary>
        private IServiceCollection _serviceCollection;

        /// <summary>
        /// Get service provider instance
        /// </summary>
        /// <returns>Service provider instance</returns>
        private IServiceProvider GetServiceProvider()
        {
            lock (this._lockObject)
            {
                if (this._serviceProvider == null)
                {
                    this._serviceProvider = this._serviceCollection.BuildServiceProvider();
                }
            }

            return _serviceProvider;
        }

        /// <summary>
        /// Set collection of service descriptors
        /// </summary>
        /// <param name="serviceCollection">Collection of service descriptors</param>
        /// <returns>This</returns>
        internal IEngine SetServiceCollection(IServiceCollection serviceCollection)
        {
            this._serviceCollection = this._serviceCollection ?? serviceCollection;

            return this;
        }

        /// <summary>
        /// Adds a singleton service of the type specified in TService with an implementation type specified in TImplementation to the specified DI container
        /// </summary>
        /// <typeparam name="TService">The type of the service to add</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation to use</typeparam>
        /// <returns>This</returns>
        IEngine IEngine.AddSingleton<TService, TImplementation>()
        {
            this._serviceCollection.TryAddSingleton<TService, TImplementation>();

            return this;
        }

        /// <summary>
        /// Adds a transient service of the type specified in TService with an implementation type specified in TImplementation to the specified DI container
        /// </summary>
        /// <typeparam name="TService">The type of the service to add</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation to use</typeparam>
        /// <returns>This</returns>
        IEngine IEngine.AddTransient<TService, TImplementation>()
        {
            this._serviceCollection.TryAddTransient<TService, TImplementation>();

            return this;
        }

        /// <summary>
        /// Adds a transient service of the type specified in TService with an implementation type specified in TImplementation to the specified DI container
        /// </summary>
        /// <param name="instance">Instace of TImplementation used to resolve DI</param>
        /// <typeparam name="TService">The type of the service to add</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation to use</typeparam>
        /// <returns>This</returns>
        IEngine IEngine.AddTransient<TService, TImplementation>(TImplementation instance)
        {
            this._serviceCollection.AddTransient<TService, TImplementation>(q => instance);

            return this;
        }

        /// <summary>
        /// Get service of type TService from the DI provider
        /// </summary>
        /// <returns>Instance of type TService</returns>
        TService IEngine.GetService<TService>()
        {
            return this.GetServiceProvider().GetService<TService>();
        }
    }
}
#endif