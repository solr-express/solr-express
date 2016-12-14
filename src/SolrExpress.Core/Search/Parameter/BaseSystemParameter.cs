using System.Collections.Generic;

namespace SolrExpress.Core.Search.Parameter
{
    public abstract class BaseSystemParameter : ISystemParameter
    {
        /// <summary>
        /// Parameters to add
        /// </summary>
        protected Dictionary<string, string> Parameters;

        /// <summary>
        /// True to indicate multiple instances of the parameter, otherwise false
        /// </summary>
        public bool AllowMultipleInstances { get; } = false;

        /// <summary>
        /// Configure current instance
        /// </summary>
        public ISystemParameter Configure()
        {
            this.Parameters = new Dictionary<string, string>
            {
                ["echoParams"] = "none",
                ["wt"] = "json",
                ["indent"] = "off",
                ["defType"] = "edismax",
                ["q.alt"] = "*:*",
                ["df"] = "id"
            };

            return this;
        }
    }
}
