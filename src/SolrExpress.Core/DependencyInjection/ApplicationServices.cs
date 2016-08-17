using System;

namespace SolrExpress.Core.DependencyInjection
{
    /// <summary>
    /// Services container
    /// </summary>
    internal class ApplicationServices
    {
        /// <summary>
        /// Object used in lock
        /// </summary>
        private static readonly object _lockObject = new object();

        /// <summary>
        /// Get current instance of ServiceContainer
        /// </summary>
        /// <returns>Instance of ServiceContainer</returns>
        internal static IEngine Current { get; set; }

        /// <summary>
        /// Create and associate dependency injection engine
        /// </summary>
        /// <param name="builder">Builder action</param>
        internal static void Initialize<TEngine>(Action<TEngine> builder = null)
            where TEngine : class, IEngine, new()
        {
            lock (ApplicationServices._lockObject)
            {
                var applicationServices = new TEngine();
                builder?.Invoke(applicationServices);

                ApplicationServices.Current = applicationServices;
            }
        }
    }
}