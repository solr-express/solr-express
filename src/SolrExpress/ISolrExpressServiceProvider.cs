namespace SolrExpress
{
    /// <summary>
    /// DI engine
    /// </summary>
    public interface ISolrExpressServiceProvider<TDocument>
        where TDocument : Document
    {
        /// <summary>
        /// Create a new instance of T using internal DI engine
        /// </summary>
        /// <returns>Instance of T</returns>
        TService GetService<TService>()
            where TService : class;

        /// <summary>
        /// Adds a singleton service of the type specified in TService with an implementation type specified in TImplementation to the specified DI container
        /// </summary>
        /// <typeparam name="TService">The type of the service to add</typeparam>
        /// <returns>This</returns>
        ISolrExpressServiceProvider<TDocument> AddSingleton<TService>(TService implementationInstance = null)
            where TService : class;

        /// <summary>
        /// Adds a singleton service of the type specified in TService with an implementation type specified in TImplementation to the specified DI container
        /// </summary>
        /// <typeparam name="TService">The type of the service to add</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation to use</typeparam>
        /// <returns>This</returns>
        ISolrExpressServiceProvider<TDocument> AddSingleton<TService, TImplementation>(TImplementation implementationInstance = null)
            where TService : class
            where TImplementation : class, TService;

        /// <summary>
        /// Adds a transient service of the type specified in TService with an implementation type specified in TImplementation to the specified DI container
        /// </summary>
        /// <typeparam name="TService">The type of the service to add</typeparam>
        /// <returns>This</returns>
        ISolrExpressServiceProvider<TDocument> AddTransient<TService>(TService implementationInstance = null)
            where TService : class;

        /// <summary>
        /// Adds a transient service of the type specified in TService with an implementation type specified in TImplementation to the specified DI container
        /// </summary>
        /// <typeparam name="TService">The type of the service to add</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation to use</typeparam>
        /// <returns>This</returns>
        ISolrExpressServiceProvider<TDocument> AddTransient<TService, TImplementation>(TImplementation implementationInstance = null)
            where TService : class
            where TImplementation : class, TService;
    }
}
