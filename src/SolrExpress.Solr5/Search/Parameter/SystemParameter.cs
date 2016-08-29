using Newtonsoft.Json.Linq;
using SolrExpress.Core.Search;
using SolrExpress.Core.Search.Parameter;
using System.Collections.Generic;

namespace SolrExpress.Solr5.Search.Parameter
{
    /// <summary>
    /// Internal use
    /// </summary>
    internal class SystemParameter : ISystemParameter, ISearchParameter<JObject>
    {
        /// <summary>
        /// True to indicate multiple instances of the parameter, otherwise false
        /// </summary>
        public bool AllowMultipleInstances { get; } = false;

        /// <summary>
        /// Configure current instance
        /// </summary>
        public ISystemParameter Configure()
        {
            return this;
        }

        /// <summary>
        /// Execute the creation of the parameter
        /// </summary>
        /// <param name="jObject">JSON object with parameters to request to SOLR</param>
        public void Execute(JObject jObject)
        {
            var parameters = new Dictionary<string, string>
            {
                ["echoParams"] = "none",
                ["wt"] = "json",
                ["indent"] = "off",
                ["defType"] = "edismax",
                ["fl"] = "*,score",
                ["q.alt"] = "*:*",
                ["sort"] = "score asc",
                ["df"] = "id",
                ["q"] = "*:*"
            };

            var jObj = (JObject)jObject["params"] ?? new JObject();

            foreach (var parameter in parameters)
            {
                if (jObj[parameter.Key] == null)
                {
                    jObj.Add(new JProperty(parameter.Key, parameter.Value));
                }
            }

            jObject["params"] = jObj;
        }
    }
}
