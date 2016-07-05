using Newtonsoft.Json.Linq;
using SolrExpress.Core.Query.Parameter;
using System.Collections.Generic;

namespace SolrExpress.Core.Query.Result
{
    /// <summary>
    /// Base interface used to parse the SOLR results when a json object is necessary
    /// </summary>
    public interface IConvertJsonObject
    {
        /// <summary>
        /// Execute the parse of the JSON object
        /// </summary>
        /// <param name="parameters">List of the parameters arranged in the queryable class</param>
        /// <param name="jsonObject">JSON object used in the parse</param>
        void Execute(List<IParameter> parameters, JObject jsonObject);
    }
}
