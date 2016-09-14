#if NET40 || NET45
using SimpleInjector;

namespace SolrExpress.Core.DependencyInjection
{
    internal class NetFrameworkEngine : IEngine
    {
        /// <summary>
        /// SimpleInjector container used in DI
        /// </summary>
        private readonly Container _container;

        /// <summary>
        /// Default constructor of class
        /// </summary>
        public NetFrameworkEngine()
        {
            this._container = new Container();
            this._container.Options.AllowOverridingRegistrations = true;
        }

        /// <summary>
        /// Adds a singleton service of the type specified in TService with an implementation type specified in TImplementation to the specified DI container
        /// </summary>
        /// <typeparam name="TService">The type of the service to add</typeparam>
        /// <returns>This</returns>
        IEngine IEngine.AddSingleton<TService>()
        {
            this._container.Register<TService>(Lifestyle.Singleton);

            return this;
        }

        /// <summary>
        /// Adds a singleton service of the type specified in TService with an implementation type specified in TImplementation to the specified DI container
        /// </summary>
        /// <typeparam name="TService">The type of the service to add</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation to use</typeparam>
        /// <returns>This</returns>
        IEngine IEngine.AddSingleton<TService, TImplementation>(TImplementation instance)
        {
            this._container.Register<TService>(() => instance, Lifestyle.Singleton);

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
            this._container.Register<TService, TImplementation>(Lifestyle.Singleton);

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
            this._container.Register<TService, TImplementation>(Lifestyle.Transient);

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
            this._container.Register<TService>(() => instance, Lifestyle.Transient);

            return this;
        }

        /// <summary>
        /// Get service of type TService from the DI provider
        /// </summary>
        /// <returns>Instance of type TService</returns>
        TService IEngine.GetService<TService>()
        {
            return this._container.GetInstance<TService>();
        }
    }
}
#endif