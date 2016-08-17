using Moq;
using SolrExpress.Core.DependencyInjection;

namespace SolrExpress.Core.Tests
{
    /// <summary>
    /// Mock services container
    /// </summary>
    internal class MockEngine : Mock<IEngine>, IEngine
    {
        /// <summary>
        /// Adds a singleton service of the type specified in TService with an implementation type specified in TImplementation to the specified DI container
        /// </summary>
        /// <typeparam name="TService">The type of the service to add</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation to use</typeparam>
        /// <returns>This</returns>
        IEngine IEngine.AddSingleton<TService, TImplementation>()
        {
            this.Object.AddSingleton<TService, TImplementation>();

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
            this.Object.AddTransient<TService, TImplementation>();

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
            this.Object.AddTransient<TService, TImplementation>(instance);

            return this;
        }

        /// <summary>
        /// Get service of type TService from the DI provider
        /// </summary>
        /// <returns>Instance of type TService</returns>
        TService IEngine.GetService<TService>()
        {
            return this.Object.GetService<TService>();
        }
    }
}
