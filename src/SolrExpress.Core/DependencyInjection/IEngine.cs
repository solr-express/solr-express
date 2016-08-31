namespace SolrExpress.Core.DependencyInjection
{
    /// <summary>
    /// Services container
    /// </summary>
    internal interface IEngine
    {
        /// <summary>
        /// Adds a singleton service of the type specified in TService with an implementation type specified in TImplementation to the specified DI container
        /// </summary>
        /// <typeparam name="TService">The type of the service to add</typeparam>
        /// <returns>This</returns>
        IEngine AddSingleton<TService>()
            where TService : class;

        /// <summary>
        /// Adds a singleton service of the type specified in TService with an implementation type specified in TImplementation to the specified DI container
        /// </summary>
        /// <typeparam name="TService">The type of the service to add</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation to use</typeparam>
        /// <returns>This</returns>
        IEngine AddSingleton<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService;

        /// <summary>
        /// Adds a transient service of the type specified in TService with an implementation type specified in TImplementation to the specified DI container
        /// </summary>
        /// <typeparam name="TService">The type of the service to add</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation to use</typeparam>
        /// <returns>This</returns>
        IEngine AddTransient<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService;

        /// <summary>
        /// Adds a transient service of the type specified in TService with an implementation type specified in TImplementation to the specified DI container
        /// </summary>
        /// <param name="instance">Instace of TImplementation used to resolve DI</param>
        /// <typeparam name="TService">The type of the service to add</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation to use</typeparam>
        /// <returns>This</returns>
        IEngine AddTransient<TService, TImplementation>(TImplementation instance)
            where TService : class
            where TImplementation : class, TService;

        /// <summary>
        /// Get service of type TService from the DI provider
        /// </summary>
        /// <returns>Instance of type TService</returns>
        TService GetService<TService>()
             where TService : class;
    }
}